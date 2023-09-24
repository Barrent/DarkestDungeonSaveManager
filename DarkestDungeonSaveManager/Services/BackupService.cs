using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DarkestDungeonSaveManager.Interfaces.Models;
using DarkestDungeonSaveManager.Interfaces.Services;

namespace DarkestDungeonSaveManager.Services;

public class BackupService : IBackupService
{
    private readonly IAppSettings _settings;

    public BackupService(IAppSettings settings)
    {
        _settings = settings;
    }

    public IReadOnlyList<string> GetSaveGamePaths(string profileFolderName)
    {
        if (!Directory.Exists(_settings.BackupFolderPath.Value))
        {
            return Array.Empty<string>();
        }

        var backupFolder = GetBackupFolder(profileFolderName);
        if (!Directory.Exists(backupFolder))
        {
            return Array.Empty<string>();
        }

        var directory = new DirectoryInfo(backupFolder);
        return directory.GetDirectories().Select(d => d.FullName).ToArray();
    }

    public void Load(string profileFolderName, ISaveGame saveGame)
    {
        var folder = new DirectoryInfo(profileFolderName);
        folder.Delete(true);
        folder.Create();
        CopyDirectory(saveGame.Path, profileFolderName, true);
    }

    public void Delete(ISaveGame saveGame)
    {
        Directory.Delete(saveGame.Path, true);
    }

    public string Save(string profileFolderName, ISaveGame saveGame)
    {
        var source = saveGame.Path;
        var backupFolder = GetBackupFolder(profileFolderName);
        var saveGameFolderName = GenerateSaveGameName(saveGame, backupFolder);
        backupFolder = Path.Combine(backupFolder, saveGameFolderName);
        CopyDirectory(source, backupFolder, true);

        return backupFolder;
    }

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="destPath"></param>
    /// <param name="recursive"></param>
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

    private string GetBackupFolder(string profileFolderName)
    {
        return Path.Combine(_settings.BackupFolderPath.Value, profileFolderName);
    }
}