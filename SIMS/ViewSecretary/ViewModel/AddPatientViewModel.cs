using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.ViewSecretary.Patients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SIMS.ViewSecretary.ViewModel
{
    public class AddPatientViewModel : ViewModelSecretary
    {
        #region Fields
        private PatientController patientController = new PatientController();
        private AllergenController allergenController = new AllergenController();
        public ObservableCollection<Component> Allergens { get; set; }
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
            if (IsValid())
            {
                Patient patient = CreatePatientFromUserInput();
                patientController.SavePatient(patient);
                ViewPatients.GetInstance().RefreshView();

                SecretaryUI.GetInstance().MainFrame.NavigationService.Navigate(ViewPatients.GetInstance());
            }
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

            List<Component> allergens = new List<Component>();
            /*foreach (Allergen a in allergensComboBox.SelectedItems)
                allergens.Add(a);*/
            foreach (string s in AllergensText.Split(","))
            {
                foreach (Component a in Allergens)
                {
                    if (s.Equals(a.Name))
                    {
                        allergens.Add(a);
                    }
                }
            }
            List<string> chronicPains = new List<string>(ChronicPains.Split());

            Patient patient = new Patient(Name, LastName, Jmbg, Username, Password, Email, PhoneNumber,
                new Address(street, number, new City(City, postalCode, new Country(Country))), Lbo, false,
                allergens, DateTime.ParseExact(BirthDate, "dd.MM.yyyy.", CultureInfo.InvariantCulture),
                GetEnumKrvneGrupe(Blood), Sex, chronicPains);
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

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(Jmbg) || String.IsNullOrEmpty(BirthDate) || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(PhoneNumber) || String.IsNullOrEmpty(AddressToSplit) || String.IsNullOrEmpty(City) || String.IsNullOrEmpty(Country) || String.IsNullOrEmpty(PostalCode) || String.IsNullOrEmpty(Blood) || String.IsNullOrEmpty(Lbo))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }
            if (Name.Trim().Equals("") || LastName.Trim().Equals("") || Jmbg.Trim().Equals("") || BirthDate.Trim().Equals("") || Username.Trim().Equals("") || Password.Trim().Equals("") || Email.Trim().Equals("") || PhoneNumber.Trim().Equals("") || AddressToSplit.Trim().Equals("") || City.Trim().Equals("") || Country.Trim().Equals("") || PostalCode.Trim().Equals("") || Blood.Trim().Equals("") || Lbo.Trim().Equals(""))
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return false;
            }

            string strRegex = @"[0-9]{13}$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(Jmbg))
            {
                CustomMessageBox.Show(TranslationSource.Instance["JmbgNumberMessage"]);
                return false;
            }
            strRegex = @"[0-9]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(Lbo))
            {
                CustomMessageBox.Show(TranslationSource.Instance["LboNumberMessage"]);
                return false;
            }
            if (!re.IsMatch(PostalCode))
            {
                CustomMessageBox.Show(TranslationSource.Instance["PostalCodeNumberMessage"]);
                return false;
            }
            strRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(Email))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectEmailFormatMessage"]);
                return false;
            }
            strRegex = @"[0-9]{2}.[0-9]{2}.[0-9]{4}.$";
            re = new Regex(strRegex);
            if (!re.IsMatch(BirthDate))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectDateFormatMessage"]);
                return false;
            }
            strRegex = @"(\+)?[0-9-\/]+$";
            re = new Regex(strRegex);
            if (!re.IsMatch(PhoneNumber))
            {
                CustomMessageBox.Show(TranslationSource.Instance["IncorrectPhoneNumberFormatMessage"]);
                return false;
            }
            try
            {
                DateTime.ParseExact(BirthDate, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
            }
            catch
            {
                CustomMessageBox.Show(TranslationSource.Instance["InvalidDateMessage"]);
                return false;
            }
            return true;
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
            Allergens = new ObservableCollection<Component>(allergenController.GetAll());
            AllergensText = "";
            ChronicPains = "";

            AddPatientCommand = new RelayCommand(Execute_AddPatientCommand, CanExecute_AddPatientCommand);
            QuitCommand = new RelayCommand(Execute_QuitCommand, CanExecute_QuitCommand);
        }
        #endregion
    }
}
