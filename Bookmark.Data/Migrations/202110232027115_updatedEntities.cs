namespace Bookmark.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "NoteId", "dbo.Note");
            DropIndex("dbo.Book", new[] { "NoteId" });
            AddColumn("dbo.Note", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Note", "BookId");
            AddForeignKey("dbo.Note", "BookId", "dbo.Book", "BookId", cascadeDelete: true);
            DropColumn("dbo.Book", "NoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "NoteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Note", "BookId", "dbo.Book");
            DropIndex("dbo.Note", new[] { "BookId" });
            DropColumn("dbo.Note", "BookId");
            CreateIndex("dbo.Book", "NoteId");
            AddForeignKey("dbo.Book", "NoteId", "dbo.Note", "NoteId", cascadeDelete: true);
        }
    }
}
