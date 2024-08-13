using System.Windows.Input;
using Caliburn.Micro;
using WixSharp;
using WixSharp.UI.Forms;
using WixSharpSetup.Dialogs;
using WixSharpSetup.Interfaces.ViewModels;

namespace WixSharpSetup.ViewModels
{
    /// <summary>
    /// Base view model of a dialog window.
    /// </summary>
    public class DialogViewModelBase : Screen, IDialogViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DialogViewModelBase"/>.
        /// </summary>
        /// <param name="host">Dialog form.</param>
        public DialogViewModelBase(ManagedForm host)
        {
            Host = host;
            NextCommand = new RelayCommand(p => Shell?.GoNext(), CanExecuteNext);
            CancelCommand = new RelayCommand(p => Shell?.Cancel(), CanExecuteCancel);
            PreviousCommand = new RelayCommand(p => Shell?.GoPrev(), CanExecutePrevious);
        }

        /// <summary>
        /// Cancels installation.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Dialog form.
        /// </summary>
        public ManagedForm Host { get; }

        /// <summary>
        /// Moves to the next step.
        /// </summary>
        public ICommand NextCommand { get; }

        /// <summary>
        /// Moves to the previous step.
        /// </summary>
        public ICommand PreviousCommand { get; }

        /// <summary>
        /// UI shell (main UI window). This property is set the ManagedUI runtime (IManagedUI).
        /// On the other hand it is consumed (accessed) by the UI dialog (IManagedDialog).
        /// </summary>
        public IManagedUIShell Shell => Host?.Shell;

        /// <summary>
        /// Checks if <see cref="NextCommand"/> can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can be executed.</returns>
        protected virtual bool CanExecuteNext(object parameter)
        {
            return Shell != null;
        }

        /// <summary>
        /// Checks if <see cref="NextCommand"/> can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can be executed.</returns>
        protected virtual bool CanExecuteCancel(object parameter)
        {
            return Shell != null;
        }

        /// <summary>
        /// Checks if <see cref="NextCommand"/> can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can be executed.</returns>
        protected virtual bool CanExecutePrevious(object parameter)
        {
            return Shell != null;
        }
    }
}