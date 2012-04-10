using System;

namespace Core.Domain
{
    public class QueryDef
    {
        public virtual Guid Id { get; set; }
        public virtual string Query { get; set; }
        public virtual int MinQueueLength { get; set; }
        public virtual int DaysOldestPost { get; set; }
        public virtual bool Enabled { get; set; }
    }
}