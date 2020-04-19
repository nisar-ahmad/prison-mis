namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JudgeType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourtHearings", "JudgeType_JudgeTypeId", "dbo.JudgeTypes");
            DropIndex("dbo.CourtHearings", new[] { "JudgeType_JudgeTypeId" });
            RenameColumn(table: "dbo.FIRs", name: "JudgeType_JudgeTypeId", newName: "JudgeTypeId");
            AddColumn("dbo.CourtHearings", "JudgeTypeId", c => c.Int());
            AddForeignKey("dbo.CourtHearings", "JudgeTypeId", "dbo.JudgeTypes", "JudgeTypeId");
            CreateIndex("dbo.CourtHearings", "JudgeTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CourtHearings", new[] { "JudgeTypeId" });
            DropForeignKey("dbo.CourtHearings", "JudgeTypeId", "dbo.JudgeTypes");
            DropColumn("dbo.CourtHearings", "JudgeTypeId");
            RenameColumn(table: "dbo.FIRs", name: "JudgeTypeId", newName: "JudgeType_JudgeTypeId");
            CreateIndex("dbo.CourtHearings", "JudgeType_JudgeTypeId");
            AddForeignKey("dbo.CourtHearings", "JudgeType_JudgeTypeId", "dbo.JudgeTypes", "JudgeTypeId");
        }
    }
}
