using System.Windows.Input;

namespace WixSharpSetup.Interfaces.ViewModels
{
    public interface IDialogViewModel
    {
        ICommand NextCommand { get; }

        ICommand CancelCommand { get; }

        ICommand PreviousCommand { get; }
    }
}