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
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIzdavanjeRecepta.xaml
    /// </summary>
    public partial class DoctorWriteReciept : Window
    {
        private Patient patient;
        private Doctor doctor = DoctorUI.GetInstance().GetUser();

        public DoctorWriteReciept(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;

            LabelDoctor.Content = "Doktor: " + doctor.FullName;
            LabelPatient.Content = "Pacijent: " + this.patient.FullName;
            LabelReceiptDate.Content = "Datum: " + DateTime.Today.ToString("MM.dd.yyyy.");

            List<Medication> availableMedicine = new List<Medication>(MedicationFileRepository.Instance.GetApprovedMedicine());
            MedicineComboBox.ItemsSource = availableMedicine;

        }

        private void AcceptReceipt(object sender, RoutedEventArgs e)
        {
            if (MedicineComboBox.SelectedItem == null || AmountText.Text.Equals("") || DiagnosisText.Text.Equals(""))
                MessageBox.Show("Greška! Molimo popunite sva polja.");

            else if (patient.IsAlergic(GetSelectedMedicine()))
            {
                Medication l = GetSelectedMedicine();
                if (MessageBox.Show("Pacijent je alergičan na odabran lek! Preporučena zamena po mogućnosti je: " + GetSubstitutionName(l) + ". Da li ste sigurni da želite da izdate lek " + l.MedicineName +"?", "Upozorenje!",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.WriteReceipt();
                }
            }
            else
            {
                this.WriteReceipt();
            } 
                
        }

        private String GetSubstitutionName(Medication medicine)
        {
            return MedicationFileRepository.Instance.FindById(medicine.IDSubstitution).MedicineName;
        }

        private void WriteReceipt()
        {
            CreateReceipt();

            this.Close();
            MessageBox.Show("Uspešno izdat recept!");

            SendNotification(GetSelectedMedicine());
        }

        private void CreateReceipt()
        {
            Medication medicine = GetSelectedMedicine();
            Receipt receipt = new Receipt(doctor, patient, medicine.MedicineName,
                AmountText.Text, DiagnosisText.Text);

            ReceiptFileRepository.Instance.Save(receipt);
        }

        private Medication GetSelectedMedicine()
        {
            return (Medication)MedicineComboBox.SelectedItem;
        }

        private void SendNotification(Medication medicine)
        {
            Notification notification = new Notification("Recept", DateTime.Now, "Prepisan recept za lek: " + medicine.MedicineName + ". Pogledajte recept na svom profilu.", new List<string>() { patient.Jmbg });
            NotificationFileRepository.Instance.Save(notification);
        }
    }
}
