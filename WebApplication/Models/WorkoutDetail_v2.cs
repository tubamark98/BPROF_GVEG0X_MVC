using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class WorkoutDetail_v2
    {
        public WorkoutTypes WorkoutType { get; set; }
        public ContestDiets ContestDiets { get; set; }
        public string GymID { get; set; }
    }
}
