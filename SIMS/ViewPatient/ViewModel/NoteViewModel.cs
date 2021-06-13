using SIMS.Commands;
using SIMS.Controller;
using SIMS.Enumerations;
using SIMS.Model;
using SIMS.PacijentGUI;
using SIMS.PacijentGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SIMS.ViewPatient.ViewModel
{
    public class NoteViewModel : ViewModelPatient
    {
        public String Title{get;set;}
        public DateTime DateReal { get; set; }
        private String content;
        private String date;
        private String time;
        public bool Checked { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        NoteController noteController;
        private Visibility vidljivost;
        String noteId;

        public String Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }
        public String Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public String Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        public Visibility Vidljivost
        {
            get { return vidljivost; }
            set
            {
                vidljivost = value;
                OnPropertyChanged();
            }
        }

        public NoteViewModel(String id)
        {
            noteId = id;
            date = "";
            time = "";
            DateReal=DateTime.Now;
            ConfirmCommand = new RelayCommand(Execute_ConfirmCommand, Can_Execute_ConfirmCommand);
            RejectCommand = new RelayCommand(ExecuteRejectCommand, canExecuteRejectCommand);
            noteController = new NoteController();
            vidljivost = Visibility.Collapsed;
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
            if (Checked==false)
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
            bool ret = true;
            if (Content == null || Title == null )
            {
                Vidljivost = Visibility.Visible;
                return false;

            }
            if ((Content != null && Title != null))
            {
                if (Content.Length == 0 || Title.Length == 0)
                {
                    Vidljivost = Visibility.Visible;
                    ret = false;
                }
                    
                
            }
            if (Checked==true && (Date==null || Time==null))
            {
                Vidljivost = Visibility.Visible;
                return false;
            }

            if (Checked == true && (DateReal==null || Time.Length == 0))
            {
                Vidljivost = Visibility.Visible;
                return false;
            }
            

            return ret;
        }

        public void ExecuteRejectCommand(Object obj)
        {
            PocetnaStranica.getInstance().frame.Content = new IstorijaPregleda();
        }

        public bool canExecuteRejectCommand(Object obj)
        {
            return true;
        }
    }
}
