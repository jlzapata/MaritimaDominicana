using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaritimaDominicana.Models
{
    public class Place
    {
        public int PlaceId { get; set; }

        [Required(ErrorMessage ="El campo :attribute es requerido")]
        [Display(Name ="Lugar")]
        public string Name { get; set; }

        public virtual ICollection<ProblemDetail> ProblemDetails { get; set; }
    }
}