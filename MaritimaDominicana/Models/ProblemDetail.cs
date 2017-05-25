namespace MaritimaDominicana.Models
{
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProblemDetail
    {

        public int ProblemDetailId { get; set; }

        public int ProblemId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Titulo de la solicitud")]
        public string Title { get; set; }


        public int DepartmentId { get; set; }

        public int CreatedBy { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Descripción")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }

        public int PlaceId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Actualizacion")]
        public DateTime? Update_at { get; set; }

        [Display(Name = "Modificado por")]
        public int? Modified_by { get; set; }

        public int? AssignedTo { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Asignado el")]
        public DateTime? AssignedAt { get; set; }

        [Display(Name ="Estado")]
        public int state { get; set; }

        public int ClientId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Problem Problem { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual User Assigned { get; set; }

        public virtual Solution Solution { get; set; }

        public virtual Place Place { get; set; }

        public virtual Client Client{ get; set; }
    }

}
