using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.Data.DBHelpers.Linq
{
    public class websites
    {
        /// <summary>
        /// Accesses Google Board Reporting DB 
        /// </summary>
        /// <returns> Returns all rows from googleWebsites Table as googleWebsites Type </returns>
        public IQueryable<googleWebsites> getWebsites()
        {
            dbContextContainer dbContext = new dbContextContainer();
            try
            {
                IQueryable<googleWebsites> googleWebsites = (from x in dbContext.googleWebsites select x);
                return googleWebsites;
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
        /// Accesses Google Board Reporting DB 
        /// </summary>
        /// <returns> Returns all rows from googleWebsites that have twitter accounts </returns>
        public IQueryable<googleWebsites> getTwitterAccounts()
        {
            dbContextContainer dbContext = new dbContextContainer();
            try
            {
                IQueryable<googleWebsites> twitterAccounts = (from x in dbContext.googleWebsites where x.twitter_ScreenName != null
                                                             select x);
                return twitterAccounts;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Accesses Google Board Reporting DB 
        /// </summary>
        /// <returns> Returns all rows from googleWebsites that have facebook accounts </returns>
        public IQueryable<googleWebsites> getFacebookAccounts()
        {
            dbContextContainer dbContext = new dbContextContainer();
            try
            {
                IQueryable<googleWebsites> facebookAccounts = (from x in dbContext.googleWebsites
                                                              where x.facebook_ScreenName != null
                                                              select x);
                return facebookAccounts;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
    }
}
