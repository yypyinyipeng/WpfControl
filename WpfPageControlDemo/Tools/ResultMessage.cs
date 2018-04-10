using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPageControlDemo.PageControl;

namespace WpfPageControlDemo.Tools
{
    public class ResultMessage<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        ///  执行结果
        /// </summary>
        public T Result { set; get; }

        /// <summary>
        /// 返回List结果
        /// </summary>
        public List<T> Items { set; get; } = new List<T>();

        /// <summary>
        /// 如果是分页
        /// </summary>
        public Pagination Pagination { set; get; } = new Pagination();

        public ResultMessage()
        {
            IsSuccess = false;
            this.Items = new List<T>();
            this.Pagination = new Pagination();
        }

        public ResultMessage(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            this.Message = message;
            this.Items = new List<T>();
            this.Pagination = new Pagination();
        }

        public static ResultMessage<T> Success(string message = null)
        {
            return new ResultMessage<T>(true, message);
        }

        public static ResultMessage<T> Fail(string message = null)
        {
            return new ResultMessage<T>(false, message);
        }

    }

    public class ResultMessage
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { set; get; }

        public ResultMessage()
        {
            IsSuccess = false;
        }

        public ResultMessage(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }

        public static ResultMessage Success(string message = null)
        {
            return new ResultMessage(true, message);
        }

        public static ResultMessage Fail(string message = null)
        {
            return new ResultMessage(false, message);
        }
    }
}
