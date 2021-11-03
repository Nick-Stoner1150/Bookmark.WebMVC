namespace Bookmark.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedProgress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Book", "BookProgress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "BookProgress", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
