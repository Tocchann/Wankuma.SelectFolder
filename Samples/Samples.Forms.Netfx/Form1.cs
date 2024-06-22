using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samples.Forms.Netfx
{
	public partial class Form1 : Form
	{
		private string baseFolder;
		public Form1()
		{
			InitializeComponent();

			//	アプリケーションのドキュメントフォルダ
			baseFolder = System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), "Wankuma" );
			System.IO.Directory.CreateDirectory( baseFolder );
		}

		private void button1_Click( object sender, EventArgs e )
		{
			Wankuma.SelectFolder.Forms.SelectFolder dlg = new Wankuma.SelectFolder.Forms.SelectFolder();
			dlg.SelectedPath = textBox1.Text;
			dlg.InitialFolder = baseFolder;
			dlg.Title = "フォルダを選択してください";
			dlg.AddPlace( baseFolder, Wankuma.SelectFolder.Forms.SelectFolder.FDAP.TOP );
			// if( dlg.ShowDialog( this ) == DialogResult.OK )
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				textBox1.Text = dlg.SelectedPath;
			}
		}

		private void button2_Click( object sender, EventArgs e )
		{
			Close();
		}

		private void button3_Click( object sender, EventArgs e )
		{
			var dlg = new FolderBrowserDialog();
			dlg.Description = "フォルダを選択してください";
			dlg.SelectedPath = textBox1.Text;
			if( dlg.ShowDialog( this ) == DialogResult.OK )
			{
				textBox1.Text = dlg.SelectedPath;
			}
		}
	}
}
