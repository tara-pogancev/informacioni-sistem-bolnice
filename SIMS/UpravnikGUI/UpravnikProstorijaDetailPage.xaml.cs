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
    /// Interaction logic for UpravnikProstorijaDetailPage.xaml
    /// </summary>
    public partial class UpravnikProstorijaDetailPage : Page

    {
        Prostorija prostorija;
        UpravnikInventarProstorijePage Inventar;
        bool isNew;

        public UpravnikProstorijaDetailPage(string broj) //izmena postojece prostorije
        {
            isNew = false;
            prostorija = ProstorijaStorage.Instance.Read(broj);
            Inventar = new UpravnikInventarProstorijePage(prostorija, this);
            InitializeComponent();

            BrojText.Text = prostorija.Broj;

            TipCombo.ItemsSource = Conversion.getTipoviProstorije();
            TipCombo.SelectedItem = prostorija.TipProstorijeToString;

            DostupnostCombo.ItemsSource = Conversion.getDostupnostiProstorije();
            DostupnostCombo.SelectedItem = prostorija.DostupnaToString;

            BrojText.IsEnabled = false;
        }

        public UpravnikProstorijaDetailPage() //nova prostorija
        {
            isNew = true;
            prostorija = new Prostorija();
            InitializeComponent();

            TipCombo.ItemsSource = Conversion.getTipoviProstorije();
            DostupnostCombo.ItemsSource = Conversion.getDostupnostiProstorije();
        }

        private void InventarProstorije_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(Inventar);
            UpravnikWindow.Instance.SetLabel("Inventar prostorije " + prostorija.Broj);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
            UpravnikWindow.Instance.SetLabel("Prostorije");
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            prostorija.Broj = BrojText.Text;
            prostorija.Dostupna = Conversion.StringToDostupnostProstorije(DostupnostCombo.Text);
            prostorija.TipProstorije = Conversion.StringToTipProstorije(TipCombo.Text);

            if (isNew)
            {
                if (ProstorijaStorage.Instance.Create(prostorija))
                {
                    UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
                    UpravnikWindow.Instance.SetLabel("Prostorije");
                }
            }
            else
            {
                if (ProstorijaStorage.Instance.Update(prostorija))
                {
                    UpravnikWindow.Instance.SetContent(new UpravnikProstorijePage());
                    UpravnikWindow.Instance.SetLabel("Prostorije");
                }
            }
        }
    }
}
