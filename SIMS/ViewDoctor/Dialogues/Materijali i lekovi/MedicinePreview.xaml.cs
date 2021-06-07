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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicinePreview.xaml
    /// </summary>
    public partial class MedicinePreview : Window
    {
        private Medication medicine;

        private MedicineController medicineController = new MedicineController();

        public MedicinePreview(Medication medicinePar)
        {
            InitializeComponent();
            medicine = medicineController.GetMedicine(medicinePar.MedicineID);

            MedicineNameLabel.Content = medicine.MedicineName;

            InitializeLabels();

        }

        private void InitializeLabels()
        {
            MedicineComponents.Inlines.Add(new Run("Sastojci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            MedicineComponents.Inlines.Add("   ");
            MedicineComponents.Inlines.Add(medicine.GetComponentsList());

            MedicineSubstitute.Inlines.Add(new Run("Zamenski lek:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            MedicineSubstitute.Inlines.Add("   ");
            MedicineSubstitute.Inlines.Add(GetSubstituteName(medicine));
        }

        private String GetSubstituteName(Medication medicine)
        {
            return medicineController.GetMedicine(medicine.IDSubstitution).MedicineName;
        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonEditMedicine(object sender, RoutedEventArgs e)
        {
            this.Close();
            new MedicineEdit(medicine).ShowDialog();
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
