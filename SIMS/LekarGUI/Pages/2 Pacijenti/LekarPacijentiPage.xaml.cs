using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI.Dialogues.Termini_CRUD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarPacijentiPage.xaml
    /// </summary>
    public partial class DoctorPatientViewPage : Page
    {
        public static DoctorPatientViewPage instance;

        private static Doctor doctorUser;

        private const String defaultSearchText = "Pretraži...";

        public ObservableCollection<Patient> PatientViewModel { get; set; }

        public static DoctorPatientViewPage GetInstance(Doctor l)
        {
            if (instance == null)
            {
                doctorUser = l;
                instance = new DoctorPatientViewPage();
            }
            return instance;
        }

        public static DoctorPatientViewPage GetInstance()
        {
            return instance;
        }

        public DoctorPatientViewPage()
        {
            InitializeComponent();

            this.DataContext = this;
            PatientViewModel = new ObservableCollection<Patient>(PatientFileRepository.Instance.GetAll());

        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void ButtonAppointment(object sender, RoutedEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                DoctorUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(patient);
            }
        }

        private void ButtonReciept(object sender, RoutedEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                DoctorWriteReciept reciept = new DoctorWriteReciept(patient);
                reciept.ShowDialog();
            }
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void SearchByIcon(object sender, MouseButtonEventArgs e)
        {
            Search();
        }

        private void SearchByEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Search();
            }
        }

        private void ClearSearchText(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
        }

        private void Search()
        {
            String searchText = searchBox.Text;

            if (searchText == "" || searchText == defaultSearchText)
            {
                resetView();
            } else
            {
                FilterView(searchText);
            }

        }

        private void SearchTextChanged(object sender, RoutedEventArgs e)
        {
            String searchText = searchBox.Text;

            if (searchText == "")
            {
                searchBox.Text = defaultSearchText;
            }
        }

        private void resetView()
        {
            PatientViewModel.Clear();

            foreach(Patient patient in PatientFileRepository.Instance.GetAll())
            {
                PatientViewModel.Add(patient);
            }
        }

        private void FilterView(String filter)
        {
            PatientViewModel.Clear();
            filter = filter.ToUpper();

            foreach (Patient patient in PatientFileRepository.Instance.GetAll())
            {
                if ((patient.Jmbg.ToUpper()).Contains(filter) || (patient.FullName.ToUpper()).Contains(filter))
                    PatientViewModel.Add(patient);
            }
        }

        private void ButtonReferral(object sender, RoutedEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient p = (Patient)dataGridPatients.SelectedItem;
                new WriteReferral(p).ShowDialog();
            }
        }
    }
}
