using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Search_Product_with_Explicit_Wait
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
            driver.FindElement(By.CssSelector("[name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.CssSelector("[title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);


            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));

            var foundProductName = wait.Until(drv => drv.FindElement(By.XPath("//table[@class='productListingData']//tr//td//a[text()='Microsoft Internet Keyboard PS/2']")));

            Assert.That(foundProductName.Text, Is.EqualTo("Microsoft Internet Keyboard PS/2"));

            var buyButton = wait.Until(drv => drv.FindElement(By.XPath("//span[text()='Buy Now']")));

            buyButton.Click();

            //Assert
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var foundProdactPage = driver.FindElement(By.TagName("h1"));

            Assert.That(foundProdactPage.Text, Is.EqualTo("What's In My Cart?"));

            var productInCart = driver.FindElement(By.XPath("//strong[text()='Microsoft Internet Keyboard PS/2']"));
            Assert.That(productInCart.Text, Is.EqualTo("Microsoft Internet Keyboard PS/2"));


        }
        [Test]
        public void Search_ForNonExisting_ProductItem()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@alt='Quick Find']")).Click();

            //Set impliit wait to 0

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            try
            {
                var buyButton = wait.Until( drv=> drv.FindElement(By.XPath("//span[text()='Buy Now']")));
                buyButton.Click();

                Assert.Fail("Buy Button was  present on the page");

            }
            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Buy button was not present on the page");
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

           

            


        }
    }
}