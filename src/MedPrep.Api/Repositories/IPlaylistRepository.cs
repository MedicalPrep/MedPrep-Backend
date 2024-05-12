using System;
using System.Collections.Generic;
using MedPrep.Api.Models;
using MedPrep.Api.Models.Common;

namespace MedPrep.Api.Repositories{
    public interface IPlaylistRepository :
    {
        Playlist GetPlaylistByName(string Name);
        IEnumerable<Playlist> GetAllPlaylist();
        void CreatePlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);

    }
}
