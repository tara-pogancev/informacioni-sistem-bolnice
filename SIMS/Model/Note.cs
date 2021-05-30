using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class Note
    {
        private String title;
        private String content;
        private Anamnesis anamnesis;
        private String noteID;
        DateTime remindDate;


        public Note() { }
        public Note(String title,String content,Anamnesis anamnesis,String NoteID)
        {
            this.title = title;
            this.content = content;
            this.anamnesis = anamnesis;
            this.noteID = NoteID;
            remindDate = DateTime.MinValue;

        }

        public Note(String title, String content, Anamnesis anamnesis, String NoteID,DateTime remindDate)
        {
            this.title = title;
            this.content = content;
            this.anamnesis = anamnesis;
            this.noteID = NoteID;
            this.remindDate = remindDate;
        }

        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public Anamnesis Anamnesis { get => anamnesis; set => anamnesis = value; }
        public string NoteID { get => noteID; set => noteID = value; }
        public DateTime RemindDate { get => remindDate; set => remindDate = value; }

        
    }
}
