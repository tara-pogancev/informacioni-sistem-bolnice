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
using Model;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PacijentKartonView : Page
    {
        private Pacijent pacijentProfile;

        public static PacijentKartonView instance;

        public static PacijentKartonView GetInstance(Pacijent p)
        {
            instance = new PacijentKartonView(p);
            return instance;
        }

        public static PacijentKartonView GetInstance()
        {
            return instance;
        }

        public PacijentKartonView(Pacijent p)
        {
            InitializeComponent();

            pacijentProfile = p;

            Ime_Top.Content = pacijentProfile.ImePrezime;
            Label_Ime.Content = pacijentProfile.ImePrezime;

            Label_Pol.Content = "Pol: " + pacijentProfile.PolString;
            Label_Datum.Content = "Datum rođenja: " + pacijentProfile.DatumString;
            Label_JMBG.Content = "JMBG: " + pacijentProfile.Jmbg;
            Label_LBO.Content = "LBO: " + pacijentProfile.Lbo;

            Label_Telefon.Content = "Broj telefona: " + pacijentProfile.Telefon;
            Label_Email.Content = "Email: " + pacijentProfile.Email;
            Label_Adresa.Content = "Adresa: " + pacijentProfile.fullAddress;

            
            Label_KrvnaGrupa.Content = "Krvna grupa: " + pacijentProfile.KrvnaGrupaString;
            Label_Alergeni.Content = "Alergeni: " + pacijentProfile.GetAlergeniString;
            Label_HronicneBolesti.Content = "Hronične bolesti: " + pacijentProfile.GetHronicneBolestiString;
            
        }

        private void Button_Recept(object sender, RoutedEventArgs e)
        {
            LekarIzdavanjeRecepta r = new LekarIzdavanjeRecepta(pacijentProfile);
            r.Show();
        }

        private void Button_Dokumenti(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_Hositalizaijca(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_Pacijenti(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(2);
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }
    }
}
