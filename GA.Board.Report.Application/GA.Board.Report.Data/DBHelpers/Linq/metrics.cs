using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.Data.DBHelpers.Linq
{
    public class metrics
    {
        /// <summary>
        /// Accesses Google Board Reporting DB 
        /// </summary>
        /// <returns> Returns all rows from googleMetrics Table as googleMetrics Type </returns>
        public IQueryable<googleMetrics> getMetrics()
        {
            dbContextContainer dbContext = new dbContextContainer();
            try
            {
                IQueryable<googleMetrics> googleMetrics = (from x in dbContext.googleMetrics select x);
                return googleMetrics;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }

        /// <summary>
        ///  Average Users divided by 28 days or 35 days
        /// </summary>
        /// <returns> Integer  </returns>
        public int getAvgUniquesPerDay()
        {
            // Todo: Make calculation and return value.
            return 0;
        }
    }
}
