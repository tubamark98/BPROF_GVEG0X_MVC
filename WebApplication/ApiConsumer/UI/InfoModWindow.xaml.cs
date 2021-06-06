using ApiConsumer.VM;
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
    /// Interaction logic for InfoModWindow.xaml
    /// </summary>
    public partial class InfoModWindow : Window
    {
        public InfoVM viewModel { get; set; }
        public InfoModWindow()
        {
            InitializeComponent();
            this.viewModel = new InfoVM();
        }

        public InfoModWindow(ExtraInfo info)
        :this()
        {
            viewModel.GymID = info.GymID;
            viewModel.InfoID = info.InfoId;
            viewModel.Information= info.Information;

            this.DataContext = viewModel;
        }

        private void OkClick(object sender, RoutedEventArgs eventArgs)
        {
            viewModel.Information = this.InfoText.Text;
            this.DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs eventArgs)
        {
            this.DialogResult = false;
        }
    }
}
