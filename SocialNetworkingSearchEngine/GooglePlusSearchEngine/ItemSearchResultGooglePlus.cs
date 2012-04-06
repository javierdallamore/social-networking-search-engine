using System.Runtime.Serialization;

namespace GooglePlusSearchEngine
{
    [DataContract]
    public class ItemSearchResultGooglePlus
    {
        [DataMember(Name = "published")]
        public string CreatedAt { get; set; }
        [DataMember(Name = "actor")]
        public Actor Actor { get; set; }
        [DataMember(Name = "object")]
        public Objeto Objeto { get; set; }
    }

    [DataContract]
    public class Actor
    {
        [DataMember(Name = "displayName")]
        public string FromUser { get; set; }
        [DataMember(Name = "url")]
        public string UserProfile { get; set; }
        [DataMember(Name = "image")]
        public Imagen Imagen { get; set; }
    }

    [DataContract]
    public class Imagen
    {
        [DataMember(Name = "url")]
        public string ProfileImageUrl { get; set; }
    }

    [DataContract]
    public class Objeto
    {
        [DataMember(Name = "url")]
        public string UrlPost { get; set; }
        [DataMember(Name = "content")]
        public string Text { get; set; }
    }
}
