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

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for KorisnickiProfil.xaml
    /// </summary>
    public partial class KorisnickiProfil : Page
    {

        private Patient pacijent;
        private String alergeni;
        public KorisnickiProfil()
        {
            InitializeComponent();
            this.DataContext = this;

            pacijent = PocetnaStranica.getInstance().Pacijent;
            alergeni = "";
            dobaviAlergene();

            LBOText.Inlines.Add(new Run("LBO:") { FontWeight = FontWeights.Bold, FontSize = 15 });
            LBOText.Inlines.Add(" ");
            LBOText.Inlines.Add((pacijent.Lbo));
            BrojKartonaText.Inlines.Add(new Run("Broj kartona:") { FontWeight = FontWeights.Bold, FontSize = 15 });
            BrojKartonaText.Inlines.Add(" ");
            BrojKartonaText.Inlines.Add("128/3");
            AlergeniText.Inlines.Add(new Run("Alergeni:") { FontWeight = FontWeights.Bold, FontSize = 15 });
            AlergeniText.Inlines.Add(" ");
            AlergeniText.Inlines.Add(alergeni);
            AlergeniText.TextWrapping = TextWrapping.Wrap;
            KrvnaGrupaText.Inlines.Add(new Run("Krvna grupa:") { FontWeight = FontWeights.Bold, FontSize = 15 });
            KrvnaGrupaText.Inlines.Add(" ");
            KrvnaGrupaText.Inlines.Add(pacijent.KrvnaGrupaString);

        }

        public Patient Pacijent { get => pacijent; set => pacijent = value; }

        private void dobaviAlergene()
        {

            foreach (String alergen in pacijent.Alergeni)
            {
                Allergen ucitaniAlergen = new AllergenRepository().Read(alergen);
                alergeni += ucitaniAlergen.Name;
                alergeni += ", ";

            }
            if (alergeni.Length == 0)
            {
                alergeni = " Nema";
                return;
            }
            alergeni = alergeni.Substring(0, alergeni.Length - 2);
        }

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            IzmjenaProfila izmjenaProfila = new IzmjenaProfila();
            NavigationService.Navigate(izmjenaProfila);
        }
    }
}
