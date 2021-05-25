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
using SIMS.Repositories.SecretaryRepo;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.AllergenRepo;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicineEdit.xaml
    /// </summary>
    public partial class MedicineEdit : Window
    {
        private Medication medicine;

        public ObservableCollection<AlergenDTO> NewComponentsView { get; set; }
        public ObservableCollection<AlergenDTO> CurrentComponentsView { get; set; }
        public List<Medication> MedicineSubstitutionList { get; set; }

        public MedicineEdit(Medication medicine)
        {
            this.medicine = medicine;

            InitializeComponent();
            DataContext = this;
            AddButton.Visibility = Visibility.Hidden;

            MedicineNameLabel.Content = "Izmena: " + medicine.MedicineName;
            CurrentSubstitute.Content = "Trenutna zamena: " + GetSubstituteName(medicine);

            NewComponentsView = new ObservableCollection<AlergenDTO>();
            CurrentComponentsView = new ObservableCollection<AlergenDTO>();
            MedicineSubstitutionList = new List<Medication>(MedicationFileRepository.Instance.GetApprovedMedicine());

            RefreshView();

        }

        private static String GetSubstituteName(Medication medicine)
        {
            return MedicationFileRepository.Instance.FindById(medicine.IDSubstitution).MedicineName;
        }

        public void RefreshView()
        {
            NewComponentsView.Clear();
            CurrentComponentsView.Clear();

            AlergenDTO component = new AlergenDTO();
            foreach (AlergenDTO viewItem in component.GetAllAlergenList(medicine))
            {
                if (viewItem.IsIncludedInMedicine == true)
                    CurrentComponentsView.Add(viewItem);
                else NewComponentsView.Add(viewItem);
            }

        }

        public List<AlergenDTO> GetSelectedComponents()
        {
            if (TabbedPanel.SelectedIndex == 0)
                return GetAddedComponents();
            else
                return GetRemovedComponents();
        }

        private List<AlergenDTO> GetAddedComponents()
        {
            List<AlergenDTO> selectedItems = new List<AlergenDTO>();

            foreach (var currentAlergen in DataGridAddNew.SelectedItems)
            {
                AlergenDTO data = currentAlergen as AlergenDTO;
                selectedItems.Add(data);
            }

            return selectedItems;
        }

        private List<AlergenDTO> GetRemovedComponents()
        {
            List<AlergenDTO> selectedItems = new List<AlergenDTO>();

            foreach (var currentAlergen in DataGridComponents.SelectedItems)
            {
                AlergenDTO data = currentAlergen as AlergenDTO;
                selectedItems.Add(data);
            }

            return selectedItems;
        }


        private void ButtonEditMedicine(object sender, RoutedEventArgs e)
        {
            SetSubstituteMedicine();
            MedicationFileRepository.Instance.Update(medicine);

            this.Close();
            MessageBox.Show("Lek uspešno izmenjen!");
        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveElements(object sender, RoutedEventArgs e)
        {
            foreach (AlergenDTO component in GetSelectedComponents())
                medicine.Components.Remove(AllergenFileRepository.Instance.FindById(component.AlergenID));

            RefreshView();
        }

        private void SetSubstituteMedicine()
        {
            if (SubstitutionMedicine.SelectedItem != null)
            {
                Medication SellectedSubstitutionMedicine = (Medication)SubstitutionMedicine.SelectedItem;
                medicine.IDSubstitution = SellectedSubstitutionMedicine.MedicineID;
            }
        }

        private void AddElements(object sender, RoutedEventArgs e)
        {
            foreach (AlergenDTO component in GetSelectedComponents())
                medicine.Components.Add(AllergenFileRepository.Instance.FindById(component.AlergenID));

            RefreshView();
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (/*RemoveButton.Visibility == Visibility.Visible*/ TabbedPanel.SelectedIndex == 0)
                {
                    //Switched to first
                    RemoveButton.Visibility = Visibility.Hidden;
                    AddButton.Visibility = Visibility.Visible;
                }
                else if (TabbedPanel.SelectedIndex == 1)
                {
                    //Switched to second
                    AddButton.Visibility = Visibility.Hidden;
                    RemoveButton.Visibility = Visibility.Visible;
                }
                else
                {
                    AddButton.Visibility = Visibility.Hidden;
                    RemoveButton.Visibility = Visibility.Hidden;
                }

            }
        }
    }
}
