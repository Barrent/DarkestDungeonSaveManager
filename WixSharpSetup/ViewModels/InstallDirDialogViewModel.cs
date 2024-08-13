using System;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Xaml.Behaviors.Core;
using WixSharp;
using WixSharp.UI.Forms;
using WixSharp.UI.WPF;
using WixSharpSetup.Interfaces.ViewModels;

namespace WixSharpSetup.ViewModels
{
    /// <summary>
    /// ViewModel for standard InstallDirDialog.
    /// <para>Follows the design of the canonical Caliburn.Micro ViewModel (MVVM).</para>
    /// <para>See https://caliburnmicro.com/documentation/cheat-sheet</para>
    /// </summary>
    public class InstallDirDialogViewModel : DialogViewModelBase, IInstallDirDialogViewModel
    {
        public InstallDirDialogViewModel(ManagedForm host) : base(host)
        {
            BrowseCommand = new ActionCommand(ChangeInstallDir);
        }

        public BitmapImage Banner => Session?.GetResourceBitmap("WixSharpUI_Bmp_Banner").ToImageSource();


        public ICommand BrowseCommand { get; }

        public string InstallDirPath
        {
            get
            {
                if (Host == null)
                {
                    return null;
                }
                
                var installDirPropertyValue = Session.Property(InstallDirProperty);

                if (string.IsNullOrWhiteSpace(installDirPropertyValue))
                {
                    // We are executed before any of the MSI actions are invoked so the INSTALLDIR (if set to absolute path)
                    // is not resolved yet. So we need to do it manually
                    var installDir = Session.GetDirectoryPath(InstallDirProperty);

                    if (installDir == "ABSOLUTEPATH")
                    {
                        installDir = Session.Property("INSTALLDIR_ABSOLUTEPATH");
                    }

                    return installDir;
                }

                // INSTALLDIR set either from the command line or by one of the early setup events (e.g. UILoaded)
                return installDirPropertyValue;
            }

            set
            {
                Session[InstallDirProperty] = value;
                NotifyOfPropertyChange(() => InstallDirPath);
            }
        }

        private string InstallDirProperty => Session?.Property("WixSharp_UI_INSTALLDIR");

        private ISession Session => Host?.Runtime.Session;

        public void ChangeInstallDir()
        {
            try
            {
                using (var dialog = new CommonOpenFileDialog())
                {
                    dialog.InitialDirectory = InstallDirPath;
                    dialog.IsFolderPicker = true;

                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        InstallDirPath = dialog.FileName;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}