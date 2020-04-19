namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsComplete2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MedicalTreatments", "IsComplete");
            DropColumn("dbo.MedicalCheckups", "IsComplete");
            DropColumn("dbo.PrescribedTests", "IsComplete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrescribedTests", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalCheckups", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalTreatments", "IsComplete", c => c.Boolean(nullable: false));
        }
    }
}
