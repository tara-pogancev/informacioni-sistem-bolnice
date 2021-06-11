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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Model;
using SIMS.Controller;
using SIMS.LekarGUI;
using SIMS.LekarGUI.Dialogues.Hospitalizacija;

namespace SIMS.ViewDoctor.Pages
{
    /// <summary>
    /// Interaction logic for PatientHospitalizationPage.xaml
    /// </summary>
    public partial class PatientHospitalizationPage : Page
    {
        private Patient patient;
        private Hospitalization hospitalization;

        private HospitalizationController hospitalizationController = new HospitalizationController();


        public PatientHospitalizationPage(Patient patientPar)
        {
            InitializeComponent();

            patient = patientPar;
            hospitalization = hospitalizationController.GetPatientCurrentHospitalization(patient);
            hospitalization.InitData();

            LabelNameTop.Content = patient.FullName;
            LabelName.Content = patient.FullName;
            LabelLeadDoctor.Content = "Glavni lekar: " +  hospitalization.LeadDoctor.FullName;
            LabelRoom.Content = "Prostorija: " + hospitalization.Room.Number;

            LabelStartDate.Content = hospitalization.GetStartDateString();
            LabelEndDate.Content = hospitalization.GetEndDateString();

        }

        private void ButtonExtendStay(object sender, RoutedEventArgs e)
        {
            new ExtendStay(hospitalization).ShowDialog();
        }

        private void ButtonReleasePatient(object sender, RoutedEventArgs e)
        {
            new HospitalizeRemove(hospitalization).ShowDialog();
        }

        private void ButtonPatientsView(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void ButtonPatientHealthRecord(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = new PatientRecordCheck(patient);
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }
    }
}
