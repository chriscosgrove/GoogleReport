using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TweetSharp;

namespace GA.Board.Report.BL.Twitter.Helpers
{
    public class twitterAccess
    {
        // Twitter app created on DC_thomson twitter account
        private const string consumerKey = "";
        private const string consumerSecret = "";

        /// <summary>
        /// Authenticates and establishes connection with twitter api.
        /// </summary>
        /// <returns> TwitterService </returns>
        public TwitterService twitterServiceConnection()
        {
            TwitterService service = new TwitterService(consumerKey, consumerSecret);

            OAuthRequestToken requestToken = service.GetRequestToken();

            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());

            Console.WriteLine("Enter verification code");
            string verifier = Console.ReadLine();
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            service.AuthenticateWith(access.Token, access.TokenSecret);

            return service;
        }
    }
}
