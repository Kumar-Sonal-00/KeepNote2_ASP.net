using System;
using System.Collections.Generic;
using System.Linq;
using Keepnote.Models;
using Microsoft.EntityFrameworkCore;

namespace Keepnote.Repository
{
    public class NoteRepository : INoteRepository
    {
        // Save the note in the database(note) table.
        private readonly KeepNoteContext _context;

        public NoteRepository(KeepNoteContext context)
        {
            _context = context;
        }
        public int AddNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            _context.Notes.Add(note);
            _context.SaveChanges();
            return note.NoteId; // Return the ID of the newly added note.
        }
        //Remove the note from the database(note) table.
        public int DeletNote(int noteId)
        {
            var existingNote = _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
            if (existingNote != null)
            {
                _context.Notes.Remove(existingNote);
                _context.SaveChanges();
                return existingNote.NoteId;
            }
            return -1; // Note with the specified ID was not found.
        }

        //can be used as helper method for controller
        public bool Exists(int noteId)
        {
            return _context.Notes.Any(n => n.NoteId == noteId);
        }

        /* retrieve all existing notes sorted by created Date in descending
         order(showing latest note first)*/
        public List<Note> GetAllNotes()
        {
            return _context.Notes.OrderByDescending(n => n.CreatedAt).ToList();
        }

        //retrieve specific note from the database(note) table
        public Note GetNoteById(int noteId)
        {
            return _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
        }
        //Update existing note
        public int UpdateNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
            return note.NoteId; // Return the ID of the updated note.
        }

    }
}
