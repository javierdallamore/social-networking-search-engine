using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Domain
{
    public class QueryDef
    {
        public QueryDef()
        {
            this.Query = "TwitterSearchEngine;FacebookSearchEngine;GooglePlusSearchEngine";
        }

        public virtual int Id { get; set; }
        public virtual string Query { get; set; }
        public virtual int MinQueueLength { get; set; }
        public virtual int DaysOldestPost { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string SearchEnginesNames { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; } 

        public virtual List<string> SearchEnginesNamesList
        {
            get { return SearchEnginesNames == null ? new List<string>() : SearchEnginesNames.Split(';').ToList(); }
        }
    }
}