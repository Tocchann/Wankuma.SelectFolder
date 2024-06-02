using System.Windows;

namespace Wankuma.SelectFolder.WPF;

internal static class Utilities
{
	/// <summary>
	/// 現在アクティブなWPF管轄でオーナーウィンドウになり得るオブジェクトを検索する
	/// </summary>
	/// <returns></returns>
	internal static Window? GetOwnerWindow()
	{
		Window? window = null;
		foreach( Window search in Application.Current.Windows )
		{
			if( search.IsActive && search.Parent == null )
			{
				window = search;
				break;
			}
		}
		if( window == null )
		{
			window = Application.Current.MainWindow;
		}
		return window;
	}
	internal static IntPtr GetHwndFromWindow( Window? window )
	{
		if( window != null )
		{
			var hwndSrc = System.Windows.Interop.HwndSource.FromVisual( window ) as System.Windows.Interop.HwndSource;
			return hwndSrc?.Handle ?? IntPtr.Zero;
		}
		return IntPtr.Zero;
	}
}
