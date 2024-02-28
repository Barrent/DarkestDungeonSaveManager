using System;
using System.Collections.Generic;
using Barrent.Common.Events;

namespace DarkestDungeonSaveManager.Interfaces.Models;

public interface IProfileManager
{
    event EventHandler<IProfileManager, EventArgs> ProfilesChanged;

    IProfile? ActiveProfile { get; set; }

    IReadOnlyList<IProfile> Profiles { get; }
    void LoadProfiles();
}