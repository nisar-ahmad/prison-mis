namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Court : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admissions", "BarrackId", c => c.Int());
            AlterColumn("dbo.Admissions", "DateOfRelease", c => c.DateTime());
            AlterColumn("dbo.Courts", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courts", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Admissions", "DateOfRelease", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Admissions", "BarrackId", c => c.Int(nullable: false));
        }
    }
}
