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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for PacijentKartonView.xaml
    /// </summary>
    public partial class PatientRecordCheck : Page
    {
        private Patient patient;

        public static PatientRecordCheck instance;

        public static PatientRecordCheck GetInstance(Patient patient)
        {
            instance = new PatientRecordCheck(patient);
            return instance;
        }

        public static PatientRecordCheck GetInstance()
        {
            return instance;
        }

        public PatientRecordCheck(Patient patient)
        {
            InitializeComponent();

            this.patient = patient;

            LabelNameTop.Content = this.patient.FullName;
            LabelName.Content = this.patient.FullName;

            LabelGender.Content = "Pol: " + this.patient.GetGenderString();
            LabelDateOfBirth.Content = "Datum rođenja: " + this.patient.GetDateOfBirthString();
            LabelJMBG.Content = "JMBG: " + this.patient.Jmbg;
            LabelLBO.Content = "LBO: " + this.patient.Lbo;

            LabelPhone.Content = "Broj telefona: " + this.patient.Phone;
            LabelEmail.Content = "Email: " + this.patient.Email;
            LabelAddress.Content = "Adresa: " + this.patient.FullAddressString;


            LabelBloodType.Content = "Krvna grupa: " + this.patient.GetBloodTypeString();
            LabelAllergens.Content = "Alergeni: " + this.patient.GetAllergenListString();
            LabelHronical.Content = "Hronične bolesti: " + this.patient.GetHronicalDiseases;

        }

        private void ButtonWriteReceipt(object sender, RoutedEventArgs e)
        {
            new DoctorWriteReceipt(patient).Show();
        }

        private void ButtonDocumentation(object sender, RoutedEventArgs e)
        {
            DoctorUI.GetInstance().SellectedTab.Content = new PatientDocumentationView(patient);
        }

        private void ButtonHospitalize(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ButtonPatientView(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(2);
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void ButtonTherapy(object sender, RoutedEventArgs e)
        {
            new TherapyCreate().ShowDialog();
        }
    }
}
