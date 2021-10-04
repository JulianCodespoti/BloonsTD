using BloonsProject;
using System.Windows;

namespace BloonsGame
{
    public partial class MainWindow : Window
    {
        private PauseWindow _pauseWindow;
        private IProgramController _programController;

        public MainWindow()
        {
            InitializeComponent();
            foreach (var map in MapManager.GetAllMaps())
                MapComboBox.Items.Add(map.Name);
        }

        public void OpenLossScreen()
        {
            var loseWindow = new LoseWindow();
            loseWindow.Show();
        }

        public void OpenPauseScreen()
        {
            _pauseWindow = new PauseWindow(_programController);
            _pauseWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectMapLabelError.Visibility = Visibility.Hidden;
            if (MapComboBox.SelectedItem == null)
            {
                SelectMapLabelError.Visibility = Visibility.Visible;
                return;
            }
            var map = MapManager.GetMapByName(MapComboBox.SelectedItem.ToString());
            _programController = new SplashKitController(map);
            OpenPauseScreen();
            _pauseWindow.Hide();
            Close();
            _programController.PauseEventHandler += OpenPauseScreen;
            _programController.LoseEventHandler += OpenLossScreen;
            _programController.Start();
        }
    }
}