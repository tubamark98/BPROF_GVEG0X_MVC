using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public enum gender
    {
        Nő,
        Férfi
    }

    public class GymClient
    {
        [Key]
        public string GymID { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [Range(14,100)]
        public int Age { get; set; }
        public gender Gender { get; set; }
        [Range(0, 86)]
        public int BeenWorkingOutFor { get; set; }
        public bool Verified { get; set; }
    }
}
