using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Perchero.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

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

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Perchero.Models.Tela> Telas { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.Tipo> Tipoes { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.Avio> Avios { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.Prenda> Prendas { get; set; }

        //public System.Data.Entity.DbSet<Perchero.Models.ApplicationUser> ApplicationUsers { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.DetallePrenda> DetallePrendas { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.Pedido> Pedidoes { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.Talle> Talles { get; set; }

        public System.Data.Entity.DbSet<Perchero.Models.TallePrenda> TallePrendas { get; set; }
    }
}