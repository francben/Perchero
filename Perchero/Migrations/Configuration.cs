namespace Perchero.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Perchero.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Perchero.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Perchero.Models.ApplicationDbContext context)
        {
            //Crear si no existe Rol Diseñador
            if (!context.Roles.Any(r => r.Name == "Administrador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrador" };

                manager.Create(role);
            }

            //Crear si no existe Rol Diseñador
            if (!context.Roles.Any(r => r.Name == "Diseñador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Diseñador" };

                manager.Create(role);
            }
            //Crear si no existe Rol CLiente
            if (!context.Roles.Any(r => r.Name == "Cliente"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Cliente" };

                manager.Create(role);
            }

            //Crear Usuario
            if (!context.Users.Any(u => u.Email == "franc@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Nombre= "Franc", Apellido = "Benitez", Direccion = "Acanomas", Telefono = "12345678", Email = "franc@gmail.com", UserName = "franc@gmail.com", PhoneNumber = "123456" };

                manager.Create(user, "1Franc*");
                //Delegador Administrador a usuario
                manager.AddToRole(user.Id, "Administrador");
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
