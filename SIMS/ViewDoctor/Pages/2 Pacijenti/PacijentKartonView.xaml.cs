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
using SIMS.LekarGUI.Dialogues.Recepti_i_terapije;
using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Controller;
using SIMS.LekarGUI.Dialogues.Termini_CRUD;
using SIMS.ViewDoctor.Dialogues.Hospitalizacija;
using SIMS.ViewDoctor.Pages;
using SIMS.LekarGUI.Dialogues.Hospitalizacija;
using SIMS.ViewDoctor.Pages._2_Pacijenti.ViewModel;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PatientRecordCheck : Page
    {
        private Patient patient;
        private HospitalizationController hospitalizationController = new HospitalizationController();

        private PatientController patientController = new PatientController();

        public PatientRecordCheck(Patient patientPar)
        {
            InitializeComponent();
            patient = patientController.GetPatient(patientPar.Jmbg);
            this.DataContext = new PatientRecordViewModel(patient);

            if (hospitalizationController.GetIfPatientHospitalzied(patient))
            {
                HospitalizationFrame.Content = new HospitalizationPage(hospitalizationController.GetPatientCurrentHospitalization(patient));
            }
        }
        
        private void ButtonPatientView(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

    }
}
