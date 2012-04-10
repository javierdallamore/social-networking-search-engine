using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain
{
    public class Query
    {
        public Query()
        {
            Id = new Guid();
        }

        public virtual Guid Id { get; set; }
        public virtual string QueryString { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime LastExecutionDate { get; set; }
        public virtual string SearchEnginesNames {get; set;}

        public virtual List<string> SearchEnginesNamesList
        {
            get { return SearchEnginesNames.Split(';').ToList(); }
        }
    }
}
