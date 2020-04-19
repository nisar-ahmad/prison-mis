namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtHearings",
                c => new
                    {
                        CourtHearingId = c.Int(nullable: false, identity: true),
                        FIRId = c.Int(nullable: false),
                        CourtId = c.Int(nullable: false),
                        JudgeId = c.Int(nullable: false),
                        DateOfCourtOrder = c.DateTime(nullable: false),
                        DateOfHearing = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        AdmissionId = c.Int(nullable: false),
                        PrisonerId = c.Int(nullable: false),
                        ApprovalStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourtHearingId)
                .ForeignKey("dbo.FIRs", t => t.FIRId)
                .ForeignKey("dbo.Courts", t => t.CourtId)
                .ForeignKey("dbo.Judges", t => t.JudgeId)
                .ForeignKey("dbo.Admissions", t => t.AdmissionId)
                .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
                .Index(t => t.FIRId)
                .Index(t => t.CourtId)
                .Index(t => t.JudgeId)
                .Index(t => t.AdmissionId)
                .Index(t => t.PrisonerId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CourtHearings", new[] { "PrisonerId" });
            DropIndex("dbo.CourtHearings", new[] { "AdmissionId" });
            DropIndex("dbo.CourtHearings", new[] { "JudgeId" });
            DropIndex("dbo.CourtHearings", new[] { "CourtId" });
            DropIndex("dbo.CourtHearings", new[] { "FIRId" });
            DropForeignKey("dbo.CourtHearings", "PrisonerId", "dbo.Prisoners");
            DropForeignKey("dbo.CourtHearings", "AdmissionId", "dbo.Admissions");
            DropForeignKey("dbo.CourtHearings", "JudgeId", "dbo.Judges");
            DropForeignKey("dbo.CourtHearings", "CourtId", "dbo.Courts");
            DropForeignKey("dbo.CourtHearings", "FIRId", "dbo.FIRs");
            DropTable("dbo.CourtHearings");
        }
    }
}
