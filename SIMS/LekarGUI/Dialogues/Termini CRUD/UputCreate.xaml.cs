using Model;
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

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for ActionsAfterReport.xaml
    /// </summary>
    public partial class UputCreate : Window
    {
        public List<Lekar> DoctorList { get; set; }
        public Specijalizacija SellectedSpecialization { get; set; }
        public List<Specijalizacija> AvailableSpecialization { get; set; }
        public List<String> SpecializationComboBox { get; set; }

        private Pacijent patient;

        public UputCreate(Pacijent patient)
        {
            this.patient = patient;

            InitializeComponent();

            LabelPacijent.Content = "Pacijent: " + patient.ImePrezime;
            LabelDatum.Content = "Datum: " + DateTime.Today.ToString("dd.MM.yyyy.");

            DoctorList = new List<Lekar>();
            InitSpecialization();
            SpecijalizationComboBox.ItemsSource = SpecializationComboBox;
            DoctorComboBox.ItemsSource = DoctorList;
            SpecijalizationComboBox.SelectedIndex = 0;
            SellectedSpecialization = GetSellectedSpecialization();
            RefreshDoctorList(SellectedSpecialization);

        }

        public void InitSpecialization()
        {
            AvailableSpecialization = new List<Specijalizacija>();
            SpecializationComboBox = new List<String>();

            foreach (Lekar doctor in LekarStorage.Instance.ReadList())
            {
                if (!AvailableSpecialization.Contains(doctor.SpecijalizacijaLekara))
                {
                    AvailableSpecialization.Add(doctor.SpecijalizacijaLekara);
                    SpecializationComboBox.Add(doctor.Specialization);
                }
            }
        }

        private void SpecializationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SellectedSpecialization != GetSellectedSpecialization())
            {
                SellectedSpecialization = GetSellectedSpecialization();
                RefreshDoctorList(SellectedSpecialization);
            }
        }

        private void RefreshDoctorList(Specijalizacija specialization)
        {
            DoctorComboBox.SelectedIndex = -1;
            DoctorList.Clear();
            foreach (Lekar doctor in LekarStorage.Instance.ReadList())
            {
                if (doctor.SpecijalizacijaLekara.Equals(specialization))
                {
                    DoctorList.Add(doctor);
                }
            }

        }

        private Specijalizacija GetSellectedSpecialization()
        {
            int idx = SpecijalizationComboBox.SelectedIndex;
            return AvailableSpecialization[idx];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem != null)
            {
                Lekar doctor = (Lekar)DoctorComboBox.SelectedItem;
                Uput refferal = new Uput(doctor, patient);
                UputStorage.Instance.Create(refferal);
                this.Close();

                String author = LekarUI.GetInstance().GetUser().ImePrezime;
                List<String> target = new List<string>();
                target.Add(this.patient.Jmbg);

                Obavestenje notification = new Obavestenje(author, DateTime.Now, ("Izdat uput za pregled kod lekara " + doctor.ImePrezime + ". Pogledajte ga na svom profilu."), target);
                ObavestenjaStorage.Instance.Create(notification);

                MessageBox.Show("Uput uspešno kreiran!");
            }
        }
    }
}
