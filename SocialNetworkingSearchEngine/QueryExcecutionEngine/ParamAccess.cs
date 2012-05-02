using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryExcecutionEngine
{
    public static class ParamAccess
    {
        public static string ISOLocationCountry
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["post_location_search_param"];
            }
        }
    }
}