using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wankuma.SelectFolder.Forms;

public class SelectFolder : SelectFolderBase
{
	public DialogResult ShowDialog()
	{
		return ShowDialog( null );
	}
	public DialogResult ShowDialog( IWin32Window? owner )
	{
		var result = base.ShowDialog( owner?.Handle??IntPtr.Zero );
		if( result != null )
		{
			return result != false ? DialogResult.OK : DialogResult.Cancel;
		}
		return DialogResult.Abort;
	}
}

