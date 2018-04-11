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
using WpfPageControlDemo.Tools;

namespace WpfPageControlDemo.Windows
{
    /// <summary>
    /// EventPageControlTest.xaml 的交互逻辑
    /// </summary>
    public partial class EventPageControlTest : Window
    {
        public EventPageControlTest()
        {
            InitializeComponent();

            DataProvider dataProvider = new DataProvider();
            ResultMessage<User> result = dataProvider.GetPagingData();
            DataGridTest.ItemsSource = result.Items;
            this.page.ShowPages(this.DataGridTest, result.Pagination.Total, result.Pagination.Current, 20);
            this.page.Query += (page, size) => dataProvider.GetPagingData(page, size).Items;
        }
    }
}
