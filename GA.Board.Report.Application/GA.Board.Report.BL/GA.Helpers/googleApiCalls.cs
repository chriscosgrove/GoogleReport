using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
//using GA.Board.Report.Data.DBHelpers.Linq;
//using GA.Board.Report.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.BL.GA.Helpers
{
    public class googleApiCalls
    {
        public KeyValuePair<string, string> getData(AnalyticsService service, Profile profile, string startDate, string endDate, string metric)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                DataResource.GaResource.GetRequest request = service.Data.Ga.Get("ga:" + profile.Id, startDate, endDate, "ga:" + metric);
                request.QuotaUser = "MyQuotaUser";
                request.MaxResults = 10;
                request.SamplingLevel = Google.Apis.Analytics.v3.DataResource.GaResource.GetRequest.SamplingLevelEnum.HIGHERPRECISION;
                result = request.Execute().TotalsForAllResults;
                // Changing return type. Google returns IDictionary. Each item would be Dictionary.
                KeyValuePair<string, string> resultItem = new KeyValuePair<string, string>(result.Keys.FirstOrDefault(), result.Values.FirstOrDefault());
                return resultItem;                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return new KeyValuePair<string, string>("","");
        }
    }
}
