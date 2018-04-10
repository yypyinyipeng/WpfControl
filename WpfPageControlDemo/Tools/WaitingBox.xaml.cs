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
using System.Windows.Shapes;

namespace WpfPageControlDemo.PageControl
{
    /// <summary>
    /// WaitingBox.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingBox : Window
    {
        public string Text
        {
            get => this.TxtMessage.Text;
            set => this.TxtMessage.Text = value;
        }

        private readonly Action _callback;

        public WaitingBox(Action callback)
        {
            InitializeComponent();
            this._callback = callback;
            this.Loaded += WaitingBox_Loaded;
        }

        private void WaitingBox_Loaded(object sender, RoutedEventArgs e)
        {
            this._callback.BeginInvoke(this.OnComplate, null);
        }

        private void OnComplate(IAsyncResult ar)
        {
            this.Dispatcher.Invoke(new Action(this.Close));
        }

        /// <summary>
        /// 显示等待框，owner指定宿主视图元素，callback为需要执行的方法体（需要自己做异常处理）。
        /// 目前等等框为模式窗体
        /// </summary>
        public static void Show(FrameworkElement owner, Action callback, string mes = "先喝杯茶休息休息...")
        {
            WaitingBox win = new WaitingBox(callback);
            Window pwin = Window.GetWindow(owner);
            win.Owner = pwin;
            win.Text = mes;
            var loc = owner.PointToScreen(new Point());
            win.Left = loc.X + (owner.ActualWidth - win.Width) / 2;
            win.Top = loc.Y + (owner.ActualHeight - win.Height) / 2;
            win.ShowDialog();
        }
    }
}
