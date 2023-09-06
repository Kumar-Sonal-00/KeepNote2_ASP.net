using System.Collections.Generic;
using Keepnote.Models;

namespace Keepnote.Repository
{
    public interface INoteRepository
    {
        int AddNote(Note note);
        int DeletNote(int noteId);
        int UpdateNote(Note note);
        bool Exists(int noteId);
        List<Note> GetAllNotes();
        Note GetNoteById(int noteId);
    }
}