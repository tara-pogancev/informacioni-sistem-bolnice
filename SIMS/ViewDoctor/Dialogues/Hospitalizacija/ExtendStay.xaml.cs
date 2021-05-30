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
using SIMS.ViewDoctor.Pages;

namespace SIMS.LekarGUI.Dialogues.Hospitalizacija
{
    /// <summary>
    /// Interaction logic for ExtendStay.xaml
    /// </summary>
    public partial class ExtendStay : Window
    {
        private Hospitalization hospitalization;
        private HospitalizationController hospitalizationController = new HospitalizationController();

        public ExtendStay(Hospitalization hospitalizationPar)
        {
            InitializeComponent();
            hospitalization = hospitalizationPar;
            hospitalization.InitData();

            LabelDoctor.Content = "Doktor: " + hospitalization.LeadDoctor.FullName;
            LabelPatient.Content = "Pacijent: " + hospitalization.Patient.FullName;
            EndDate.SelectedDate = hospitalization.EndDate;

        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
                MessageBox.Show("Nije odabran validan datum. Molimo odaberite datum nakon trenutnog završnog datuma hospitalizacije.");

            else
                UpdateHospitalization();
        }

        private bool ValidateForm()
        {
            return EndDate.SelectedDate < hospitalization.EndDate && EndDate.SelectedDate != null;
        }

        private void UpdateHospitalization()
        {
            hospitalization.EndDate = (DateTime)EndDate.SelectedDate;
            hospitalizationController.UpdateHospitalization(hospitalization);
            this.Close();
            MessageBox.Show("Boravak uspešno produžen!");
            DoctorUI.GetInstance().SellectedTab.Content = new PatientHospitalizationPage(hospitalization.Patient);
        }
    }
}
