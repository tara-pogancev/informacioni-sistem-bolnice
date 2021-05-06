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
using Model;
using SIMS.DTO;

namespace SIMS.LekarGUI.Dialogues.Materijali_i_lekovi
{
    /// <summary>
    /// Interaction logic for MedicineEdit.xaml
    /// </summary>
    public partial class MedicineEdit : Window
    {
        private Lek medicine;
        public ObservableCollection<AlergenDTO> ComponentsView { get; set; }

        public MedicineEdit(Lek medicine)
        {
            this.medicine = medicine;

            InitializeComponent();
            DataContext = this;

            MedicineNameLabel.Content = "Izmena: " + medicine.MedicineName;

            ComponentsView = new ObservableCollection<AlergenDTO>();
            InitView();

        }

        public void InitView()
        {
            AlergenDTO component = new AlergenDTO();
            foreach (AlergenDTO viewItem in component.GetAllAlergenList(medicine))
            {
                ComponentsView.Add(viewItem);
            }

        }

        public List<AlergenDTO> GetSelectedComponents()
        {
            var selectedItems = new List<AlergenDTO>();

            foreach (AlergenDTO currentAlergen in DataGridComponents.ItemsSource)
            {
                if (((CheckBox)checkedComponent.GetCellContent(currentAlergen)).IsChecked == true)
                {
                    selectedItems.Add(currentAlergen);
                }
            }

            return selectedItems;
        }

        private void ButtonEditMedicine(object sender, RoutedEventArgs e)
        {
            medicine.Components.Clear();

            foreach (AlergenDTO component in GetSelectedComponents())
                medicine.Components.Add(component.AlergenID);

            LekStorage.Instance.Update(medicine);

            this.Close();
            MessageBox.Show("Lek uspešno izmenjen!");
        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
