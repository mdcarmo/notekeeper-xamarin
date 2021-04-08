using NoteKeepper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteKeepper.Services
{
    public interface INoteDataStore
    {
        Task<string> AddNoteAsync(Note courseNote);
        Task<bool> UpdateNoteAsync(Note courseNote);
        Task<Note> GetNoteAsync(string id);
        Task<IList<Note>> GetNotesAsync();
        Task<IList<string>> GetCoursesAsync();
    }
}
