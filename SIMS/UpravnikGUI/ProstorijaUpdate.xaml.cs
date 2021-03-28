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
using System.Windows.Shapes;

namespace SIMS.UpravnikGUI
{
    public partial class ProstorijaUpdate : Window
    {
        private Prostorija prostorija;
        public ProstorijaUpdate()
        {
            InitializeComponent();
        }

        private void Ucitaj_Click(object sender, RoutedEventArgs e)
        {
            prostorija = ProstorijaStorage.Read(int.Parse(BrojProstorijeTextBox.Text));

            if (prostorija == null) return;

            GradTextBox.Text = prostorija.Adresa.Grad.Naziv;
            UlicaTextBox.Text = prostorija.Adresa.Ulica;
            BrojUUliciTextBox.Text = prostorija.Adresa.Broj.ToString();
            SpratTextBox.Text = prostorija.Sprat.ToString();
            DostupnostComboBox.SelectedIndex = prostorija.Dostupna ? 0 : 1;
            TipComboBox.SelectedIndex = (int)prostorija.TipProstorije;
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = new Prostorija(
                new Grad(GradTextBox.Text, 0, null),
                UlicaTextBox.Text,
                int.Parse(BrojUUliciTextBox.Text),
                int.Parse(SpratTextBox.Text),
                int.Parse(BrojProstorijeTextBox.Text),
                DostupnostComboBox.SelectedIndex == 0,
                (TipProstorije)TipComboBox.SelectedIndex
                );

            ProstorijaStorage.Update(prostorija);
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaStorage.Delete(prostorija.Broj);
        }
    }
}
