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

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicinePreview.xaml
    /// </summary>
    public partial class MedicinePreview : Window
    {
        private Medication medicine;

        public MedicinePreview(Medication medicine)
        {
            InitializeComponent();
            medicine = MedicationFileRepository.Instance.FindById(medicine.MedicineID);

            this.medicine = medicine;

            MedicineNameLabel.Content = medicine.MedicineName;

            MedicineComponents.Inlines.Add(new Run("Sastojci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            MedicineComponents.Inlines.Add("   ");
            MedicineComponents.Inlines.Add(medicine.getComponentsList());

            MedicineSubstitute.Inlines.Add(new Run("Zamenski lek:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            MedicineSubstitute.Inlines.Add("   ");
            MedicineSubstitute.Inlines.Add(GetSubstituteName(medicine));

        }

        private String GetSubstituteName(Medication medicine)
        {
            return MedicationFileRepository.Instance.FindById(medicine.IDSubstitution).MedicineName;
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
    }
}
