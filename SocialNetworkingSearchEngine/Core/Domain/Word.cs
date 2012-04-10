using System;

namespace Core.Domain
{
    public class Word
    {
        public Word()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Sentiment { get; set; }
        public virtual int Weigth { get; set; }
    }
}
