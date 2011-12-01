using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetWorkingSearchEngine.Models
{
    #region Models
    public class SearchModel
    {
        [Required]
        [DisplayName("Parameters")]
        public string Parameters { get; set; }

        [DisplayName("Result")]
        public string Result { get; set; }
    }
    #endregion
}
