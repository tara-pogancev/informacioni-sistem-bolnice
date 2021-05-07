using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        private ObservableCollection<Alergen> listaAlergena;
        public DodajPacijentaPage()
        {
            InitializeComponent();

            listaAlergena = new ObservableCollection<Alergen>(AlergeniStorage.Instance.ReadList());

            alergeni.ItemsSource = listaAlergena;
            alergeni.DisplayMemberPath = "Naziv";
            alergeni.SelectedMemberPath = "ID";
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

            List<string> alerg = new List<string>();
            foreach(Alergen a in alergeni.SelectedItems)
            {
                alerg.Add(a.ID);
            }
            List<string> hron_bol = new List<string>(hronicne_bolesti.Text.Split());

            Pacijent pacijent = new Pacijent(ime.Text, prezime.Text, jmbg.Text, kor_ime.Text, lozinka.Text, email.Text, telefon.Text, new Adresa(ulica, broj, new Grad(grad.Text, post_broj, new Drzava(drzava.Text))), lbo.Text, false, alerg, DateTime.ParseExact(datum_rodjenja.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), GetEnumKrvneGrupe((string)krvna_grupa.SelectionBoxItem), (Pol)pol.SelectionBoxItem, hron_bol);
            PacijentStorage.Instance.Create(pacijent);
            SekretarPacijentiPage.GetInstance().refresh();

            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        public static Krvne_Grupe GetEnumKrvneGrupe(string s)
        {
            return s switch
            {
                "O+" => Krvne_Grupe.Op,
                "O-" => Krvne_Grupe.On,
                "A+" => Krvne_Grupe.Ap,
                "A-" => Krvne_Grupe.An,
                "B+" => Krvne_Grupe.Bp,
                "B-" => Krvne_Grupe.Bn,
                "AB+" => Krvne_Grupe.ABp,
                "AB-" => Krvne_Grupe.ABn,
                _ => Krvne_Grupe.Op,
            };
        }
    }
}
