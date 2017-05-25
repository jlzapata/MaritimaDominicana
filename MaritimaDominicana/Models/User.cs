namespace MaritimaDominicana.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Cryptography;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ProblemDetails = new HashSet<ProblemDetail>();
            Assigneds = new HashSet<ProblemDetail>();
            Solutions = new HashSet<Solution>();
            Followeds = new HashSet<User>();
            Followers = new HashSet<User>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Apellidos")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        [Required]
        [Display(Name = "Password")]
        public string Pasword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo electronico no valido.")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public int TypeId { get; set; }

        [Display(Name = "Activo")]
        public bool? Active { get; set; }

        public bool? Connected { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<ProblemDetail> ProblemDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProblemDetail> Assigneds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Solution> Solutions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<User> Followers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<User> Followeds { get; set; }

        public virtual Type Type { get; set; }

        //Encriptacion de la password
        public static string EncyptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public bool VerifyPassword(string savedPassword)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPassword);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(this.Pasword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }
}
