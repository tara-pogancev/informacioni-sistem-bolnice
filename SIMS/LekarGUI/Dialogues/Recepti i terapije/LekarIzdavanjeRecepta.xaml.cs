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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIzdavanjeRecepta.xaml
    /// </summary>
    public partial class LekarIzdavanjeRecepta : Window
    {
        private Pacijent pacijent;
        private Lekar lekar = LekarUI.GetInstance().GetUser();

        public LekarIzdavanjeRecepta(Pacijent p)
        {
            InitializeComponent();
            pacijent = p;

            LabelDoktor.Content = "Doktor: " + lekar.ImePrezime;
            LabelPacijent.Content = "Pacijent: " + pacijent.ImePrezime;
            LabelDatum.Content = "Datum: " + DateTime.Today.ToString("MM.dd.yyyy.");

            List<Lek> lekovi = new List<Lek>(LekStorage.Instance.ReadList());
            LekComboBox.ItemsSource = lekovi;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LekComboBox.SelectedItem == null || KolicinaTxt.Text.Equals("") || DijagnozaTxt.Text.Equals(""))
                MessageBox.Show("Greška! Molimo popunite sva polja.");
            else if (pacijent.IsAlergic((Lek)LekComboBox.SelectedItem))
            {
                Lek l = (Lek)LekComboBox.SelectedItem;
                if (MessageBox.Show("Pacijent je alergičan na odabran lek! Da li ste sigurni da želite da izdate lek " + l.Naziv +"?", "Upozorenje!",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.PrepisiRecept();
                }

            }
            else
            {
                this.PrepisiRecept();
            } 
                
        }

        private void PrepisiRecept()
        {
            Lek l = (Lek)LekComboBox.SelectedItem;
            Recept r = new Recept(lekar.Jmbg, pacijent.Jmbg, l.Naziv,
                KolicinaTxt.Text, DijagnozaTxt.Text);

            ReceptStorage.Instance.Create(r);
            this.Close();
            MessageBox.Show("Uspešno izdat recept!");
            Obavestenje obavestenje = new Obavestenje("Recept", DateTime.Now, "Prepisan recept za lek: " + l.Naziv + ". Pogledajte recept na svom profilu.", pacijent.Jmbg);
            ObavestenjaStorage obavestenjaStorage = new ObavestenjaStorage();
            obavestenjaStorage.Create(obavestenje);
        }
    }
}
