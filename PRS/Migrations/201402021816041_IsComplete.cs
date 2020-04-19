namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsComplete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prisoners", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Admissions", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Admissions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.FIRs", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.FIRs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourtHearings", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourtHearings", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalTreatments", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalTreatments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalCheckups", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalCheckups", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.PrescribedTests", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.PrescribedTests", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescribedTests", "IsActive");
            DropColumn("dbo.PrescribedTests", "IsComplete");
            DropColumn("dbo.MedicalCheckups", "IsActive");
            DropColumn("dbo.MedicalCheckups", "IsComplete");
            DropColumn("dbo.MedicalTreatments", "IsActive");
            DropColumn("dbo.MedicalTreatments", "IsComplete");
            DropColumn("dbo.CourtHearings", "IsActive");
            DropColumn("dbo.CourtHearings", "IsComplete");
            DropColumn("dbo.FIRs", "IsActive");
            DropColumn("dbo.FIRs", "IsComplete");
            DropColumn("dbo.Admissions", "IsActive");
            DropColumn("dbo.Admissions", "IsComplete");
            DropColumn("dbo.Prisoners", "IsActive");
        }
    }
}
