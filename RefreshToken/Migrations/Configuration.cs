using System.Collections.Generic;
using RefreshToken.Models;

namespace RefreshToken.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            context.Clients.AddRange(BuildClientsList());
            context.SaveChanges();
        }

        private static IEnumerable<Client> BuildClientsList()
        {

            var clientsList = new List<Client>
            {
                new Client
                {
                    Id = "webApp",
                    Secret = Helper.GetHash("abc@123"),
                    Name = "Web Client",
                    ApplicationType = Models.ApplicationTypes.Web,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://localhost:62032/"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret = Helper.GetHash("123@abc"),
                    Name = "Console Application",
                    ApplicationType = Models.ApplicationTypes.Console,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return clientsList;
        }
    }
}