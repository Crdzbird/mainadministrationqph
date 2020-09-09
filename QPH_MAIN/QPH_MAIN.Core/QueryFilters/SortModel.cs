using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Core.QueryFilters
{
    public class SortModel
    {
        public string code { get; set; }
        public string sort { get; set; }
        public string PairAsSqlExpression {
            get
            {
                return $"{code} {sort}";
            } 
        }
    }
}
