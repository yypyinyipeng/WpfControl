using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPageControlDemo.PageControl
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { set; get; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Current { set; get; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 每页的数量
        /// </summary>
        public int Size { set; get; }

        public Pagination()
        {
            this.Size = 20;
            this.Current = 1;
        }
    }
}
