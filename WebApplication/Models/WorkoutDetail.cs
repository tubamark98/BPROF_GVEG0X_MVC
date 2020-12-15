using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public enum WorkoutTypes
    {
        calisthenics, crossfit, weightlifting, powerlifting
    }
    public enum ContestDiets
    {
        carbCycling, intermittentFasting, lowCarb, 
    }
    public class WorkoutDetail
    {
        [Key]
        public string WorkoutId { get; set; }
        public WorkoutTypes WorkoutType { get; set; }
        public ContestDiets ContestDiets { get; set; }
        [NotMapped]
        public virtual GymClient GymClient { get; set; }
        public virtual ICollection<ExtraInfo> AdditionalInfo { get; set; }
    }
}
