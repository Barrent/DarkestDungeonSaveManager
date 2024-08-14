using System.Windows.Input;

namespace WixSharpSetup.Interfaces.ViewModels
{
    /// <summary>
    /// View model of a installation path selection step.
    /// </summary>
    public interface IInstallDirDialogViewModel : IDialogViewModel
    {
        /// <summary>
        /// Command to display dialog to select installation path.
        /// </summary>
        ICommand BrowseCommand { get; }

        /// <summary>
        /// Selected installation path. 
        /// </summary>
        string InstallDirPath { get; set; }
    }
}