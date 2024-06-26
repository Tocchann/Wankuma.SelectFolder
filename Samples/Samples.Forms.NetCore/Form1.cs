namespace Samples.Forms.NetCore
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
			Wankuma.SelectFolder.Forms.SelectFolder dlg = new();
			dlg.SelectedPath = textBox1.Text;
			dlg.InitialFolder = baseFolder;
			dlg.Title = "フォルダを選択してください";
			dlg.AddPlace( baseFolder, Wankuma.SelectFolder.Forms.SelectFolder.FDAP.TOP );
			if( dlg.ShowDialog( this ) == DialogResult.OK )
			{
				textBox1.Text = dlg.SelectedPath;
			}
		}

		private void button2_Click( object sender, EventArgs e )
		{
			Close();
		}
	}
}
