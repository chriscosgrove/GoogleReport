using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.Board.Report.BL.GA.Helpers;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using GA.Board.Report.Data;
using GA.Board.Report.Data.DBHelpers.Linq;
using GA.Board.Report.BL.HelperClass;
using GA.Board.Report.BL.Twitter.Helpers;
using GA.Board.Report.BL.Facebook.Helpers;
using TweetSharp;
using Facebook;

namespace GA.Board.Report.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> info = new List<string>();
            string startDate = string.Empty;
            string endDate = string.Empty;
            bool isComplete;

            int websiteID;
            int periodID;
            int metricID;

            // Create Google Analytics API connection
            googleAccess googleConnection = new googleAccess();
            AnalyticsService googleService = googleConnection.accessGoogleAnalytics();
            googleApiCalls googleApiCalls = new googleApiCalls();
            // Google Profile used for credentials in web api call
            Profile profile = new Profile();

            // Create Twitter API Connection 
            twitterAccess twitterAccess = new twitterAccess();
            twitterApiCalls twitterApiCalls = new twitterApiCalls();
            TwitterService twitterService = twitterAccess.twitterServiceConnection();

            // Create Facebook API Connection 
            facebookAccess facebookAccess = new facebookAccess();
            FacebookClient FacebookClient = facebookAccess.createFacebookConnection();
            facebookApiCalls facebookApiCalls = new facebookApiCalls(); 
            
            // Create instances of DAL classes for accessing db data.
            websites googleWebsites = new websites();
            financialPeriods googlefinancialPeriods = new financialPeriods();
            metrics googleMetrics = new metrics();
            results googleResults = new results();
            helper helper = new helper();

            KeyValuePair<string, string> metricResult = new KeyValuePair<string, string>();
            IDictionary<string, string> results = new Dictionary<string, string>();

            //Get all Websites from table
            List<googleWebsites> websites = googleWebsites.getWebsites().ToList();
            //Get all financial dates from table
            List<googleFinancialPeriods> financialPeriods = googlefinancialPeriods.getFinancialPeriods().ToList();
            //Get all metrics from table
            List<googleMetrics> metrics = googleMetrics.getMetrics().ToList();
            //Get all twitter accounts from website table where twitter screen name column is not null. 
            List<googleWebsites> twitterAccounts = googleWebsites.getTwitterAccounts().ToList();
            //Get all facebook accounts from website table where facebook screen name column is not null. 
            List<googleWebsites> facebookAccounts = googleWebsites.getFacebookAccounts().ToList();

            for (int ifinancialPeriods = 0; ifinancialPeriods < financialPeriods.Count(); ifinancialPeriods++)
            {
                for (int iwebsites = 0; iwebsites < websites.Count; iwebsites++)
                {
                    for (int imetrics = 0; imetrics < metrics.Count(); imetrics++)
                    {
                        profile.Id = websites[iwebsites].google_ProfileID.ToString();
                        isComplete = financialPeriods[ifinancialPeriods].completed;

                        if (!isComplete)
                        { 
                            // Changing to correct date format. 
                            startDate = financialPeriods[ifinancialPeriods].start_Date.ToString("yyyy-MM-dd");
                            endDate = financialPeriods[ifinancialPeriods].end_Date.ToString("yyyy-MM-dd");

                            //Check if the metric is not null. Daily uniques are calculated separately. 
                            if (!string.IsNullOrEmpty(metrics[imetrics].metric_GoogleQuery) && metrics[imetrics].metric_Source.ToLower() == "website")
                            {
                                //Create row in results table.
                                websiteID = websites[iwebsites].ID;
                                periodID = financialPeriods[ifinancialPeriods].ID;
                                metricID = metrics[imetrics].ID;

                                metricResult = googleApiCalls.getData(googleService, profile, startDate, endDate, metrics[imetrics].metric_GoogleQuery);
                                googleResults.insertMetric(websiteID, periodID, metricID, metricResult.Value);

                                #region 'Writing to text file'
                                info.Add("websiteID: " + websiteID + " " + websites[iwebsites].google_AccountName);
                                info.Add("periodID: " + periodID + " " + financialPeriods[ifinancialPeriods].start_Date + " - " + financialPeriods[ifinancialPeriods].end_Date);
                                info.Add("metricID: " + metricID + " " + metrics[imetrics].metric_Name);
                                info.Add(imetrics.ToString());
                                #endregion
                            }

                            // Check if the current metric is unique visitors by id (10)
                            if (metrics[imetrics].ID == 10)
                            {
                                websiteID = websites[iwebsites].ID;
                                periodID = financialPeriods[ifinancialPeriods].ID;
                                metricID = metrics[imetrics].ID;

                                //// Get number of days of financial Period
                                int numberDaysInPeriod = financialPeriods[ifinancialPeriods].days;
                                //// Make user request to get number of unique visitors of the current period.
                                metricResult = googleApiCalls.getData(googleService, profile, startDate, endDate, "users");

                                int avgDailyUniques = Convert.ToInt32(helper.checkEmpty(metricResult.Value)) / numberDaysInPeriod;
                                //// Insert into results table
                                googleResults.insertMetric(websiteID, periodID, metricID, avgDailyUniques.ToString());
                                Console.WriteLine("Added a Unique visitor row: " + avgDailyUniques);
                            }

                            if (!string.IsNullOrEmpty(websites[iwebsites].twitter_ScreenName) && metrics[imetrics].metric_Source.ToLower() == "twitter")
                            {
                                websiteID = websites[iwebsites].ID;
                                periodID = financialPeriods[ifinancialPeriods].ID;
                                metricID = metrics[imetrics].ID;
                                //Twitter Call in here
                                int twiiterFollowerCount = twitterApiCalls.getTwitterFollowers(twitterService, websites[iwebsites].twitter_ScreenName);
                                // Insert twitter metric
                                googleResults.insertMetric(websiteID, periodID, metricID, twiiterFollowerCount.ToString());

                                #region 'Writing to text file'
                                info.Add("Twitter Stats");
                                info.Add(websites[iwebsites].twitter_Url);
                                info.Add("Followers: " + twiiterFollowerCount.ToString());
                                #endregion
                                Console.WriteLine("Made Twitter Call: " + websites[iwebsites].twitter_ScreenName);
                            }

                            if (!string.IsNullOrEmpty(websites[iwebsites].facebook_ScreenName) && metrics[imetrics].metric_Source.ToLower() == "facebook")
                            {
                                websiteID = websites[iwebsites].ID;
                                periodID = financialPeriods[ifinancialPeriods].ID;
                                metricID = metrics[imetrics].ID;
                                //Facebook Call in here
                                facebookJson facebookJson = facebookApiCalls.getFacebookFollowers(FacebookClient, websites[iwebsites].facebook_ScreenName);
                                // Insert twitter metric
                                googleResults.insertMetric(websiteID, periodID, metricID, facebookJson.likes.ToString());

                                #region 'Writing to text file'
                                info.Add("Facebook Stats");
                                info.Add(websites[iwebsites].facebook_Url);
                                info.Add(facebookJson.likes);
                                info.Add("");
                                #endregion
                                Console.WriteLine("Made Facebook Call: " + websites[iwebsites].facebook_ScreenName);
                            }
                        }
                    }
                }
                googlefinancialPeriods.updateComplete(financialPeriods[ifinancialPeriods].ID);
                Console.WriteLine("Financial Period: " + ifinancialPeriods );
            }
            System.IO.File.WriteAllLines(@"C:\Users\ccosgrove\Desktop\program\Debug.txt", info);
        }
    }
}

