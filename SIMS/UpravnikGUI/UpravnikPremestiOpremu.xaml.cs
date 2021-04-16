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
            ProsInv src = ProsInvStorage.Instance.Read(BrojProstorije, Oprema.Id);
            ProsInv dst = ProsInvStorage.Instance.Read(BrojPremestanja.Text, Oprema.Id);

            int delta;

            try 
            {
                delta = int.Parse(Kolicina.Text);
            } 
            catch (Exception)
            {
                return;
            }
            if (src.Kolicina < delta)
            {
                return;
            }

            src.Kolicina -= delta;
            dst.Kolicina += delta;

            ProsInvStorage.Instance.Update(src);
            ProsInvStorage.Instance.Update(dst);

            ParentPage.Update();

            UpravnikWindow.Instance.SetContent(ParentPage);
            UpravnikWindow.Instance.SetLabel("Prostorija " + BrojProstorije);
        }
    }
}
