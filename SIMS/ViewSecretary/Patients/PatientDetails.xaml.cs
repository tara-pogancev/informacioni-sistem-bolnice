using SIMS.Model;
using System.Windows;
using System.Windows.Controls;
using SIMS.Controller;
using System.Collections.ObjectModel;

namespace SIMS.ViewSecretary.Patients
{
    public partial class PatientDetails : Page
    {
        private ObservableCollection<Allergen> _allergens;
        private AllergenController allergenController = new AllergenController();
        public PatientDetails(Patient patient)
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

            sexTextBox.Text = patient.GetGenderString();
            bloodGroupTextBox.Text = patient.GetBloodTypeString();
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

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewPatients.GetInstance());
        }
    }
}
