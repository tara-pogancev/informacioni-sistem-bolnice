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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Filters;
using SIMS.Repositories.AllergenRepo;

namespace SIMS.UpravnikGUI
{
    public partial class AlergeniPage : Page
    {
        private ObservableCollection<Allergen> alergeni;

        public AlergeniPage()
        {
            InitializeComponent();
            alergeni = new ObservableCollection<Allergen>(AllergenFileRepository.Instance.GetAll());
            tabelaAlergeni.ItemsSource = alergeni;
        }

        private void DodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new AlergeniDetailPage());
            UpravnikWindow.Instance.SetLabel("Nov alergen");
        }

        private void IzbrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            Allergen SelectedAlergen = tabelaAlergeni.SelectedItem as Allergen;
            AllergenFileRepository.Instance.Delete(SelectedAlergen.ID);
            alergeni = new ObservableCollection<Allergen>(AllergenFileRepository.Instance.GetAll());
            tabelaAlergeni.ItemsSource = alergeni;
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Allergen SelectedAlergen = tabelaAlergeni.SelectedItem as Allergen;
            if (SelectedAlergen == null)
            {
                MessageBox.Show("Izabrati alergen.");
                return;
            }
            UpravnikWindow.Instance.SetContent(new AlergeniDetailPage(SelectedAlergen.ID));
            UpravnikWindow.Instance.SetLabel("Alergen " + SelectedAlergen.ID);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            tabelaAlergeni.ItemsSource = AlergeniFilter.Instance.ApplyFilters(alergeni, SearchBox.Text, false);
        }
    }
}
