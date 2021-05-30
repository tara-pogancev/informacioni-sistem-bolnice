using SIMS.Commands;
using SIMS.Controller;
using SIMS.Filters;
using SIMS.Model;
using SIMS.PacijentGUI;
using SIMS.PacijentGUI.ViewModel;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace SIMS.ViewPatient.ViewModel
{
    public class PastAppointmentsViewModel:ViewModelPatient
    {
        private NavigationService navService;
        private ObservableCollection<Appointment> pastAppointments;
        private Patient patient;
        public RelayCommand GradeAppointmentCommand { get; set; }
        public RelayCommand DetailsAppointmentCommand { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand OpenNoteCommand { get; set; }
       

        public ObservableCollection<Appointment> PastAppointments
        {
            get { return pastAppointments; }
            set { pastAppointments = value; OnPropertyChanged("pastAppointments"); }
        }

        public PastAppointmentsViewModel(NavigationService navService,Patient patient)
        {
            this.navService = navService;
            this.patient = patient;
            pastAppointments = new ObservableCollection<Appointment>(new AppointmentController().GetPastAppointmentsForPatient(patient));
            LoadDoctors();
            GradeAppointmentCommand = new RelayCommand(Execute_GradeAppointmentCommand,CanExecute_GradeAppointmentCommand);
            DetailsAppointmentCommand = new RelayCommand(Execute_DetailsAppointmentCommand, CanExecute_DetailsAppointmentCommand);
            SearchCommand = new RelayCommand(Execute_SearchCommand, CanExecute_SearchCommand);
            OpenNoteCommand = new RelayCommand(Execute_OpenNoteCommand, CanExecute_OpenNoteCommand);

        }


        private void LoadDoctors()//ucitavanje doktora iz fajla za svaki termin
        {
            IDoctorRepository lekarStorage = new DoctorFileRepository();
            for (int i = 0; i < PastAppointments.Count; i++)
            {
                PastAppointments[i].Doctor = lekarStorage.FindById(PastAppointments[i].Doctor.Jmbg);
            }
        }

        public bool CanExecute_OpenNoteCommand(object obj)
        {
            return true;
        }

        public void Execute_OpenNoteCommand(object obj)
        {
            NoteController noteController = new NoteController();
            NoteViewModel noteVM = new NoteViewModel(SelectedAppointment.AppointmentID);
            PocetnaStranica.getInstance().frame.Content = new BeleskaPage(noteVM);
        }

        public bool CanExecute_GradeAppointmentCommand(object obj)
        {
            return true;
        }

        public void Execute_GradeAppointmentCommand(object obj)
        {

            Appointment appointment = new AppointmentController().GetAppointment(SelectedAppointment.AppointmentID);
            appointment.Doctor = new DoctorController().GetDoctor(appointment.Doctor.Jmbg);
            navService.Navigate(new OcijeniPregled(appointment));
        }
        public bool CanExecute_SearchCommand(object obj)
        {
            return true;
        }

        public void Execute_SearchCommand(object obj)
        {

            
        }
        public bool CanExecute_DetailsAppointmentCommand(object obj)
        {
            return true;
        }

        public void Execute_DetailsAppointmentCommand(object obj)
        {

            Anamnesis anamneza=new AnamnesisController().GetAnamnesis(SelectedAppointment.AppointmentID);
            if (anamneza == null)
            {
                anamneza = new Anamnesis();
                anamneza.AnamnesisAppointment =new AppointmentController().GetAppointment(SelectedAppointment.AppointmentID);
            }
            navService.Navigate(new DetaljiPregleda(anamneza));
        }

    }
}
