using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TrainerID { get; set; }
        [StringLength(200)]
        public string TrainerName { get; set; }
        public virtual ICollection<GymClient> GymClients { get; set; }
        public Trainer()
        {
            GymClients = new List<GymClient>();
        }
    }
}
