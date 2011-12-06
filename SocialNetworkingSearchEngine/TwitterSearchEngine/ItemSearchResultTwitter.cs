using System.Runtime.Serialization;

namespace TwitterSearchEngine
{
    [DataContract]
    public class ItemSearchResultTwitter
    {
        [DataMember(Name = "text", Order = 0)]
        public string Text { get; set; }

        [DataMember(Name = "to_user_id", Order = 1)]
        public int? ToUserId { get; set; }

        [DataMember(Name = "from_user", Order = 2)]
        public string FromUser { get; set; }

        [DataMember(Name = "id_str", Order = 3)]
        public string Id { get; set; }

        [DataMember(Name = "from_user_id", Order = 4)]
        public int? FromUserId { get; set; }

        [DataMember(Name = "iso_language_code", Order = 5)]
        public string IsoLanguageCode { get; set; }

        [DataMember(Name = "profile_image_url", Order = 6)]
        public string ProfileImageUrl { get; set; }

        [DataMember(Name = "created_at", Order = 7)]
        public string CreatedAt { get; set; }
    }
}