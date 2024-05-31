using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.RequstFeatures
{
    public class PlayerParameter : QueryStringParameters
    {
        public DateTime MinDateCreate { get; set; }
        public DateTime MaxDateCreate { get; set; } = DateTime.Now;

        public bool ValidateDateCratedRange => MaxDateCreate > MinDateCreate;

        public string? Account { get; set; }

        public PlayerParameter()
        {
            OrderBy = "account";
        }
    }
}
