using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
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
    /// Stranica Lekara u okviru glavnog prozora koja daje u uvid sve aktivne termine uz mogucnost editovanja istih
    /// </summary>
    public partial class LekarTerminiPage : Page
    {
        public static LekarTerminiPage instance;

        private static Doctor lekarUser;

        private ObservableCollection<Appointment> terminiView;
        public ObservableCollection<Appointment> TerminiView { get => terminiView; set => terminiView = value; }

        public static LekarTerminiPage GetInstance(Doctor l)
        {
            if (instance == null)
            {
                lekarUser = l;
                instance = new LekarTerminiPage();
            }
            return instance;
        }

        public static LekarTerminiPage GetInstance()
        {
            return instance;
        }

        public LekarTerminiPage()
        {
            InitializeComponent();

            //Tabela pregleda

            this.DataContext = this;
            terminiView = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            refreshView();
        }

        private void refreshView()
        {
            terminiView.Clear();
            List<Appointment> temp = new List<Appointment>(AppointmentFileRepository.Instance.GetDoctorAppointments(lekarUser));
            
            foreach (Appointment t in temp)
            {
                if (!t.IsPast && !t.IsRecorded)
                   terminiView.Add(t);

                t.Patient = new PatientFileRepository().FindById(t.Patient.Jmbg);
                t.Room = new RoomFileRepository().FindById(t.Room.Number);
            }
        }

        private void Button_Pregled(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            TerminCreate terminCreate = new TerminCreate();
            terminCreate.ShowDialog();
        }

        private void Button_Operacija(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi operaciju
            OperacijaCreate operacijaCreate = new OperacijaCreate();
            operacijaCreate.ShowDialog();
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
            if (dataGridTermini.SelectedItem != null)
            {
                TerminUpdate terminUpdate = new TerminUpdate((Appointment)dataGridTermini.SelectedItem);
                terminUpdate.ShowDialog();
            }

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (dataGridTermini.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Appointment toDelete = (Appointment)dataGridTermini.SelectedItem;
                    AppointmentFileRepository.Instance.Delete(toDelete.AppointmentID);
                    refreshView();
                    MessageBox.Show("Termin je uspešno otkazan!");
                }

            }
        }

        public void dodajTermin(Appointment termin)
        {
            AppointmentFileRepository.Instance.Save(termin);
            refreshView();
            MessageBox.Show("Termin uspešno zakazan.");
        }

        public void refresh()
        {
            refreshView();
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void Event_Karton(object sender, MouseButtonEventArgs e)
        {
            if (dataGridTermini.SelectedItem != null)
            {
                Appointment t = (Appointment)dataGridTermini.SelectedItem;
                Patient p = PatientFileRepository.Instance.FindById(t.Patient.Jmbg);

                DoctorUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(p);
            }
        }
    }
}
