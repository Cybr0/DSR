using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace DSRTestTask.Helpers
{
    public class UserLoginData
    {
        public string firstName;
        public string lastName;
        public string email;
        public string phoneNumber;
        public Gender? gender;
        public bool? isAgree;

        public string GetStringGender()
        {
            if (gender == Gender.Male)
                return "Male";
            return "Female";
        }

        public bool IsUserLoginDataCorrect()
        {
            if (IsFirstNameCorrect() && IsLastNameCorrect() && IsEmailCorrect() && IsPhoneNumberCorrect() && IsGenderChecked() && IsAgreementChecked())
                return true;
            else return false;
        }

        public bool IsFirstNameCorrect()
        {
            return IsNameCorrect(firstName);
        }

        public bool IsLastNameCorrect()
        {
            return IsNameCorrect(lastName);
        }

        private bool IsNameCorrect(string value)
        {
            if (value == null || value.Length < 2 || value.Length > 25) return false;
            else return true;
        }

        public bool IsEmailCorrect()
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public bool IsPhoneNumberCorrect()
        {
            if (phoneNumber == null || phoneNumber.Length < 7 || phoneNumber.Length > 12 || !phoneNumber.All(Char.IsDigit)) return false;
            else return true;
        }

        public bool IsGenderChecked()
        {
            if (gender == null) return false;
            else return true;
        }

        public bool IsAgreementChecked()
        {
            if (isAgree == null) return false;
            else return true;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
