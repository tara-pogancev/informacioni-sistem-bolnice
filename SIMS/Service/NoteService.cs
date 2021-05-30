using SIMS.Model;
using SIMS.Repositories.NoteRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class NoteService
    {
        private INoteRepository noteRepository;

        public NoteService()
        {
            noteRepository = new NoteFileRepository();
        }

        public List<Note> GetAllNotes() => noteRepository.GetAll();

        public void UpdateNote(Note note)=>noteRepository.Update(note);

        public void DeleteNote(String key) => noteRepository.Delete(key);

        public void SaveNote(Note note)=>noteRepository.Save(note);

        public void CreateOrUpdateNote(Note note)=>noteRepository.CreateOrUpdate(note);

        public Note FindNoteById(string key)=>noteRepository.FindById(key);
    }
}
