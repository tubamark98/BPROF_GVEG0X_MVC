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
using System.Windows.Shapes;

namespace ApiConsumer.UI
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        private string token;
        private string url = "https://gymapideploy.azurewebsites.net/";

        public InfoWindow()
        {
            InitializeComponent();
        }

        public InfoWindow(IEnumerable<GymClient> clients, string token)
        : this()
        {
            cbox.ItemsSource = clients;
            cbox.SelectedIndex = 0;

            this.token = token;
        }

        private async void Add_View(object sender, RoutedEventArgs e)
        {
            InfoModWindow infoMod = new InfoModWindow();
            if (infoMod.ShowDialog() == true)
            {
                var helper = infoMod.viewModel;
                try
                {
                    NullCheck(helper);
                }
                catch (NullCheckException)
                {
                    return;
                }

                ExtraInfo newInfo = new ExtraInfo()
                {
                    InfoId = Guid.NewGuid().ToString(),
                    GymID = (cbox.SelectedItem as GymClient).GymID,
                    Information = helper.Information
                };

                RestService restservice = new RestService(url, "/Info", token);
                restservice.Post(newInfo);

                MessageBox.Show("Added " + newInfo.Information + " in the database");
                await this.GetGymClients();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        private async void Mod_View(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(lbox.SelectedItem);
            }
            catch (NullCheckException)
            {
                return;
            }

            InfoModWindow infoMod = new InfoModWindow(lbox.SelectedItem as ExtraInfo);
            if (infoMod.ShowDialog() == true)
            {
                var helper = infoMod.viewModel;
                try
                {
                    NullCheck(helper);
                }
                catch (NullCheckException)
                {
                    return;
                }

                ExtraInfo newInfo = new ExtraInfo()
                {
                    InfoId = helper.InfoID,
                    GymID = helper.GymID,
                    Information = helper.Information
                };

                RestService restservice = new RestService(url, "/Info", token);
                restservice.Put<string, ExtraInfo>(newInfo.InfoId, newInfo);

                MessageBox.Show("Updated " + newInfo.Information + " in the database");
                await this.GetGymClients();
            }
            else
            {
                MessageBox.Show("Pain");
            }
        }

        private async void Del_View(object sender, RoutedEventArgs e)
        {
            try
            {
                NullCheck(lbox.SelectedItem);
            }
            catch (NullCheckException)
            {
                return;
            }

            ExtraInfo helper = lbox.SelectedItem as ExtraInfo;

            if (helper != null)
            {
                ;
                RestService restService = new RestService(url, "/Info", token);
                restService.Delete<string>(helper.InfoId);
                MessageBox.Show("The following information, " + helper.Information + " has been deleted.");

                await this.GetGymClients();
            }
            else
            {
                MessageBox.Show("No information selected.");
            }
        }

        private async Task GetGymClients()
        {
            cbox.ItemsSource = null;
            RestService restservice = new RestService(url, "/Client", token);
            IEnumerable<GymClient> clients =
                await restservice.Get<GymClient>();

            cbox.ItemsSource = clients;
            cbox.SelectedIndex = 0;
        }

        private void NullCheck(object o)
        {
            if (o == null)
            {
                MessageBox.Show("No bueno");
                throw new NullCheckException();
            }
        }
    }
}
