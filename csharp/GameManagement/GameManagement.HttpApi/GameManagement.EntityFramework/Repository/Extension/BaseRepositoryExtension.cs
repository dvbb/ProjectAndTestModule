using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace GameManagement.EntityFramework.Repository.Extension
{
    public static class BaseRepositoryExtension
    {
        public static IQueryable<T> OrderByQuery<T>(this IQueryable<T> queryable, string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return queryable;
            }

            // example input: account, dateCreated desc
            var orderParams = queryString.Trim().Split(separator: ',');
            // 通过反射获取属性名
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(separator: " ")[0];
                // 检索需要排序的[属性名]是否存在
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                {
                    continue;
                }
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}");
            }

            // Final output: account ascending, dataCreated descending
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return queryable.OrderBy(orderQuery);
        }
    }
}
