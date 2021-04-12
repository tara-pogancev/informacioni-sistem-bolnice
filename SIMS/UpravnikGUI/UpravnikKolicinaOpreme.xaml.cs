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
        Oprema Oprema;

        public UpravnikKolicinaOpreme(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Oprema Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();

            ID.Text = Oprema.Id;
            Naziv.Text = Oprema.Naziv;
            Tip.ItemsSource = Conversion.getTipoviOpreme();
            Tip.SelectedItem = Oprema.TipToString;

            ID.IsEnabled = false;
            Naziv.IsEnabled = false;
            Tip.IsEnabled = false;
            Kolicina.Text = ProstorijaStorage.Instance.Read(BrojProstorije).GetKolicinaOpreme(Oprema.Id).ToString();
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
            Prostorija prostorija = ProstorijaStorage.Instance.Read(BrojProstorije);
            prostorija.SetKolicinaOpreme(Oprema.Id, int.Parse(Kolicina.Text));
            ProstorijaStorage.Instance.Update(prostorija);

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
