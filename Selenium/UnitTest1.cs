using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace Selenium
{
    public class UnitTest1
    {

        private readonly IWebDriver _driver;

        public UnitTest1()
        {
            _driver = new ChromeDriver();
        }
        [Fact]
        public void testRead()
        {
            _driver.Navigate().GoToUrl("https://localhost:7093/Api/Create");
            var titleInput = _driver.FindElement(By.CssSelector("input[name='Book.Title']"));
            titleInput.SendKeys("Test Book Title");
            var descriptionInput = _driver.FindElement(By.CssSelector("input[name='Book.Description']"));
            descriptionInput.SendKeys("Test Book Description");

            var authorDropdown = new SelectElement(_driver.FindElement(By.Id("authorId")));
            authorDropdown.SelectByText("Phil Dibbert");

            var submitButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            _driver.Navigate().GoToUrl("https://localhost:7093/");
            //read
            _driver.Navigate().GoToUrl("https://localhost:7093/");

            var cards = _driver.FindElements(By.ClassName("card"));
            Assert.NotNull(cards);
            _driver.Quit();
        }

        [Fact]
        public void create()
        {
            

            _driver.Navigate().GoToUrl("https://localhost:7093/Api/Create");
            var titleInput = _driver.FindElement(By.CssSelector("input[name='Book.Title']"));
            titleInput.SendKeys("Test Book Title");
            var descriptionInput = _driver.FindElement(By.CssSelector("input[name='Book.Description']"));
            descriptionInput.SendKeys("Test Book Description");

            var authorDropdown = new SelectElement(_driver.FindElement(By.Id("authorId")));
            authorDropdown.SelectByText("Phil Dibbert");

            var submitButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            _driver.Navigate().GoToUrl("https://localhost:7093/");
            var bookCards = _driver.FindElements(By.ClassName("card"));

            var newlyAddedBookCard = bookCards.FirstOrDefault(card => card.FindElement(By.ClassName("card-title")).Text == "Test Book Title");

            var lastBookTitle = newlyAddedBookCard.FindElement(By.CssSelector(".card-title")).Text;
            Assert.NotNull(newlyAddedBookCard);
            Assert.Contains("Test Book Title", lastBookTitle);
            _driver.Quit();


        }
        [Fact]
        public void UpdateTest()
        {
            _driver.Navigate().GoToUrl("https://localhost:7093/Api/Create");
            var titleInput = _driver.FindElement(By.CssSelector("input[name='Book.Title']"));
            titleInput.SendKeys("Test Book Title");
            var descriptionInput = _driver.FindElement(By.CssSelector("input[name='Book.Description']"));
            descriptionInput.SendKeys("Test Book Description");

            var authorDropdown = new SelectElement(_driver.FindElement(By.Id("authorId")));
            authorDropdown.SelectByText("Phil Dibbert");

            var submitButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            _driver.Navigate().GoToUrl("https://localhost:7093/");
            //update
            _driver.Navigate().GoToUrl("https://localhost:7093/");

            var initialBookCardCount = _driver.FindElements(By.ClassName("card")).Count;
            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var deleteLink = _driver.FindElement(By.LinkText("Edit"));
            deleteLink.Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.TagName("h4")).Text == "Book");

            var titleInput2 = _driver.FindElement(By.CssSelector("input[name='Book.Title']"));
            titleInput2.Clear();
            titleInput2.SendKeys("Test Book Updated");
            var bookIdInput = _driver.FindElement(By.CssSelector("input[name='bookId']"));

            var authorDropdown2 = new SelectElement(_driver.FindElement(By.Id("authorId")));
            authorDropdown2.SelectByText("Jerry Yost");
            var saveButton = _driver.FindElement(By.CssSelector("input[type='submit'][value='Save']"));
            saveButton.Click();

            _driver.Navigate().GoToUrl("https://localhost:7093/");
            var bookCards = _driver.FindElements(By.ClassName("card"));

            var newlyAddedBookCard = bookCards.FirstOrDefault(card => card.FindElement(By.ClassName("card-title")).Text == "Test Book Updated");

            var lastBookTitle = newlyAddedBookCard.FindElement(By.CssSelector(".card-title")).Text;
            Assert.NotNull(newlyAddedBookCard);
            Assert.Contains("Test Book Updated", lastBookTitle);
            _driver.Quit();

            
        }

        [Fact]
        public void TestBookDeletion()
        {
            _driver.Navigate().GoToUrl("https://localhost:7093/Api/Create");
            var titleInput = _driver.FindElement(By.CssSelector("input[name='Book.Title']"));
            titleInput.SendKeys("Test Book Title");
            var descriptionInput = _driver.FindElement(By.CssSelector("input[name='Book.Description']"));
            descriptionInput.SendKeys("Test Book Description");

            var authorDropdown = new SelectElement(_driver.FindElement(By.Id("authorId")));
            authorDropdown.SelectByText("Phil Dibbert");

            var submitButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            submitButton.Click();
            _driver.Navigate().GoToUrl("https://localhost:7093/");
            Thread.Sleep(25000);
            //delete
            _driver.Navigate().GoToUrl("https://localhost:7093/");

            var initialBookCardCount = _driver.FindElements(By.ClassName("card")).Count;
            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var deleteLink = _driver.FindElement(By.LinkText("Delete"));
            deleteLink.Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.TagName("h3")).Text == "Are you sure you want to delete this?");

            var confirmDeleteButton = _driver.FindElement(By.CssSelector("input[type='submit'][value='Delete']"));
            confirmDeleteButton.Click();

            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait2.Until(driver => driver.FindElements(By.ClassName("card")).Count != initialBookCardCount);


            var finalBookCardCount = _driver.FindElements(By.ClassName("card")).Count;

            Assert.Equal(initialBookCardCount-1, finalBookCardCount);
            _driver.Quit();

        }


    }
}