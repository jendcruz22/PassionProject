namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPoster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Poster", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Poster");
        }
    }
}
