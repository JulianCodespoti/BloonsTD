using BloonsProject;
using System.Windows;

namespace BloonsGame
{
    public partial class PauseWindow : Window
    {
        private readonly ProgramController _programController;

        public PauseWindow(ProgramController programController)
        {
            InitializeComponent();
            this._programController = programController;
        }

        private void OnContinueButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _programController.Unpause();
            Close();
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _programController.CloseGame();
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}