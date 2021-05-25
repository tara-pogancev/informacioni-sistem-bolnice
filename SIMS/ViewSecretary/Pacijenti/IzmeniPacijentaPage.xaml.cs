using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.SekretarGUI
{
    public partial class IzmeniPacijentaPage : Page
    {
        private ObservableCollection<Allergen> _allergens;
        public IzmeniPacijentaPage(Patient patient)
        {
            InitializeComponent();

            FillFieldsWithOldPatientValues(patient);
        }

        private void FillFieldsWithOldPatientValues(Patient patient)
        {
            FillTextBoxAndComboBoxFields(patient);
            FillAdressFields(patient);
            FillAllergenFields(patient);
        }

        private void FillTextBoxAndComboBoxFields(Patient patient)
        {
            nameTextBox.Text = patient.Name;
            lastNameTextBox.Text = patient.LastName;
            jmbgTextBox.Text = patient.Jmbg;
            usernameTextBox.Text = patient.Username;
            passwordTextBox.Text = patient.Password;
            emailTextBox.Text = patient.Email;
            phoneNumberTextBox.Text = patient.Phone;

            lboTextBox.Text = patient.Lbo;
            guestCheckBox.IsChecked = patient.Guest;

            sexComboBox.SelectedIndex = (int)patient.PatientGender;
            bloodGroupComboBox.SelectedIndex = (int)patient.BloodType;
            birthDateTextBox.Text = patient.DateOfBirth.ToString("dd.MM.yyyy.");
            chronicPainsTextBox.Text = patient.GetHronicalDiseases;
        }

        private void FillAllergenFields(Patient patient)
        {
            _allergens = new ObservableCollection<Allergen>(AllergenFileRepository.Instance.GetAll());

            allergensComboBox.ItemsSource = _allergens;
            allergensComboBox.DisplayMemberPath = "Naziv";
            allergensComboBox.SelectedMemberPath = "ID";

            foreach (string id in patient.Allergens)
            {
                foreach (Allergen a in _allergens)
                {
                    if (id.Equals(a.ID))
                    {
                        allergensComboBox.SelectedItems.Add(a);
                        break;
                    }
                }
            }
        }

        private void FillAdressFields(Patient patient)
        {
            if (patient.Address == null)
            {
                adressTextBox.Text = "";
                cityTextBox.Text = "";
                postalCodeTextBox.Text = "";
                countryTextBox.Text = "";
            }
            else
            {
                adressTextBox.Text = patient.Address.ToString();
                cityTextBox.Text = patient.Address.City.Name;
                postalCodeTextBox.Text = patient.Address.City.PostalCode.ToString();
                countryTextBox.Text = patient.Address.City.Country.Name;
            }
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient pacijent = UpdatePatientFromUserInput();
            PatientFileRepository.Instance.Update(pacijent);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private Patient UpdatePatientFromUserInput()
        {
            GetStreetAndNumberFromAdress(out string street, out string number);
            int.TryParse(postalCodeTextBox.Text, out int post_broj);

            List<string> allergens = new List<string>();
            foreach (Allergen a in allergensComboBox.SelectedItems)
            {
                allergens.Add(a.ID);
            }
            List<string> chronicPains = new List<string>(chronicPainsTextBox.Text.Split());

            Patient pacijent = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, phoneNumberTextBox.Text, new Address(street, number, new City(cityTextBox.Text, post_broj, new Country(countryTextBox.Text))), lboTextBox.Text, (bool)guestCheckBox.IsChecked, allergens, DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), DodajPacijentaPage.GetEnumKrvneGrupe((string)bloodGroupComboBox.SelectionBoxItem), (SexType)sexComboBox.SelectionBoxItem, chronicPains);
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
