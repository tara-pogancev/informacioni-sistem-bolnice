using SIMS.Repositories.SecretaryRepo;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class WriteReferral : Window
    {
        public ObservableCollection<DoctorDTO> DoctorList { get; set; }
        public Specialization SellectedSpecialization { get; set; }
        public List<SpecializationDTO> AvailableSpecialization { get; set; }

        private ReferralController referralController = new ReferralController();
        private DoctorController doctorController = new DoctorController();
        private NotificationController notificationController = new NotificationController();
        private PatientController patientController = new PatientController();

        private Patient patient;

        public WriteReferral(Patient patientPar)
        {
            patient = patientController.GetPatient(patientPar.Jmbg);

            InitializeComponent();
            DataContext = this;

            LabelPatientName.Content = "Pacijent: " + patient.FullName;
            LabelDate.Content = "Datum: " + DateTime.Today.ToString("dd.MM.yyyy.");

            DoctorList = new ObservableCollection<DoctorDTO>();
            InitSpecialization();
            SpecijalizationComboBox.ItemsSource = AvailableSpecialization;
            DoctorComboBox.ItemsSource = DoctorList;

        }

        public void InitSpecialization()
        {
            AvailableSpecialization = new List<SpecializationDTO>();

            foreach (Doctor doctor in doctorController.GetAllDoctors())
            {
                SpecializationDTO currentDoctorSpecialization = new SpecializationDTO(doctor.DoctorSpecialization);

                if (!AvailableSpecialization.Contains(currentDoctorSpecialization));
                    AvailableSpecialization.Add(currentDoctorSpecialization);
            }
        }

        private void SpecializationChanged(object sender, SelectionChangedEventArgs e)
        {
            SellectedSpecialization = GetSelectedSpecialization();
            RefreshDoctorList(SellectedSpecialization);
        }

        private void RefreshDoctorList(Specialization specialization)
        {
            DoctorList = new ObservableCollection<DoctorDTO>();
            foreach (Doctor doctor in doctorController.GetAllDoctors())
            {
                if (doctor.DoctorSpecialization.Equals(specialization))
                {
                    DoctorList.Add(doctorController.GetDTO(doctor));
                }
            }

            DoctorComboBox.ItemsSource = DoctorList;
        }

        private Specialization GetSelectedSpecialization()
        {
            int idx = SpecijalizationComboBox.SelectedIndex;
            return AvailableSpecialization[idx].Specialization;
        }            

        private Doctor GetSelectedDoctor()
        {
            DoctorDTO dto = (DoctorDTO)DoctorComboBox.SelectedItem;
            return doctorController.GetDoctor(dto.Jmbg);
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem != null)
            {
                CreateRefferal();
                this.Close();

                SendNotifications(GetSelectedDoctor());

                MessageBox.Show("Uput uspešno kreiran!");
            }
            else
            {
                MessageBox.Show("Molimo izaberite doktora!");
            }

        }

        private void CreateRefferal()
        {
            Doctor doctor = GetSelectedDoctor();
            Referral referral = new Referral(doctor, patient);
            referralController.SaveReferral(referral);
        }

        private void SendNotifications(Doctor doctor)
        {
            String author = DoctorUI.GetInstance().GetUser().FullName;
            List<String> target = new List<string>();
            target.Add(patient.Jmbg);

            Notification notification = new Notification(author, DateTime.Now, ("Izdat uput za pregled kod lekara " + doctor.FullName + ". Pogledajte ga na svom profilu."), target);
            notificationController.SaveNotification(notification);
        }
    }
}
