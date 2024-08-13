using System.Windows;
using Caliburn.Micro;
using WixSharp;
using WixSharp.UI.WPF;
using WixSharpSetup.ViewModels;

namespace WixSharpSetup
{
    /// <summary>
    /// The standard InstallDirDialog.
    /// <para>Follows the design of the canonical Caliburn.Micro View (MVVM).</para>
    /// <para>See https://caliburnmicro.com/documentation/cheat-sheet</para>
    /// </summary>
    /// <seealso cref="WixSharp.UI.WPF.WpfDialog" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="WixSharp.IWpfDialog" />
    public partial class InstallDirDialog : WpfDialog, IWpfDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallDirDialog"/> class.
        /// </summary>
        public InstallDirDialog()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Localize();
        }

        /// <summary>
        /// This method is invoked by WixSHarp runtime when the custom dialog content is internally fully initialized.
        /// This is a convenient place to do further initialization activities (e.g. localization).
        /// </summary>
        public void Init()
        {
            ViewModelBinder.Bind(new InstallDirDialogViewModel(ManagedFormHost), this, null);
        }
    }
}