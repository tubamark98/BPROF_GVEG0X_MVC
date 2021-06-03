using Models;
using Models.ViewModels;
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
        private int localhostIP;
        private string token;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Login();
        }

        public async Task Login()
        {
            PasswordWindow pw = new PasswordWindow();
            if (pw.ShowDialog() == true)
            {
                this.localhostIP = 7766;
                RestService restservice = new RestService("https://localhost:" + localhostIP + "/", "/Auth");
                TokenViewModel tvm = restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                }).Result;

                token = tvm.Token;

                this.GetTrainerNames();
            }
            else
            {
                this.Close();
            }
        }

        public async Task GetTrainerNames()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService("https://localhost:" + localhostIP + "/", "/Trainer", token);
            IEnumerable<Trainer> playlistnames =
                await restservice.Get<Trainer>();

            cbox.ItemsSource = playlistnames;
            cbox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GymClient newClient = new GymClient()
            {
                GymID = tb_clientID.Text,
                FullName = tb_name.Text,
                Age = int.Parse(tb_age.Text),
                Gender = Genders.Nő,
                BeenWorkingOutFor = 0,
                Verified = false,
                TrainerID = (cbox.SelectedItem as Trainer).TrainerID
            };

            RestService restservice = new RestService("https://localhost:" + localhostIP + "/", "/Trainer", token);
            restservice.Post(newClient);
            this.GetTrainerNames();
        }
    }
}
