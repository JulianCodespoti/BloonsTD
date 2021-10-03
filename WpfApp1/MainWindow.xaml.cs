using BloonsProject;
using BloonsProject.Models.Maps;
using System.Windows;

namespace BloonsGame
{
    public partial class MainWindow : Window
    {
        private ProgramController _programController;
        private MapManager _mapManager;
        private PauseWindow _pauseWindow;

        public MainWindow()
        {
            InitializeComponent();
            _mapManager = new MapManager();

            foreach (var map in _mapManager.Maps)
            {
                MapComboBox.Items.Add(map.Name);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectMapLabelError.Visibility = Visibility.Hidden;
            if (MapComboBox.SelectedItem == null)
            {
                SelectMapLabelError.Visibility = Visibility.Visible;
                return;
            }
            var map = _mapManager.GetMapByName(MapComboBox.SelectedItem.ToString());
            _programController = new ProgramController(map);
            OpenPauseScreen();
            _pauseWindow.Hide();
            Close();
            _programController.PauseEventHandler += OpenPauseScreen;
            _programController.LoseEventHandler += OpenLossScreen;
            _programController.RunGame();
        }

        public void OpenPauseScreen()
        {
            _pauseWindow = new PauseWindow(_programController);
            _pauseWindow.Show();
        }

        public void OpenLossScreen()
        {
            var loseWindow = new LoseWindow(_programController);
            loseWindow.Show();
        }

        private void MapComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}