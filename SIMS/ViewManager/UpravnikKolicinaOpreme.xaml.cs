using System;
using System.Collections.Generic;
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
using SIMS.Model;
using SIMS.Controller;
using SIMS.Repositories.RoomInventoryRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikKolicinaOpreme.xaml
    /// </summary>
    public partial class UpravnikKolicinaOpreme : Page
    {
        private UpravnikInventarProstorijePage ParentPage;
        private string BrojProstorije;
        private Inventory Oprema;
        private InventoryController inventoryController = new InventoryController();
        private RoomInventoryController roomInventoryController = new RoomInventoryController();

        public UpravnikKolicinaOpreme(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();

            ID.Text = Oprema.ID;
            Naziv.Text = Oprema.Name;
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
            Tip.SelectedItem = Oprema.TypeToString;

            ID.IsEnabled = false;
            Naziv.IsEnabled = false;
            Tip.IsEnabled = false;
            Kolicina.Text = RoomInventoryFileRepository.Instance.Read(BrojProstorije, Oprema.ID).Kolicina.ToString();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Kolicina.Text = (int.Parse(Kolicina.Text) + int.Parse(Delta.Text)).ToString();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Kolicina.Text = (int.Parse(Kolicina.Text) - int.Parse(Delta.Text)).ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            roomInventoryController.Update(BrojProstorije, Oprema.ID, Kolicina.Text);

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }
    }
}
