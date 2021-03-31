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
    
    public partial class Dodaj : Window
    {
        public Dodaj()
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
            Model.Pacijent pacijent = new Model.Pacijent(ime.Text, prezime.Text, jmbg.Text, kor_ime.Text, lozinka.Text, email.Text, telefon.Text, new Model.Adresa(ulica, broj, new Model.Grad()), lbo.Text, (bool)gost.IsChecked);

            PacijentStorage.Instance.Create(pacijent);
            SekretarUI.getInstance().refresh();

            this.Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
