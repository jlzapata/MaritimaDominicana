namespace MaritimaDominicana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Solution
    {
        [ForeignKey("ProblemDetail")]
        public int SolutionId { get; set; }

        [Display(Name = "Solucion")]
        [Required(ErrorMessage = "El campo solucion es requerido.")]
        public string SolutionDescription { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        public virtual ProblemDetail ProblemDetail { get; set; }

        public virtual User User { get; set; }
    }
}
