using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaritimaDominicana.Models
{
    public class Type
    {
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}