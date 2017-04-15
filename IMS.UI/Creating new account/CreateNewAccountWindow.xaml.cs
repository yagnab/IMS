using MahApps.Metro.Controls;
using IMS.UI.ViewModels;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for CreateNewAccountWindow.xaml
    /// </summary>
    public partial class CreateNewAccountWindow : MetroWindow
    {
        public CreateNewAccountWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Deletes window stored
            CreateNewAccountService.NullCurrentWindow();
        }
    }
}
