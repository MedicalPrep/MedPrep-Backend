namespace MedPrep.Api.Repositories;

using MedPrep.Api.Context;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;

public class PlaylistRepository(MedPrepContext context) : IPlaylistRepository
{
    private readonly MedPrepContext context = context;

    public Playlist? GetPlaylistByName(string name) =>
        this.context.Playlist.Where(p => p.Name == name).FirstOrDefault();

    public IEnumerable<Playlist> GetAllPlaylists() => this.context.Playlist.ToList();

    public void CreatePlaylist(Playlist playlist) => _ = this.context.Playlist.Add(playlist);

    public void UpdatePlaylist(Playlist playlist) => _ = this.context.Playlist.Update(playlist);
}
