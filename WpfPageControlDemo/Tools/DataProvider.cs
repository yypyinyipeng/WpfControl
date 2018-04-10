using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPageControlDemo.Tools
{
    public class DataProvider
    {
        private readonly List<User> _data;

        public DataProvider()
        {
            _data = new List<User>();

            Random random = new Random();

            for (int i = 1; i <= 150; i++)
            {
                int num = random.Next(20,100);

                User user = new User
                {
                    Id = i,
                    Name = "测试" + i * num,
                    Gender = "汉纸",
                    Age = $"{num}岁",
                    Height = $"18{num % 10}cm",
                    Weight = $"7{num % 10}kg"
                };

                _data.Add(user);
            }


        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public ResultMessage<User> GetPagingData(int page = 1, int size = 20)
        {
            var query = _data.AsQueryable();

            ResultMessage<User> result = query.ToPage(x => x.Id, page, size);

            return ResultMessage<User>.Success().SetItems(result.Items).SetPagination(result.Pagination);
        }
    }
}
