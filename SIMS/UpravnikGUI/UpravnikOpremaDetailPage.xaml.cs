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
using SIMS.Repositories.PatientRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikOpremaDetailPage.xaml
    /// </summary>
    public partial class UpravnikOpremaDetailPage : Page
    {
        Inventory oprema;

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

            ID.Text = oprema.Id;
            Naziv.Text = oprema.Naziv;
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
            Tip.SelectedItem = oprema.TipToString;

            ID.IsEnabled = false;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikOpremaPage());
            UpravnikWindow.Instance.SetLabel("Oprema");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            oprema.Id = ID.Text;
            oprema.Naziv = Naziv.Text;
            oprema.TipOpreme = Conversion.StringToTipOpreme(Tip.Text);

            InventoryFileRepository.Instance.CreateOrUpdate(oprema);

            UpravnikWindow.Instance.SetContent(new UpravnikOpremaPage());
            UpravnikWindow.Instance.SetLabel("Oprema");
        }
    }
}
