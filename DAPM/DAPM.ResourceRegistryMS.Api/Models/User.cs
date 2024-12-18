using System.ComponentModel.DataAnnotations;

namespace DAPM.ResourceRegistryMS.Api.Models
{
    // Author: Maxime Rochat - s241741
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public Guid Organization { get; set; }
        [Required]
        public string HashPassword { get; set; }
        [Required]
        public int UserRole { get; set; }
        [Required]
        public int accepted { get; set; }
    }

    // Author: Maxime Rochat - s241741
    enum UserRole
    {
        Custom = 0,
        Admin,
        Manager,
        User,
    }
}
