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
using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;

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

            LabelDoktor.Content = "Doktor: " + receipt.Doctor.FullName;
            LabelPacijent.Content = "Pacijent: " + receipt.Patient.FullName;
            LabelDatum.Content = "Datum: " + receipt.GetRecieptDateString();

            NazivLeka.Content = receipt.MedicineName;
            Kolicina.Content = receipt.Amount;
            Dijagnoza.Content = receipt.Diagnosis;

        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
