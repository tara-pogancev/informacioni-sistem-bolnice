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
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PacijentKartonView : Page
    {
        private Patient pacijentProfile;

        public static PacijentKartonView instance;

        public static PacijentKartonView GetInstance(Patient p)
        {
            instance = new PacijentKartonView(p);
            return instance;
        }

        public static PacijentKartonView GetInstance()
        {
            return instance;
        }

        public PacijentKartonView(Patient p)
        {
            InitializeComponent();

            pacijentProfile = p;

            Ime_Top.Content = pacijentProfile.FullName;
            Label_Ime.Content = pacijentProfile.FullName;

            Label_Pol.Content = "Pol: " + pacijentProfile.Gender;
            Label_Datum.Content = "Datum rođenja: " + pacijentProfile.DateString;
            Label_JMBG.Content = "JMBG: " + pacijentProfile.Jmbg;
            Label_LBO.Content = "LBO: " + pacijentProfile.Lbo;

            Label_Telefon.Content = "Broj telefona: " + pacijentProfile.Phone;
            Label_Email.Content = "Email: " + pacijentProfile.Email;
            Label_Adresa.Content = "Adresa: " + pacijentProfile.FullAddressString;


            Label_KrvnaGrupa.Content = "Krvna grupa: " + pacijentProfile.BloodTypeString;
            Label_Alergeni.Content = "Alergeni: " + pacijentProfile.GetAllergenListString;
            Label_HronicneBolesti.Content = "Hronične bolesti: " + pacijentProfile.GetHronicalDiseases;

        }

        private void Button_Recept(object sender, RoutedEventArgs e)
        {
            DoctorWriteReciept r = new DoctorWriteReciept(pacijentProfile);
            r.Show();
        }

        private void Button_Dokumenti(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = new PacijentDokumentacijaView(pacijentProfile);
        }

        private void Button_Hositalizaijca(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_Pacijenti(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void Button_Terapija(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
