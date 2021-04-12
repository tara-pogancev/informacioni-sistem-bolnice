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
    /// Interaction logic for SekretarObavestenjaPage.xaml
    /// </summary>
    public partial class SekretarObavestenjaPage : Page
    {
        private ObservableCollection<Obavestenje> listaTekstova;
        public SekretarObavestenjaPage()
        {
            InitializeComponent();
            List<Obavestenje> listaObavestenja = ObavestenjaStorage.Instance.ReadList();
            listaObavestenja.Reverse();
            listaTekstova = new ObservableCollection<Obavestenje>(listaObavestenja);
            
            viewerObavestenja.ItemsSource = listaTekstova;
        }

        private void Button_Dodaj(object sender, RoutedEventArgs e)
        {
            Obavestenje obavestenje = new Obavestenje("Sekretarijat", DateTime.Now, obavestenjeTextBox.Text.Trim(), "All");
            ObavestenjaStorage.Instance.Create(obavestenje);

            listaTekstova.Insert(0, obavestenje);
        }

        private void Button_Obrisi(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje zaBrisanje = null;
            foreach (Obavestenje o in listaTekstova)
            {
                if (o.ID.Equals(button.CommandParameter))
                {
                    zaBrisanje = o;
                }
            }
            listaTekstova.Remove(zaBrisanje);
            ObavestenjaStorage.Instance.Delete(zaBrisanje.ID);

        }

        private void Button_Izmeni(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Obavestenje zaIzmenu = null;
            foreach (Obavestenje o in listaTekstova)
            {
                if (o.ID.Equals(button.CommandParameter))
                {
                    zaIzmenu = o;
                }
            }
            this.NavigationService.Navigate(new IzmeniObavestenjePage(listaTekstova, zaIzmenu));
        }
    }
}
