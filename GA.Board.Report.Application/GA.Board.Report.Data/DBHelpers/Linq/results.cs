using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.Data.DBHelpers.Linq
{
    public class results
    {
        /// <summary>
        /// Accesses Google Board Reporting DB. Inserts metric along with primary keys into results table. Updates financial period table column completed with true. 
        /// </summary>
        /// <param name="websiteID"></param>
        /// <param name="periodID"></param>
        /// <param name="metricID"></param>
        /// <param name="metricValue"></param>
        public void insertMetric( int websiteID, int periodID, int metricID, string metricValue )
        {
            dbContextContainer dbContext = new dbContextContainer();
            googleResults googleResult = new googleResults();
            
            try
            {
                googleResult.websiteID = websiteID;
                googleResult.periodID = periodID;
                googleResult.metricID = metricID;
                googleResult.value = metricValue;

                dbContext.googleResults.Add(googleResult);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                googleResult = null;
            }
        }
    }
}
