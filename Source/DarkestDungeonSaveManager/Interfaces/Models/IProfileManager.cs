using System;
using System.Collections.Generic;
using Barrent.Common.Events;

namespace DarkestDungeonSaveManager.Interfaces.Models;

/// <summary>
/// Stores game profiles.
/// </summary>
public interface IProfileManager
{
    /// <summary>
    /// Raised on change of <see cref="Profiles"/>.
    /// </summary>
    event EventHandler<IProfileManager, EventArgs>? ProfilesChanged;

    /// <summary>
    /// Currently active game profile.
    /// </summary>
    IProfile? ActiveProfile { get; set; }

    /// <summary>
    /// Exiting game profiles.
    /// </summary>
    IReadOnlyList<IProfile> Profiles { get; }

    /// <summary>
    /// Loads game profiles from the game folder.
    /// </summary>
    void LoadProfiles();
}