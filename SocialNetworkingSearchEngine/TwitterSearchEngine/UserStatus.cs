using System;

namespace TwitterSearchEngine
{
    /// <summary>
    /// This class represent a User status from any social networking
    /// </summary>
    public class UserStatus
    {
        public string UserName { get; set; }

        public string ProfileImage { get; set; }

        public DateTime StatusDate { get; set; }

        public string Status { get; set; }
    }
}
