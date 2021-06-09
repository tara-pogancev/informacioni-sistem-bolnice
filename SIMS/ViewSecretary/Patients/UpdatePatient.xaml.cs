using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.Controller;
using SIMS.ViewSecretary.ViewModel;
using System.Text.RegularExpressions;

namespace SIMS.ViewSecretary.Patients
{
    public partial class UpdatePatient : Page
    {
        private ObservableCollection<Allergen> _allergens;
        private PatientController patientController = new PatientController();
        private AllergenController allergenController = new AllergenController();
        public UpdatePatient(Patient patient)
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
            chronicPainsTextBox.Text = patient.GetHronicalDiseases();
        }

        private void FillAllergenFields(Patient patient)
        {
            _allergens = new ObservableCollection<Allergen>(allergenController.GetAll());

            allergensComboBox.ItemsSource = _allergens;
            allergensComboBox.DisplayMemberPath = "Name";
            allergensComboBox.SelectedMemberPath = "ID";

            foreach (var allergen in patient.Allergens)
            {
                foreach (Allergen a in _allergens)
                {
                    if (allergen.ID == a.ID)
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
            Patient patient = UpdatePatientFromUserInput();
            if (patient != null)
            {
                patientController.UpdatePatient(patient);
                ViewPatients.GetInstance().RefreshView();

                NavigationService.Navigate(ViewPatients.GetInstance());
            }
        }

        private Patient UpdatePatientFromUserInput()
        {
            if (IsValid())
            {
                GetStreetAndNumberFromAdress(out string street, out string number);
                int.TryParse(postalCodeTextBox.Text, out int post_broj);

                List<Allergen> allergens = new List<Allergen>();
                foreach (Allergen a in allergensComboBox.SelectedItems)
                {
                    allergens.Add(a);
                }
                List<string> chronicPains = new List<string>(chronicPainsTextBox.Text.Split());
                
                Patient pacijent = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, phoneNumberTextBox.Text, new Address(street, number, new City(cityTextBox.Text, post_broj, new Country(countryTextBox.Text))), lboTextBox.Text, (bool)guestCheckBox.IsChecked, allergens, DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), AddPatientViewModel.GetEnumKrvneGrupe((string)bloodGroupComboBox.SelectionBoxItem), (SexType)sexComboBox.SelectionBoxItem, chronicPains);
                return pacijent;
            }
            return null;
            
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

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(lastNameTextBox.Text) || String.IsNullOrEmpty(jmbgTextBox.Text) || String.IsNullOrEmpty(birthDateTextBox.Text) || String.IsNullOrEmpty(usernameTextBox.Text) || String.IsNullOrEmpty(passwordTextBox.Text) || String.IsNullOrEmpty(emailTextBox.Text) || String.IsNullOrEmpty(phoneNumberTextBox.Text) || String.IsNullOrEmpty(adressTextBox.Text) || String.IsNullOrEmpty(cityTextBox.Text) || String.IsNullOrEmpty(countryTextBox.Text) || String.IsNullOrEmpty(postalCodeTextBox.Text) || String.IsNullOrEmpty(lboTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }
            if (nameTextBox.Text.Trim().Equals("") || lastNameTextBox.Text.Trim().Equals("") || jmbgTextBox.Text.Trim().Equals("") || birthDateTextBox.Text.Trim().Equals("") || usernameTextBox.Text.Trim().Equals("") || passwordTextBox.Text.Trim().Equals("") || emailTextBox.Text.Trim().Equals("") || phoneNumberTextBox.Text.Trim().Equals("") || adressTextBox.Text.Trim().Equals("") || cityTextBox.Text.Trim().Equals("") || countryTextBox.Text.Trim().Equals("") || postalCodeTextBox.Text.Trim().Equals("") || lboTextBox.Text.Trim().Equals(""))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }

            string strRegex = @"[0-9]+$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(lboTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["LboNumberMessage"]);
                return false;
            }
            if (!re.IsMatch(postalCodeTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["PostalCodeNumberMessage"]);
                return false;
            }
            strRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(emailTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectEmailFormatMessage"]);
                return false;
            }
            strRegex = @"[0-9]{2}.[0-9]{2}.[0-9]{4}.$";
            re = new Regex(strRegex);
            if (!re.IsMatch(birthDateTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectDateFormatMessage"]);
                return false;
            }
            strRegex = @"(\+)?[0-9-\/]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(phoneNumberTextBox.Text))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectPhoneNumberFormatMessage"]);
                return false;
            }
            try
            {
                DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
            }
            catch
            {
                CustomMessageBox.Show(TranslationSource.Instance["InvalidDateMessage"]);
                return false;
            }
            return true;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewPatients.GetInstance());
        }
    }
}
