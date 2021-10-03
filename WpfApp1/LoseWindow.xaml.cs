using System;
using System.Windows;
using BloonsProject;

namespace BloonsGame
{
    public partial class LoseWindow : Window
    {
        private readonly ProgramController _programController;

        public LoseWindow(ProgramController programController)
        {
            InitializeComponent();
            _programController = programController;
            var gameState = GameState.GetControllerSingletonInstance();
            var plural = " rounds.";
            if (gameState.Player.Round == 1)
            {
                plural = " round.";
            }
            RoundSurvivedLabel.Content = "You have survived " + gameState.Player.Round + plural;
            RoundSurvivedLabel.Visibility = Visibility.Visible;
        }

        private void PlayAgainButton_OnClickButtonClick(object sender, RoutedEventArgs e)
        {
            _programController.CloseGame();
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}