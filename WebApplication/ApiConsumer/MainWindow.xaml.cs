using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetTrainerNames();
        }

        public async Task GetTrainerNames()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService("https://localhost:5001/", "/Trainer");
            IEnumerable<Trainer> playlistnames =
                await restservice.Get<Trainer>();

            cbox.ItemsSource = playlistnames;
            cbox.SelectedIndex = 0;
        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GymClient newvideo = new GymClient()
            {
                FullName = tb_name.Text,
                Title = tb_title.Text,
                YoutubeId = tb_youtube.Text,
                Rating = 5,
                PlayListUid = (cbox.SelectedItem as Playlist).UID
            };

            RestService restservice = new RestService("https://localhost:5001/", "/Video");
            restservice.Post<GymClient>(newvideo);
            GetTrainerNames();
        }
        */
    }
}
