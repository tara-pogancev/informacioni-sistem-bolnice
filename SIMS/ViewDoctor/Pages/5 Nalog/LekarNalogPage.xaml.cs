using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Termini_CRUD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Model;
using SIMS.Controller;
using SIMS.Repositories.PatientRepo;
using SIMS.ViewDoctor.Dialogues.Izmena_naloga;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class DoctorAccountPage : Page
    {
        private Doctor doctorUser = DoctorUI.GetInstance().GetUser();
        private LastLoginController loginController = new LastLoginController();

        public DoctorAccountPage()
        {
            InitializeComponent();
            LabelName.Content = doctorUser.FullName;
            LabelUsername.Content = "Korisničko ime: " + doctorUser.Username;
            LabelEmail.Content = "Mejl adresa: " + doctorUser.Email;
            LabelPhone.Content = "Telefon: " + doctorUser.Phone;
            LabelAddress.Content = "Adresa: " + doctorUser.AddressString;

            if (loginController.IsSelfLastLogged(doctorUser))
                RememberMeCheckbox.IsChecked = true;

        }

        private void Button(object sender, RoutedEventArgs e)
        {
            var window = new ActionsAfterReport(PatientFileRepository.Instance.GetAll()[0]);
            window.Show();
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonChangeAccount(object sender, RoutedEventArgs e)
        {
            new AccountPreferences().ShowDialog();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().LogOut();
        }

        private void DontRememberMe(object sender, RoutedEventArgs e)
        {
            loginController.ClearAll();
        }

        private void RememberMe(object sender, RoutedEventArgs e)
        {
            loginController.SaveLoggedUser((LoggedUser)doctorUser);
        }

        private void RateApplication(object sender, RoutedEventArgs e)
        {
            new RateApplicationDialog().ShowDialog();
        }
    }
}
