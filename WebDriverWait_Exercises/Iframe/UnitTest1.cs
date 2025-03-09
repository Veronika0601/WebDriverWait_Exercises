using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Iframe
{
    public class Tests
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
            wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));


        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Test_IframeByIndex()
        {
            driver.SwitchTo().Frame(0);
            driver.FindElement(By.XPath("//button[@class='dropbtn']")).Click();

            var dropDownOptions = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var dropDownOption in dropDownOptions)
            {
                Console.WriteLine(dropDownOption.Text);

                Assert.That(dropDownOption.Displayed, Is.True);
            }
        }
        [Test]
        public void TestIframeById()

           
        {

            //driver.SwitchTo().Frame("result");
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));
            driver.FindElement(By.XPath("//button[@class='dropbtn']")).Click();

            var dropDownOptions = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var dropDownOption in dropDownOptions)
            {
                Console.WriteLine(dropDownOption.Text);

                Assert.That(dropDownOption.Displayed, Is.True);
            }
        }
        [Test]
        public void TestIframeByXpath()


        {

            //driver.SwitchTo().Frame("result");
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@class='result-iframe']")));
            driver.FindElement(By.XPath("//button[@class='dropbtn']")).Click();

            var dropDownOptions = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var dropDownOption in dropDownOptions)
            {
                Console.WriteLine(dropDownOption.Text);

                Assert.That(dropDownOption.Displayed, Is.True);
            }
        }
    }
}

