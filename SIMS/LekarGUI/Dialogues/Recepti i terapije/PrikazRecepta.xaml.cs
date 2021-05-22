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
using SIMS.Repositories.PatientRepo;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for Prikaz_recepta.xaml
    /// </summary>
    public partial class PrikazRecepta : Window
    {
        public PrikazRecepta(Receipt receipt)
        {
            InitializeComponent();

            receipt.InitData();

            Patient patient = receipt.Pacijent;
            Doctor doctor = receipt.Lekar;

            LabelDoktor.Content = "Doktor: " + doctor.ImePrezime;
            LabelPacijent.Content = "Pacijent: " + patient.ImePrezime;
            LabelDatum.Content = "Datum: " + receipt.DateString;

            NazivLeka.Content = receipt.NazivLeka;
            Kolicina.Content = receipt.Kolicina;
            Dijagnoza.Content = receipt.Dijagnoza;

        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
