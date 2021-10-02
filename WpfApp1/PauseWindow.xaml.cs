using System.Windows;
using BloonsProject;

namespace BloonsGame
{
    public partial class PauseWindow : Window
    {
        private ProgramController programController;

        public PauseWindow(ProgramController programController)
        {
            InitializeComponent();

            this.programController = programController;
        }

        private void OnContinueButtonClick(object sender, RoutedEventArgs e)
        {
            programController.Unpause();
            Close();
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(-1);
        }
    }
}