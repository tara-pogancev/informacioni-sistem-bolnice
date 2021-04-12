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
    /// Interaction logic for UpravnikPremestiOpremu.xaml
    /// </summary>
    public partial class UpravnikPremestiOpremu : Page
    {

        UpravnikInventarProstorijePage ParentPage;
        string BrojProstorije;
        Oprema Oprema;
        public UpravnikPremestiOpremu(UpravnikInventarProstorijePage ParentPage, string BrojProstorije, Oprema Oprema)
        {
            this.ParentPage = ParentPage;
            this.BrojProstorije = BrojProstorije;
            this.Oprema = Oprema;
            InitializeComponent();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Prostorija dst = ProstorijaStorage.Instance.Read(BrojPremestanja.Text);
            dst.SetKolicinaOpreme(Oprema.Id, dst.GetKolicinaOpreme(Oprema.Id) + int.Parse(Kolicina.Text));

            Prostorija src = ProstorijaStorage.Instance.Read(BrojProstorije);
            src.SetKolicinaOpreme(Oprema.Id, src.GetKolicinaOpreme(Oprema.Id) - int.Parse(Kolicina.Text));

            ProstorijaStorage.Instance.Update(dst);
            ProstorijaStorage.Instance.Update(src);

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }
    }
}
