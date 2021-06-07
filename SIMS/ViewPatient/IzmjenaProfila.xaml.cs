using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
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
using SIMS.Repositories.PatientRepo;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for IzmjenaProfila.xaml
    /// </summary>
    public partial class IzmjenaProfila : Page
    {
        private Patient pacijent;
        private int _noOfErrorsOnScreen = 0;
        private String alergeni;
        public IzmjenaProfila()
        {
            InitializeComponent();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            this.DataContext = this;
            alergeni = "";
            dobaviAlergene();
            
            LBOText.Inlines.Add(new Run("LBO:") { FontWeight = FontWeights.Bold ,FontSize=15});
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
            KrvnaGrupaText.Inlines.Add(pacijent.GetBloodTypeString());
        }
        

        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        

        private void dobaviAlergene()
        {
            
            foreach(var alergen in pacijent.Allergens)
            {
                alergeni += alergen.Name;
                alergeni += ", ";

            }
            if (alergeni.Length == 0)
            {
                alergeni = "Nema";
                return;
            }
            alergeni=alergeni.Substring(0,alergeni.Length - 2);
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            pacijent.Email = EmailBox.Text;
            pacijent.Username = KorisnickoImeBox.Text;
            pacijent.Password = LozinkaBox.Text;
            pacijent.Phone = BrojTelefonaBox.Text;
            pacijent.Address.Street= AdresaBox.Text.Split(",")[0];
            pacijent.Address.Number = AdresaBox.Text.Split(",")[1];
            pacijent.Address.City.Name = AdresaBox.Text.Split(",")[2];
            new PatientController().UpdatePatient(pacijent);
            NavigationService.GoBack();

        }

        private void Save_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;

            Potvrdi.IsEnabled = _noOfErrorsOnScreen > 0 ? false : true;

        }
    }
}
