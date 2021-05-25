using SIMS.PacijentGUI.ViewModel;
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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for IzborLjekara.xaml
    /// </summary>
    public partial class IzborLjekara : Page
    {
        public IzborLjekara()
        {
            InitializeComponent();
            this.DataContext = new IzborLjekaraViewModel(PocetnaStranica.getInstance().Pacijent);
            SlikaLjekara.Source=new BitmapImage(new Uri(@"/src/muskiLjekar.png",UriKind.Relative));
        }

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListaIzborLjekaraPage());
        }
    }
}
