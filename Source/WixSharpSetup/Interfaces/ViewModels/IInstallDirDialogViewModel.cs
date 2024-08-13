using System.Windows.Input;

namespace WixSharpSetup.Interfaces.ViewModels
{
    public interface IInstallDirDialogViewModel : IDialogViewModel
    {
        string InstallDirPath { get; set; }
        ICommand BrowseCommand { get; }
    }
}