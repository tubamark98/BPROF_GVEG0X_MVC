using ApiConsumer.VM;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ClientModWindow.xaml
    /// </summary>
    public partial class ClientModWindow : Window
    {
        public ClientVM viewModel { get; set; }
        public ClientModWindow()
        {
            InitializeComponent();
            this.viewModel = new ClientVM();
        }

        public ClientModWindow(GymClient client)
        {
            this.DataContext = client;
        }

        private void OkClick(object sender, RoutedEventArgs eventArgs)
        {
            var ageHelper = int.Parse(this.AgeText.Text);
            if(ageHelper <14 || ageHelper >90)
            {
                viewModel.Age = 18;
            }
            else
            {
                viewModel.Age = ageHelper;
            }

            var workoutLengthHelper = int.Parse(this.LengthText.Text);
            if (workoutLengthHelper < 0 || workoutLengthHelper > 90)
            {
                viewModel.BeenWorkingOutFor = 0;
            }
            else
            {
                viewModel.BeenWorkingOutFor = workoutLengthHelper;
            }

            viewModel.FullName = this.FullNameText.Text;

            this.DialogResult = true;
        }

        public GymClient GymClient { get; set; }

        private void CancelClick(object sender, RoutedEventArgs eventArgs)
        {
            this.DialogResult = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
