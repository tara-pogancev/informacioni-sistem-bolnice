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
    /// Interaction logic for MedicineApproval.xaml
    /// </summary>
    public partial class MedicineApproval : Window
    {
        public ObservableCollection<Medication> MedicineViewModel { get; set; }
        public List<Medication> MedicineChanges { get; set; }

        private MedicineController medicineController = new MedicineController();

        public MedicineApproval()
        {
            InitializeComponent();

            DataContext = this;

            MedicineViewModel = new ObservableCollection<Medication>();
            MedicineChanges = new List<Medication>(medicineController.GetMedicineWaitingForApproval());
            refresh();

        }

        private void refresh()
        {
            MedicineViewModel.Clear();
            foreach (Medication medicine in MedicineChanges)
                MedicineViewModel.Add(medicine);
        }

        private void ApproveSellecetedMedicine(object sender, RoutedEventArgs e)
        {
            foreach (Medication medicine in GetSellectedItems())
                medicine.ApprovalStatus = MedicineApprovalStatus.Accepted;

            refresh();
        }

        private void RejectSellecetedMedicine(object sender, RoutedEventArgs e)
        {
            foreach (Medication medicine in GetSellectedItems())
                medicine.ApprovalStatus = MedicineApprovalStatus.Denied;

            refresh();
        }

        private void PreviewSellectedMedicine()
        {
            if (DataGridMedicine.SelectedItem != null)
                new MedicinePreview((Medication)DataGridMedicine.SelectedItem).Show();
        }

        private void AcceptChanges(object sender, RoutedEventArgs e)
        {
            DoAcceptChanges();
        }

        private void DoAcceptChanges()
        {
            foreach (Medication medicine in MedicineViewModel)
                medicineController.UpdateMedicine(medicine);

            if (CheckIfMedicineRejected())
                if (MessageBox.Show("Promene uspešno sačuvane! Da li želite da napišete poruku o odbijenim lekovima?", "Napisati poruku?",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    new MedicineDenialWriteMessage().Show();

                else
                    MessageBox.Show("Izmene uspešno sačuvane!");

            this.Close();
        }

        private void PreviewSellectedMedicine(object sender, MouseButtonEventArgs e)
        {
            PreviewSellectedMedicine();
        }

        private void PreviewSellectedMedicine(object sender, RoutedEventArgs e)
        {
            PreviewSellectedMedicine();
        }

        private List<Medication> GetSellectedItems()
        {
            var selectedItems = new List<Medication>();

            foreach (Medication currentMedicine in DataGridMedicine.ItemsSource)
                if (((CheckBox)checkedMedicine.GetCellContent(currentMedicine)).IsChecked == true)
                    selectedItems.Add(currentMedicine);
            
            return selectedItems;
        }

        private Boolean CheckIfMedicineRejected()
        {
            foreach (Medication medicine in MedicineViewModel)
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Denied)
                    return true;
            
            return false;
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Space)
                DoAcceptChanges();
        }

    }
}
