using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IzmeniPacijentaPage.xaml
    /// </summary>
    public partial class IzmeniPacijentaPage : Page
    {
        Pacijent pacijent;
        private ObservableCollection<Alergen> listaAlergena;
        public IzmeniPacijentaPage(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            ime.Text = pacijent.Ime;
            prezime.Text = pacijent.Prezime;
            jmbg.Text = pacijent.Jmbg;
            kor_ime.Text = pacijent.KorisnickoIme;
            lozinka.Text = pacijent.Lozinka;
            email.Text = pacijent.Email;
            telefon.Text = pacijent.Telefon;
            if (pacijent.Adresa == null)
            {
                adresa.Text = "";
                grad.Text = "";
                postanski_broj.Text = "";
                drzava.Text = "";
            }
            else
            {
                adresa.Text = pacijent.Adresa.ToString();
                grad.Text = pacijent.Adresa.Grad.Naziv;
                postanski_broj.Text = pacijent.Adresa.Grad.PostanskiBroj.ToString();
                drzava.Text = pacijent.Adresa.Grad.Drzava.Naziv;
            }

            //alergeni.Text = pacijent.GetAlergeniString;
            lbo.Text = pacijent.Lbo;
            gost.IsChecked = pacijent.Gost;

            pol.SelectedIndex = (int)pacijent.Pol_Pacijenta;
            krvna_grupa.SelectedIndex = (int)pacijent.Krvna_Grupa;
            datum_rodjenja.Text = pacijent.Datum_Rodjenja.ToString("MM.dd.yyyy.");
            hronicne_bolesti.Text = pacijent.GetHronicneBolestiString;

            listaAlergena = new ObservableCollection<Alergen>(AlergeniStorage.Instance.ReadList());
            /*listaAlergena.Add(new Alergen("a1", "polen1"));
            listaAlergena.Add(new Alergen("a2", "laktoza2"));
            listaAlergena.Add(new Alergen("a3", "polen3"));
            listaAlergena.Add(new Alergen("a4", "laktoza4"));
            listaAlergena.Add(new Alergen("a5", "polen5"));
            listaAlergena.Add(new Alergen("a6", "laktoza6"));
            listaAlergena.Add(new Alergen("a7", "polen7"));
            listaAlergena.Add(new Alergen("a8", "laktoza8"));
            listaAlergena.Add(new Alergen("a9", "polen9"));
            listaAlergena.Add(new Alergen("a10", "laktoza10"));*/

            alergeni.ItemsSource = listaAlergena;
            alergeni.DisplayMemberPath = "Naziv";
            alergeni.SelectedMemberPath = "ID";

            List<Alergen> selektovaniAlergeni = new List<Alergen>();
            foreach (string id in pacijent.Alergeni)
            {
                foreach (Alergen a in listaAlergena)
                {
                    if (id.Equals(a.ID))
                    {
                        selektovaniAlergeni.Add(a);
                        break;
                    }
                }
            }
           
            alergeni.SelectedItemsOverride = selektovaniAlergeni;
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
            foreach (Alergen a in alergeni.SelectedItems)
            {
                alerg.Add(a.ID);
            }
            //List<string> alerg = new List<string>(alergeni.Text.Split());
            //List<Alergen> alerg = new List<Alergen>((List<Alergen>)alergeni.SelectedItems);
            List<string> hron_bol = new List<string>(hronicne_bolesti.Text.Split());

            Pacijent pacijent = new Pacijent(ime.Text, prezime.Text, jmbg.Text, kor_ime.Text, lozinka.Text, email.Text, telefon.Text, new Adresa(ulica, broj, new Grad(grad.Text, post_broj, new Drzava(drzava.Text))), lbo.Text, (bool)gost.IsChecked, alerg, DateTime.Parse(datum_rodjenja.Text), DodajPacijentaPage.GetEnumKrvneGrupe((string)krvna_grupa.SelectionBoxItem), (Pol)pol.SelectionBoxItem, hron_bol);
            PacijentStorage.Instance.Update(pacijent);
            SekretarPacijentiPage.GetInstance().refresh();

            //this.Close();
            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            this.NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }
    }
}
