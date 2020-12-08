using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class WorkoutDetail
    {
        [Key]
        public string WorkoutId { get; set; }
        public string Detail { get; set; }

    }
}
