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
using SIMS.LekarGUI.Dialogues.Recepti_i_terapije;
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class ActionsAfterReport : Window
    {
        private Patient patient;
        private PatientController patientController = new PatientController();


        public ActionsAfterReport(Patient patientPar)
        {
            this.patient = patientController.GetPatient(patientPar.Jmbg);
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowPatientRecordCheck(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = PatientRecordCheck.GetInstance(patient);
            this.Close();
        }

        private void WriteReceipt(object sender, RoutedEventArgs e)
        {
            new DoctorWriteReceipt(patient).ShowDialog();
        }

        private void WriteRefferal(object sender, RoutedEventArgs e)
        {
            new WriteReferral(patient).ShowDialog();
        }

        private void CreateSurgery(object sender, RoutedEventArgs e)
        {
            new SurgeryCreate(patient).ShowDialog();
        }

        private void WriteTherapy(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void CreateAppointment(object sender, RoutedEventArgs e)
        {
            new AppointmentCreate(patient).ShowDialog();
        }

        private void UrgentSurgery(object sender, RoutedEventArgs e)
        {
            new UrgentSurgeryCreate(patient).ShowDialog();
        }
    }
}
