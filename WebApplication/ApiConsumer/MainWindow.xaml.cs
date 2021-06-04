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
                RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Auth");
                TokenViewModel tvm = await restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                });

                token = tvm.Token;

                await this.RefreshLists();
            }
            else
            {
                this.Close();
            }
        }

        public async Task RefreshLists()
        {
            await this.GetTrainerNames();
            await this.GetClientNames();
        }

        private async Task GetClientNames()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Client", token);
            IEnumerable<GymClient> clients =
                await restservice.Get<GymClient>();

            cbox.ItemsSource = clients;
            cbox.SelectedIndex = 0;
        }

        public async Task GetTrainerNames()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Trainer", token);
            IEnumerable<Trainer> trainers =
                await restservice.Get<Trainer>();

            cbox.ItemsSource = trainers;
            cbox.SelectedIndex = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
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

            RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Client", token);
            restservice.Post(newClient);
            await this.GetTrainerNames();
        }

        private async void Delete_Trainer(object sender, RoutedEventArgs e)
        {
            var item = cbox.SelectedItem as Trainer;
            if (item != null)
            {
                RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Trainer", token);
                restservice.Delete<string>(item.TrainerID);

                MessageBox.Show("The following trainer, " + item.TrainerName + " has been deleted.");

                await RefreshLists();

                return;
            }

            MessageBox.Show("No trainer selected.");
        }

        private async void Delete_Client(object sender, RoutedEventArgs e)
        {
            var item = lbox.SelectedItem as GymClient;
            if (item != null)
            {
                RestService restservice = new RestService("https://gymapideploy.azurewebsites.net/", "/Client", token);
                restservice.Delete<string>(item.GymID);

                MessageBox.Show("The following client, " + item.FullName + " has been deleted.");

                await RefreshLists();

                return;
            }
            MessageBox.Show("No client selected.");
        }
    }
}
