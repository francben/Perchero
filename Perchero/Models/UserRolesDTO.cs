using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Perchero.Models
{
    public class ExpandedUserDTO
    {
        [Key]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Telefono { get; set; }
        public string Empresa { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        public IEnumerable<UserRolesDTO> Roles { get; set; }
    }

    public class UserRolesDTO
    {
        [Key]
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }

    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }

    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }

    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        public List<UserRoleDTO> colUserRoleDTO { get; set; }
    }
}