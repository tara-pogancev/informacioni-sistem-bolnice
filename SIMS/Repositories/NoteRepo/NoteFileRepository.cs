using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.NoteRepo
{
    class NoteFileRepository : GenericFileRepository<string, Note, NoteFileRepository>,INoteRepository
    {
        protected override string getKey(Note entity)
        {
            return entity.NoteID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\beleske.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        protected override void ShouldSerialize(Note entity)
        {
            entity.Anamnesis.Serialize = false;
        }
    }
}
