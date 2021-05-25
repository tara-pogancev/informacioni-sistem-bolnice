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
using SIMS.Controller;
using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;

namespace SIMS.UpravnikGUI
{
    /// <summary>
    /// Interaction logic for UpravnikProstorijaDetailPage.xaml
    /// </summary>
    public partial class UpravnikProstorijaDetailPage : Page

    {
        private Room prostorija;
        private UpravnikInventarProstorijePage Inventar;
        private RoomController roomController = new RoomController();

        public UpravnikProstorijaDetailPage(string broj) //izmena postojece prostorije
        {
            prostorija = RoomFileRepository.Instance.FindById(broj);
            Inventar = new UpravnikInventarProstorijePage(prostorija, this);
            InitializeComponent();

            BrojText.Text = prostorija.Number;

            TipCombo.ItemsSource = Conversion.GetTipoviProstorije();
            TipCombo.SelectedItem = prostorija.TypeToString;

            DostupnostCombo.ItemsSource = Conversion.GetDostupnostiProstorije();
            DostupnostCombo.SelectedItem = prostorija.AvailableToString;

            PocetakRenoviranjaText.Text = prostorija.RenovationStart.ToString();
            KrajRenoviranjaText.Text = prostorija.RenovationEnd.ToString();


            DostupnostCombo.IsEnabled = false;
            BrojText.IsEnabled = false;
            PocetakRenoviranjaText.IsEnabled = false;
            KrajRenoviranjaText.IsEnabled = false;


        }

        public UpravnikProstorijaDetailPage() //nova prostorija
        {
            prostorija = new Room();
            InitializeComponent();

            TipCombo.ItemsSource = Conversion.GetTipoviProstorije();

            DostupnostLabel.Visibility = Visibility.Hidden;
            PocetakRenoviranjaLabel.Visibility = Visibility.Hidden;
            KrajRenoviranjaLabel.Visibility = Visibility.Hidden;

            DostupnostCombo.Visibility = Visibility.Hidden;
            DostupnostCombo.IsEnabled = false;

            InventarButton.Visibility = Visibility.Hidden;
            InventarButton.IsEnabled = false;

            PocetakRenoviranjaText.Visibility = Visibility.Hidden;
            PocetakRenoviranjaText.IsEnabled = false;

            KrajRenoviranjaText.Visibility = Visibility.Hidden;
            KrajRenoviranjaText.IsEnabled = false;

        }

        private void InventarProstorije_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(Inventar);
            UpravnikWindow.Instance.SetLabel("Inventar prostorije " + prostorija.Number);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            prostorija.Number = BrojText.Text;
            prostorija.RoomType = Conversion.StringToTipProstorije(TipCombo.Text);

            roomController.CreateOrUpdate(prostorija);

            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }
    }
}
