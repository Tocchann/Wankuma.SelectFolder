using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Samples.WPF.Netfx
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private string baseFolder;
		public MainWindow()
		{
			InitializeComponent();

			//	アプリケーションのドキュメントフォルダ
			baseFolder = System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), "Wankuma" );
			System.IO.Directory.CreateDirectory( baseFolder );
		}

		private void OnClick_SelectFolder( object sender, RoutedEventArgs e )
		{
			Wankuma.SelectFolder.WPF.SelectFolder dlg = new Wankuma.SelectFolder.WPF.SelectFolder();
			dlg.SelectedPath = EditSelFolder.Text;
			dlg.InitialFolder = baseFolder;
			dlg.Title = "フォルダを選択してください";
			dlg.AddPlace( baseFolder, Wankuma.SelectFolder.WPF.SelectFolder.FDAP.TOP );
			if( dlg.ShowDialog( this ) == true )
			{
				EditSelFolder.Text = dlg.SelectedPath;
			}
		}

		private void OnClick_Cancel( object sender, RoutedEventArgs e )
		{
			Close();
		}
	}
}
