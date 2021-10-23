namespace Bookmark.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDateTimeOffset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Note", "ModifiedDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "ModifiedDate");
            DropColumn("dbo.Note", "CreatedDate");
        }
    }
}
