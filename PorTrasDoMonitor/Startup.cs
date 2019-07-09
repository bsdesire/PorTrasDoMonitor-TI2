using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PorTrasDoMonitor.Models;
using Owin;
using System;

namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            iniciaAplicacao();
        }


        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Admin'
            if (!roleManager.RoleExists("Admin"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // criar a Role 'Jornalista'
            if (!roleManager.RoleExists("Jornalista"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Jornalista";
                roleManager.Create(role);
            }  
            // criar a Role 'Utilizador'
            if (!roleManager.RoleExists("Utilizador"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }




            // criar um utilizador 'Admin'
            var user = new ApplicationUser();
            user.UserName = "admin@ipt.pt";
            user.Email = "admin@ipt.pt";

            string userPWD = "123Qwe.";
            var chkUser = userManager.Create(user, userPWD);
            Utilizadores utilizador = new Utilizadores {Avatar="default.jpg",ID=1,Descricao="Administrador da Aplicaçao web",Nome="Carlos Poupado",Username=user.Email,DataNascimento= new DateTime(1995, 2, 22) };


            // criar um utilizador 'Jornalista'
            var user1 = new ApplicationUser();
            user1.UserName = "Jornalista@ipt.com";
            user1.Email = "Jornalista@ipt.com";

            string userPWD2 = "123Qwe.";
            var chkUser2 = userManager.Create(user1, userPWD2);
            Utilizadores utilizador1 = new Utilizadores { Avatar = "default.jpg", ID = 2, Descricao = "Jornalista da Aplicaçao web", Nome = "António Silva", Username = user1.Email, DataNascimento = new DateTime(1970, 2, 09) };


            //Adicionar o Utilizador à respetiva Role-Admin
            if (chkUser.Succeeded)
            {
                db.Utilizadores.Add(utilizador);
                db.SaveChanges();
                var result1 = userManager.AddToRole(user.Id, "Admin");
         
            }
            if (chkUser2.Succeeded)
            {
                db.Utilizadores.Add(utilizador1);
                db.SaveChanges();
                var result2 = userManager.AddToRole(user1.Id, "Jornalista");
            }
        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97




    }
}
