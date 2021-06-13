using SIMS.Repositories.SecretaryRepo;
using SIMS.Filters;
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
using SIMS.Model;
using SIMS.Controller;
using SIMS.Repositories.InventoryRepo;

namespace SIMS.UpravnikGUI
{
    public partial class UpravnikInventarProstorijePage : Page
    {
        private Room Prostorija;
        private UpravnikProstorijaDetailPage ParentPage;
        private ObservableCollection<Inventory> SvaOprema;
        private InventoryController inventoryController = new InventoryController();
        public UpravnikInventarProstorijePage(Room prostorija, UpravnikProstorijaDetailPage parent)
        {
            Prostorija = prostorija;
            ParentPage = parent;
            SvaOprema = new ObservableCollection<Inventory>(inventoryController.GetAll());
            foreach (Inventory op in SvaOprema)
            {
                op.RoomNumber = Prostorija.Number;
            }
            InitializeComponent();
            tabelaInventara.ItemsSource = SvaOprema;
        }

        private void SetNumber()
        {
            foreach (Inventory op in SvaOprema)
            {
                op.RoomNumber = Prostorija.Number;
            }
        }
        private void Premesti_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaInventara.SelectedItem as Inventory;
            if (SelectedOprema == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikPremestiOpremu(this, Prostorija.Number, SelectedOprema));
            UpravnikWindow.Instance.SetLabel("Premesti iz prostorije " + Prostorija.Number + " - " + SelectedOprema.Name);
        }

        private void PromeniKolicinu_Click(object sender, RoutedEventArgs e)
        {
            Inventory SelectedOprema = tabelaInventara.SelectedItem as Inventory;
            if (SelectedOprema == null)
            {
                return;
            }
            UpravnikWindow.Instance.SetContent(new UpravnikKolicinaOpreme(this, Prostorija.Number, SelectedOprema));
            UpravnikWindow.Instance.SetLabel("Prostorija " + Prostorija.Number + " - " + SelectedOprema.Name);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + Prostorija.Number);
        }

        internal void Update()
        {
            SvaOprema = new ObservableCollection<Inventory>(InventoryFileRepository.Instance.GetAll());
            tabelaInventara.Items.Refresh();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            SetNumber();
            Filter();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetNumber();
            Filter();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetNumber();
            Filter();
        }

        private void Filter()
        {
            InventarProstorijeFilter filter = new InventarProstorijeFilter();
            filter.SetKeywordsFromInput(SearchBox.Text);
            filter.CheckBoxChecked = (bool)CheckBox.IsChecked;
            tabelaInventara.ItemsSource = filter.ApplyFilters(SvaOprema);
        }
    }
}
