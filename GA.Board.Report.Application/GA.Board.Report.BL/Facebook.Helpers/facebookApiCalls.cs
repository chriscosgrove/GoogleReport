using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GA.Board.Report.BL.Facebook.Helpers
{
    public class facebookApiCalls
    {
        /// <summary>
        /// Passes in facebook client and facebook site name to query link. Converts to json object before returning. 
        /// </summary>
        /// <param name="FacebookClient"></param>
        /// <param name="facebookSiteName"></param>
        /// <returns></returns>
        public facebookJson getFacebookFollowers(FacebookClient FacebookClient, string facebookSiteName)
        {
            object facebookUserObject = FacebookClient.Get("https://graph.facebook.com/" + facebookSiteName + "?fields=likes&access_token=" + FacebookClient.AccessToken);
            facebookJson facebookJson = JsonConvert.DeserializeObject<facebookJson>(facebookUserObject.ToString());

            return facebookJson;
        }
    }

    /// <summary>
    /// Structure created for json return.
    /// </summary>
    public struct facebookJson
    {
        public string ID { get; set; }
        public string likes { get; set; }
    }
}
