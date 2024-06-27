using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Wankuma.SelectFolder.Interops;
using static Wankuma.SelectFolder.Interops.IFileOpenDialog;
using static Wankuma.SelectFolder.Interops.IShellItem;

namespace Wankuma.SelectFolder;

public class SelectFolderBase
{
	/// <summary>
	/// フォルダ追加をリストの上下のどちらに追加するか
	/// </summary>
	public enum FDAP
	{
		BOTTOM = 0,
		TOP = 1
	}
	public string? InitialFolder { get; set; }
	public string? SelectedPath { get; set; }
	public string? Title { get; set; }

	public void AddPlace( string folder, FDAP fdap )
	{
		m_places.Add( (folder, fdap) );
	}

	public bool? ShowDialog( IntPtr hwndOwner )
	{
		hwndOwner = Utilities.GetSafeOwnerWindow( hwndOwner );
		var dlg = new FileOpenDialog() as IFileOpenDialog;
		if( dlg != null )
		{
			try
			{
				//	フォルダ選択モードに切り替え
				dlg.SetOptions( FOS.FORCEFILESYSTEM | FOS.PICKFOLDERS );
				//	以前選択されていたフォルダを指定
				bool setFolder = false;
				var item = Utilities.CreateItem( SelectedPath );
				if( item != null )
				{
					dlg.SetFolder( item );
					var refCount = Marshal.ReleaseComObject( item );
					Debug.Assert( refCount == 0 );
					setFolder = true;
				}
				//	まだフォルダを設定していない場合は初期フォルダを設定する
				if( !setFolder )
				{
					item = Utilities.CreateItem( InitialFolder );
					if( item != null )
					{
						dlg.SetFolder( item );
						var refCount = Marshal.ReleaseComObject( item );
						Debug.Assert( refCount == 0 );
					}
				}
				//	タイトル
				if( !string.IsNullOrWhiteSpace( Title ) )
				{
					dlg.SetTitle( Title! );
				}
				//	ショートカット追加
				foreach( var place in m_places )
				{
					item = Utilities.CreateItem( place.folder );
					if( item != null )
					{
						dlg.AddPlace( item, (IFileOpenDialog.FDAP)place.fdap );
						var refCount = Marshal.ReleaseComObject( item );
						Debug.Assert( refCount == 0 );
					}
				}
				//	ダイアログを表示
				var hRes = dlg.Show( hwndOwner );
				if( NativeMethods.SUCCEEDED( hRes ) )
				{
					item = dlg.GetResult();
					SelectedPath = item.GetName( SIGDN.FILESYSPATH );
					var refCount = Marshal.ReleaseComObject( item );
					Debug.Assert( refCount == 0 );
					return true;
				}
				// キャンセル以外のエラーが来た場合はなにかしら問題ありなので例外を投げる
				else if( hRes != NativeMethods.HRESULT_FROM_WIN32( NativeMethods.Win32Error.Cancelled ) )
				{
					throw new COMException( "IFileOpenDialog.Show()のエラー", hRes );
				}
				return false;
			}
			finally
			{
				var refCount = Marshal.ReleaseComObject( dlg );
				Debug.Assert( refCount == 0 );
			}
		}
		return null;
	}
	private List<(string folder, FDAP fdap)> m_places = new();
}
