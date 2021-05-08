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
using Model;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIzdavanjeRecepta.xaml
    /// </summary>
    public partial class LekarIzdavanjeRecepta : Window
    {
        private Pacijent patient;
        private Lekar doctor = LekarUI.GetInstance().GetUser();

        public LekarIzdavanjeRecepta(Pacijent patient)
        {
            InitializeComponent();
            this.patient = patient;

            LabelDoctor.Content = "Doktor: " + doctor.ImePrezime;
            LabelPatient.Content = "Pacijent: " + this.patient.ImePrezime;
            LabelReceiptDate.Content = "Datum: " + DateTime.Today.ToString("MM.dd.yyyy.");

            List<Lek> availableMedicine = new List<Lek>(LekStorage.Instance.getApprovedMedicine());
            MedicineComboBox.ItemsSource = availableMedicine;

        }

        private void AcceptReceipt(object sender, RoutedEventArgs e)
        {
            if (MedicineComboBox.SelectedItem == null || AmountText.Text.Equals("") || DiagnosisText.Text.Equals(""))
                MessageBox.Show("Greška! Molimo popunite sva polja.");

            else if (patient.IsAlergic(GetSelectedMedicine()))
            {
                Lek l = GetSelectedMedicine();
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

        private String GetSubstitutionName(Lek medicine)
        {
            return LekStorage.Instance.Read(medicine.IDSubstitution).MedicineName;
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
            Lek medicine = GetSelectedMedicine();
            Recept receipt = new Recept(doctor, patient, medicine.MedicineName,
                AmountText.Text, DiagnosisText.Text);

            ReceptStorage.Instance.Create(receipt);
        }

        private Lek GetSelectedMedicine()
        {
            return (Lek)MedicineComboBox.SelectedItem;
        }

        private void SendNotification(Lek medicine)
        {
            Obavestenje notification = new Obavestenje("Recept", DateTime.Now, "Prepisan recept za lek: " + medicine.MedicineName + ". Pogledajte recept na svom profilu.", new List<string>() { patient.Jmbg });
            ObavestenjaStorage.Instance.Create(notification);
        }
    }
}
