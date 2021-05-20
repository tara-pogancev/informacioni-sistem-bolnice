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
            terminiView = new ObservableCollection<Appointment>(AppointmentRepository.Instance.ReadEntities());
            refreshView();
        }

        private void refreshView()
        {
            terminiView.Clear();
            List<Appointment> temp = new List<Appointment>(AppointmentRepository.Instance.ReadByDoctor(lekarUser));
            
            foreach (Appointment t in temp)
            {
                if (!t.IsPast && !t.Evidentiran)
                   terminiView.Add(t);

                t.Pacijent = new PatientRepository().ReadEntity(t.Pacijent.Jmbg);
                t.Prostorija = new RoomRepository().ReadEntity(t.Prostorija.Number);
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
                    AppointmentRepository.Instance.DeleteEntity(toDelete.TerminKey);
                    refreshView();
                    MessageBox.Show("Termin je uspešno otkazan!");
                }

            }
        }

        public void dodajTermin(Appointment termin)
        {
            AppointmentRepository.Instance.CreateEntity(termin);
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
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void Event_Karton(object sender, MouseButtonEventArgs e)
        {
            if (dataGridTermini.SelectedItem != null)
            {
                Appointment t = (Appointment)dataGridTermini.SelectedItem;
                Patient p = PatientRepository.Instance.ReadEntity(t.Pacijent.Jmbg);

                LekarUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(p);
            }
        }
    }
}
