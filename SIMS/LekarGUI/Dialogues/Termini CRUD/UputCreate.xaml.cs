using Model;
using SIMS.DTO;
using SIMS.Model;
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

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class UputCreate : Window
    {
        public ObservableCollection<Lekar> DoctorList { get; set; }
        public Specijalizacija SellectedSpecialization { get; set; }
        public List<SpecializationDTO> AvailableSpecialization { get; set; }

        private Pacijent patient;

        public UputCreate(Pacijent patient)
        {
            this.patient = patient;

            InitializeComponent();

            DataContext = this;

            LabelPacijent.Content = "Pacijent: " + patient.ImePrezime;
            LabelDatum.Content = "Datum: " + DateTime.Today.ToString("dd.MM.yyyy.");

            DoctorList = new ObservableCollection<Lekar>();
            InitSpecialization();
            SpecijalizationComboBox.ItemsSource = AvailableSpecialization;
            DoctorComboBox.ItemsSource = DoctorList;

        }

        public void InitSpecialization()
        {
            AvailableSpecialization = new List<SpecializationDTO>();

            foreach (Lekar doctor in LekarStorage.Instance.ReadList())
            {
                SpecializationDTO currentDoctorSpecialization = new SpecializationDTO(doctor.SpecijalizacijaLekara);

                if (!AvailableSpecialization.Contains(currentDoctorSpecialization));
                    AvailableSpecialization.Add(currentDoctorSpecialization);
            }
        }

        private void SpecializationChanged(object sender, SelectionChangedEventArgs e)
        {
            SellectedSpecialization = GetSellectedSpecialization();
            RefreshDoctorList(SellectedSpecialization);
        }

        private void RefreshDoctorList(Specijalizacija specialization)
        {
            DoctorList = new ObservableCollection<Lekar>();
            foreach (Lekar doctor in LekarStorage.Instance.ReadList())
            {
                if (doctor.SpecijalizacijaLekara.Equals(specialization))
                {
                    DoctorList.Add(doctor);
                }
            }

            DoctorComboBox.ItemsSource = DoctorList;
        }

        private Specijalizacija GetSellectedSpecialization()
        {
            int idx = SpecijalizationComboBox.SelectedIndex;
            return AvailableSpecialization[idx].Specialization;
        }            

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem != null)
            {
                CreateRefferal();
                this.Close();

                SendNotifications((Lekar)DoctorComboBox.SelectedItem);

                MessageBox.Show("Uput uspešno kreiran!");
            }
            else
            {
                MessageBox.Show("Molimo izaberite doktora!");
            }

        }

        private void CreateRefferal()
        {
            Lekar doctor = (Lekar)DoctorComboBox.SelectedItem;
            Uput refferal = new Uput(doctor, patient);
            UputStorage.Instance.Create(refferal);
        }

        private void SendNotifications(Lekar doctor)
        {
            String author = LekarUI.GetInstance().GetUser().ImePrezime;
            List<String> target = new List<string>();
            target.Add(this.patient.Jmbg);

            Obavestenje notification = new Obavestenje(author, DateTime.Now, ("Izdat uput za pregled kod lekara " + doctor.ImePrezime + ". Pogledajte ga na svom profilu."), target);
            ObavestenjaStorage.Instance.Create(notification);
        }
    }
}
