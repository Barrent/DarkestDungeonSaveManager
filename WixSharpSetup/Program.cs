using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CommandLine;
using Microsoft.WindowsAPICodePack.Dialogs;
using WixSharp;
using WixSharpSetup.Extensions;
using WixSharpSetup.Models;
using File = WixSharp.File;

namespace WixSharpSetup
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run).WithNotParsed(HandleParseError);
        }

        private static IEnumerable<WixEntity> CreateFileList()
        {
            var sourceFolder = Environment.GetEnvironmentVariable("ide") == "true" ? Preferences.IdeSourceFolder : Preferences.SourceFolder;
            var directory = new DirectoryInfo(sourceFolder);
            var programMenuDirFiles = new List<WixEntity>();
            var desktopDirFiles = new List<WixEntity>();

            foreach (var fileInfo in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                if (Preferences.ExcludedFiles.Any(e => fileInfo.FullName.EndsWith(e)))
                {
                    continue;
                }

                var relative = GetRelativePath(fileInfo.FullName, Environment.CurrentDirectory);
                Console.WriteLine(relative);
                var file = new File(relative);
                if (fileInfo.Extension == ".exe")
                {
                    var shortcut = new ExeFileShortcut(fileInfo.Name, Path.Combine("[INSTALLDIR]", fileInfo.Name), string.Empty)
                    {
                        WorkingDirectory = "[INSTALLDIR]"
                    };
                    desktopDirFiles.Add(shortcut);
                    programMenuDirFiles.Add(shortcut.Copy());
                }

                yield return file;
            }

            var uninstallShortCut = new ExeFileShortcut($"Uninstall {Preferences.ProductName}",
                "[System64Folder]msiexec.exe",
                "/x [ProductCode]");
            programMenuDirFiles.Add(uninstallShortCut);

            yield return uninstallShortCut.Copy();

            yield return new Dir(Path.Combine("%ProgramMenu%", Preferences.ProductName), programMenuDirFiles.ToArray());
            yield return new Dir("%Desktop%", desktopDirFiles.ToArray());
        }

        /// <summary>
        /// Returns a relative path string from a full path based on a base path
        /// provided.
        /// </summary>
        /// <param name="fullPath">The path to convert. Can be either a file or a directory</param>
        /// <param name="basePath">The base path on which relative processing is based. Should be a directory.</param>
        /// <returns>
        /// String of the relative path.
        /// 
        /// Examples of returned values:
        ///  test.txt, ..\test.txt, ..\..\..\test.txt, ., .., subdir\test.txt
        /// </returns>
        private static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            var baseUri = new Uri(basePath);
            var fullUri = new Uri(fullPath);

            var relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            return relativeUri.ToString().Replace("/", "\\");
        }

        private static void HandleParseError(IEnumerable<CommandLine.Error> obj)
        {
        }

        private static void OnUnhandledException(ExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private static void Run(Options options)
        {
            var project = new ManagedProject(Preferences.ProductName,
                new Dir(Preferences.DefaultInstallFolder,
                    CreateFileList().ToArray()));

            project.GUID = Preferences.ProductCode;
            project.UpgradeCode = Preferences.UpgradeCode;
            project.Version = new Version(options.Version);
            project.ControlPanelInfo.Manufacturer = "Barrent";

            // project.ManagedUI = ManagedUI.DefaultWpf; // all stock UI dialogs

            //custom set of UI WPF dialogs
            project.ManagedUI = new ManagedUI();

            project.ManagedUI.InstallDialogs.Add<WelcomeDialog>()
                .Add<LicenceDialog>()
                .Add<InstallDirDialog>()
                .Add<ProgressDialog>()
                .Add<ExitDialog>();

            project.ManagedUI.ModifyDialogs.Add<MaintenanceTypeDialog>()
                .Add<ProgressDialog>()
                .Add<ExitDialog>();

            project.DefaultRefAssemblies.Add(typeof(DialogControl).Assembly.Location);
            project.DefaultRefAssemblies.Add(typeof(CommonOpenFileDialog).Assembly.Location);

            // project.SourceBaseDir = "<input dir path>";
            project.OutDir = "Installer";
            project.Platform = Platform.x64;
            project.UnhandledException += OnUnhandledException;

            project.BuildMsi();
        }
    }
}