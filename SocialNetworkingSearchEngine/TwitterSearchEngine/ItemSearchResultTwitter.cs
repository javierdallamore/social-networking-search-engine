using System.Runtime.Serialization;

namespace TwitterSearchEngine
{
    [DataContract]
    public class ItemSearchResultTwitter
    {
        //"created_at":"Tue, 06 Dec 2011 03:37:35 +0000",
        [DataMember(Name = "created_at", Order = 0)]
        public string CreatedAt { get; set; }
        //"from_user":"MeliCiccarella",
        [DataMember(Name = "from_user", Order = 1)]
        public string FromUser { get; set; }
        //"from_user_id_str":"137823667",
        [DataMember(Name = "from_user_id_str", Order = 2)]
        public string FromUserIdString { get; set; }
        //"from_user_name":"Meli Ciccarella",
        [DataMember(Name = "from_user_name", Order = 3)]
        public string FromUserName { get; set; }
        //"geo":null,
        [DataMember(Name = "geo", Order = 4)]
        public string Geo { get; set; }
        //"id_str":"143896843598827522",
        [DataMember(Name = "id_str", Order = 5)]
        public string IdString { get; set; }
        //"iso_language_code":"es",
        [DataMember(Name = "iso_language_code", Order = 6)]
        public string IsoLanguageCode { get; set; }
        //"metadata":{"result_type":"recent"},
        //[DataMember(Name = "", Order = 7)]
        //public string Metadata { get; set; }
        //"profile_image_url":"http://a3.twimg.com/profile_images/1676411471/tumblr_lmghs0NzIf1qdboqao1_500_normal.jpg",
        [DataMember(Name = "profile_image_url", Order = 8)]
        public string ProfileImageUrl { get; set; }
        //"source":"&lt;a href=&quot;http://twitter.com/&quot;&gt;web&lt;/a&gt;",
        [DataMember(Name = "source", Order = 9)]
        public string Source { get; set; }
        //"text":"@MarianIglesias proba esto http://t.co/Pr9MvOVL",
        [DataMember(Name = "text", Order = 10)]
        public string Text { get; set; }
        //"to_user":"MarianIglesias",
        [DataMember(Name = "to_user", Order = 11)]
        public string ToUser { get; set; }
        //"to_user_id_str":"71945220",
        [DataMember(Name = "to_user_id_str", Order = 12)]
        public string ToUserIdString { get; set; }
        //"to_user_name":"Marian Iglesias \u2661",
        [DataMember(Name = "to_user_name", Order = 13)]
        public string ToUserName { get; set; }
        //"in_reply_to_status_id_str":"143896250515853312"
        [DataMember(Name = "in_reply_to_status_id_str", Order = 14)]
        public string InReplyToStatusIdString { get; set; }
    }
}