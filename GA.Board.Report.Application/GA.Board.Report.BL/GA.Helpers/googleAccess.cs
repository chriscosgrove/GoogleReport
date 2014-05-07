using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GA.Board.Report.BL.GA.Helpers
{
    public class googleAccess
    {
        AnalyticsService service = new AnalyticsService();
        const string clientId = "";
        const string clientSecret = "";
        const string keyFile = @"";


        /// <summary>
        /// Creates Connection to Google analytics 
        /// </summary>
        public AnalyticsService accessGoogleAnalytics()
        {

            UserCredential credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
                new[] { AnalyticsService.Scope.AnalyticsReadonly },
                "user",
                CancellationToken.None
                ).Result;

            AnalyticsService service = new AnalyticsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "Google Analytic Board Report",
                });

            return service;
        }
    }
}
