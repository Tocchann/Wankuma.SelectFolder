using System.Windows;

namespace Samples.WPF.NetCore;

/// <summary>
/// Interaction logic for MainWindow.xaml
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
		Wankuma.SelectFolder.WPF.SelectFolder dlg = new();
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
