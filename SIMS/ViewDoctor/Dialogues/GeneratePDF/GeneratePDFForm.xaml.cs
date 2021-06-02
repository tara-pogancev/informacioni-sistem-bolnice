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
using SIMS.ViewDoctor.Dialogues.GeneratePDF.ViewModel;

namespace SIMS.ViewDoctor.Dialogues.GeneratePDF
{
    /// <summary>
    /// Interaction logic for GeneratePDFForm.xaml
    /// </summary>
    public partial class GeneratePDFForm : Window
    {
        private Patient patient;

        public GeneratePDFForm(Patient patientPar)
        {
            patient = patientPar;
            InitializeComponent();
            LabelDoctor.Content = DoctorUI.GetInstance().GetUser().FullName;
            LabelPatient.Content = patient.FullName;
        }

        public GeneratePDFForm(PDFViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                new PDFReport(patient, GetStartDate(), GetEndDate());
                this.Close();
                MessageBox.Show("Izveštaj uspešno sačuvan!");
            }
            else
                MessageBox.Show("Odabrani datumi nisu validni!", "Upozorenje!");
        }

        private DateTime GetEndDate()
        {
            return (DateTime)EndDate.SelectedDate;
        }

        private DateTime GetStartDate()
        {
            return (DateTime)StartDate.SelectedDate;
        }

        private bool ValidateForm()
        {
            return StartDate.SelectedDate <= EndDate.SelectedDate;
        }
    }
}
