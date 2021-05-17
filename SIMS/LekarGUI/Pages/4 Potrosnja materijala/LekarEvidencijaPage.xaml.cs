using Model;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarEvidencijaPage.xaml
    /// </summary>
    public partial class LekarEvidencijaPage : Page
    {
        public static LekarEvidencijaPage instance;

        private static Doctor lekarUser;

        public static LekarEvidencijaPage GetInstance(Doctor l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarEvidencijaPage();
            }
            return instance;
        }

        public static LekarEvidencijaPage GetInstance()
        {
            return instance;
        }


        public LekarEvidencijaPage()
        {
            InitializeComponent();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void ButtonMaterialConsumption(object sender, RoutedEventArgs e)
        {
            new PotrosnjaMaterijala().Show();
        }
    }
}
