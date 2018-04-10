using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfPageControlDemo.PageControl;

namespace WpfPageControlDemo.Tools
{
    public static class QueryableExtension
    {
        public static ResultMessage<T> ToPage<T, TKey>(this IQueryable<T> list, Expression<Func<T, TKey>> order,
            int page, int size, bool isAsc = false)
        {
            ResultMessage<T> result = ResultMessage<T>.Success();
            Pagination pagination = new Pagination();
            pagination.Total = list.Count();
            pagination.Current = page;
            if (isAsc)
            {
                result.Items = list.OrderBy(order).Skip((page - 1) * size).Take(size).ToList();
            }
            else
            {
                result.Items = list.OrderByDescending(order).Skip((page - 1) * size).Take(size).ToList();
            }
            pagination.Count = GetPageCount(pagination.Total, size);
            return result.SetPagination(pagination);
        }


        public static ResultMessage<T> ToPage<T>(this IQueryable<T> list, int page, int size)
        {
            ResultMessage<T> result = ResultMessage<T>.Success();
            Pagination pagination = new Pagination();
            pagination.Total = list.Count();
            pagination.Current = page;
            result.Items = list.Skip((page - 1) * size).Take(size).ToList();
            pagination.Count = GetPageCount(pagination.Total, size);
            pagination.Size = size;
            return result.SetPagination(pagination);
        }


        private static int GetPageCount(int count, int size)
        {
            int total = 0;
            total = count / size;
            if (count % size != 0)
                total = total + 1;
            return total;
        }
    }
}