using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.Controller;
using System.Windows.Input;

namespace SIMS.ViewSecretary.Patients
{
    public partial class ViewPatients : Page
    {
        public ObservableCollection<Patient> _patients { get; }
        private static ViewPatients _instance = null;

        private PatientController patientController = new PatientController();

        public static ViewPatients GetInstance()
        {
            if (_instance == null)
                _instance = new ViewPatients();
            return _instance;
        }
        private ViewPatients()
        {
            InitializeComponent();

            _patients = new ObservableCollection<Patient>(patientController.GetAllPatients());
            patientsView.ItemsSource = _patients;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatient());
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGuestPatient());
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientsView.SelectedItem == null)
            {
                CustomMessageBox.Show(TranslationSource.Instance["ChoosePatientToUpdateMessage"]);
            }
            else
            {
                NavigationService.Navigate(new UpdatePatient((Patient)patientsView.SelectedItem));
            }
        }

        private void UnblockPatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientsView.SelectedItem == null)
            {
                CustomMessageBox.Show(TranslationSource.Instance["ChoosePatientToUnblockMessage"]);
            }
            else
            {
                Patient toUnblock = (Patient)patientsView.SelectedItem;
                if (toUnblock.Guest == true)
                {
                    CustomMessageBox.Show(TranslationSource.Instance["PatientWithoutAccountMessage"]);
                    return;
                }
                else if (toUnblock.IsBanned == false)
                {
                    CustomMessageBox.Show(TranslationSource.Instance["PatientNotBannedMessage"]);
                    return;
                }
                toUnblock.IsBanned = false;
                patientController.UpdatePatient(toUnblock);
                CustomMessageBox.Show(TranslationSource.Instance["PatientUnblockedMessage"]);
            }
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientsView.SelectedItem == null)
            {
                CustomMessageBox.Show(TranslationSource.Instance["ChoosePatientToDeleteMessage"]);
            }
            else
            {
                Patient toDelete = (Patient)patientsView.SelectedItem;
                patientController.DeletePatient(toDelete.Jmbg);
                CustomMessageBox.Show(TranslationSource.Instance["PatientDeletedMessage"]);
                RefreshView();
            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Patient details = (Patient)patientsView.SelectedItem;
            NavigationService.Navigate(new PatientDetails(details));

        }

        public void RefreshView()
        {
            _patients.Clear();
            List<Patient> pacijentiAll = patientController.GetAllPatients();
            foreach (Patient p in pacijentiAll)
                _patients.Add(p);
            patientsView.ItemsSource = _patients;
        }

        public void RemoveInstance()
        {
            _instance = null;
        }
    }
}
