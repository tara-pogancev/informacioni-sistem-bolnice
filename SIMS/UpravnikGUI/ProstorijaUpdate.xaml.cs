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
            prostorija = ProstorijaStorage.Instance.Read(BrojProstorijeTextBox.Text);

            if (prostorija == null) return;

            DostupnostComboBox.SelectedIndex = prostorija.Dostupna ? 0 : 1;
            TipComboBox.SelectedIndex = (int)prostorija.TipProstorije;
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = new Prostorija(
                BrojProstorijeTextBox.Text,
                DostupnostComboBox.SelectedIndex == 0,
                (TipProstorije)TipComboBox.SelectedIndex
                );

            ProstorijaStorage.Instance.Update(prostorija);
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaStorage.Instance.Delete(prostorija.Broj.ToString());
        }
    }
}
