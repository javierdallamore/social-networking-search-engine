using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Domain
{
    public class QueryDef
    {
        public virtual int Id { get; set; }
        public virtual string Query { get; set; }
        public virtual int MinQueueLength { get; set; }
        public virtual int DaysOldestPost { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string SearchEnginesNames { get; set; }

        public virtual List<string> SearchEnginesNamesList
        {
            get { return SearchEnginesNames.Split(';').ToList(); }
        }
    }
}