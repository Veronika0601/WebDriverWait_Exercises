using System.Linq.Expressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Search_Product_with_Implicit_Wait
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");

        }
        [TearDown]

        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchForKeyboard()
        {
            var searchField = driver.FindElement(By.CssSelector("[name='keywords']"));
            searchField.SendKeys("keyboard");

            driver.FindElement(By.CssSelector("[title=' Quick Find ']")).Click();
            var productName = driver.FindElement(By.XPath("//td//a[text()='Microsoft Internet Keyboard PS/2']"));
            Assert.That(productName.Displayed, Is.True);

            var buyButton = driver.FindElement(By.XPath("//span[text()='Buy Now']"));

            buyButton.Click();

            var cartPageTitle = driver.FindElement(By.TagName("h1"));

            Assert.That(cartPageTitle.Text, Is.EqualTo("What's In My Cart?"));

            var productNameInCart = driver.FindElement(By.XPath("//strong[text()='Microsoft Internet Keyboard PS/2']"));

            Assert.That(productNameInCart.Displayed, Is.True);



        }
        [Test]
        public void Search_NonExisting_ItemName()
        {
            var searchField = driver.FindElement(By.XPath("//input[@name='keywords']"));
            searchField.SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.XPath("//span[text()='Buy Now']")).Click();
            }
            catch(NoSuchElementException ex)
            {
                Assert.Pass(ex.Message);
            }
            catch(StaleElementReferenceException st)
            {

            }
           

           

        }
    }
}