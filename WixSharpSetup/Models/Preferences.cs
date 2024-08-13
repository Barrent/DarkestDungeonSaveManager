using System;

namespace WixSharpSetup.Models
{
    public static class Preferences
    {
        public const string Manufacturer = "Barrent";

        public const string ProductName = "Darkest Dungeon SaveGame Manager";

        public static readonly Guid ProductCode = new Guid("6478d664-2a90-4ae9-bdaf-fc96e6d53eb9");

        public static readonly Guid UpgradeCode = new Guid("4a22c919-4365-4279-995f-94048d7e64e6");

        public static readonly string DefaultInstallFolder = $@"%ProgramFiles%\{ProductName}";

        public static readonly string IdeSourceFolder = @"..\DarkestDungeonSaveManager\bin\x64\Release\net8.0-windows";

        public static readonly string SourceFolder = @"..\..\..\..\DarkestDungeonSaveManager\bin\x64\Release\net8.0-windows";

        public static readonly string[] ExcludedFiles = new[] {
            "DarkestDungeonSaveManager.deps.json",
            "DarkestDungeonSaveManager.dll.config",
            "runtimes\\win\\lib\\net8.0\\System.Diagnostics.EventLog.dll",
            ".pdb"
        };
    }
}