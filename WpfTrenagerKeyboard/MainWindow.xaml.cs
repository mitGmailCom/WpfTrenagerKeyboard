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

namespace WpfTrenagerKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void wind_Initialized(object sender, EventArgs e)
        {
            wind.Height = System.Windows.SystemParameters.VirtualScreenHeight * 0.6;
            wind.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            //gridSettings1.Width = System.Windows.SystemParameters.VirtualScreenWidth / 2;
            //gridSettings2.Width = System.Windows.SystemParameters.VirtualScreenWidth / 2;
            //btnStart.Width = System.Windows.SystemParameters.VirtualScreenWidth * 0.25;
            //btnStop.Width = System.Windows.SystemParameters.VirtualScreenWidth * 0.25;
            //this.Left = SystemParameters.WorkArea.Width - this.Width;
            //this.Top = SystemParameters.WorkArea.Height - this.Height;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gridSettings1.Width = wind.ActualWidth / 2;
            gridSettings2.Width = wind.ActualWidth / 2;
            txtBlShowText.Height = firstRowKyeboard.ActualHeight/2;
            txtBlInputText.Height = firstRowKyeboard.ActualHeight / 2;
            //StPanelButtons.Width = wind.ActualWidth;
            //btnStart.Width = wind.Width * 0.25;
            //btnStop.Width = wind.Width * 0.25;
            double t1 = firstRowKyeboard.ActualHeight;

        }

       
    }
}
