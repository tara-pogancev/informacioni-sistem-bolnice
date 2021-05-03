using Model;
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

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for AvailableMedicineView.xaml
    /// </summary>
    public partial class AvailableMedicineView : Window
    {
        public ObservableCollection<Lek> MedicineView { get; set; }

        public AvailableMedicineView()
        {
            InitializeComponent();

            DataContext = this;

            MedicineView = new ObservableCollection<Lek>(LekStorage.Instance.getApprovedMedicine());

        }
        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
               
        private void PreviewSellectedMedicine()
        {
            if (DataGridMedicine.SelectedItem != null)
            {
                MedicinePreview window = new MedicinePreview((Lek)DataGridMedicine.SelectedItem);
                window.Show();
            }
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
