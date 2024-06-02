using System;
using System.Runtime.InteropServices;

namespace Wankuma.SelectFolder.Interops;
internal static class NativeMethods
{
	//	HWND 関連
	[DllImport( "user32.dll" )]
	[return: MarshalAs( UnmanagedType.Bool )]
	public static extern bool IsWindow( IntPtr hWnd );
	[DllImport( "user32.dll" )]
	public static extern IntPtr GetForegroundWindow();
	[DllImport( "user32.dll" )]
	public static extern IntPtr GetParent( IntPtr hwnd );
	[DllImport( "user32.dll" )]
	public static extern IntPtr GetLastActivePopup( IntPtr hwnd );

	// Shell関連
	[DllImport( "shell32.dll", CharSet = CharSet.Unicode, PreserveSig = true )]
	public static extern int SHCreateItemFromParsingName(
		[In][MarshalAs( UnmanagedType.LPWStr )] string pszPath,
		[In] IntPtr pbc,
		[In][MarshalAs( UnmanagedType.LPStruct )] Guid riid,
		[Out][MarshalAs( UnmanagedType.Interface, IidParameterIndex = 2 )] out IShellItem ppv );

	//	COM 関連(HRESULT マクロ)
	public static bool SUCCEEDED( int result ) => result >= 0;
	public static bool FAILED( int result ) => result < 0;
	public static int HRESULT_FROM_WIN32( int result ) =>
		result <= 0 ? result : (int)(0x80000000 | (int)(result & 0xFFFF) | (FACILITY_WIN32 << 16));
	public static int HRESULT_FROM_WIN32( Win32Error result ) =>
		(int)(0x80000000 | ((int)result & 0xFFFF) | (FACILITY_WIN32 << 16));

	static uint FACILITY_WIN32 = 7;
	public enum Win32Error : int
	{
		Success,
		Cancelled = 1223,
	}
}
