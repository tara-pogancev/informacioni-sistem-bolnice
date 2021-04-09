using Model;
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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for DodajPacijentaPage.xaml
    /// </summary>
    public partial class DodajPacijentaPage : Page
    {
        public DodajPacijentaPage()
        {
            InitializeComponent();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            string[] ulicaBroj = adresa.Text.Split(" ");
            string ulica = "";
            string broj = "";
            for (int i = 0; i < ulicaBroj.Length; ++i)
            {
                if (i != ulicaBroj.Length - 1 && i != ulicaBroj.Length - 2)
                    ulica += ulicaBroj[i] + " ";
                else if (i == ulicaBroj.Length - 2)
                    ulica += ulicaBroj[i];
                else
                    broj = ulicaBroj[i];
            }
            int post_broj;
            int.TryParse(postanski_broj.Text, out post_broj);

            List<string> alerg = new List<string>(alergeni.Text.Split());
            List<string> hron_bol = new List<string>(hronicne_bolesti.Text.Split());

            Pacijent pacijent = new Pacijent(ime.Text, prezime.Text, jmbg.Text, kor_ime.Text, lozinka.Text, email.Text, telefon.Text, new Adresa(ulica, broj, new Grad(grad.Text, post_broj, new Drzava(drzava.Text))), lbo.Text, (bool)gost.IsChecked, alerg, DateTime.Parse(datum_rodjenja.Text), GetEnumKrvneGrupe((string)krvna_grupa.SelectionBoxItem), (Pol)pol.SelectionBoxItem, hron_bol);
            PacijentStorage.Instance.Create(pacijent);
            SekretarPacijentiPage.GetInstance().refresh();

            //this.Close();
            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        public static Krvne_Grupe GetEnumKrvneGrupe(string s)
        {
            switch (s)
            {
                case "O+": return Krvne_Grupe.Op;
                case "O-": return Krvne_Grupe.On;
                case "A+": return Krvne_Grupe.Ap;
                case "A-": return Krvne_Grupe.An;
                case "B+": return Krvne_Grupe.Bp;
                case "B-": return Krvne_Grupe.Bn;
                case "AB+": return Krvne_Grupe.ABp;
                case "AB-": return Krvne_Grupe.ABn;
            }

            return Krvne_Grupe.Op;
        }

    }
}
