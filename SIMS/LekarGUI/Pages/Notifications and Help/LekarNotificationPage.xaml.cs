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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica u glavnom prozoru koja se odnosi na nalog lekara, uz mogucnosti podesavanja profila.
    /// </summary>
    /// 

    public partial class LekarNotificationPage : Page
    {
        private Lekar lekarUser;

        private ObservableCollection<Obavestenje> obavestenjeView;

        public LekarNotificationPage()
        {
            InitializeComponent();

            lekarUser = LekarUI.GetInstance().GetUser();

            List<Obavestenje> listaObavestenja = ObavestenjaStorage.Instance.ReadByUser(lekarUser.Jmbg);
            listaObavestenja.Reverse();
            obavestenjeView = new ObservableCollection<Obavestenje>(listaObavestenja);

            viewerObavestenja.ItemsSource = listaObavestenja;

        }

        private void Button_Odobravanje(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void Button_Lekovi(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
