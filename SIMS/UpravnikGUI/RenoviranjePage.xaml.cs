using Model;
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

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for RenoviranjePage.xaml
    /// </summary>
    public partial class RenoviranjePage : Page
    {
        Prostorija prostorija;
        public RenoviranjePage(string BrojProstorije)
        {
            InitializeComponent();
            prostorija = ProstorijaStorage.Instance.Read(BrojProstorije);
            if (prostorija.pocetakRenoviranja != null && prostorija.krajRenoviranja != null)
            {
                Pocetak.SelectedDate = prostorija.pocetakRenoviranja;
                Kraj.SelectedDate = prostorija.krajRenoviranja;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Pocetak.SelectedDate != null && Kraj.SelectedDate != null)
            {
                prostorija.pocetakRenoviranja = Pocetak.SelectedDate;
                prostorija.krajRenoviranja = Kraj.SelectedDate;
                ProstorijaStorage.Instance.Update(prostorija);
            }
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Otkaz_Click(object sender, RoutedEventArgs e)
        {
            prostorija.pocetakRenoviranja = null;
            prostorija.krajRenoviranja = null;
            ProstorijaStorage.Instance.Update(prostorija);
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }
    }
}
