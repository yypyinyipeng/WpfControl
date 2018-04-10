using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WpfPageControlDemo.PageControl;

namespace WpfPageControlDemo.ViewModel
{
    public class ViewModelPageBase : ViewModelBase
    {
        public ViewModelPageBase()
        {
            QueryCommand = new RelayCommand<object>(ExecuteQuery);
        }


        public RelayCommand<object> QueryCommand { set; get; }


        #region 属性

        private Pagination _pagination;

        public Pagination Pagination
        {
            get => _pagination;
            set
            {
                _pagination = value; RaisePropertyChanged
                    (() => Pagination);
            }
        }
        #endregion



        #region 附加方法 执行查询 子类需要重写

        public virtual void ExecuteQuery(object param)
        {
            //子类去实现
        }

        #endregion
    }
}
