using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        CopyDirectory(saveGame.Path, profile.FolderPath.Value!, true);
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
        CopyDirectory(source, backupFolder, true);

        return backupFolder;
    }

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
    /// TODO: use extension method from new version of Barrent.Common package once its released.
    /// </summary>
    /// <param name="sourcePath">Directory to copy.</param>
    /// <param name="destPath">Destination path.</param>
    /// <param name="recursive">Indicates if subdirectories should be copied as well.</param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    private void CopyDirectory(string sourcePath, string destPath, bool recursive)
    {
        // Get information about the source directory
        var sourceDirectory = new DirectoryInfo(sourcePath);

        // Check if the source directory exists
        if (!sourceDirectory.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory.FullName}");

        // Cache directories before we start copying
        var dirs = sourceDirectory.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destPath);

        // Get the files in the source directory and copy to the destination directory
        foreach (var file in sourceDirectory.GetFiles())
        {
            var targetFilePath = Path.Combine(destPath, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (var subDir in dirs)
            {
                var newDestinationDir = Path.Combine(destPath, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
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