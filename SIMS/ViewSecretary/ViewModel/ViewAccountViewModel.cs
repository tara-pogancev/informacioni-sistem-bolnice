using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.ViewSecretary.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SIMS.ViewSecretary.ViewModel
{
    public class ViewAccountViewModel : ViewModelSecretary
    {
        public string FullName { get; set; }
        public string Jmbg { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RelayCommand UpdateSecretaryCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }

        private SecretaryController secretaryController = new SecretaryController();
        private Secretary secretary;
        public ViewAccountViewModel()
        {
            secretary = SecretaryUI.GetInstance().GetSecretary();
            FullName = secretary.FullName;
            Jmbg = secretary.Jmbg;
            Username = secretary.Username;
            Password = secretary.Password;
            Email = secretary.Email;
            PhoneNumber = secretary.Phone;

            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
            UpdateSecretaryCommand = new RelayCommand(Execute_UpdateSecretaryCommand, CanExecute_UpdateSecretaryCommand);
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(PhoneNumber))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }
            if (Username.Trim().Equals("") || Password.Trim().Equals("") || Email.Trim().Equals("") || PhoneNumber.Trim().Equals(""))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }

            string strRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(Email))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectEmailFormatMessage"]);
                return false;
            }
            strRegex = @"(\+)?[0-9-\/]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(PhoneNumber))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectPhoneNumberFormatMessage"]);
                return false;
            }
            return true;
        }
        private void Execute_UpdateSecretaryCommand(object obj)
        {
            if (IsValid())
            {
                secretary.Username = Username;
                secretary.Password = Password;
                secretary.Email = Email;
                secretary.Phone = PhoneNumber;
                secretaryController.UpdateSecretary(secretary);

                SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
                SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
            }
        }

        private bool CanExecute_UpdateSecretaryCommand(object obj)
        {
            return true;
        }

        private void Execute_QuitCommand(object obj)
        {
            SecretaryUI.GetInstance().Caption.Content = TranslationSource.Instance["HomePageListItem"];
            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewHome.GetInstance());
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return true;
        }
    }
}
