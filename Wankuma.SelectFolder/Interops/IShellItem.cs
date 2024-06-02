using System;
using System.Runtime.InteropServices;

namespace Wankuma.SelectFolder.Interops;
/// <summary>
/// IShellItem フォルダ選択で利用しない部分のパラメータは省略
/// </summary>
[
	ComImport,
	Guid( "43826D1E-E718-42EE-BC55-A1E261C37BFE" ),
	InterfaceType( ComInterfaceType.InterfaceIsIUnknown )
]
internal interface IShellItem // : IUnknown
{
	// 今回使う識別子のみ移植
	public enum SIGDN : uint
	{
		FILESYSPATH = 0x80058000,
	}
	void BindToHandler();
	void GetParent();
	/// <summary>
	/// このオブジェクトの文字列表記を取得
	/// GetDisplayName が本来のメソッド名。名前によるアクセスではないことを証明するためにわざと名称変更
	/// </summary>
	/// <param name="sigdnName"></param>
	/// <returns>sigdnName に応じた文字列</returns>
	[return: MarshalAs( UnmanagedType.LPWStr )]
	string GetName( SIGDN sigdnName );
	void GetAttributes();
	void Compare();
}
