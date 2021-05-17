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
using Model;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikKolicinaOpreme.xaml
    /// </summary>
    public partial class UpravnikKolicinaOpreme : Page
    {
        UpravnikInventarProstorijePage ParentPage;
        string BrojProstorije;
        Inventory Oprema;

        public UpravnikKolicinaOpreme(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Inventory Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();

            ID.Text = Oprema.Id;
            Naziv.Text = Oprema.Naziv;
            Tip.ItemsSource = Conversion.GetTipoviOpreme();
            Tip.SelectedItem = Oprema.TipToString;

            ID.IsEnabled = false;
            Naziv.IsEnabled = false;
            Tip.IsEnabled = false;
            Kolicina.Text = RoomInventoryRepository.Instance.Read(BrojProstorije, Oprema.Id).Kolicina.ToString();
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
            int amount;
            try
            {
                amount = int.Parse(Kolicina.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Uneti broj.");
                return;
            }

            if (amount < 0)
            {
                MessageBox.Show("Uneti broj veći od 0.");
                return;
            }
            RoomInventoryRepository.Instance.Update(new RoomInventory(BrojProstorije, Oprema.Id, amount));

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
