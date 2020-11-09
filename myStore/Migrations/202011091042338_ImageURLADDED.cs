namespace myStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURLADDED : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImageURL");
        }
    }
}
