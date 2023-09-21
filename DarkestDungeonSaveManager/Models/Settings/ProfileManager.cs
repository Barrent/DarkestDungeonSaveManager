using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DarkestDungeonSaveManager.Models.Settings;

public class ProfileManager
{
    private readonly List<Profile> _profiles;

    public Profile? SelectedProfile { get; set; }

    public IReadOnlyList<Profile> Profiles => _profiles;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"> Path to folder containing game profiles.
    /// D:\Program Files (x86)\Steam\userdata\112191091\262060\remote
    /// </param>
    public void LoadProfiles(string path)
    {
        var folders = Directory.EnumerateDirectories(path);
        _profiles.Clear();
        _profiles.AddRange(folders.Select(CreateProfile));
    }

    private Profile CreateProfile(string path)
    {

        return new Profile(path);
    }


}