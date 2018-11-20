namespace HR.WebApi.Migrations
{
    using HR.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HR.WebApi.Repositories.Common.DbContextRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HR.WebApi.Repositories.Common.DbContextRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            

            //var user1 = new User() { FirstName = "Sumit", Email = "sumit@gmail.com", Accounts = new List<Account>() };
            //var user2 = new User() { FirstName = "Rahul", Email = "rahul@gmail.com", Accounts = new List<Account>() };
            //var user3 = new User() { FirstName = "Anto", Email = "anto@gmail.com", Accounts = new List<Account>() };


            //var account1 = new Account() { AccName = "facebook", };
            //var account2 = new Account() { AccName = "google" };
            //var account3 = new Account() { AccName = "twitter" };
            //var setting1 = new UserSetting() { Settings = "test" };

            //user1.Accounts.Add(account1);
            //user1.Accounts.Add(account1);
            //user1.Accounts.Add(account1);
            //user1.Accounts.Add(account2);
            //user1.Accounts.Add(account2);
            //user1.Accounts.Add(account2);
            //user1.Accounts.Add(account3);
            //user1.Accounts.Add(account3);
            //user1.Accounts.Add(account3);

            //user2.Accounts.Add(account1);
            //user2.Accounts.Add(account1);
            //user2.Accounts.Add(account1);
            //user2.Accounts.Add(account2);
            //user2.Accounts.Add(account2);
            //user2.Accounts.Add(account2);
            //user2.Accounts.Add(account3);
            //user2.Accounts.Add(account3);
            //user2.Accounts.Add(account3);

            //user3.Accounts.Add(account1);
            //user3.Accounts.Add(account1);
            //user3.Accounts.Add(account1);
            //user3.Accounts.Add(account2);
            //user3.Accounts.Add(account2);
            //user3.Accounts.Add(account2);
            //user3.Accounts.Add(account3);
            //user3.Accounts.Add(account3);
            //user3.Accounts.Add(account3);
            

            //context.Users.Add(user1);
            //context.Users.Add(user2);
            //context.Users.Add(user3);
           
            //context.SaveChanges();
           
        }
    }
}
