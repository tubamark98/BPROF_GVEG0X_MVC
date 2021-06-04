using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConsumer.VM
{
    public class TrainerVM : ObservableObject
    {
        private string trainerID;
        private string trainerName;

        public string TrainerID
        {
            get { return this.trainerID; }
            set { this.Set(ref this.trainerID, value); }
        }

        public string TrainerName
        {
            get { return this.trainerName; }
            set { this.Set(ref this.trainerName, value); }
        }

    }
}
