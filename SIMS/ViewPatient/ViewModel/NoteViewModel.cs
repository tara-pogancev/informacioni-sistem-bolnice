using SIMS.Commands;
using SIMS.Controller;
using SIMS.Enumerations;
using SIMS.Model;
using SIMS.PacijentGUI;
using SIMS.PacijentGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.ViewPatient.ViewModel
{
    public class NoteViewModel : ViewModelPatient
    {
        public String Title{get;set;}
        public DateTime DateReal { get; set; }
        public String Content { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public bool Checked { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        NoteController noteController;
        String noteId;

        public NoteViewModel(String id)
        {
            noteId = id;
            Date = "";
            Time = "";
            DateReal = DateTime.Now;
            ConfirmCommand = new RelayCommand(Execute_ConfirmCommand, Can_Execute_ConfirmCommand);
            noteController = new NoteController();
            
            LoadNote(id);
            
        }

        private void LoadNote(String id)
        {
            
            Note note = noteController.FindNoteById(id);
            
            if (note != null)
            {
                Title = note.Title;
                Content = note.Content;
                if (note.RemindDate !=DateTime.MinValue)
                {
                    Date = note.RemindDate.ToString("dd.MM.yyyy.");
                    Time = note.RemindDate.ToString("HH:mm");
                    Checked = true;
                }
                else
                {
                    Date = DateTime.Now.ToShortDateString();
                    Time = DateTime.Now.ToString("HH:mm");
                }
                
            }
            
        }

        public void Execute_ConfirmCommand(Object obj)
        {
            AnamnesisController anamnesisController = new AnamnesisController();
            Note note;
            if (Date.Length==0 && Time.Length==0)
            {

                note = new Note(Title, Content, anamnesisController.GetAnamnesis(noteId), noteId);
            }
            else
            {
                String DatePlusTime = DateReal.ToString("dd.MM.yyyy.") +" "+ Time;
                DateTime date = DateTime.Parse(DatePlusTime);
                note = new Note(Title, Content, anamnesisController.GetAnamnesis(noteId), noteId, date);
            }

            noteController.CreateOrUpdateNote(note);
            List<String> targets = new List<string>();
            targets.Add(HomePageViewModel.patient.Jmbg);
            Notification notification = new Notification("Podsjetnik", note.RemindDate, "Pogledajte vase beleske i podsjetnike", targets, false, NotificationType.NoteReminder);
            new NotificationController().SaveNotification(notification); 
            PocetnaStranica.getInstance().frame.GoBack();
        }

        public bool Can_Execute_ConfirmCommand(Object obj)
        {
            return true;
        }
    }
}
