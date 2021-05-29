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
using SIMS.Repositories.InventoryRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikOpremaDetailPage.xaml
    /// </summary>
    public partial class UpravnikOpremaDetailPage : Page
    {
        private Inventory oprema;
        private InventoryController inventoryController = new InventoryController();

        public UpravnikOpremaDetailPage()
        {
            oprema = new Inventory();
            InitializeComponent();
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
        }
        public UpravnikOpremaDetailPage(string Id)
        {
            oprema = InventoryFileRepository.Instance.FindById(Id);
            InitializeComponent();

            ID.Text = oprema.ID;
            Naziv.Text = oprema.Name;
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
            Tip.SelectedItem = oprema.TypeToString;

            ID.IsEnabled = false;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaPage());
            UpravnikWindow.Instance.SetLabel("Oprema");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            oprema.ID = ID.Text;
            oprema.Name = Naziv.Text;
            oprema.Type = Conversion.StringToTipOpreme(Tip.Text);

            inventoryController.CreateOrUpdate(oprema);

            UpravnikWindow.Instance.SetContent(new UpravnikOpremaPage());
            UpravnikWindow.Instance.SetLabel("Oprema");
        }
    }
}
