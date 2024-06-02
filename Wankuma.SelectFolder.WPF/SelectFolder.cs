using System.Windows;

namespace Wankuma.SelectFolder.WPF;

public class SelectFolder : SelectFolderBase
{
	public Window? Owner { get; set; }
	public bool? ShowDialog()
	{
		return ShowDialog( Owner );
	}
	public bool? ShowDialog( Window? window )
	{
		if( window == null )
		{
			window = Utilities.GetOwnerWindow();
		}
		return ShowDialog( Utilities.GetHwndFromWindow( window ) );
	}
}
