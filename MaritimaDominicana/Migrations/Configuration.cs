namespace MaritimaDominicana.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MaritimaDominicana.Models.SupportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MaritimaDominicana.Models.SupportContext context)
        {
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

            context.Clients.AddOrUpdate(
                c => c.ClientId,
                new Models.Client { ClientId = 1, Name = "Maritima Dominicana", Email = "maritima@hotmail.com", Telephone = "8099905050" },
                new Models.Client { ClientId = 2, Name = "ABCD" , Email = "abcd@gmail.com" , Telephone="8099097865"},
                new Models.Client { ClientId = 3, Name = "Altice", Email = "mvp@altice.com", Telephone = "8099875678" },
                new Models.Client { ClientId = 4, Name = "Grupo Ramos", Email = "mvp@gruporamos.com", Telephone = "8097656576" }
                );

            context.Places.AddOrUpdate(
                p => p.PlaceId,
                new Models.Place { PlaceId = 1, Name = "Desarrollo" },
                new Models.Place { PlaceId = 2, Name = "Soporte" }
                );
            context.Types.AddOrUpdate(
                t => t.TypeId,
                new Models.Type {TypeId = 1, Name = "Admin"},
                new Models.Type {TypeId = 2, Name = "Member"}
                );

            string[] passwords = new string[3];
            passwords[0] = User.EncyptPassword("Jz070192.");
            passwords[1] = User.EncyptPassword("Alejandro");
            passwords[2] = User.EncyptPassword("Alfredo");

            context.Users.AddOrUpdate(
                u => u.UserId,
                new User {
                    UserId =1,
                    FirstName = "Julian",
                    LastName ="Zapata",
                    Pasword = passwords[0],
                    Email = "jl.zapata0@gmail.com",
                    TypeId = 1,
                    Active = true
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Alejandro",
                    LastName = "Zapata",
                    Pasword = passwords[1],
                    Email = "alejandro@gmail.com",
                    TypeId = 2,
                    Active = true
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Alfredo",
                    LastName = "Zapata",
                    Pasword = passwords[2],
                    Email = "Alfredo@gmail.com",
                    TypeId = 2,
                    Active = true
                },

            new User
            {
                UserId = 4,
                FirstName = "Aristides",
                LastName = "Villalona",
                Pasword = passwords[1],
                Email = "Aristides@gmail.com",
                TypeId = 2,
                Active = true
            },
            
            new User
            {
                UserId = 5,
                FirstName = "David",
                LastName = "Ortega",
                Pasword = passwords[1],
                Email = "david@gmail.com",
                TypeId = 2,
                Active = true
            }
            );

            context.Departments.AddOrUpdate(
                d=> d.DepartmentId,
                new Department {DepartmentId =1, Name = "IT"},
                new Department { DepartmentId = 2, Name = "Ventas" },
                new Department { DepartmentId = 3, Name = "Recursos Humanos" },
                new Department { DepartmentId = 4, Name = "Produccion" },
                new Department { DepartmentId = 5, Name = "Compras" }
                );

            context.Problems.AddOrUpdate(
                p => p.ProblemId,
                new Problem {ProblemId = 1, Name ="PC no enciende"},
                new Problem { ProblemId = 2, Name = "PC sin conexion a internet" },
                new Problem { ProblemId = 3, Name = "Dispositivo de entrada no funciona" },
                new Problem { ProblemId = 4, Name = "Dispositivo de salida no funciona" },
                new Problem { ProblemId = 5, Name = "Servidor" },
                new Problem { ProblemId = 6, Name = "ISP" },
                new Problem { ProblemId = 7, Name = "Acceso al Sistema" }
                );

            context.ProblemDetails.AddOrUpdate(
                p => p.ProblemDetailId,
                new ProblemDetail
                {
                    ProblemDetailId = 1,
                    ProblemId = 1,
                    Title = "Pc-04 no enciende",
                    DepartmentId = 1,
                    CreatedBy = 1,
                    Description = "Pc-04 no enciende y esta correctamente conectada a la corriente. Se probo el enchunfe.",
                    Date = new DateTime(2017, 05, 24, 05, 50, 00),
                    PlaceId = 1,
                    state = 1,
                    ClientId = 1
                    
                },
                new ProblemDetail
                {
                    ProblemDetailId = 2,
                    ProblemId = 2,
                    Title = "Pc-04 no enciende",
                    DepartmentId = 2,
                    CreatedBy = 2,
                    Description = "Pc-04 no navega y esta correctamente conectada. Se probo la conexion con otro equipo.",
                    Date = new DateTime(2017, 05, 25, 05, 50, 00),
                    PlaceId = 2,
                    state = 1,
                    ClientId = 2

                },
                 new ProblemDetail
                 {
                     ProblemDetailId = 3,
                     ProblemId = 1,
                     Title = "Pc-04 no enciende",
                     DepartmentId = 1,
                     CreatedBy = 2,
                     Description = "Pc-04 no enciende y esta correctamente conectada a la corriente. Se probo el enchunfe",
                     Date = DateTime.Now,
                     PlaceId = 2,
                     state = 1,
                     ClientId = 2

                 },
                  new ProblemDetail
                  {
                      ProblemDetailId = 4,
                      ProblemId = 3,
                      Title = "Mouse no funciona.",
                      DepartmentId = 1,
                      CreatedBy = 1,
                      Description = "Click derecho del mouse no funciona.Estacion 04.",
                      Date = new DateTime(2017, 05, 25, 11, 43, 54),
                      PlaceId = 1,
                      state = 2,
                      ClientId = 1,
                      AssignedTo = 2,
                      AssignedAt = new DateTime(2017, 05, 25, 11, 59, 29)

                  },

                   new ProblemDetail
                   {
                       ProblemDetailId = 5,
                       ProblemId = 4,
                       Title = "No audio.",
                       DepartmentId = 1,
                       CreatedBy = 1,
                       Description = "Los dispositivos de sonido no funcionan.",
                       Date = new DateTime(2017, 05, 25, 11, 45, 32),
                       PlaceId = 1,
                       state = 2,
                       ClientId = 1,
                       AssignedTo = 2,
                       AssignedAt = new DateTime(2017, 05, 25, 11, 59, 29)
                   },

                   new ProblemDetail
                   {
                       ProblemDetailId = 6,
                       ProblemId = 2,
                       Title = "Pc-04 no navega.",
                       DepartmentId = 4,
                       CreatedBy = 1,
                       Description = "PC-04 tiene conexion a internet pero no navega.",
                       Date = new DateTime(2017, 05, 25, 11, 45, 32),
                       PlaceId = 1,
                       state = 3,
                       ClientId = 1
                   }


                );

            context.Solutions.AddOrUpdate(
                s => s.SolutionId,
                new Solution
                {
                    SolutionId = 1,
                    SolutionDescription = "Cpu averiado. Se reemplazo.",
                    UserId = 1,
                    Date = new DateTime(2017, 05, 26, 02, 37, 10)
                },
                new Solution
                {
                    SolutionId = 3,
                    SolutionDescription = "Configuracion de tarjeta de red inadecuada. Solucionado.",
                    UserId = 1,
                    Date = new DateTime(2017, 05, 26, 02, 34, 10)
                },

                 new Solution
                 {
                     SolutionId = 6,
                     SolutionDescription = "Bocinas dañadas. Fueron reemplazadas.",
                     UserId = 1,
                     Date = new DateTime(2017, 05, 26, 02, 33, 10)
                 }


                );
        }


    }
}
