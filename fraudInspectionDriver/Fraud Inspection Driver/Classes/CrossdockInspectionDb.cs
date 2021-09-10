using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Class is not used in the project.
 */
namespace RLJones.FraudInspectionDriver.Classes
{
    class CrossdockInspectionDb
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="station"></param>
        public void InsertCrossdockInspection(string orderNumber,string station)
        {
            db_elptestDataSetTableAdapters.tbl_CrossdockInspectionTableAdapter adapter =
                new db_elptestDataSetTableAdapters.tbl_CrossdockInspectionTableAdapter();

            adapter.Insert(orderNumber, station, DateTime.Now);
        }
    }
}
