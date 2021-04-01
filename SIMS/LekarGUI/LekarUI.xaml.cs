
using Model;
using SIMS.LekarGUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace SIMS
{
    /// <summary>
    /// Glavni prozor za lekara
    /// </summary>
    public partial class LekarUI : Window
    {
        public static LekarUI instance;

        private static Lekar lekarUser;

        public static LekarUI GetInstance(Lekar l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarUI();
            }
            return instance;
        }

        public static LekarUI GetInstance()
        {
            return instance;
        }

        public LekarUI()
        {
            InitializeComponent();

            //Tred za prikazivanje sata i datuma

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateAndTime.Content = DateTime.Now.ToString("HH:mm │ dd/MM/yyyy");
            }, this.Dispatcher);

            this.UsernameLabel.Content = lekarUser.ImePrezime;
        }

        private void Button_Termini(object sender, RoutedEventArgs e)
        {
            //Button: Termini
            SellectedTab.Content = LekarTerminiPage.GetInstance(lekarUser);
        }

        private void Button_Pacijenti(object sender, RoutedEventArgs e)
        {
            //Button: Pacijenti
            SellectedTab.Content = LekarPacijentiPage.GetInstance(lekarUser);
        }

        private void Button_Istorija(object sender, RoutedEventArgs e)
        {
            //Button: Istorija pregleda
            SellectedTab.Content = LekarIstorijaPage.GetInstance(lekarUser);
        }

        private void Button_Evidencija(object sender, RoutedEventArgs e)
        {
            //Button: Evidentiranje materijala
            SellectedTab.Content = LekarEvidencijaPage.GetInstance(lekarUser);
        }

        private void Button_Nalog(object sender, RoutedEventArgs e)
        {
            //Button: Nalog, DEBUG po potrebi
            SellectedTab.Content = LekarNalogPage.GetInstance(lekarUser);

            //MessageBox.Show("Ukupno termina: " + TerminStorage.Instance.ReadList().Count);

        }

        public Lekar getUser()
        {
            return lekarUser;
        }

    }
}
