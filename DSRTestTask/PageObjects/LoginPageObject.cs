using DSRTestTask.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace DSRTestTask.PageObjects
{
    class LoginPageObject
    {
        private By FirstNameInput => By.CssSelector("input[name='FirstName']");
        private By FirstNameErrMsg => By.XPath("//input[@name='FirstName']/following-sibling::p[1]");
        private By LastNameInput => By.CssSelector("input[name='LastName']");
        private By LastNameErrMsg => By.XPath("//input[@name='LastName']/following-sibling::p[1]");
        private By EmailInput => By.CssSelector("input[name='Email']");
        private By EmailErrMsg => By.XPath("//input[@name='Email']/following-sibling::p[1]");
        private By PhoneNumberInput => By.CssSelector("input[name='PhoneNumber']");
        private By PhoneNumberErrMsg => By.XPath("//input[@name='PhoneNumber']/following-sibling::p[1]");
        private By MaleCheckbox => By.CssSelector("input[value='Male']");
        private By FemaleCheckbox => By.CssSelector("input[value='Female']");
        private By GenderErrMsg => By.XPath("//div/input[@value='Female']/../following-sibling::p[1]");
        private By AgreementCheckbox => By.CssSelector("input[name='Agreement']");
        private By AgreementErrMsg => By.XPath("//input[@name='Agreement']/following-sibling::p[1]");
        private By SubmitButton => By.CssSelector("input[name='submitbutton']");

        private IWebDriver driver;

        public LoginPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CheckErrorMessages(UserLoginData loginData)
        {
            if (!loginData.IsFirstNameCorrect())
                Assert.AreEqual(GetText(FirstNameErrMsg), "Valid first name is required.");

            if (!loginData.IsLastNameCorrect())
                Assert.AreEqual(GetText(LastNameErrMsg), "Valid last name is required.");

            if (!loginData.IsEmailCorrect())
                Assert.AreEqual(GetText(EmailErrMsg), "Valid email is required.");

            if (!loginData.IsPhoneNumberCorrect())
                Assert.AreEqual(GetText(PhoneNumberErrMsg), "Valid phone number is required.");

            if (!loginData.IsGenderChecked())
                Assert.AreEqual(GetText(GenderErrMsg), "Choose your gender.");

            if (!loginData.IsAgreementChecked())
                Assert.AreEqual(GetText(AgreementErrMsg), "You must agree to the processing of personal data.");
        }

        private string GetText(By locator)
        {
            return driver.FindElement(locator).Text;
        }

        public void EnterFirstName(string name)
        {
            if(name != null)
                driver.FindElement(FirstNameInput).SendKeys(name);
        }

        public void EnterLastName(string name)
        {
            if (name != null)
                driver.FindElement(LastNameInput).SendKeys(name);
        }

        public void EnterEmailName(string email)
        {
            if (email != null)
                driver.FindElement(EmailInput).SendKeys(email);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            if (phoneNumber != null)
                driver.FindElement(PhoneNumberInput).SendKeys(phoneNumber);
        }

        public void SelectGender(Gender? genderType)
        {
            if (genderType != null)
            {
                if (genderType == Gender.Male) SelectMaleGender();
                else SelectFemaleGender();
            }
                
        }

        private void SelectMaleGender()
        {
            driver.FindElement(MaleCheckbox).Click();
        }

        private void SelectFemaleGender()
        {
            driver.FindElement(FemaleCheckbox).Click();
        }

        public void ConfirmAgreement(bool? isAgree)
        {
            if (isAgree != null && isAgree == true)
                driver.FindElement(AgreementCheckbox).Click();
        }

        public void ClickSubmitButton()
        {
            driver.FindElement(SubmitButton).Click();
        }
    }
}
