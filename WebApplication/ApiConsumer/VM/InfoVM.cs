using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiConsumer.VM
{
    public class InfoVM : ObservableObject
    {
        private string infoID;
        private string information;
        private string gymID;

        public string Information
        {
            get { return this.information; }
            set { this.Set(ref this.information, value); }
        }

        public string InfoID
        {
            get { return this.infoID; }
            set { this.Set(ref this.infoID, value); }
        }

        public string GymID
        {
            get { return this.gymID; }
            set { this.Set(ref this.gymID, value); }
        }
    }
}
