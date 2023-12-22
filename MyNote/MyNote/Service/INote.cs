using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNote.Models;

namespace MyNote.Service
{
    public interface INote
    {
        Task<List<Note>> GetByUserId(int userid);
        Task<List<Note>> GetByNtbId(int ntbid);
        Task<List<Note>> GetFavpr(int isfavor, int userid);
        Task<bool> AddNote(Note note);
        Task<bool> UpdNote(int noteid, Note note);

        Task<bool> FavorChange(int noteid, Note note);
        Task<bool> DeleteNote(int noteid);
    }
}
