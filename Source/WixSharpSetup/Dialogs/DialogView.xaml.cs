using System.Windows;
using System.Windows.Controls;

namespace WixSharpSetup.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        public static readonly DependencyProperty DlgTitleProperty = DependencyProperty.Register(
            nameof(DlgTitle), typeof(string), typeof(DialogView), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DlgDescriptionProperty = DependencyProperty.Register(
            nameof(DlgDescription), typeof(string), typeof(DialogView), new PropertyMetadata(default(string)));

        public string DlgDescription
        {
            get { return (string)GetValue(DlgDescriptionProperty); }
            set { SetValue(DlgDescriptionProperty, value); }
        }
        public string DlgTitle
        {
            get { return (string)GetValue(DlgTitleProperty); }
            set { SetValue(DlgTitleProperty, value); }
        }

        public DialogView()
        {
            InitializeComponent();
        }
    }
}
