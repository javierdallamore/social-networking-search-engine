using System;
using System.Collections.Generic;
using Facebook;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Utils;

namespace FacebookSearchEngine
{
    public class FacebookSearchEngine : SearchEnginesBase.Interfaces.ISearchEngine
    {
        public string Name
        {
            get { return "Facebook"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            List<SocialNetworkingItem> list;
            try
            {
                var api = new FacebookAPI();
                var engineURL = GetEngineUrl();
                JSONObject json = api.Get(engineURL + searchParameters + "&type=post&limit=100");

                list = SocialNetworkingItemList(json);
                return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = Name };
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
            }
            return null;
        }

        //Este metodo itera los resultados y crea las entidades de dominio
        private List<SocialNetworkingItem> SocialNetworkingItemList(JSONObject json)
        {
            List<SocialNetworkingItem> list = new List<SocialNetworkingItem>();
            SocialNetworkingItem user;

            JSONObject[] data = json.Dictionary["data"].Array;

            for (int i = 0; i < data.Length; i++)
            {
                var post = data[i].Dictionary;

                if (!post.ContainsKey("message")) continue;

                user = new SocialNetworkingItem();
                user.SocialNetworkName = Name;
                user.UserName = post["from"].Dictionary["name"].String;
                user.Content = post["message"].String;
                user.CreatedAt = DateTimeOffset.Parse(post["created_time"].String).UtcDateTime;
                user.ProfileImage = GetProfilePictureUrl().Replace("?", post["from"].Dictionary["id"].String);

                var postid = post["id"].String;
                
                user.UrlPost = GetProfileUrl() + postid.Substring(postid.IndexOf('_') + 1);
                user.UrlProfile = GetProfileUrl() + post["from"].Dictionary["id"].String;
                // Facebook no me da la fuente del post
                user.Source = "";

                list.Add(user);
            }

            return list;
        }

        private string GetProfilePictureUrl()
        {
            return "https://graph.facebook.com/?/picture";
        }

        private string GetProfileUrl()
        {
            return "https://www.facebook.com/";
        }

        private string GetEngineUrl()
        {
            return "/search?q=";
        }
    }
}
