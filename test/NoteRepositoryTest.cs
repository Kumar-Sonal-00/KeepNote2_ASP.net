using Keepnote.Models;
using Keepnote.Repository;
using Xunit;

namespace Test
{
    [TestCaseOrderer("Test.PriorityOrderer", "commander")]
    public class NoteRepositoryTest:IClassFixture<DatabaseFixture>
    {
        private readonly NoteRepository noteRepository;

        public NoteRepositoryTest(DatabaseFixture fixture)
        {
            noteRepository = new NoteRepository(fixture.context);
        }

        [Fact, TestPriority(0)]
        public void GetNotesShouldReturnList()
        {
            var notes = noteRepository.GetAllNotes();
            Assert.Equal(2, notes.Count);
        }
        [Fact, TestPriority(1)]
        public void AddNoteShouldSuccess()
        {
            Note note = new Note { NoteTitle= "Testing", NoteContent="Unit Testing", NoteStatus= "Started" };
            int result= noteRepository.AddNote(note);
            Assert.NotEqual(0, result);
            Assert.True(noteRepository.Exists(note.NoteId));
            Assert.Equal(3, note.NoteId);
        }

        [Fact, TestPriority(2)]
        public void DeleteNoteShouldSuccess()
        {
            int id = 2;
            int result= noteRepository.DeletNote(id);
            Assert.NotEqual(0, result);
            Assert.False(noteRepository.Exists(id));
        }
       
        [Fact, TestPriority(3)]
        public void GetNoteByIdShouldReturnANote()
        {
            int id = 1;
            var result = noteRepository.GetNoteById(id);
            Assert.IsAssignableFrom<Note>(result);
            Assert.Equal("Technology", result.NoteTitle);
        }

        [Fact, TestPriority(4)]
        public void UpdateNoteShouldSuccess()
        {
            var note= noteRepository.GetNoteById(1);
            note.NoteTitle = "Tech-Stack";
            note.NoteContent = "DotNet";

            var result = noteRepository.UpdateNote(note);
            Assert.NotEqual(0, result);

            var updatedNote= noteRepository.GetNoteById(note.NoteId);
            Assert.Equal("Tech-Stack", updatedNote.NoteTitle);
            Assert.Equal("DotNet", updatedNote.NoteContent);
        }
    }
}
