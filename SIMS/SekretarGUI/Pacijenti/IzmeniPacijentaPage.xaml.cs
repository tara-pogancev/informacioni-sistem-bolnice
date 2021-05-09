using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class IzmeniPacijentaPage : Page
    {
        private ObservableCollection<Alergen> _allergens;
        public IzmeniPacijentaPage(Pacijent patient)
        {
            InitializeComponent();

            FillFieldsWithOldPatientValues(patient);
        }

        private void FillFieldsWithOldPatientValues(Pacijent patient)
        {
            FillTextBoxAndComboBoxFields(patient);
            FillAdressFields(patient);
            FillAllergenFields(patient);
        }

        private void FillTextBoxAndComboBoxFields(Pacijent patient)
        {
            nameTextBox.Text = patient.Ime;
            lastNameTextBox.Text = patient.Prezime;
            jmbgTextBox.Text = patient.Jmbg;
            usernameTextBox.Text = patient.KorisnickoIme;
            passwordTextBox.Text = patient.Lozinka;
            emailTextBox.Text = patient.Email;
            phoneNumberTextBox.Text = patient.Telefon;

            lboTextBox.Text = patient.Lbo;
            guestCheckBox.IsChecked = patient.Gost;

            sexComboBox.SelectedIndex = (int)patient.Pol_Pacijenta;
            bloodGroupComboBox.SelectedIndex = (int)patient.Krvna_Grupa;
            birthDateTextBox.Text = patient.Datum_Rodjenja.ToString("dd.MM.yyyy.");
            chronicPainsTextBox.Text = patient.GetHronicneBolestiString;
        }

        private void FillAllergenFields(Pacijent patient)
        {
            _allergens = new ObservableCollection<Alergen>(AlergeniStorage.Instance.ReadList());

            allergensComboBox.ItemsSource = _allergens;
            allergensComboBox.DisplayMemberPath = "Naziv";
            allergensComboBox.SelectedMemberPath = "ID";

            foreach (string id in patient.Alergeni)
            {
                foreach (Alergen a in _allergens)
                {
                    if (id.Equals(a.ID))
                    {
                        allergensComboBox.SelectedItems.Add(a);
                        break;
                    }
                }
            }
        }

        private void FillAdressFields(Pacijent patient)
        {
            if (patient.Adresa == null)
            {
                adressTextBox.Text = "";
                cityTextBox.Text = "";
                postalCodeTextBox.Text = "";
                countryTextBox.Text = "";
            }
            else
            {
                adressTextBox.Text = patient.Adresa.ToString();
                cityTextBox.Text = patient.Adresa.Grad.Naziv;
                postalCodeTextBox.Text = patient.Adresa.Grad.PostanskiBroj.ToString();
                countryTextBox.Text = patient.Adresa.Grad.Drzava.Naziv;
            }
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = UpdatePatientFromUserInput();
            PacijentStorage.Instance.Update(pacijent);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private Pacijent UpdatePatientFromUserInput()
        {
            GetStreetAndNumberFromAdress(out string street, out string number);
            int.TryParse(postalCodeTextBox.Text, out int post_broj);

            List<string> allergens = new List<string>();
            foreach (Alergen a in allergensComboBox.SelectedItems)
            {
                allergens.Add(a.ID);
            }
            List<string> chronicPains = new List<string>(chronicPainsTextBox.Text.Split());

            Pacijent pacijent = new Pacijent(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, phoneNumberTextBox.Text, new Adresa(street, number, new Grad(cityTextBox.Text, post_broj, new Drzava(countryTextBox.Text))), lboTextBox.Text, (bool)guestCheckBox.IsChecked, allergens, DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), DodajPacijentaPage.GetEnumKrvneGrupe((string)bloodGroupComboBox.SelectionBoxItem), (Pol)sexComboBox.SelectionBoxItem, chronicPains);
            return pacijent;
        }

        private void GetStreetAndNumberFromAdress(out string street, out string number)
        {
            string[] adress = adressTextBox.Text.Split(" ");
            street = "";
            number = "";
            for (int i = 0; i < adress.Length; ++i)
            {
                if (i != adress.Length - 1)
                    street += adress[i] + " ";
                else
                    number = adress[i];
            }
            street = street.Trim();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }
    }
}
