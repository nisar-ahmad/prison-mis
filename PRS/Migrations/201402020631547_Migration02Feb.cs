namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration02Feb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admissions", "AuthorityForRelease", c => c.String());
            AddColumn("dbo.MedicalCheckups", "DateOfCheckup", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalCheckups", "DateOfDischarge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalCheckups", "DateOfDischarge", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalCheckups", "DateOfCheckup");
            DropColumn("dbo.Admissions", "AuthorityForRelease");
        }
    }
}
