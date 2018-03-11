namespace VkNet.Examples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Bot.LoginPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Bot.SearchFilterInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilterName = c.String(),
                        Count = c.Int(),
                        Sex = c.Int(),
                        FamilyState = c.Int(),
                        YearMin = c.Int(),
                        YearMax = c.Int(),
                        CountryId = c.Int(),
                        CityId = c.Int(),
                        FriendsMin = c.Int(),
                        FriendsMax = c.Int(),
                        SubscribersMin = c.Int(),
                        SubscribersMax = c.Int(),
                        IsOnline = c.Boolean(),
                        HasPhoto = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Bot.SearchFilterInfos");
            DropTable("Bot.LoginPasswords");
        }
    }
}
