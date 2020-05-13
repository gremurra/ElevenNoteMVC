using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;      //creating a private field of type Guid

        public NoteService(Guid userId)     //this is a constructor
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)        //this creates the instance of Note
        {
            var entity =
                new Note()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()     //allows us to see all the notes that belong to a specific user
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new NoteListItem
                        {
                            NoteId = e.NoteId,
                            Title = e.Title,
                            CreatedUtc = e.CreatedUtc
                        }
                        );

                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == id && e.OwnerId == _userId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateNote(NoteEdit model)      //returns a bool based on if the NoteId is in the database and the note belongs to the specific _userId
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
