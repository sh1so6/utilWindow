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
using HongliangSoft.Utilities.Gui;
using Shell32;
using SHDocVw;
using System.IO;
using System.Collections.ObjectModel;

namespace utilWindow
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private KeyboardHook KeyboardHook;
		private ObservableCollection<ExplorerItem> Data { get; } = new ObservableCollection<ExplorerItem>();
		public MainWindow()
		{
			InitializeComponent();
			KeyboardHook = new KeyboardHook(CallMainWindow);

			Shell shell = new Shell();
			ShellWindows shellWindows = new ShellWindows();
			foreach (InternetExplorer ieObj in shellWindows)
			{
				//エクスプローラのみ(IEを除外)
				if (System.IO.Path.GetFileName(ieObj.FullName).ToUpper() == "EXPLORER.EXE")
				{
					//パスを取得できなかったものを除外
					if (ieObj.LocationURL != "")
					{
						Data.Add(new ExplorerItem { handle = ieObj.HWND, path = new Uri(ieObj.LocationURL).LocalPath + " : " + ieObj.LocationName });
					}

				}
			}
			this.GridExplorer.ItemsSource = Data;
		}
		/// <summary>
		/// globalHookを実現する自作イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CallMainWindow(object sender, HongliangSoft.Utilities.Gui.KeyboardHookedEventArgs e)
		{
			if (e.UpDown != KeyboardUpDown.Down)
			{
				if (e.AltDown && e.KeyCode == System.Windows.Forms.Keys.Q)
				{
					if (!this.IsVisible)
					{
						this.Show();
					}

					if (this.WindowState == WindowState.Minimized)
					{
						this.WindowState = WindowState.Normal;
					}
					this.Activate();
					this.Topmost = true;  // important
					this.Topmost = false; // important
					this.Focus();         // important
				}
			}
		}
	}
}
