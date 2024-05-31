using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.RequstFeatures
{
    public abstract class QueryStringParameters
    {
        private const int _maxPageSize = 100;
        private int _pagesize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pagesize;
            set => _pagesize = value > _maxPageSize ? _maxPageSize : value;
        }
        public string OrderBy { get; set; }
    }
}
