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

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicinePreview.xaml
    /// </summary>
    public partial class MedicinePreview : Window
    {
        private Lek medicine;

        public MedicinePreview(Lek medicine)
        {
            InitializeComponent();

            this.medicine = medicine;

            MedicineNameLabel.Content = medicine.MedicineName;

            MedicineComponents.Inlines.Add(new Run("Sastojci:") { FontWeight = FontWeights.Bold, TextDecorations = TextDecorations.Underline });
            MedicineComponents.Inlines.Add("   ");
            MedicineComponents.Inlines.Add(medicine.getComponentsList());

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
