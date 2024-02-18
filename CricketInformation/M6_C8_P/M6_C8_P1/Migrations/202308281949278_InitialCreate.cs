namespace M6_C8_P1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
                      
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 50),
                        Established = c.DateTime(nullable: false, storeType: "date"),
                        Country = c.Int(nullable: false),
                        Coach = c.String(nullable: false, maxLength: 50),
                        Captain = c.String(nullable: false, maxLength: 50),
                        BoardPresident = c.String(nullable: false, maxLength: 50),
                        Ranking = c.Int(nullable: false),
                        TeamLogo = c.String(nullable: false, maxLength: 50),
                        HomeGround = c.String(nullable: false, maxLength: 100),
                        IsWCWinner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PlayerName = c.String(nullable: false, maxLength: 50),
                    Age = c.Int(nullable: false),
                    PlayingRole = c.String(nullable: false, maxLength: 50),
                    Matches = c.Int(nullable: false),
                    Runs = c.Int(nullable: false),
                    Hundred = c.Int(nullable: false),
                    Fifty = c.Int(nullable: false),
                    Wicket = c.Int(nullable: false),
                    Salary = c.Decimal(nullable: false, storeType: "money"),
                    TeamId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);

            CreateStoredProcedure("dbo.DeleteTeam",
                p => new { id = p.Int() },
                "DELETE FROM dbo.Teams WHERE id=@id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropStoredProcedure("dbo.DeleteTeam");

        }
    }
}
