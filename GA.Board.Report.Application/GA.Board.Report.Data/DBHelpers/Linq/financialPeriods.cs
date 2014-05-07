using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.Data.DBHelpers.Linq
{
    public class financialPeriods
    {
        /// <summary>
        /// Accesses Google Board Reporting DB 
        /// </summary>
        /// <returns> Returns all rows from googleFinancialPeriods Table as googleFinancialPeriods Type </returns>
        public IQueryable<googleFinancialPeriods> getFinancialPeriods()
        {
            dbContextContainer dbContext = new dbContextContainer();
            try
            {
                IQueryable<googleFinancialPeriods> googleFinancialPeriods = (from x in dbContext.googleFinancialPeriods select x);
                return googleFinancialPeriods;
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
        /// 
        /// </summary>
        /// <param name="periodID"></param>
        public void updateComplete(int periodID)
        {
            dbContextContainer dbContext = new dbContextContainer();
            googleFinancialPeriods googleFinancialPeriod = new googleFinancialPeriods();
            try
            {
                // Updates the financial period table completed column.
                googleFinancialPeriod = (from x in dbContext.googleFinancialPeriods
                                 where x.ID == periodID
                                 select x).First();

                googleFinancialPeriod.completed = true;
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                googleFinancialPeriod = null;
            }
        }
    }
}
