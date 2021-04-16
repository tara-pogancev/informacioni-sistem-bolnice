using Model;
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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for IzmeniObavestenjePage.xaml
    /// </summary>
    public partial class IzmeniObavestenjePage : Page
    {
        private ObservableCollection<Obavestenje> listaTekstova;
        private Obavestenje obavestenje;
        public IzmeniObavestenjePage(ObservableCollection<Obavestenje> listaTekstova, Obavestenje obavestenje)
        {
            InitializeComponent();

            this.listaTekstova = listaTekstova;
            this.obavestenje = obavestenje;

            viewerObavestenja.ItemsSource = this.listaTekstova;
            obavestenjeTextBox.Text = obavestenje.Tekst;
        }

        private void Button_Izmeni(object sender, RoutedEventArgs e)
        {
            listaTekstova.Remove(obavestenje);
            ObavestenjaStorage.Instance.Delete(obavestenje.ID);
            obavestenje = new Obavestenje("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), "All");
            obavestenje.Tekst = obavestenjeTextBox.Text;
            obavestenje.Vreme = DateTime.Now;
            obavestenje.ID = obavestenje.Autor + obavestenje.Vreme.ToString("yyMMddHHmmss");
            ObavestenjaStorage.Instance.Create(obavestenje);

            //listaTekstova.Insert(0, obavestenje);

            this.NavigationService.Navigate(new SekretarObavestenjaPage());
        }

        private void Button_Otkazi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SekretarObavestenjaPage());
        }
    }
}
