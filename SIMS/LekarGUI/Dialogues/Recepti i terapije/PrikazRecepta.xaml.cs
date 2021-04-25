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
    /// Interaction logic for Prikaz_recepta.xaml
    /// </summary>
    public partial class PrikazRecepta : Window
    {
        public PrikazRecepta(Recept r)
        {
            InitializeComponent();


            Pacijent pacijent = r.Pacijent;
            Lekar lekar = r.Lekar;

            LabelDoktor.Content = "Doktor: " + lekar.ImePrezime;
            LabelPacijent.Content = "Pacijent: " + pacijent.ImePrezime;
            LabelDatum.Content = "Datum: " + r.DateString;

            NazivLeka.Content = r.NazivLeka;
            Kolicina.Content = r.Kolicina;
            Dijagnoza.Content = r.Dijagnoza;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
