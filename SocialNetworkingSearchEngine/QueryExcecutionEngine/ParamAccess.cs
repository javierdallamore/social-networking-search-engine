using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryExcecutionEngine
{
    public static class ParamAccess
    {
        /// <summary>
        /// Codigo de pais en ISO 3166-1
        /// </summary>
        public static string ISOLocationCountry
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["post_location_search_param"];
            }
        }

        /// <summary>
        /// Lenguaje en ISO 639-1
        /// </summary>
        public static string ISOLanguageCode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["post_language_search_param"];
            }
        }

        public static int Interval_between_searchs
        {
            get
            {
                int result;
                if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings["time_to_look_for_querys_in_min_param"], out result))
                    return result * 60 * 1000;
                return 30 * 60 * 1000;
            }
        }
    }
}