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
using System.Windows.Shapes;
using Model;

namespace SIMS
{
    public partial class Izmeni : Window
    {
        Pacijent pacijent;
        public Izmeni(Pacijent pacijent)
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
            
            alergeni.Text = pacijent.GetAlergeniString;
            lbo.Text = pacijent.Lbo;
            gost.IsChecked = pacijent.Gost;

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

            Pacijent pacijent = new Pacijent(ime.Text, prezime.Text, jmbg.Text, kor_ime.Text, lozinka.Text, email.Text, telefon.Text, new Adresa(ulica, broj, new Grad(grad.Text, post_broj, new Drzava(drzava.Text))), lbo.Text, (bool)gost.IsChecked, alerg);
            PacijentStorage.Instance.Update(pacijent);
            SekretarUI.GetInstance().refresh();

            this.Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
