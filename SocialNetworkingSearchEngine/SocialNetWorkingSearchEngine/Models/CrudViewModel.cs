using System.Collections.Generic;
using Core.Domain;
using Webdiyer.WebControls.Mvc;

namespace SocialNetWorkingSearchEngine.Models
{
    public class CrudViewModel<T>:PagedList<T>
    {
        public CrudViewModel(IList<T> items, int pageIndex, int pageSize) : base(items, pageIndex, pageSize)
        {
        }

        public CrudViewModel(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount) : base(items, pageIndex, pageSize, totalItemCount)
        {
        }

        public string Filter { get; set; }
    }
}