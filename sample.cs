using System;
using OpenQA.Selenium;
using static System.Environment;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace csharp_selenium_lambdatest
{
    class SingleTest
    {
        public static void execute()
        {
            // Update your lambdatest credentials
            String LT_USERNAME = GetEnvironmentVariable("LT_USERNAME");
            String LT_ACCESS_KEY = GetEnvironmentVariable("LT_ACCESS_KEY");
            IWebDriver driver;
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "latest";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", LT_USERNAME);
            ltOptions.Add("accessKey", LT_ACCESS_KEY);
            ltOptions.Add("platformName", "Windows 10");
            ltOptions.Add("project", "Demo LT");
            ltOptions.Add("smartUI.build", "Build 1");

            ltOptions.Add("w3c", true);
            ltOptions.Add("smartUI.project", "27th_Feb");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);

            driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), capabilities);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.Navigate().GoToUrl("https://lambdatest.com");
                ((IJavaScriptExecutor)driver).ExecuteScript("smartui.takeScreenshot=pic1");


                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=passed");
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("lambda-status=failed");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
