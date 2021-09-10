using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLJones.FraudInspectionDriver.Classes
{
    class Tools
    {
        /// <summary>
        /// Static object to singleton the usage of webdriver.
        /// </summary>
        static public FlexLinkChromeDriver FlexLinkChrome = new FlexLinkChromeDriver();
    }
}
