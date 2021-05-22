using SIMS.Repositories.SecretaryRepo;
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
using SIMS.LekarGUI.Dialogues.Izvestaji;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.AnamnesisRepository;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for LekarIstorijaPage.xaml
    /// </summary>
    public partial class LekarIstorijaPage : Page
    {
        private static Doctor lekarUser;

        private ObservableCollection<Appointment> evidentiraniView;
        public ObservableCollection<Appointment> EvidentiraniView { get => evidentiraniView; set => evidentiraniView = value; }

        private ObservableCollection<Appointment> prazniView;
        public ObservableCollection<Appointment> PrazniView { get => prazniView; set => prazniView = value; }

        public LekarIstorijaPage(Doctor l)
        {
            InitializeComponent();

            lekarUser = l;

            //Evidentirani - pregledi sa anamnezom
            //Prazni - pregledi bez anamneze

            this.DataContext = this;
            evidentiraniView = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            prazniView = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            dobaviPodatkeOPacijenuILekaru();
            refreshView();
        }

        public void refreshView()
        {
            prazniView.Clear();
            evidentiraniView.Clear();

            List<Appointment> temp = new List<Appointment>(AppointmentFileRepository.Instance.GetDoctorAppointments(lekarUser));
            popuniInformacijeODoktoruIPacijentu(temp);
            foreach (Appointment t in temp)
            {
                t.Pacijent = new PatientFileRepository().FindById(t.Pacijent.Jmbg);

                if (t.Evidentiran == true)
                    evidentiraniView.Add(t);

                else if (t.IsPast == true)
                {
                    prazniView.Add(t);
                }

            }

        }

        private void popuniInformacijeODoktoruIPacijentu(List<Appointment> temp)
        {
            foreach (Appointment termin in temp)
            {
                termin.Pacijent = new PatientFileRepository().FindById(termin.Pacijent.Jmbg);
                termin.Lekar = new DoctorFileRepository().FindById(termin.Lekar.Jmbg);
            }
        }

        private void Button_Anamneza(object sender, RoutedEventArgs e)
        {
            if (dataGridEvidentirani.SelectedItem != null)
            {
                Appointment t = (Appointment)dataGridEvidentirani.SelectedItem;

                if (t.VrstaTermina == AppointmentType.pregled)
                {
                    Anamnesis anamneza = AnamnesisFileRepository.Instance.FindById(t.TerminKey);
                    if (anamneza != null)
                    {
                        AnamnezaView a = new AnamnezaView(anamneza);
                        a.Show();
                    }
                        
                }
                else
                {
                    SurgeryReport report = SurgeryReportFileRepository.Instance.FindById(t.TerminKey);
                    if (report != null)
                    {
                        OperacijaIzvestajView a = new OperacijaIzvestajView(report);
                        a.Show();
                    }
                }
            }
        }

        private void Button_Evidencija(object sender, RoutedEventArgs e)
        {
            if (dataGridPrazni.SelectedItem != null)
            {
                Appointment t = (Appointment)dataGridPrazni.SelectedItem;

                if (t.VrstaTermina == AppointmentType.pregled)
                {
                    AnamnezaCreate a = new AnamnezaCreate((Appointment)dataGridPrazni.SelectedItem);
                    a.ShowDialog();
                    refreshView();
                }
                else
                {
                    OperacijaIzvestajCreate o = new OperacijaIzvestajCreate((Appointment)dataGridPrazni.SelectedItem);
                    o.ShowDialog();
                    refreshView();
                }
            }
        }

        private void Button_Home(object sender, MouseButtonEventArgs e)
        {
            LekarUI.GetInstance().ChangeTab(0);
        }

        private void dobaviPodatkeOPacijenuILekaru()
        {
            foreach (Appointment termin in EvidentiraniView)
            {
                termin.Pacijent = new PatientFileRepository().FindById(termin.Pacijent.Jmbg);
                termin.Lekar = new DoctorFileRepository().FindById(termin.Lekar.Jmbg);
            }
            
            foreach (Appointment termin in prazniView)
            {
               termin.Pacijent = new PatientFileRepository().FindById(termin.Pacijent.Jmbg);
               termin.Lekar = new DoctorFileRepository().FindById(termin.Lekar.Jmbg);
            }
            
        }

    }
}
