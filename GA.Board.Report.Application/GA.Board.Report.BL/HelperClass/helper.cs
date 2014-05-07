using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Board.Report.BL.HelperClass
{
    public class helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object checkEmpty(object input)
        {
            if ((input == null) || ((string)input == ""))
            {
                return 0;
            }
            else
            {
                return input;
            }
        }
    }
}
