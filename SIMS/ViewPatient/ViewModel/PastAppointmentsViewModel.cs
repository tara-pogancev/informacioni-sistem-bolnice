using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using SIMS.PacijentGUI;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace SIMS.ViewPatient.ViewModel
{
    class PastAppointmentsViewModel
    {
        private NavigationService navService;
        public ObservableCollection<Appointment> PastAppointments { get; set; }
        private Patient patient;
        public RelayCommand GradeAppointmentCommand { get; set; }
        public RelayCommand DetailsAppointmentCommand { get; set; }
        public Appointment SelectedAppointment { get; set; }


        public PastAppointmentsViewModel(NavigationService navService,Patient patient)
        {
            this.navService = navService;
            this.patient = patient;
            PastAppointments = new ObservableCollection<Appointment>(new AppointmentController().GetPastAppointmentsForPatient(patient));
            LoadDoctors();
            GradeAppointmentCommand = new RelayCommand(Execute_GradeAppointmentCommand,CanExecute_GradeAppointmentCommand);
            DetailsAppointmentCommand = new RelayCommand(Execute_DetailsAppointmentCommand, CanExecute_DetailsAppointmentCommand);

        }


        private void LoadDoctors()//ucitavanje doktora iz fajla za svaki termin
        {
            IDoctorRepository lekarStorage = new DoctorFileRepository();
            for (int i = 0; i < PastAppointments.Count; i++)
            {
                PastAppointments[i].Doctor = lekarStorage.FindById(PastAppointments[i].Doctor.Jmbg);
            }
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
