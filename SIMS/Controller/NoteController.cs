using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class NoteController
    {
        private NoteService noteService;

        public NoteController()
        {
            noteService = new NoteService();
        }

        public List<Note> GetAllNotes() => noteService.GetAllNotes();

        public void UpdateNote(Note note) => noteService.UpdateNote(note);

        public void DeleteNote(String key) => noteService.DeleteNote(key);

        public void SaveNote(Note note) => noteService.SaveNote(note);

        public void CreateOrUpdateNote(Note note) => noteService.CreateOrUpdateNote(note);

        public Note FindNoteById(string key) => noteService.FindNoteById(key);
    }
}
