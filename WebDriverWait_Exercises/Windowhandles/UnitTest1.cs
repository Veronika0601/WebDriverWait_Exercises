using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Windowhandles
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");


        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Handle_MultipleWindows()
        {
            ReadOnlyCollection<string> windowHandlesIdBefore = driver.WindowHandles;
            driver.FindElement(By.XPath("//a[text()='Click Here']")).Click();
            ReadOnlyCollection<string> windowHandlesId = driver.WindowHandles;
            driver.SwitchTo().Window(windowHandlesId[1]);

            Assert.That(windowHandlesId.Count, Is.EqualTo(2));
            var newWindowTitle = driver.FindElement(By.TagName("h3"));
            Assert.That(newWindowTitle.Text, Is.EqualTo("New Window"));

            driver.SwitchTo().Window(windowHandlesId[0]);



        }
        [Test]
        public void NoSuchWindowsIteraction()
        {
            driver.FindElement(By.XPath(" //a[text()='Click Here']")).Click();

            ReadOnlyCollection<string> windowsId = driver.WindowHandles;
            driver.SwitchTo().Window(windowsId[1]);

            driver.Close();

            try
            {
                driver.SwitchTo().Window(windowsId[1]);


            }
            catch(NoSuchWindowException)
            {
                Assert.Pass("Window was closed");
            }
            finally
            {
                driver.SwitchTo().Window(windowsId[0]);
            }
        }
    }
}