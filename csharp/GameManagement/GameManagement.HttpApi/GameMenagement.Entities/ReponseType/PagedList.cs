using GameManagement.Entities.ReponseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.ReponseType
{
    public class PagedList<T> : List<T>
    {
        public PagedMetaData MetaData { get; set; }
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pagesize)
        {
            MetaData = new PagedMetaData
            {
                TotalCount = count,
                PageSize = pagesize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize)
            };
            AddRange(items);
        }

    }

}
