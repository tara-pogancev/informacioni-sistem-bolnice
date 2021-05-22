using SIMS.Repositories.PatientRepo;
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
    public partial class LekarPacijentiPage : Page
    {
        public static LekarPacijentiPage instance;

        private static Doctor lekarUser;

        private const String defaultSearchText = "Pretraži...";

        public ObservableCollection<Patient> PacijentiView { get; set; }

        public static LekarPacijentiPage GetInstance(Doctor l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarPacijentiPage();
            }
            return instance;
        }

        public static LekarPacijentiPage GetInstance()
        {
            return instance;
        }

        public LekarPacijentiPage()
        {
            InitializeComponent();

            this.DataContext = this;
            PacijentiView = new ObservableCollection<Patient>(PatientFileRepository.Instance.GetAll());

        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Pregled(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenti.SelectedItem != null)
            {
                Patient p = (Patient)dataGridPacijenti.SelectedItem;
                LekarUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(p);
            }
        }

        private void Button_Recept(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenti.SelectedItem != null)
            {
                Patient p = (Patient)dataGridPacijenti.SelectedItem;
                LekarIzdavanjeRecepta r = new LekarIzdavanjeRecepta(p);
                r.ShowDialog();
            }
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void Button_Terapija(object sender, RoutedEventArgs e)
        {
            //TODO
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
                filterView(searchText);
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
            PacijentiView.Clear();

            foreach(Patient patient in PatientFileRepository.Instance.GetAll())
            {
                PacijentiView.Add(patient);
            }
        }

        private void filterView(String filter)
        {
            PacijentiView.Clear();
            filter = filter.ToUpper();

            foreach (Patient patient in PatientFileRepository.Instance.GetAll())
            {
                if ((patient.Jmbg.ToUpper()).Contains(filter) || (patient.ImePrezime.ToUpper()).Contains(filter))
                    PacijentiView.Add(patient);
            }
        }

        private void Button_Uput(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenti.SelectedItem != null)
            {
                Patient p = (Patient)dataGridPacijenti.SelectedItem;
                new UputCreate(p).ShowDialog();
            }
        }
    }
}
