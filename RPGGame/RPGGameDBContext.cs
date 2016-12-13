namespace TeamAppleThief
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Migrations;
    using Objects;
    using System.Linq;

    public class RPGGameDBContext : DbContext
    {
        // Your context has been configured to use a 'RPGGameDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TeamAppleThief.RPGGameDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RPGGameDBContext' 
        // connection string in the application configuration file.
        public RPGGameDBContext()
            : base("name=RPGGameDBContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<RPGGameDBContext, TeamAppleThief.Migrations.Configuration>());
        }

        public virtual DbSet<TeamAppleThief.Objects.Characters.PlayableCharacters.Player> Players { get; set; }
        public virtual DbSet<TeamAppleThief.Objects.Items.Potion> ActivePotions { get; set; }
        public virtual DbSet<TeamAppleThief.Objects.Items.Item> Items { get; set; }
        public virtual DbSet<TeamAppleThief.Objects.Items.Equipment.Equipment> EquipedItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        //public class MyEntity
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}
    }
}