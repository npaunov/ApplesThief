using TeamAppleThief.Objects.Characters.PlayableCharacters;

namespace TeamAppleThief.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal class Configuration : DbMigrationsConfiguration<TeamAppleThief.RPGGameDBContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeamAppleThief.RPGGameDBContext context)
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

            if (context.Players.Any())
            {
                return;
            }

            this.SeedPlayers(context);
        }

        protected void SeedPlayers(RPGGameDBContext context)
        {
            var players = new Player[] {new Warrior(0, 0), new Rogue(0, 0), new Wizard(0, 0)};
            context.Players.AddOrUpdate(x=>x.Name,players);
            context.SaveChanges();
        }
    }
}
