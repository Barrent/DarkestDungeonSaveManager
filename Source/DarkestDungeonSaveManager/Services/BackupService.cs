using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barrent.Common.Extensions;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Services;

namespace DarkestDungeonSaveManager.Services;

/// <summary>
/// Service to copy files from game folder and backup storage back and forth.
/// </summary>
/// <param name="settings">App settings.</param>
public class BackupService(IAppSettings settings) : IBackupService
{
    /// <summary>
    /// Deletes save game from backup storage.
    /// </summary>
    /// <param name="saveGame">Save game to delete.</param>
    public void Delete(ISaveGame saveGame)
    {
        Directory.Delete(saveGame.Path, true);
    }

    /// <summary>
    /// Reads all the available save games from backup folder for the specified profile.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <returns>Full paths to backed uo save games.</returns>
    public IReadOnlyList<string> GetSaveGamePaths(IProfile profile)
    {
        if (!Directory.Exists(settings.BackupFolderPath.Value))
        {
            return Array.Empty<string>();
        }

        var backupFolder = GetBackupFolder(profile.FolderName.Value!);
        if (!Directory.Exists(backupFolder))
        {
            return Array.Empty<string>();
        }

        var directory = new DirectoryInfo(backupFolder);
        return directory.GetDirectories().Select(d => d.FullName).ToArray();
    }

    /// <summary>
    /// Copies save game from the backup folder to the profile folder.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <param name="saveGame">Save game to load.</param>
    public void Load(IProfile profile, ISaveGame saveGame)
    {
        var folder = new DirectoryInfo(profile.FolderPath.Value!);
        folder.Delete(true);
        folder.Create();

        CopyDirectory(saveGame.Path, profile.FolderPath.Value!);
    }

    /// <summary>
    /// Copies active save game to the backup folder.
    /// </summary>
    /// <param name="profile">Game profile.</param>
    /// <param name="saveGame">Save game to backup.</param>
    public string Save(IProfile profile, ISaveGame saveGame)
    {
        var source = saveGame.Path;
        var backupFolder = GetBackupFolder(profile.FolderName.Value!);
        var saveGameFolderName = GenerateSaveGameName(saveGame, backupFolder);
        backupFolder = Path.Combine(backupFolder, saveGameFolderName);
        CopyDirectory(source, backupFolder);

        return backupFolder;
    }

    /// <summary>
    /// Copies directory recursive.
    /// </summary>
    /// <param name="sourcePath">Directory to copy.</param>
    /// <param name="destPath">Destination path.</param>
    private void CopyDirectory(string sourcePath, string destPath)
    {
        var sourceDirectory = new DirectoryInfo(sourcePath);
        sourceDirectory.CopyDirectory(destPath, true);
    }

    /// <summary>
    /// Generates name of a folder containing backup.
    /// </summary>
    /// <param name="saveGame">Save game.</param>
    /// <param name="profileBackupFolder">Backup folder of profile.</param>
    /// <returns>Folder name of a backup.</returns>
    private string GenerateSaveGameName(ISaveGame saveGame, string profileBackupFolder)
    {
        var backupFolder = new DirectoryInfo(profileBackupFolder);
        
        if (!backupFolder.Exists)
        {
            backupFolder.Create();
        }

        var existingFolders = backupFolder.GetDirectories();
        var week = saveGame.Week.Value.ToString("0000");
        var location = saveGame.IsInRaid.Value ? "Dungeon" : "Town";

        var baseFolderName = $"Week{week}_{location}";
        var usedSuffixes = existingFolders.Select(f => f.Name.Split(baseFolderName).Last()).Where(v => !string.IsNullOrEmpty(v)).Select(v => int.Parse(v.Substring(1))).ToArray();
        var suffix = 0;
        if (usedSuffixes.Length > 0)
        {
            suffix = usedSuffixes.Max() + 1;
        }

        baseFolderName += "_" + suffix.ToString("0000");

        return baseFolderName;
    }

    /// <summary>
    /// Builds full path to profile backup folder.
    /// </summary>
    /// <param name="profileFolderName">Profile folder name.</param>
    /// <returns>Full path to profile backup folder.</returns>
    private string GetBackupFolder(string profileFolderName)
    {
        return Path.Combine(settings.BackupFolderPath.Value!, profileFolderName);
    }
}