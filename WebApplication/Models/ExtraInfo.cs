using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class ExtraInfo
    {
        [Key]
        public string InfoId { get; set; }
        public string Information { get; set; }

        public virtual WorkoutDetail WorkoutDetail{get;set;}
    }
}
