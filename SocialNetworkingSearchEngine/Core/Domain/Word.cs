using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Core.Domain
{
    public class Word
    {
        public Word()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        [DisplayName("Palabra")]
        [Required()]
        public virtual string Name { get; set; }
        [DisplayName("Sentimiento")]
        [Required()]
        public virtual string Sentiment { get; set; }
        [DisplayName("Peso")]
        [Required()]
        public virtual int Weigth { get; set; }
    }
}
