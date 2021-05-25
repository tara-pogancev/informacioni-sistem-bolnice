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
using SIMS.Daemon.PremestajOpreme;
using SIMS.Model;
using SIMS.Controller;
using SIMS.DTO;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikPremestiOpremu.xaml
    /// </summary>
    public partial class UpravnikPremestiOpremu : Page
    {
        private InventoryController inventoryController = new InventoryController();
        private UpravnikInventarProstorijePage ParentPage;
        private string BrojProstorije;
        private Inventory Oprema;
        public UpravnikPremestiOpremu(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();

            if (Oprema.Type != InventoryType.statička)
            {
                DatumPicker.Visibility = Visibility.Hidden;
                DatumLabel.Visibility = Visibility.Hidden;
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            inventoryController.MoveInventory(new MovingInventoryDTO(BrojProstorije, BrojPremestanja.Text, Kolicina.Text, DatumPicker.SelectedDate, Oprema.ID));

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }
    }
}
