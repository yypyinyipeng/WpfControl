using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPageControlDemo.PageControl;

namespace WpfPageControlDemo.Tools
{
    public static class ResultMessageExtension
    {
        public static ResultMessage<T> SetMessage<T>(this ResultMessage<T> resultMessage, string message)
        {
            resultMessage.Message = message;
            return resultMessage;
        }

        public static ResultMessage SetMessage(this ResultMessage resultMessage, string message)
        {
            resultMessage.Message = message;
            return resultMessage;
        }

        public static ResultMessage<T> SetItems<T>(this ResultMessage<T> resultMessage, List<T> list)
        {
            resultMessage.Items = list;
            return resultMessage;
        }

        public static ResultMessage<T> SetResult<T>(this ResultMessage<T> resultMessage, T result)
        {
            resultMessage.Result = result;
            return resultMessage;
        }

        public static ResultMessage<T> SetPagination<T>(this ResultMessage<T> resultMessage, Pagination pagination)
        {
            resultMessage.Pagination = pagination;
            return resultMessage;
        }
    }
}
