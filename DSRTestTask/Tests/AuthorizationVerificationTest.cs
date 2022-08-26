using DSRTestTask.Helpers;
using DSRTestTask.PageObjects;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace DSRTestTask
{
    public class Tests
    {
        IWebDriver driver;
        string url = "https://vladimirwork.github.io/web-ui-playground/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(DriverPath.ChromeDriverPath);
            driver.Navigate().GoToUrl(url);
        }

        [Test]
        [TestCase("(2-25)", "(2-25)",   "mail",     "(7-12)",       Gender.Male,    true)]
        [TestCase("(2-25)", "(2-25)",   "mail",     "(7-12)",       Gender.Female,  true)]
        [TestCase(null,     "(2-25)",   "mail",     "(7-12)",       Gender.Male,    true)]
        [TestCase(null,     ">2",       "other",    ">7",           Gender.Female,  null)]
        [TestCase(null,     "<25",      "@@",       "<12",          null,           true)]
        [TestCase("(2-25)", "(2-25)",   "other",    "<12",          Gender.Male,    null)]
        [TestCase("(2-25)", ">2",       "@@",       "other chars",  null,           true)]
        [TestCase("(2-25)", "<25",      null,       null,           Gender.Male,    null)]
        [TestCase("(2-25)", null,       null,       "(7-12)",       Gender.Female,  true)]
        [TestCase("(2-25)", null,       "mail",     ">7",           null,           null)]
        [TestCase(">2",     ">2",       null,       "(7-12)",       null,           null)]
        [TestCase(">2",     "<25",      null ,      ">7",           Gender.Male,    true)]
        [TestCase(">2",     null,       "mail",     "<12",          null,           null)]
        [TestCase(">2",     null,       "other",    "other chars",  Gender.Male,    true)]
        [TestCase(">2",     "(2-25)",   "@@",       null,           Gender.Female,  null)]
        [TestCase("<25",    "<25",      "mail",     "other chars",  Gender.Female,  null)]
        [TestCase("<25",    null,       "other",    null,           null,           true)]
        [TestCase("<25",    null,       "@@",       "(7-12)",       Gender.Male,    null)]
        [TestCase("<25",    "(2-25)",   null,       ">7",           null,           true)]
        [TestCase("<25",    ">2",       null,       "<12",          Gender.Male,    null)]
        [TestCase(null,     null,       "@@",       ">7",           Gender.Male,    null)]
        [TestCase(null,     null,       null,       "<12",          Gender.Female,  true)]
        [TestCase(null,     "(2-25)",   null,       "other chars",  null,           null)]
        [TestCase(null,     ">2",       "mail",     null,           Gender.Male,    true)]
        [TestCase(null,     "<25",      "other",    "(7-12)",       null,           null)]
        [TestCase(null,     null,       null,       null,           null,           null)]
        public void AuthorizationVerificationTest(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            Gender gender,
            bool isAgree)
        {
            UserLoginData loginData = new UserLoginData
            {
                firstName = StringGenerator.GenerateName(firstName),
                lastName = StringGenerator.GenerateName(lastName),
                email = StringGenerator.GenerateMail(email),
                phoneNumber = StringGenerator.GeneratePhoneNumber(phoneNumber),
                gender = gender,
                isAgree = isAgree
            };

            LoginPageObject loginPage = new LoginPageObject(driver);
            loginPage.EnterFirstName(loginData.firstName);
            loginPage.EnterLastName(loginData.lastName);
            loginPage.EnterEmailName(loginData.email);
            loginPage.EnterPhoneNumber(loginData.phoneNumber);
            loginPage.SelectGender(loginData.gender);
            loginPage.ConfirmAgreement(loginData.isAgree);
            loginPage.ClickSubmitButton();

            if (loginData.IsUserLoginDataCorrect())
            {
                IAlert alert = driver.SwitchTo().Alert();
                var alertMessage = JsonConvert.DeserializeObject<Dictionary<string, string>>(alert.Text);
                CheckAlertMessage(loginData, alertMessage);
                alert.Accept();
            }
            else
            {
                loginPage.CheckErrorMessages(loginData);
            }
        }

        [TearDown]
        public void AfterEachTests()
        {
            driver.Close();
        }

        private void CheckAlertMessage(UserLoginData expected, Dictionary<string, string> actual)
        {
            Assert.AreEqual(expected.firstName, actual["FirstName"]);
            Assert.AreEqual(expected.lastName, actual["LastName"]);
            Assert.AreEqual(expected.email, actual["Email"]);
            Assert.AreEqual(expected.phoneNumber, actual["PhoneNumber"]);
            Assert.AreEqual(expected.GetStringGender(), actual["Gender"]);
            Assert.AreEqual(expected.isAgree, bool.Parse(actual["Agreement"]));
        }

    }
}