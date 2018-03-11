namespace VkNet.Examples.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VkNet.Examples.DataBaseBehaviour.BotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "VkNet.Examples.DataBaseBehaviour.BotContext";
            ContextKey = "BotContext";
        }

        protected override void Seed(VkNet.Examples.DataBaseBehaviour.BotContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
