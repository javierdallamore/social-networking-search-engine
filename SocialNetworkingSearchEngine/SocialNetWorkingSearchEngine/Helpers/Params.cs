namespace SocialNetWorkingSearchEngine.Helpers
{
    public static class Params
    {
        //public const int MAX_POST_TO_PROCESS_PER_USER = 4;
        public static readonly int MaxPostToProcessPerUser =
            int.Parse(System.Configuration.ConfigurationManager.AppSettings["max_post_to_process_per_user"]);
    }
}