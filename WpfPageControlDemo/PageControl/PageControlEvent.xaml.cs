using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfPageControlDemo.PageControl
{
    /// <summary>
    /// 分页控件   事件驱动
    /// </summary>
    public partial class PageControlEvent : UserControl
    {
        public delegate dynamic PageQuery(int page, int size);


        public PageControlEvent()
        {
            InitializeComponent();

            this.Loaded += delegate
            {
                //上一页
                this.btnPrev.Click += new RoutedEventHandler(btnPrev_Click);

                //下一页
                this.btnNext.Click += new RoutedEventHandler(btnNext_Click);

                //跳转
                this.btnGo.Click += new RoutedEventHandler(btnGo_Click);
            };
        }

        //每页显示多少条
        private int _pageSize = 10;

        //当前是第几页
        private int _pageIndex = 1;

        //DataGrid控件对象
        private DataGrid _grdList;

        //最大页数
        private int _maxIndex = 1;

        //一共多少条
        private int _allNum;

        //查询
        public event PageQuery Query;



        #region 初始化数据

        public void ShowPages(DataGrid grd, int total, int pageIndex, int pageSize)
        {
            this._grdList = grd;
            this._pageSize = pageSize;
            this._pageIndex = pageIndex;
            this._allNum = total;
            SetMaxIndex();
            DisplayPagingInfo();
            if (this._maxIndex > 1)
            {
                this.pageGo.IsEnabled = true;
                this.btnGo.IsEnabled = true;
            }
        }

        #endregion



        #region 画数据

        private void ReadDataTable()
        {
            try
            {
                WaitingBox.Show(App.Current.MainWindow, () =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        IEnumerable result = Query?.Invoke(_pageIndex, _pageSize);
                        this._grdList.ItemsSource = result;
                    });
                }, "正在努力的加载，请稍后...");
            }
            catch (Exception)
            {
                MessageBox.Show("加载出现错误");
            }
            finally
            {
                DisplayPagingInfo();
            }
        }

        #endregion



        #region 画每页显示等数据

        private void DisplayPagingInfo()
        {
            if (this._pageIndex == 1)
            {
                this.btnPrev.IsEnabled = false;
            }
            else
            {
                this.btnPrev.IsEnabled = true;
            }
            if (this._pageIndex == this._maxIndex)
            {
                this.btnNext.IsEnabled = false;
            }
            else
            {
                this.btnNext.IsEnabled = true;
            }
            this.tbkRecords.Text = $"共{this._allNum}条";
            int first = 1;
            int last = _maxIndex;
            this.grid.Children.Clear();
            if (_maxIndex <= 5)
                for (int i = first; i <= last; i++)
                {
                    NumberDisplay(i);
                }
            else
            {
                if (_pageIndex < 4)   //选中页位于前三页   1~3
                {
                    for (int i = first; i <= last; i++)   //初始化分页
                    {
                        if (i < 4)
                        {
                            NumberDisplay(i);
                        }
                        else if (i == 4)
                        {
                            EllipsisDisplay();
                        }
                        else if (i == last)
                        {
                            NumberDisplay(i);
                        }
                    }
                }
                if (_pageIndex > 3 && _pageIndex < last - 2)  //选中页位于中间页码 
                {
                    for (int i = first; i <= last; i++)   //初始化分页
                    {
                        if (i == 1)
                        {
                            NumberDisplay(i);
                        }
                        else if (i == 2)
                        {
                            EllipsisDisplay();
                        }
                        else if (i >= _pageIndex - 1 && i <= _pageIndex + 1)
                        {
                            NumberDisplay(i);
                        }
                        else if (i == last - 1)
                        {
                            EllipsisDisplay();
                        }
                        else if (i == last)
                        {
                            NumberDisplay(i);
                        }
                    }
                }
                else if (_pageIndex > last - 3)   //选中页位于最后三页
                {
                    for (int i = first; i <= last; i++)   //初始化分页
                    {
                        if (i == 1)
                        {
                            NumberDisplay(i);
                        }
                        else if (i == 2)
                        {
                            EllipsisDisplay();
                        }
                        else if (i > last - 3)
                        {
                            NumberDisplay(i);
                        }
                    }
                }
            }
        }

        #endregion



        #region 画显示数据模板

        private void NumberDisplay(int i)
        {
            ColumnDefinition cdf = new ColumnDefinition();
            this.grid.ColumnDefinitions.Add(cdf);
            Button tbl = new Button();
            tbl.BorderThickness = new Thickness(1);
            tbl.Height = 26;
            if (0 < i && i < 10)
            {
                tbl.Width = 22;
            }
            else if (10 <= i && i < 100)
            {
                tbl.Width = 32;
            }
            else if (100 <= i && i < 1000)
            {
                tbl.Width = 36;
            }
            else if (1000 <= i && i < 10000)
            {
                tbl.Width = 44;
            }
            else if (10000 <= i && i < 100000)
            {
                tbl.Width = 52;
            }
            else if (100000 <= i && i < 1000000)
            {
                tbl.Width = 58;
            }
            else
            {
                tbl.Width = 65;
            }
            tbl.Content = i.ToString();
            tbl.Style = FindResource("PageTextBlock3") as Style;
            tbl.Click += new RoutedEventHandler(tbl_Click);
            if (i == this._pageIndex)
                tbl.IsEnabled = false;
            Grid.SetColumn(tbl, this.grid.ColumnDefinitions.Count - 1);
            Grid.SetRow(tbl, 0);
            this.grid.Children.Add(tbl);
        }

        private void EllipsisDisplay()
        {
            ColumnDefinition cdf = new ColumnDefinition();
            this.grid.ColumnDefinitions.Add(cdf);
            TextBlock tbk = new TextBlock();
            tbk.VerticalAlignment = VerticalAlignment.Center;
            tbk.HorizontalAlignment = HorizontalAlignment.Center;
            tbk.TextAlignment = TextAlignment.Center;
            tbk.Margin = new Thickness(5, 0, 0, 10);
            tbk.Width = 12;
            tbk.Height = 15;
            tbk.Text = "…";
            Grid.SetColumn(tbk, this.grid.ColumnDefinitions.Count - 1);
            Grid.SetRow(tbk, 0);
            this.grid.Children.Add(tbk);
        }

        #endregion



        #region 上一页

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this._pageIndex <= 1)
                return;
            this._pageIndex--;
            ReadDataTable();
        }

        #endregion



        #region 下一页

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._pageIndex >= this._maxIndex)
                return;
            this._pageIndex++;
            ReadDataTable();
        }

        #endregion



        #region 设置最多大页面

        private void SetMaxIndex()
        {
            int pages = this._allNum / _pageSize;
            if (this._allNum != (pages * _pageSize))
            {
                if (_allNum < (pages * _pageSize))
                    pages--;
                else
                    pages++;
            }
            this._maxIndex = pages;
        }

        #endregion



        #region 跳转到多少页

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumber(this.pageGo.Text))
            {
                int pageNum = int.Parse(this.pageGo.Text);
                if (pageNum > 0 && pageNum <= this._maxIndex)
                {
                    this._pageIndex = pageNum;
                    ReadDataTable();
                }
                else if (pageNum > this._maxIndex)
                {
                    this._pageIndex = this._maxIndex;
                    ReadDataTable();
                }
            }
            this.pageGo.Text = "";
        }

        #endregion



        #region 分页数字的点击触发事件

        private void tbl_Click(object sender, EventArgs e)
        {
            string text = ((ContentControl)((RoutedEventArgs)e).Source).Content as string;
            if (string.IsNullOrEmpty(text))
                return;
            int index = int.Parse(text);
            this._pageIndex = index;
            if (index > this._maxIndex)
                this._pageIndex = this._maxIndex;
            if (index < 1)
                this._pageIndex = 1;
            ReadDataTable();
        }

        #endregion



        #region 判断是否是数字

        public static bool IsNumber(string valString)
        {
            Match m = RegNumber.Match(valString);
            return m.Success;
        }

        private static readonly Regex RegNumber = new Regex("^[0-9]+$");

        #endregion



        #region  页码输入框宽度适应

        private void pageGo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pageGoLength = this.pageGo.Text.Trim().Length;
            if (pageGoLength <= 3)
            {
                this.pageGo.Width = 36;
            }
            else if (pageGoLength == 4)
            {
                this.pageGo.Width = 44;
            }
            else if (pageGoLength == 5)
            {
                this.pageGo.Width = 52;
            }
            else if (pageGoLength == 6)
            {
                this.pageGo.Width = 58;
            }
            else
            {
                this.pageGo.Width = 65;
            }
        }

        #endregion
    }
}
