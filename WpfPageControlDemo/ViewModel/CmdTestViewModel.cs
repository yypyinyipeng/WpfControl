using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPageControlDemo.Tools;
using WpfPageControlDemo.ViewModel;

namespace WpfPageControlDemo.ViewModel
{
    public class CmdTestViewModel : ViewModelPageBase
    {

        #region  构造

        public CmdTestViewModel() { }

        #endregion



        #region  数据源

        private List<User> _userList;

        public List<User> UserList
        {
            get => _userList;
            set { _userList = value; RaisePropertyChanged(() => UserList); }
        }

        #endregion



        #region  子类实现加载

        public override void ExecuteQuery(object param)
        {
            int page = Convert.ToInt32(param);
            LoadUsers(page);
        }

        #endregion



        #region   查询数据

        public void LoadUsers(int page, int size = 20)
        {
            DataProvider dataProvider = new DataProvider();
            ResultMessage<User> result = dataProvider.GetPagingData(page, size);
            UserList = result.Items;
            Pagination = result.Pagination;
        }

        #endregion
    }
}
