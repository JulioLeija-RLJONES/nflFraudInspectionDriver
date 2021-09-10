using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace RLJones.FraudInspectionDriver.Classes
{
    public class FlexLinkChromeDriver
    {
        private IWebDriver Driver;
        /// <summary>
        /// Class carries out operations to launch Chrome with Selenium API.
        /// </summary>
        public FlexLinkChromeDriver()
        {
            
        }
        /// <summary>
        /// Launches a new Chrome browser using
        /// Remarks:
        /// <list type="number">
        /// <item><description>If path of chromedriver.exe is not specified, the tool will use the one included in the project package.</description></item>
        /// </list>
        /// </summary>
        /// <param name="chromeDriverPath"></param>
        /// <param name="hideCmdPrompt"></param>
        public void Open(string chromeDriverPath = "", bool hideCmdPrompt = true)
        {
            if (chromeDriverPath == "")
                chromeDriverPath = Path.GetDirectoryName(Application.ExecutablePath);

            var chromeDriverService =
                ChromeDriverService.CreateDefaultService(chromeDriverPath);

            chromeDriverService.HideCommandPromptWindow = hideCmdPrompt;

            Driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
        }
        /// <summary>
        /// With the launched Chrome browser, navigates to the given URL.
        /// </summary>
        /// <param name="url"></param>
        public void Navigate(string url)
        {
            if (Driver == null) 
                return;

            Driver.Url = url;
        }
        /// <summary>
        /// When browser is loading the web document, waits for the provided elementId to be available then continue the execution of tasks.
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        public IWebElement WaitForElementById(string elementId, int timeoutSeconds=10)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
                var element = wait.Until(x => x.FindElement(By.Id(elementId)));
                return element;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// When browser is loading the web document, waits for the provided tagName to be available then continue the execution of tasks.
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        public IWebElement WaitForElementByTagName(string tagName, int timeoutSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
                var element = wait.Until(x => x.FindElement(By.TagName(tagName)));
                return element;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Searches inside the definition of the web element for the element located inthe provided xPath
        /// </summary>
        /// <param name="elementId"></param>
        /// <param name="timeoutSeconds"></param>
        /// <returns></returns>
        public IWebElement FindElementByXPath(IWebElement element, string xPath)
        {
            try
            {
                var outputElement = element.FindElement(By.XPath(xPath));
                return outputElement;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Minimizes window of browser under control of webdriver.
        /// </summary>
        public void Minimize()
        {
            if (Driver == null)
                return;

            try
            {
                Driver.Manage().Window.Minimize();
            }
            catch { }
        }
        /// <summary>
        /// Maximizes window of browser under control of webdriver.
        /// </summary>
        public void Maximize()
        {
            if (Driver == null)
                return;

            try
            {
                Driver.Manage().Window.Maximize();
            }
            catch { }
        }
        /// <summary>
        /// Closes window of browser under control of webdriver.
        /// </summary>
        public void Close()
        {
            if (Driver != null)
            {
                try
                {
                    Driver.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// Checks if the chrome browser launched is still active by trying to retrieve the document title. 
        /// </summary>
        /// <returns>True if webdriver still sees the browser and False otherwise.</returns>
                public bool IsAlive()
        {
            try
            {
                string title = Driver.Title;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Closes the browser under control.
        /// </summary>
        public void Quit()
        {
            try
            {
                if (Driver != null)
                    Driver.Quit();
            }
            catch { }
        }
    }
}
