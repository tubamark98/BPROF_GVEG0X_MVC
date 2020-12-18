using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public enum Genders
    {
        Nő,
        Férfi,
        Helikopter
    }

    public class GymClient
    {
        [Key]
        public string GymID { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [Range(14,100)]
        public int Age { get; set; }
        public Genders Gender { get; set; }
        [Range(0, 86)]
        public int BeenWorkingOutFor { get; set; }
        public bool Verified { get; set; }
        public string TrainerID { get; set; }
        

        public override string ToString()
        {
            return $"{GymID}-{FullName}-{Age}-{Gender}-{BeenWorkingOutFor}-{Verified}";
        }

        [NotMapped]
        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<ExtraInfo> ExtraInfos { get; set; }

        //deprecated
        public virtual WorkoutDetail WorkoutDetail { get; set; }
        
        [NotMapped]
        public virtual WorkoutDetail_v2 Detail_V2 { get; set; }
    }
}