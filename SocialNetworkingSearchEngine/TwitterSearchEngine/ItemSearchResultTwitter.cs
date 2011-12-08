using System.Runtime.Serialization;

namespace TwitterSearchEngine
{
    [DataContract]
    public class ItemSearchResultTwitter
    {
        [DataMember(Name = "created_at", Order = 0)]
        public string CreatedAt { get; set; }
        [DataMember(Name = "from_user", Order = 1)]
        public string FromUser { get; set; }
        [DataMember(Name = "from_user_id_str", Order = 2)]
        public string FromUserIdString { get; set; }
        [DataMember(Name = "from_user_name", Order = 3)]
        public string FromUserName { get; set; }
        [DataMember(Name = "geo", Order = 4)]
        public string Geo { get; set; }        
        [DataMember(Name = "id_str", Order = 5)]
        public string IdString { get; set; }
        [DataMember(Name = "iso_language_code", Order = 6)]
        public string IsoLanguageCode { get; set; }
        //"metadata":{"result_type":"recent"},
        //[DataMember(Name = "metadata", Order = 7)]
        //public string Metadata { get; set; }
        [DataMember(Name = "profile_image_url", Order = 8)]
        public string ProfileImageUrl { get; set; }
        [DataMember(Name = "source", Order = 9)]
        public string Source { get; set; }
        [DataMember(Name = "text", Order = 10)]
        public string Text { get; set; }
        [DataMember(Name = "to_user", Order = 11)]
        public string ToUser { get; set; }
        [DataMember(Name = "to_user_id_str", Order = 12)]
        public string ToUserIdString { get; set; }
        [DataMember(Name = "to_user_name", Order = 13)]
        public string ToUserName { get; set; }
        [DataMember(Name = "in_reply_to_status_id_str", Order = 14)]
        public string InReplyToStatusIdString { get; set; }
    }
}