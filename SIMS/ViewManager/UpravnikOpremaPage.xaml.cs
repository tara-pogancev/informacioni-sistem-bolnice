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
using SIMS.Model;
using SIMS.Controller;
using SIMS.Repositories.InventoryRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikOpremaPage.xaml
    /// </summary>
    public partial class UpravnikOpremaPage : Page
    {
        private ObservableCollection<Inventory> inventories;
        private InventoryController inventoryController = new InventoryController();
        public UpravnikOpremaPage()
        {
            InitializeComponent();
            inventories = new ObservableCollection<Inventory>(InventoryFileRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = inventories;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage());
            UpravnikWindow.Instance.SetLabel("Nova oprema");
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaOpreme.SelectedItem as Inventory;
            inventoryController.Delete(SelectedOprema.ID);
            inventories = new ObservableCollection<Inventory>(InventoryFileRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = inventories;
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaOpreme.SelectedItem as Inventory;
            if (SelectedOprema == null)
            {
                MessageBox.Show("Izabrati opremu.");
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage(SelectedOprema.ID));
            UpravnikWindow.Instance.SetLabel("Oprema " + SelectedOprema.ID);
            inventories = new ObservableCollection<Inventory>(InventoryFileRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = inventories;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            InventarFilter filter = new InventarFilter();
            filter.SetKeywordsFromInput(SearchBox.Text);
            tabelaOpreme.ItemsSource = filter.ApplyFilters(inventories);
        }
    }
}
