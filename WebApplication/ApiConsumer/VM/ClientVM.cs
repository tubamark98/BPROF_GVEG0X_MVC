using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConsumer.VM
{
    public class ClientVM : ObservableObject
    {
        private string gymID;
        private string fullName;
        private int age;
        private int beenWorkingOutFor;
        private bool verified;
        private string trainerID;
        private Genders gender;

        public string GymID
        {
            get { return this.gymID; }
            set { this.Set(ref this.gymID, value); }
        }

        public string FullName
        {
            get { return this.fullName; }
            set { this.Set(ref this.fullName, value); }
        }

        public int Age
        {
            get { return this.age; }
            set { this.Set(ref this.age, value); }
        }

        public int BeenWorkingOutFor
        {
            get { return this.beenWorkingOutFor; }
            set { this.Set(ref this.beenWorkingOutFor, value); }
        }

        public bool Verified
        {
            get { return this.verified; }
            set { this.Set(ref this.verified, value); }
        }

        public string TrainerID
        {
            get { return this.trainerID; }
            set { this.Set(ref this.trainerID, value); }
        }

        public Genders Gender
        {
            get { return this.gender; }
            set { this.Set(ref this.gender, value); }
        }
    }
}
