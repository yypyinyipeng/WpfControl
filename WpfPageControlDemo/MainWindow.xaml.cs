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
using WpfPageControlDemo.Windows;

namespace WpfPageControlDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CmdBtn_Click(object sender, RoutedEventArgs e)
        {
            CmdPageControlTest cmdPageControlTest = new CmdPageControlTest();
            cmdPageControlTest.ShowDialog();
        }

        private void EvtBtn_Click(object sender, RoutedEventArgs e)
        {
            EventPageControlTest eventPageControlTest = new EventPageControlTest();
            eventPageControlTest.ShowDialog();
        }
    }
}
