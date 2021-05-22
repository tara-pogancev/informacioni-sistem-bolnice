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
using Model;
using SIMS.Filters;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikOpremaPage.xaml
    /// </summary>
    public partial class UpravnikOpremaPage : Page
    {
        private ObservableCollection<Inventory> opreme;
        public UpravnikOpremaPage()
        {
            InitializeComponent();
            opreme = new ObservableCollection<Inventory>(InventoryRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = opreme;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage());
            UpravnikWindow.Instance.SetLabel("Nova oprema");
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaOpreme.SelectedItem as Inventory;
            InventoryRepository.Instance.Delete(SelectedOprema.Id);
            opreme = new ObservableCollection<Inventory>(InventoryRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = opreme;
        }

        private void PregledajUredi_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaOpreme.SelectedItem as Inventory;
            if (SelectedOprema == null)
            {
                MessageBox.Show("Izabrati opremu.");
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaDetailPage(SelectedOprema.Id));
            UpravnikWindow.Instance.SetLabel("Oprema " + SelectedOprema.Id);
            opreme = new ObservableCollection<Inventory>(InventoryRepository.Instance.GetAll());
            tabelaOpreme.ItemsSource = opreme;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            tabelaOpreme.ItemsSource = InventarFilter.Instance.ApplyFilters(opreme, SearchBox.Text, false);
        }
    }
}
