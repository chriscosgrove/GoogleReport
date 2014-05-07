using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Facebook;

namespace GA.Board.Report.BL.Facebook.Helpers
{
    public class facebookAccess 
    {

        public string ApplicationId 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings["ApplicationId"]; 
            } 
        }

        public string ExtendedPermissions 
        { 
            get
            {
                return ConfigurationManager.AppSettings["ExtendedPermissions"];
            } 
        }

        public string AppSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationSecret"];
            }
        }

        public string AccessToken { get; set; }

        /// <summary>
        /// Creates connection to Facebook. passes parameters via app config settings.
        /// </summary>
        /// <returns>A Facebook client connection</returns>
        public FacebookClient createFacebookConnection()
        {
            FacebookClient FacebookClient = new FacebookClient();

            try
            {
                dynamic result = FacebookClient.Get("oauth/access_token", new
                {
                    client_id = this.ApplicationId,
                    client_secret = this.AppSecret,
                    grant_type = this.ExtendedPermissions,
                    redirect_uri = "http://www.dcthomson.co.uk/"
                });

                FacebookClient.AccessToken = result.access_token;
                return FacebookClient;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        
    }
}
