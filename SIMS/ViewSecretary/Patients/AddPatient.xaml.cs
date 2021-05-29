using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AllergenRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using SIMS.Repositories.PatientRepo;

namespace SIMS.ViewSecretary.Patients
{
    public partial class AddPatient : Page
    {
        private ObservableCollection<Allergen> _allergens;
        public AddPatient()
        {
            InitializeComponent();

            _allergens = new ObservableCollection<Allergen>(AllergenFileRepository.Instance.GetAll());

            allergensComboBox.ItemsSource = _allergens;
            allergensComboBox.DisplayMemberPath = "Naziv";
            allergensComboBox.SelectedMemberPath = "ID";
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient pacijent = CreatePatientFromUserInput();
            PatientFileRepository.Instance.Save(pacijent);
            ViewPatients.GetInstance().RefreshView();

            NavigationService.Navigate(ViewPatients.GetInstance());
        }

        private Patient CreatePatientFromUserInput()
        {
            GetStreetAndNumberFromAdress(out string street, out string number);
            int.TryParse(postalCodeTextBox.Text, out int postalCode);

            List<Allergen> allergens = new List<Allergen>();
            foreach (Allergen a in allergensComboBox.SelectedItems)
                allergens.Add(a);
            List<string> chronicPains = new List<string>(chronicPainsTextBox.Text.Split());

            Patient patient = new Patient(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, phoneNumberTextBox.Text, new Address(street, number, new City(cityTextBox.Text, postalCode, new Country(countryTextBox.Text))), lboTextBox.Text, false, allergens, DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), GetEnumKrvneGrupe((string)bloodGroupComboBox.SelectionBoxItem), (SexType)sexComboBox.SelectionBoxItem, chronicPains);
            return patient;
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
            NavigationService.Navigate(ViewPatients.GetInstance());
        }

        public static BloodType GetEnumKrvneGrupe(string s)
        {
            return s switch
            {
                "O+" => BloodType.Op,
                "O-" => BloodType.On,
                "A+" => BloodType.Ap,
                "A-" => BloodType.An,
                "B+" => BloodType.Bp,
                "B-" => BloodType.Bn,
                "AB+" => BloodType.ABp,
                "AB-" => BloodType.ABn,
                _ => BloodType.Op,
            };
        }
    }
}
