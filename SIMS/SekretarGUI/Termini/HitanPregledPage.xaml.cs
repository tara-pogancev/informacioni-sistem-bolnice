using Model;
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

namespace SIMS.SekretarGUI
{
    /// <summary>
    /// Interaction logic for HitanPregledPage.xaml
    /// </summary>
    public partial class HitanPregledPage : Page
    {
        private List<String> SpecializationList;
        private List<Specijalizacija> SpecializationEnumList;
        private List<String> DurationList = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
        private List<Pacijent> PatientList;

        private ObservableCollection<Termin> AvailableAppointments;

        public HitanPregledPage()
        {
            InitializeComponent();

            DataContext = this;
            DurationComboBox.SelectedIndex = 0;

            AvailableAppointments = new ObservableCollection<Termin>();
            AvailableComboBox.DataContext = AvailableAppointments;
            SpecializationList = LekarStorage.Instance.GetAvailableSpecializationString();
            SpecializationList.Remove("Lekar opšte prakse");
            SpecializationEnumList = LekarStorage.Instance.GetAvailableSpecialization();
            SpecializationEnumList.Remove(Specijalizacija.OpstaPraksa);
            PatientList = PacijentStorage.Instance.ReadList();

            SpecializationComboBox.ItemsSource = SpecializationList;
            DurationComboBox.ItemsSource = DurationList;
            AvailableComboBox.ItemsSource = AvailableAppointments;
            PatientComboBox.ItemsSource = PatientList;
        }

        private Specijalizacija GetSelectedSpecialization()
        {
            int idx = SpecializationComboBox.SelectedIndex;
            if (idx == -1)
                return SpecializationEnumList[0];
            else return SpecializationEnumList[idx];
        }

        private int GetSelectedDuration()
        {
            int idx = DurationComboBox.SelectedIndex;

            if (idx == -1)
                return 0;
            else return (idx + 1) * 30;

        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (PatientComboBox.SelectedItem != null && DurationComboBox.SelectedItem != null && SpecializationComboBox.SelectedItem != null)
            {
                Termin selecetdApp = (Termin)AvailableComboBox.SelectedItem;
                selecetdApp.InitData();

                TerminStorage.Instance.Create(selecetdApp);

                SendNotification(selecetdApp);

                SekretarTerminiPage.GetInstance().refreshView();

                this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
                MessageBox.Show("Hitan pregled uspesno zakazan!");
            }
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }

        private void SendNotification(Termin selectedApp)
        {
            //String author = LekarUI.GetInstance().GetUser().ImePrezime;
            List<String> target = new List<String>();
            target.Add(((Pacijent)PatientComboBox.SelectedItem).Jmbg);
            //target.Add(selectedApp.Lekar.Jmbg);
            foreach (Sekretar s in SekretarStorage.Instance.ReadList())
            {
                target.Add(s.Jmbg);
            }
            Obavestenje notification = new Obavestenje("Sekretarijat", DateTime.Now,
                ("Zakazan hitan pregled [" + selectedApp.Datum + " " + selectedApp.Vrijeme + ", " + selectedApp.NazivProstorije + "] za pacijenta "
                + selectedApp.ImePacijenta + ", vodeći lekar " + selectedApp.ImeLekara + "."), target);

            ObavestenjaStorage.Instance.Create(notification);
        }

        private void DurationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void SpecializationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }
        private void PatientChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void GetAvailableAppointments()
        {
            AvailableAppointments.Clear();

            if (PatientComboBox.SelectedItem != null && DurationComboBox.SelectedItem != null && SpecializationComboBox.SelectedItem != null)
            {
                List<Termin> allAppointments = GetAvailableAppointmentsForAllDoctors();
                SortAppointments(allAppointments);

                foreach (Termin app in allAppointments)
                {
                    app.InitData();

                    AvailableAppointments.Add(app);
                    if (AvailableAppointments.Count >= 5)
                        break;
                }

            }

            AvailableComboBox.ItemsSource = AvailableAppointments;
        }

        private List<Termin> GetAvailableAppointmentsForAllDoctors()
        {
            List<Termin> retVal = new List<Termin>();

            foreach (Lekar doctor in LekarStorage.Instance.ReadBySpecialization(GetSelectedSpecialization()))
            {
                List<DateTime> potentialAppointmentTimeList = GetNearPotentialAppointments();
                int counterByDoctor = 0;

                foreach (DateTime appTime in potentialAppointmentTimeList)
                {
                    //TODO: Promeniti prostoriju!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    Termin appointment = new Termin(appTime, GetSelectedDuration(), TipTermina.pregled, doctor, (Pacijent)PatientComboBox.SelectedItem, ProstorijaStorage.Instance.ReadList()[0]);
                    if (doctor.IsFree(appointment))
                    {
                        counterByDoctor++;
                        retVal.Add(appointment);
                    }

                    if (counterByDoctor >= 5)
                        break;
                }

            }

            return retVal;
        }

        private void SortAppointments(List<Termin> appointments)
        {
            for (int i = 0; i < appointments.Count; i++)
                for (int j = 0; j < appointments.Count; j++)
                    if (appointments[i].PocetnoVreme < appointments[j].PocetnoVreme)
                    {
                        var temp = appointments[i];
                        appointments[i] = appointments[j];
                        appointments[j] = temp;
                    }
        }

        private List<DateTime> GetNearPotentialAppointments()
        {
            DateTime currentTime = DateTime.Now;

            List<String> availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<String> availableDates = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                DateTime currentDate = DateTime.Today.AddDays(i);
                availableDates.Add(currentDate.ToString("dd.MM.yyyy."));
            }

            List<DateTime> potentialAppointmentTimeList = new List<DateTime>();
            foreach (String date in availableDates)
                foreach (String time in availableTimes)
                {
                    String dateAndTime = date + " " + time;
                    DateTime appointmentTime = DateTime.Parse(dateAndTime);
                    if (appointmentTime >= currentTime)
                        potentialAppointmentTimeList.Add(appointmentTime);
                }

            return potentialAppointmentTimeList;
        }
    }
}
