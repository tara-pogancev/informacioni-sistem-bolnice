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
        private static Lekar lekarUser;

        public LekarDashboard(Lekar l)
        {
            lekarUser = l;

            InitializeComponent();

            this.DataContext = this;

            WelcomeMSG.Content = lekarUser.Ime + ", dobro došli!";
            refresh();

        }

        public void refresh()
        {
            setAktivanTermin();
            RefreshGraphs();
        }

        public void setAktivanTermin()
        {
            //TODO uraditi varijaciju
            List<Termin> termini = TerminStorage.Instance.ReadByDoctor(lekarUser);

            foreach (Termin t in termini)
            {
                if (t.IsCurrent && t.Evidentiran == false)
                {
                    AktivniTermin.Content = LDBAktivanTermin.GetInstance(t);
                    return;
                }
            }

            AktivniTermin.Content = LDBNemaTermin.GetInstance();
        }

        private void Button_Termini(object sender, RoutedEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(1);
        }

        private void Button_Hitno(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void RefreshGraphs()
        {
            List<Termin> termini = TerminStorage.Instance.ReadByDoctor(lekarUser);

            int evidentirani = 0;
            int ukupno = termini.Count;

            foreach (Termin t in termini)
                if (t.Evidentiran)
                    evidentirani++;

            GraphEvidentirani.To = ukupno;
            GraphEvidentirani.Value = evidentirani;

        }
    }
}
