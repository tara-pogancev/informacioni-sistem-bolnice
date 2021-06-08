using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.ViewSecretary.Patients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace SIMS.ViewSecretary.ViewModel
{
    public class AddPatientViewModel : ViewModelSecretary
    {
        #region Fields
        private PatientController patientController = new PatientController();
        private AllergenController allergenController = new AllergenController();
        public ObservableCollection<Allergen> Allergens { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Jmbg { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string AddressToSplit { get; set; }
        public SexType Sex { get; set; }
        public string Blood { get; set; }
        private string allergenText;
        public string AllergensText
        {
            get
            {
                return allergenText;
            }
            set
            {
                allergenText = value;
                OnPropertyChanged("AllergenText");
            }
        }
        public string ChronicPains { get; set; }
        public string Lbo { get; set; }

        #endregion

        #region Commands
        public RelayCommand AddPatientCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }
        #endregion

        #region Actions
        private void Execute_AddPatientCommand(object obj)
        {
            Patient patient = CreatePatientFromUserInput();
            patientController.SavePatient(patient);
            ViewPatients.GetInstance().RefreshView();

            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewPatients.GetInstance());
        }

        private bool CanExecute_AddPatientCommand(object obj)
        {
            return true;
        }

        private void Execute_QuitCommand(object obj)
        {
            SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewPatients.GetInstance());
        }

        private bool CanExecute_QuitCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Helper functions
        private Patient CreatePatientFromUserInput()
        {
            GetStreetAndNumberFromAdress(out string street, out string number);
            int.TryParse(PostalCode, out int postalCode);

            List<Allergen> allergens = new List<Allergen>();
            /*foreach (Allergen a in allergensComboBox.SelectedItems)
                allergens.Add(a);*/
            foreach (string s in AllergensText.Split(","))
            {
                foreach (Allergen a in Allergens)
                {
                    if (s.Equals(a.Name))
                    {
                        allergens.Add(a);
                    }

                }
            }
            List<string> chronicPains = new List<string>(ChronicPains.Split());

            Patient patient = new Patient(Name, LastName, Jmbg, Username, Password, Email, PhoneNumber, new Address(street, number, new City(City, postalCode, new Country(Country))), Lbo, false, allergens, DateTime.ParseExact(BirthDate, "dd.MM.yyyy.", CultureInfo.InvariantCulture), GetEnumKrvneGrupe(Blood), Sex, chronicPains);
            return patient;
        }

        private void GetStreetAndNumberFromAdress(out string street, out string number)
        {
            string[] adress = AddressToSplit.Split(" ");
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
        #endregion

        #region Constructors
        public AddPatientViewModel()
        {
            Allergens = new ObservableCollection<Allergen>(allergenController.GetAll());
            AllergensText = "";
            ChronicPains = "";

            AddPatientCommand = new RelayCommand(Execute_AddPatientCommand, CanExecute_AddPatientCommand);
            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
        }
        #endregion
    }
}
