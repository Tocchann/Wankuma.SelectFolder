using System;
using System.Runtime.InteropServices;

namespace Wankuma.SelectFolder.Interops;

/// <summary>
/// FileOpenDialog の coclass 定義
/// </summary>
[ComImport, Guid( "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7" )]
internal class FileOpenDialog { }

/// <summary>
/// IFileOpenDialog のインターフェース定義
/// 呼び出すもののみパラメータを定義。それ以外はメソッドのプレースフォルダとして存在しているだけでよい
/// </summary>
[
	ComImport,
	Guid( "42f85136-db7e-439c-85f1-e4075d135fc8" ),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )
]
internal interface IFileOpenDialog
{
	/// <summary>
	/// enum FOS(_FILEOPENDIALOGOPTIONSの略称)
	/// enum 名は、C/C++ 定義の略称名を利用。
	/// 今回使用するファイルシステム限定フラグ(フォルダの指定がメインなので)と
	/// フォルダ選択モードのフラグのみ転写。
	/// </summary>
	[Flags]
	public enum FOS
	{
		FORCEFILESYSTEM = 0x40,
		PICKFOLDERS = 0x20,
	}
	public enum FDAP
	{
		BOTTOM = 0,
		TOP = 1
	}
	// IUnknown は、InterfaceType 属性で指定しているため不要
	// IModalWindow
	/// <summary>
	/// ダイアログを表示
	/// </summary>
	/// <param name="hwndOwner">オーナーウィンドウ</param>
	/// <returns>PreserveSig 属性をつけているので、HRESULTがそのまま戻り値になる</returns>
	[PreserveSig]
	int Show( IntPtr hwndOwner );
	// IFileDialog
	void SetFileTypes();
	void SetFileTypeIndex();
	void GetFileTypeIndex();
	void Advise();
	void Unadvise();
	void SetOptions( FOS fos );
	void GetOptions();
	void SetDefaultFolder();
	void SetFolder( IShellItem psi );
	void GetFolder();
	void GetCurrentSelection();
	void SetFileName();
	void GetFileName();
	void SetTitle( [MarshalAs( UnmanagedType.LPWStr )] string pszTitle );
	void SetOkButtonLabel();
	void SetFileNameLabel();
	IShellItem GetResult();
	void AddPlace( IShellItem item, FDAP fdap );
	void SetDefaultExtension();
	void Close();
	void SetClientGuid();
	void ClearClientData();
	void SetFilter();
	//	IFileOpenDialog
	void GetResults();
	void GetSelectedItems();
}
