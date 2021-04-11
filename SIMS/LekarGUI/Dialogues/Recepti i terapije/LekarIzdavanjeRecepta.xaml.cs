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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NazivLekaTxt.Text.Equals("") || KolicinaTxt.Text.Equals("") || DijagnozaTxt.Text.Equals(""))
                MessageBox.Show("Greška! Molimo popunite sva polja.");
            else
            {
                Recept r = new Recept(lekar.Jmbg, pacijent.Jmbg, NazivLekaTxt.Text,
                    KolicinaTxt.Text, DijagnozaTxt.Text);

                ReceptStorage.Instance.Create(r);
                this.Close();
                MessageBox.Show("Uspešno izdat recept!");
            } 
                
        }
    }
}
