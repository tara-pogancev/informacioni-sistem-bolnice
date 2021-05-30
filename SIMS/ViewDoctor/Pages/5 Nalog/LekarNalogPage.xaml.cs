﻿using SIMS.Repositories.SecretaryRepo;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class DoctorAccountPage : Page
    {
        public static DoctorAccountPage instance;

        private static Doctor doctorUser = DoctorUI.GetInstance().GetUser();
        private LastLoginController loginController = new LastLoginController();

        public static DoctorAccountPage GetInstance()
        {
            if (instance == null)
            {
                instance = new DoctorAccountPage();
            }
            return instance;
        }

        public DoctorAccountPage()
        {
            InitializeComponent();

            if (loginController.IsSelfLastLogged(doctorUser))
                RememberMeCheckbox.IsChecked = true;

        }

        public void RemoveInstance()
        {
            instance = null;
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
    }
}
