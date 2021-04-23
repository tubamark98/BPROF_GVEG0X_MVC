using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class ExtraInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InfoId { get; set; }
        [StringLength(200)]
        public string Information { get; set; }
        public string GymID { get; set; }

        [NotMapped]
        public virtual GymClient GymClient{get;set;}
    }
}
