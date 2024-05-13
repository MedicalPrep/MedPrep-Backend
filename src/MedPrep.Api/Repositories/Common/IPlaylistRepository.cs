namespace MedPrep.Api.Repositories.Common;

using MedPrep.Api.Models;

public interface IPlaylistRepository
{
    Playlist? GetPlaylistByName(string name);
    IEnumerable<Playlist> GetAllPlaylists();
    void CreatePlaylist(Playlist playlist);
    void UpdatePlaylist(Playlist playlist);
}
