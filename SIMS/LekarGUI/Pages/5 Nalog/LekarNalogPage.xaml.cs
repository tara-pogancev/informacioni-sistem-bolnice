using SIMS.Repositories.PatientRepo;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class LekarNalogPage : Page
    {
        public static LekarNalogPage instance;

        private static Doctor lekarUser;

        public static LekarNalogPage GetInstance(Doctor l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarNalogPage();
            }
            return instance;
        }

        public static LekarNalogPage GetInstance()
        {
            return instance;
        }

        public LekarNalogPage()
        {
            InitializeComponent();
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

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void ChangeAccount(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().LogOut();
        }

        private void DontRememberMe(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void RememberMe(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
