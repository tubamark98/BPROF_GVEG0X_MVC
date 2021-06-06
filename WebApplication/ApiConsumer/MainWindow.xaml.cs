using ApiConsumer.CustomCode;
using ApiConsumer.UI;
using ApiConsumer.VM;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token;
        private string url = "https://gymapideploy.azurewebsites.net/";

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
                RestService restservice = new RestService(url, "/Auth");
                TokenViewModel tvm = await restservice.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                });

                token = tvm.Token;

                await this.GetTrainerNames();
            }
            else
            {
                this.Close();
            }
        }

        public async Task GetTrainerNames()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService(url, "/Trainer", token);
            IEnumerable<Trainer> trainers =
                await restservice.Get<Trainer>();

            cbox.ItemsSource = trainers;
            cbox.SelectedIndex = 0;
        }

        private async void Delete_Trainer(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(cbox.SelectedItem);
            }
            catch(NullCheckException)
            {
                return;
            }
            
            Trainer helper = cbox.SelectedItem as Trainer;

            if (helper != null)
            {
                foreach(var item in helper.GymClients)
                {
                    RestService helpService = new RestService(url, "/Client", token);
                    helpService.Delete<string>(item.GymID);
                }

                RestService restService = new RestService(url, "/Trainer", token);
                restService.Delete<string>(helper.TrainerID);
                MessageBox.Show("The following trainer, " + helper.TrainerName + " has been deleted.");

                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("No trainer selected.");
            }
        }

        private async void Delete_Client(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(lbox.SelectedItem);
            }
            catch (NullCheckException)
            {
                return;
            }

            GymClient helper = lbox.SelectedItem as GymClient;

            if (helper != null)
            {
                ;
                RestService restService = new RestService(url, "/Client", token);
                restService.Delete<string>(helper.GymID);
                MessageBox.Show("The following client, " + helper.FullName + " has been deleted.");

                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("No client selected.");
            }
        }

        private async void Mod_Trainer(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(cbox.SelectedItem);
            }
            catch (NullCheckException)
            {
                return;
            }

            TrainerModWindow trainerMod = new TrainerModWindow(cbox.SelectedItem as Trainer);

            if (trainerMod.ShowDialog() == true)
            {
                var helper = trainerMod.cucc;
                NullCheck(helper);

                Trainer newTrainer = new Trainer()
                {
                    TrainerID = helper.TrainerID,
                    TrainerName = helper.TrainerName
                };

                RestService restservice = new RestService(url, "/Trainer", token);
                restservice.Put<string, Trainer>(newTrainer.TrainerID, newTrainer);

                MessageBox.Show("Updated " + newTrainer.TrainerName + " in the database");
                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        private async void Mod_Client(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(lbox.SelectedItem);
            }
            catch (NullCheckException)
            {
                return;
            }

            ClientModWindow clientMod = new ClientModWindow(lbox.SelectedItem as GymClient);
            if (clientMod.ShowDialog() == true)
            {
                var helper = clientMod.viewModel;
                try
                {
                    NullCheck(helper);
                }
                catch (NullCheckException)
                {
                    return;
                }

                GymClient newClient = new GymClient()
                {
                    GymID = helper.GymID,
                    FullName = helper.FullName,
                    Age = helper.Age,
                    Gender = helper.Gender,
                    BeenWorkingOutFor = helper.BeenWorkingOutFor,
                    Verified = helper.Verified,
                    TrainerID = (cbox.SelectedItem as Trainer).TrainerID
                };

                RestService restservice = new RestService(url, "/Client", token);
                restservice.Put<string, GymClient>(newClient.GymID, newClient);

                MessageBox.Show("Updated " + newClient.FullName + " in the database");
                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        private async void Add_Trainer(object sender, RoutedEventArgs e)
        {
            TrainerModWindow trainerMod = new TrainerModWindow();

            if(trainerMod.ShowDialog() == true)
            {
                var helper = trainerMod.cucc.TrainerName;
                try
                {
                    NullCheck(helper);
                }
                catch (NullCheckException)
                {
                    return;
                }

                Trainer newTrainer = new Trainer()
                {
                    TrainerID = Guid.NewGuid().ToString(),
                    TrainerName = helper
                };

                RestService restservice = new RestService(url, "/Trainer", token);
                restservice.Post(newTrainer);

                MessageBox.Show("Added " + newTrainer.TrainerName + " to the database");
                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        private async void Add_Client(object sender, RoutedEventArgs e)
        {
            ClientModWindow clientMod = new ClientModWindow();
            if (clientMod.ShowDialog() == true)
            {
                var helper = clientMod.viewModel;
                try
                {
                    NullCheck(helper);
                }
                catch (NullCheckException)
                {
                    return;
                }

                GymClient newClient = new GymClient()
                {
                    GymID = Guid.NewGuid().ToString(),
                    FullName = helper.FullName,
                    Age = helper.Age,
                    Gender = helper.Gender,
                    BeenWorkingOutFor = helper.BeenWorkingOutFor,
                    Verified = helper.Verified,
                    TrainerID = (cbox.SelectedItem as Trainer).TrainerID
                };

                RestService restservice = new RestService(url, "/Client", token);
                restservice.Post(newClient);

                MessageBox.Show("Added " + newClient.FullName + " to the database");
                await this.GetTrainerNames();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        public async void View_Info(object sender, RoutedEventArgs e)
        {
            RestService restservice = new RestService(url, "/Client", token);
            IEnumerable<GymClient> clients =
                await restservice.Get<GymClient>();

            try
            {
                NullCheck(clients);
            }
            catch (NullCheckException)
            {
                return;
            }

            InfoWindow infoWindow = new(clients, token);
            infoWindow.ShowDialog();
        }

        public void NullCheck(object o)
        {
            if (o == null)
            {
                MessageBox.Show("No bueno");
                throw new NullCheckException();
            }
        }
    }
}
