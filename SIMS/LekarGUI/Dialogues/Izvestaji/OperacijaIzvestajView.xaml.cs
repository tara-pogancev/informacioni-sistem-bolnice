using SIMS.Model;
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

namespace SIMS.LekarGUI.Dialogues.Izvestaji
{
    /// <summary>
    /// Interaction logic for OperacijaIzvestajView.xaml
    /// </summary>
    public partial class SurgeryReportRead : Window
    {
        public SurgeryReportRead(SurgeryReport report)
        {
            InitializeComponent();
            report.InitData();

            LabelDoctor.Content = "Doktor: " + report.ImeLekara;
            LabelDate.Content = "Datum operacije: " + report.DatumOperacijeIspis;

            LabelPacijent.Content = "Pacijent: " + report.ImePacijenta;
            LabelBirthDate.Content = "Datum rođenja: " + report.DatumRodjenjaPacijenta;

            LabelRoom.Content = "Prostorija: " + report.SobaOperacije;

            OperationName.Content = report.NazivOperacije;

            OperationDescription.Text = report.NapomeneOperacije;

        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
