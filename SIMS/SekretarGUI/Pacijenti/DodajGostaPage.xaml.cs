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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for DodajGostaPage.xaml
    /// </summary>
    public partial class DodajGostaPage : Page
    {
        public DodajGostaPage()
        {
            InitializeComponent();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = new Pacijent(ime.Text, prezime.Text, jmbg.Text);
            PacijentStorage.Instance.Create(pacijent);
            SekretarPacijentiPage.GetInstance().refresh();

            this.NavigationService.GoBack();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
