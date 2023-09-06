using System;
using System.Collections.Generic;
using System.Text;
using Keepnote.Models;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class DatabaseFixture : IDisposable
    {
        public KeepNoteContext context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<KeepNoteContext>()
                .UseInMemoryDatabase(databaseName: "NoteDB")
                .Options;

            //Initializing DbContext with InMemory
            context = new KeepNoteContext(options);

            // Insert seed data into the database using one instance of the context
            context.Notes.Add(new Note { NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Completed" });
            context.SaveChanges();
            context.Notes.Add(new Note { NoteTitle = "Stack", NoteContent = "DOTNET", NoteStatus = "Started" });
            context.SaveChanges();
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
