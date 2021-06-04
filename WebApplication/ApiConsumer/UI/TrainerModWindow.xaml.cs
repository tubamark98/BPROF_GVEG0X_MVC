using ApiConsumer.VM;
using Models;
using System.Windows;

namespace ApiConsumer.UI
{
    /// <summary>
    /// Interaction logic for TrainerModWindow.xaml
    /// </summary>
    public partial class TrainerModWindow : Window
    {
        public TrainerVM cucc { get; set; }
        public TrainerModWindow()
        {
            cucc = new TrainerVM();
            InitializeComponent();
        }

        public TrainerModWindow(TrainerVM trainer)
            : this()
        {
            this.DataContext = trainer;
        }

        private void OkClick(object sender, RoutedEventArgs eventArgs)
        {
            if(this.IDText.Content != null)
            {
                cucc.TrainerID = this.IDText.Content.ToString();
            }
            
            cucc.TrainerName = this.TrainerText.Text;
            this.DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs eventArgs)
        {
            this.DialogResult = false;
        }
    }
}
