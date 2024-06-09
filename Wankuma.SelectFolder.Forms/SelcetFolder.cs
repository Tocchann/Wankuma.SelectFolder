using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wankuma.SelectFolder.Forms;

public class SelectFolder : SelectFolderBase
{
	public new DialogResult ShowDialog()
	{
		var result = base.ShowDialog( IntPtr.Zero );
		if( result != null )
		{
			return result != false ? DialogResult.OK : DialogResult.Cancel;
		}
		return DialogResult.Abort;
	}
	public DialogResult ShowDialog( IWin32Window owner )
	{
		var result = base.ShowDialog( owner.Handle );
		if( result != null )
		{
			return result != false ? DialogResult.OK : DialogResult.Cancel;
		}
		return DialogResult.Abort;
	}
}

