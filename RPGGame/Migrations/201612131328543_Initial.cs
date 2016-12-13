namespace TeamAppleThief.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpeedPointsBuff = c.Int(nullable: false),
                        DefensePointsBuff = c.Int(nullable: false),
                        AttackPointsBuff = c.Int(nullable: false),
                        HealthPointsBuff = c.Int(nullable: false),
                        Name = c.Int(nullable: false),
                        Duration = c.Int(),
                        Slot = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Drinker_Id = c.Int(),
                        Owner_Id = c.Int(nullable: false),
                        Holder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Drinker_Id)
                .ForeignKey("dbo.Players", t => t.Owner_Id)
                .ForeignKey("dbo.Players", t => t.Holder_Id)
                .Index(t => t.Drinker_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Holder_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Health = c.Int(nullable: false),
                        Name = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Holder_Id", "dbo.Players");
            DropForeignKey("dbo.Items", "Owner_Id", "dbo.Players");
            DropForeignKey("dbo.Items", "Drinker_Id", "dbo.Players");
            DropIndex("dbo.Items", new[] { "Holder_Id" });
            DropIndex("dbo.Items", new[] { "Owner_Id" });
            DropIndex("dbo.Items", new[] { "Drinker_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.Items");
        }
    }
}
