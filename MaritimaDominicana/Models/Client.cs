using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaritimaDominicana.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage ="El campo :attribute es requerido")]
        [Display(Name ="Cliente")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo :attribute es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo :attribute es requerido")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Telefono")]
        public string Telephone { get; set; }

        public virtual ICollection<ProblemDetail> ProblemDetails { get; set; }
    }
}