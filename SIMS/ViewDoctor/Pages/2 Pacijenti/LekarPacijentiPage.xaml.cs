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
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarPacijentiPage.xaml
    /// </summary>
    public partial class DoctorPatientViewPage : Page
    {
        public static DoctorPatientViewPage instance;


        private const String defaultSearchText = "Pretraži...";

        public ObservableCollection<PatientDTO> PatientViewModel { get; set; }

        private PatientController patientController = new PatientController();


        public static DoctorPatientViewPage GetInstance()
        {
            if (instance == null)
            {
                instance = new DoctorPatientViewPage();
            }
            return instance;
        }

        public DoctorPatientViewPage()
        {
            InitializeComponent();

            this.DataContext = this;
            PatientViewModel = new ObservableCollection<PatientDTO>(AllPatientsDTO());

        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private Patient GetSelectedPatient()
        {
            PatientDTO dto = (PatientDTO)dataGridPatients.SelectedItem;
            return patientController.GetPatient(dto.Jmbg);
        }

        private void ButtonAppointment(object sender, RoutedEventArgs e)
        {
            DoShowPatientPage();
        }

        private void DoShowPatientPage()
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient patient = GetSelectedPatient();
                DoctorUI.GetInstance().SellectedTab.Content = new PatientRecordCheck(patient);
            }
        }

        private void ButtonReciept(object sender, RoutedEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient patient = GetSelectedPatient();
                new DoctorWriteReceipt(patient).ShowDialog();                
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
                ResetView();
            else
                FilterView(searchText);

        }

        private void SearchTextChanged(object sender, RoutedEventArgs e)
        {
            String searchText = searchBox.Text;

            if (searchText == "")
                searchBox.Text = defaultSearchText;
        }

        private void ResetView()
        {
            PatientViewModel.Clear();
            foreach (PatientDTO dto in AllPatientsDTO())
                PatientViewModel.Add(dto);
        }

        private void FilterView(String filter)
        {
            PatientViewModel.Clear();
            filter = filter.ToUpper();

            foreach (PatientDTO dto in AllPatientsDTO())
            {
                if ((dto.Jmbg.ToUpper()).Contains(filter) || (dto.FullName.ToUpper()).Contains(filter))
                    PatientViewModel.Add(dto);
            }
        }

        private List<PatientDTO> AllPatientsDTO()
        {
            return patientController.GetDTOFromList(patientController.GetAllPatients());
        }

        private void ButtonReferral(object sender, RoutedEventArgs e)
        {
            if (dataGridPatients.SelectedItem != null)
            {
                Patient p = GetSelectedPatient();
                new WriteReferral(p).ShowDialog();
            }
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                DoShowPatientPage();
        }
    }
}
