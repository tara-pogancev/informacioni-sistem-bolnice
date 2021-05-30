using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Materijali_i_lekovi;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarEvidencijaPage.xaml
    /// </summary>
    public partial class DoctorInverntoyPage : Page
    {
        public static DoctorInverntoyPage instance;

        private static Doctor doctorUser = DoctorUI.GetInstance().GetUser();

        public static DoctorInverntoyPage GetInstance()
        {
            if (instance == null)
            {
                instance = new DoctorInverntoyPage();
            }
            return instance;
        }

        public DoctorInverntoyPage()
        {
            InitializeComponent();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonMaterialConsumption(object sender, RoutedEventArgs e)
        {
            new PotrosnjaMaterijala().Show();
        }
    }
}
