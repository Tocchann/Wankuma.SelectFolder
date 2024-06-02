using System;
using Wankuma.SelectFolder.Interops;

namespace Wankuma.SelectFolder;
internal static class Utilities
{
	internal static IntPtr GetSafeOwnerWindow( IntPtr hwndOwner )
	{
		// 有効なHWNDかを確認
		if( hwndOwner != IntPtr.Zero && !NativeMethods.IsWindow( hwndOwner ) )
		{
			hwndOwner = IntPtr.Zero;
		}
		// ウィンドウが指定されていない場合は現在のフォアグラウンドウィンドウを取得する
		if( hwndOwner == IntPtr.Zero )
		{
			hwndOwner = NativeMethods.GetForegroundWindow();
		}
		// 指定されたウィンドウのトップレベルウィンドウを取得する(オーナーウィンドウは常にトップレベルウィンドウ)
		IntPtr hwndParent = hwndOwner;
		while( hwndParent != IntPtr.Zero )
		{
			hwndOwner = hwndParent;
			hwndParent = NativeMethods.GetParent( hwndOwner );
		}
		// ウィンドウを見つけたら、オーナーの管理する現在アクティブなウィンドウを取得する
		if( hwndOwner != IntPtr.Zero )
		{
			var hwndLastActive = NativeMethods.GetLastActivePopup( hwndOwner );
			if( hwndLastActive != IntPtr.Zero )
			{
				hwndOwner = hwndLastActive;
			}
		}
		return hwndOwner;
	}
	internal static IShellItem? CreateItem( string? folder )
	{
		if( !string.IsNullOrWhiteSpace( folder ) )
		{
			// デバッグした時に戻り値を表示できるようにしておく(若干冗長)
			var result = NativeMethods.SHCreateItemFromParsingName( folder!, IntPtr.Zero, typeof( IShellItem ).GUID, out var item );
			if( NativeMethods.SUCCEEDED( result ) )
			{
				return item;
			}
		}
		return null;
	}
}
