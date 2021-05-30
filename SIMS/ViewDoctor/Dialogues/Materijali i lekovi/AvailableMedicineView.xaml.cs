using SIMS.Repositories.SecretaryRepo;
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
using SIMS.Model;
using SIMS.Controller;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for AvailableMedicineView.xaml
    /// </summary>
    public partial class AvailableMedicineView : Window
    {
        public ObservableCollection<Medication> MedicineViewModel { get; set; }
        private MedicineController medicineController = new MedicineController();

        public AvailableMedicineView()
        {
            InitializeComponent();
            DataContext = this;

            MedicineViewModel = new ObservableCollection<Medication>(medicineController.GetApprovedMedicine());

        }
        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
               
        private void PreviewSellectedMedicine()
        {
            if (DataGridMedicine.SelectedItem != null)
            {
                new MedicinePreview(GetSelectedMedicine()).Show();
            }
        }

        private Medication GetSelectedMedicine()
        {
            return (Medication)DataGridMedicine.SelectedItem;
        }

        private void ReadMedicine(object sender, RoutedEventArgs e)
        {
            PreviewSellectedMedicine();
        }

        private void PreviewSellectedMedicine(object sender, MouseButtonEventArgs e)
        {
            PreviewSellectedMedicine();
        }

    }
}
