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
using SIMS.Model;
using SIMS.Controller;
using System.Collections.ObjectModel;

namespace SIMS.ViewDoctor.Dialogues.GeneratePDF
{
    /// <summary>
    /// Interaction logic for PDFReport.xaml
    /// </summary>
    public partial class PDFReport : Window
    {
        public ObservableCollection<Anamnesis> AnamnesisViewModel { get; set; }
        private AnamnesisController anamnesisController = new AnamnesisController();

        public PDFReport(Patient patient, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            DataContext = this;
            InitializeData(patient, startDate, endDate);
            dataGridAnamnesis.ItemsSource = AnamnesisViewModel;

            if (AnamnesisViewModel.Count == 0)
                MessageBox.Show("Nema postojanih zdravstevnih izveštaja za odabran preriod.");

            else
                InitiatePDFSave();
        }

        private void InitiatePDFSave()
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                    printDialog.PrintVisual(print, "inovice");
            }
            finally
            {
            }
        }

        private void InitializeData(Patient patient, DateTime startDate, DateTime endDate)
        {
            AnamnesisViewModel = new ObservableCollection<Anamnesis>(anamnesisController.GetListForPatientByDate(patient, startDate, endDate));
            LabelPatientName.Content = "Pacijent: " + patient.FullName;
            LabelBirthDate.Content = "Datum rođenja: " + patient.GetDateOfBirthString();
            LabelGender.Content = "Pol: " + patient.GetGenderString();
            LabelDateReport.Content = "Izveštaj za period: " + startDate.ToString("dd.MM.yyyy.") + " - " + endDate.ToString("dd.MM.yyyy.");
        }
    }
}
