using Model;
using SIMS.LekarGUI.Pages;
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
    /// Interaction logic for LekarDashboard.xaml
    /// </summary>
    public partial class LekarDashboard : Page
    {
        public static LekarDashboard instance;

        private static Lekar lekarUser;

        public static LekarDashboard GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarDashboard();
            }
            return instance;
        }

        public static LekarDashboard GetInstance()
        {
            return instance;
        }

        public LekarDashboard()
        {
            InitializeComponent();

            WelcomeMSG.Content = lekarUser.Ime + ", dobro došli!";
            refresh();

        }

        public void refresh()
        {
            setAktivanTermin();
        }

        public void setAktivanTermin()
        {
            //TODO uraditi varijaciju
            List<Termin> termini = TerminStorage.Instance.ReadByDoctor(lekarUser);

            foreach (Termin t in termini)
            {
                if (t.isCurrent && t.Evidentiran == false)
                {
                    AktivniTermin.Content = LDBAktivanTermin.GetInstance(t);
                    return;
                }
            }

            AktivniTermin.Content = LDBNemaTermin.GetInstance();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Termini(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(1);
        }
    }
}
