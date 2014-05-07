using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;

namespace GA.Board.Report.BL.Twitter.Helpers
{
    public class twitterApiCalls
    {
        /// <summary>
        /// Passes in connected twitter service and screen name to query twitter api.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="screenName"></param>
        /// <returns>returns followers count as an integer.</returns>
        public int getTwitterFollowers(TwitterService service, string screenName)
        {
            TwitterUser TwitterUser = service.GetUserProfileFor(
            new GetUserProfileForOptions() { IncludeEntities = false, ScreenName = screenName });

            int followerCount = TwitterUser.FollowersCount;
            return followerCount != null ? followerCount : 0;
        }
    }
}
