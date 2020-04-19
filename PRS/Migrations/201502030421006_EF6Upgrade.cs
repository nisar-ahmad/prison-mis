namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EF6Upgrade : DbMigration
    {
        public override void Up()
        {
            // For Camp Jail
            RenameTable("FIRSections", "SectionFIRs");
            Sql("Update Prisoners SET Age = 26 WHERE Age = '26s'");
            Sql("Update Prisoners SET Age = 0 WHERE Age IS NULL");
            Sql("Update Occupations SET Name = 'Labourer / Construction Worker' WHERE OccupationId = 11");

            DropForeignKey("dbo.ExternalContacts", "PrisonerId", "dbo.Prisoners");
            DropIndex("dbo.ExternalContacts", new[] { "PrisonerId" });
            CreateTable(
                "dbo.CourtDecisions",
                c => new
                    {
                        CourtDecisionId = c.Int(nullable: false, identity: true),
                        DecisionStatus = c.Int(nullable: false),
                        DecisionAuthority = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        FIRId = c.Int(nullable: false),
                        CourtHearingId = c.Int(nullable: false),
                        AdmissionId = c.Int(nullable: false),
                        PrisonerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourtDecisionId)
                .ForeignKey("dbo.Admissions", t => t.AdmissionId)
                .ForeignKey("dbo.CourtHearings", t => t.CourtHearingId)
                .ForeignKey("dbo.FIRs", t => t.FIRId)
                .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
                .Index(t => t.FIRId)
                .Index(t => t.CourtHearingId)
                .Index(t => t.AdmissionId)
                .Index(t => t.PrisonerId);
            
            CreateTable(
                "dbo.CourtDecisionSections",
                c => new
                    {
                        CourtDecisionSectionId = c.Int(nullable: false, identity: true),
                        SectionDecisionType = c.Int(nullable: false),
                        SentenceType = c.Int(nullable: false),
                        SentenceYears = c.Int(nullable: false),
                        SentenceMonths = c.Int(nullable: false),
                        SentenceDays = c.Int(nullable: false),
                        FurtherSentenceType = c.Int(nullable: false),
                        FurtherSentenceYears = c.Int(nullable: false),
                        FurtherSentenceMonths = c.Int(nullable: false),
                        FurtherSentenceDays = c.Int(nullable: false),
                        FineAmount = c.Int(nullable: false),
                        IsFinePaid = c.Boolean(nullable: false),
                        Stripes = c.Int(nullable: false),
                        Is382BApplied = c.Boolean(nullable: false),
                        CourtDecisionType = c.Int(nullable: false),
                        Remarks = c.String(),
                        FIRId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        CourtDecisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourtDecisionSectionId)
                .ForeignKey("dbo.CourtDecisions", t => t.CourtDecisionId)
                .ForeignKey("dbo.FIRs", t => t.FIRId)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.FIRId)
                .Index(t => t.SectionId)
                .Index(t => t.CourtDecisionId);
            
            CreateTable(
                "dbo.EarnedRemissions",
                c => new
                    {
                        EarnedRemissionId = c.Int(nullable: false, identity: true),
                        DateOfRemission = c.DateTime(nullable: false),
                        RemissionBy = c.String(nullable: false),
                        LabourType = c.Int(nullable: false),
                        DateOfLabourStart = c.DateTime(),
                        DateOfLabourEnd = c.DateTime(),
                        Years = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        DateOfProbableRelease = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        RemissionTypeId = c.Int(nullable: false),
                        AdmissionId = c.Int(nullable: false),
                        PrisonerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EarnedRemissionId)
                .ForeignKey("dbo.Admissions", t => t.AdmissionId)
                .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
                .ForeignKey("dbo.RemissionTypes", t => t.RemissionTypeId)
                .Index(t => t.RemissionTypeId)
                .Index(t => t.AdmissionId)
                .Index(t => t.PrisonerId);
            
            CreateTable(
                "dbo.RemissionTypes",
                c => new
                    {
                        RemissionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RemissionTypeId);

            Sql("INSERT INTO RemissionTypes(Name)VALUES ('Ordinary')");
            Sql("INSERT INTO RemissionTypes(Name)VALUES ('AGCR')");
            Sql("INSERT INTO RemissionTypes(Name)VALUES ('SSR')");
            Sql("INSERT INTO RemissionTypes(Name)VALUES ('General Amnesty')");
            Sql("INSERT INTO RemissionTypes(Name)VALUES ('Education Remission')");
            
            CreateTable(
                "dbo.RemissionOrders",
                c => new
                    {
                        RemissionOrderId = c.Int(nullable: false, identity: true),
                        DateOfOrder = c.DateTime(nullable: false),
                        OrderBy = c.String(nullable: false),
                        Occasion = c.String(),
                        Reference = c.String(),
                        Description = c.String(),
                        RemissionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RemissionOrderId)
                .ForeignKey("dbo.RemissionTypes", t => t.RemissionTypeId)
                .Index(t => t.RemissionTypeId);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        VisitorType = c.Int(nullable: false),
                        RelationTypeId = c.Int(nullable: false),
                        CNIC = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        FatherOrHusbandName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        Designation = c.String(),
                        Department = c.String(),
                        AuthorityLetter = c.String(),
                        DateOfStart = c.DateTime(),
                        DateOfExpiry = c.DateTime(),
                        OccupationId = c.Int(),
                        NationalityCountryId = c.Int(nullable: false),
                        PresentCountryId = c.Int(nullable: false),
                        PresentProvinceId = c.Int(nullable: false),
                        PresentDistrictId = c.Int(nullable: false),
                        PresentCityId = c.Int(),
                        PresentMouza = c.String(),
                        PresentHouseNumber = c.String(),
                        PresentStreet = c.String(),
                        PresentAreaOrColony = c.String(),
                        Mobile = c.String(),
                        Landline = c.String(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        JailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitorId)
                .ForeignKey("dbo.Jails", t => t.JailId)
                .ForeignKey("dbo.Countries", t => t.NationalityCountryId)
                .ForeignKey("dbo.Occupations", t => t.OccupationId)
                .ForeignKey("dbo.Cities", t => t.PresentCityId)
                .ForeignKey("dbo.Countries", t => t.PresentCountryId)
                .ForeignKey("dbo.Districts", t => t.PresentDistrictId)
                .ForeignKey("dbo.Provinces", t => t.PresentProvinceId)
                .ForeignKey("dbo.RelationTypes", t => t.RelationTypeId)
                .Index(t => t.RelationTypeId)
                .Index(t => t.OccupationId)
                .Index(t => t.NationalityCountryId)
                .Index(t => t.PresentCountryId)
                .Index(t => t.PresentProvinceId)
                .Index(t => t.PresentDistrictId)
                .Index(t => t.PresentCityId)
                .Index(t => t.JailId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        VisitId = c.Int(nullable: false, identity: true),
                        DateOfVisit = c.DateTime(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        BatchNumber = c.Int(nullable: false),
                        LuggageDetails = c.String(),
                        PurposeOfVisit = c.String(),
                        MobileTokenNumber = c.Int(nullable: false),
                        Remarks = c.String(),
                        PrisonerId = c.Int(nullable: false),
                        VisitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitId)
                .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
                .ForeignKey("dbo.Visitors", t => t.VisitorId)
                .Index(t => t.PrisonerId)
                .Index(t => t.VisitorId);
            
            AddColumn("dbo.Settings", "ProvinceId", c => c.Int(nullable: false, defaultValue: 7));
            AddColumn("dbo.Settings", "DistrictId", c => c.Int(nullable: false, defaultValue: 104));
            AddColumn("dbo.Settings", "CityId", c => c.Int(nullable: false, defaultValue: 104));            
            AlterColumn("dbo.Prisoners", "Age", c => c.Int(nullable: false));
            CreateIndex("dbo.Settings", "ProvinceId");
            CreateIndex("dbo.Settings", "DistrictId");
            CreateIndex("dbo.Settings", "CityId");
            AddForeignKey("dbo.Settings", "CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.Settings", "DistrictId", "dbo.Districts", "DistrictId");
            AddForeignKey("dbo.Settings", "ProvinceId", "dbo.Provinces", "ProvinceId");
            DropTable("dbo.ExternalContacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExternalContacts",
                c => new
                    {
                        ExternalContactId = c.Int(nullable: false, identity: true),
                        PrisonerId = c.Int(nullable: false),
                        ContactPerson = c.Int(nullable: false),
                        TypeOfContact = c.Int(nullable: false),
                        DateOfContact = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ExternalContactId);
            
            DropForeignKey("dbo.Visits", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.Visits", "PrisonerId", "dbo.Prisoners");
            DropForeignKey("dbo.Visitors", "RelationTypeId", "dbo.RelationTypes");
            DropForeignKey("dbo.Visitors", "PresentProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Visitors", "PresentDistrictId", "dbo.Districts");
            DropForeignKey("dbo.Visitors", "PresentCountryId", "dbo.Countries");
            DropForeignKey("dbo.Visitors", "PresentCityId", "dbo.Cities");
            DropForeignKey("dbo.Visitors", "OccupationId", "dbo.Occupations");
            DropForeignKey("dbo.Visitors", "NationalityCountryId", "dbo.Countries");
            DropForeignKey("dbo.Visitors", "JailId", "dbo.Jails");
            DropForeignKey("dbo.Settings", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Settings", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Settings", "CityId", "dbo.Cities");
            DropForeignKey("dbo.RemissionOrders", "RemissionTypeId", "dbo.RemissionTypes");
            DropForeignKey("dbo.EarnedRemissions", "RemissionTypeId", "dbo.RemissionTypes");
            DropForeignKey("dbo.EarnedRemissions", "PrisonerId", "dbo.Prisoners");
            DropForeignKey("dbo.EarnedRemissions", "AdmissionId", "dbo.Admissions");
            DropForeignKey("dbo.CourtDecisions", "PrisonerId", "dbo.Prisoners");
            DropForeignKey("dbo.CourtDecisions", "FIRId", "dbo.FIRs");
            DropForeignKey("dbo.CourtDecisions", "CourtHearingId", "dbo.CourtHearings");
            DropForeignKey("dbo.CourtDecisionSections", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.CourtDecisionSections", "FIRId", "dbo.FIRs");
            DropForeignKey("dbo.CourtDecisionSections", "CourtDecisionId", "dbo.CourtDecisions");
            DropForeignKey("dbo.CourtDecisions", "AdmissionId", "dbo.Admissions");
            DropIndex("dbo.Visits", new[] { "VisitorId" });
            DropIndex("dbo.Visits", new[] { "PrisonerId" });
            DropIndex("dbo.Visitors", new[] { "JailId" });
            DropIndex("dbo.Visitors", new[] { "PresentCityId" });
            DropIndex("dbo.Visitors", new[] { "PresentDistrictId" });
            DropIndex("dbo.Visitors", new[] { "PresentProvinceId" });
            DropIndex("dbo.Visitors", new[] { "PresentCountryId" });
            DropIndex("dbo.Visitors", new[] { "NationalityCountryId" });
            DropIndex("dbo.Visitors", new[] { "OccupationId" });
            DropIndex("dbo.Visitors", new[] { "RelationTypeId" });
            DropIndex("dbo.Settings", new[] { "CityId" });
            DropIndex("dbo.Settings", new[] { "DistrictId" });
            DropIndex("dbo.Settings", new[] { "ProvinceId" });
            DropIndex("dbo.RemissionOrders", new[] { "RemissionTypeId" });
            DropIndex("dbo.EarnedRemissions", new[] { "PrisonerId" });
            DropIndex("dbo.EarnedRemissions", new[] { "AdmissionId" });
            DropIndex("dbo.EarnedRemissions", new[] { "RemissionTypeId" });
            DropIndex("dbo.CourtDecisionSections", new[] { "CourtDecisionId" });
            DropIndex("dbo.CourtDecisionSections", new[] { "SectionId" });
            DropIndex("dbo.CourtDecisionSections", new[] { "FIRId" });
            DropIndex("dbo.CourtDecisions", new[] { "PrisonerId" });
            DropIndex("dbo.CourtDecisions", new[] { "AdmissionId" });
            DropIndex("dbo.CourtDecisions", new[] { "CourtHearingId" });
            DropIndex("dbo.CourtDecisions", new[] { "FIRId" });
            AlterColumn("dbo.Prisoners", "Age", c => c.String());
            DropColumn("dbo.Settings", "CityId");
            DropColumn("dbo.Settings", "DistrictId");
            DropColumn("dbo.Settings", "ProvinceId");
            DropTable("dbo.Visits");
            DropTable("dbo.Visitors");
            DropTable("dbo.RemissionOrders");
            DropTable("dbo.RemissionTypes");
            DropTable("dbo.EarnedRemissions");
            DropTable("dbo.CourtDecisionSections");
            DropTable("dbo.CourtDecisions");
            CreateIndex("dbo.ExternalContacts", "PrisonerId");
            AddForeignKey("dbo.ExternalContacts", "PrisonerId", "dbo.Prisoners", "PrisonerId");
        }
    }
}
