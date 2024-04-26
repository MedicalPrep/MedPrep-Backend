using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPrep.Api.Repositories
{
    public interface PlaylistRepository : IPlaylistRepository
    {
        private readonly MedPrepContext _context

        public PlaylistRepository(MedPrepcontext context){
            _context = context;
        }

        public Playlist GetPlaylistByName(string Name){
            return _context.Playists.FirstOrDefault(p => p.Name == Name);
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return _context.Playlists.ToList();
        }

        public void CreatePlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
        }

    }
}
