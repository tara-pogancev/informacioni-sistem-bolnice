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
    /// Interaction logic for MedicineApproval.xaml
    /// </summary>
    public partial class MedicineApproval : Window
    {
        public ObservableCollection<Lek> MedicineView { get; set; }

        public MedicineApproval()
        {
            InitializeComponent();

            DataContext = this;

            MedicineView = new ObservableCollection<Lek>(LekStorage.Instance.getMedicineWaitingForApproval());

        }

        private void refresh()
        {
            //TODO
        }

        private void ApproveSellecetedMedicine(object sender, RoutedEventArgs e)
        {
            foreach (Lek medicine in getSellectedItems())
            {
                medicine.ApprovalStatus = MedicineApprovalStatus.Accepted;
            }
        }

        private void RejectSellecetedMedicine(object sender, RoutedEventArgs e)
        {
            foreach (Lek medicine in getSellectedItems())
            {
                medicine.ApprovalStatus = MedicineApprovalStatus.Denied;
            }
        }

        private void PreviewSellectedMedicine()
        {
            if (DataGridMedicine.SelectedItem != null)
            {
                MedicinePreview window = new MedicinePreview((Lek)DataGridMedicine.SelectedItem);
                window.Show();
            }
        }

        private void AcceptChanges(object sender, RoutedEventArgs e)
        {
            foreach (Lek medicine in MedicineView)
            {
                LekStorage.Instance.Update(medicine);
            }

            if (CheckIfMedicineRejected())
            {
                if (MessageBox.Show("Promene uspešno sačuvane! Da li želite da napišete poruku o odbijenim lekovima?", "Napisati poruku?",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var window = new MedicineDenialWriteMessage();
                    window.Show();
                }
            }
            else
            {
                MessageBox.Show("Izmene uspešno sačuvane!");
            }

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

        private List<Lek> getSellectedItems()
        {
            var selectedItems = new List<Lek>();

            foreach (Lek currentMedicine in DataGridMedicine.ItemsSource)
            {
                if (((CheckBox)checkedMedicine.GetCellContent(currentMedicine)).IsChecked == true)
                {
                    selectedItems.Add(currentMedicine);
                }
            }
            return selectedItems;
        }

        private Boolean CheckIfMedicineRejected()
        {
            foreach (Lek medicine in MedicineView)
            {
                if (medicine.ApprovalStatus == MedicineApprovalStatus.Denied)
                    return true;
            }

            return false;
        }

    }
}
