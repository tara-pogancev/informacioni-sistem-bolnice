using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class DodajPacijentaPage : Page
    {
        private ObservableCollection<Alergen> _allergens;
        public DodajPacijentaPage()
        {
            InitializeComponent();

            _allergens = new ObservableCollection<Alergen>(AlergeniStorage.Instance.ReadList());

            allergensComboBox.ItemsSource = _allergens;
            allergensComboBox.DisplayMemberPath = "Naziv";
            allergensComboBox.SelectedMemberPath = "ID";
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = CreatePatientFromUserInput();
            PacijentStorage.Instance.Create(pacijent);
            SekretarPacijentiPage.GetInstance().RefreshView();

            NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        private Pacijent CreatePatientFromUserInput()
        {
            GetStreetAndNumberFromAdress(out string street, out string number);
            int.TryParse(postalCodeTextBox.Text, out int postalCode);

            List<string> allergens = new List<string>();
            foreach (Alergen a in allergensComboBox.SelectedItems)
                allergens.Add(a.ID);
            List<string> chronicPains = new List<string>(chronicPainsTextBox.Text.Split());

            Pacijent patient = new Pacijent(nameTextBox.Text, lastNameTextBox.Text, jmbgTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, phoneNumberTextBox.Text, new Adresa(street, number, new Grad(cityTextBox.Text, postalCode, new Drzava(countryTextBox.Text))), lboTextBox.Text, false, allergens, DateTime.ParseExact(birthDateTextBox.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture), GetEnumKrvneGrupe((string)bloodGroupComboBox.SelectionBoxItem), (Pol)sexComboBox.SelectionBoxItem, chronicPains);
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
            NavigationService.Navigate(SekretarPacijentiPage.GetInstance());
        }

        public static Krvne_Grupe GetEnumKrvneGrupe(string s)
        {
            return s switch
            {
                "O+" => Krvne_Grupe.Op,
                "O-" => Krvne_Grupe.On,
                "A+" => Krvne_Grupe.Ap,
                "A-" => Krvne_Grupe.An,
                "B+" => Krvne_Grupe.Bp,
                "B-" => Krvne_Grupe.Bn,
                "AB+" => Krvne_Grupe.ABp,
                "AB-" => Krvne_Grupe.ABn,
                _ => Krvne_Grupe.Op,
            };
        }
    }
}
