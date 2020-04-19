namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Acts",
            //    c => new
            //        {
            //            ActId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            ShortName = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ActId);
            
            //CreateTable(
            //    "dbo.Admissions",
            //    c => new
            //        {
            //            AdmissionId = c.Int(nullable: false, identity: true),
            //            DateOfAdmission = c.DateTime(nullable: false),
            //            PrisonerNumber = c.String(nullable: false),
            //            BarrackId = c.Int(),
            //            BlockNumber = c.String(),
            //            CellNumber = c.String(),
            //            DateOfWarrantCommitment = c.DateTime(),
            //            DateOfRemand = c.DateTime(),
            //            Remarks = c.String(),
            //            HealthOnAdmission = c.String(),
            //            WeightOnAdmission = c.String(),
            //            KnownAilment = c.String(),
            //            DiseaseId = c.Int(),
            //            ExplainedInjuries = c.String(),
            //            UnexplainedInjuries = c.String(),
            //            MedicalRemarks = c.String(),
            //            DateOfRelease = c.DateTime(),
            //            DecisionStatus = c.Int(nullable: false),
            //            JudgeTypeId = c.Int(),
            //            AuthorityForRelease = c.String(),
            //            HealthOnRelease = c.String(),
            //            WeightOnRelease = c.String(),
            //            PrisonerId = c.Int(nullable: false),
            //            ApprovalStatus = c.Int(nullable: false),
            //            IsComplete = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            Act_ActId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.AdmissionId)
            //    .ForeignKey("dbo.Barracks", t => t.BarrackId)
            //    .ForeignKey("dbo.Diseases", t => t.DiseaseId)
            //    .ForeignKey("dbo.JudgeTypes", t => t.JudgeTypeId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .ForeignKey("dbo.Acts", t => t.Act_ActId)
            //    .Index(t => t.BarrackId)
            //    .Index(t => t.DiseaseId)
            //    .Index(t => t.JudgeTypeId)
            //    .Index(t => t.PrisonerId)
            //    .Index(t => t.Act_ActId);
            
            //CreateTable(
            //    "dbo.Barracks",
            //    c => new
            //        {
            //            BarrackId = c.Int(nullable: false, identity: true),
            //            JailId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BarrackId)
            //    .ForeignKey("dbo.Jails", t => t.JailId)
            //    .Index(t => t.JailId);
            
            //CreateTable(
            //    "dbo.Jails",
            //    c => new
            //        {
            //            JailId = c.Int(nullable: false, identity: true),
            //            DistrictId = c.Int(nullable: false),
            //            JailType = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //            Capacity = c.Int(nullable: false),
            //            Address = c.String(),
            //            Telephone = c.String(),
            //            SuperintendentName = c.String(),
            //            SuperintendentContactNumber = c.String(),
            //        })
            //    .PrimaryKey(t => t.JailId)
            //    .ForeignKey("dbo.Districts", t => t.DistrictId)
            //    .Index(t => t.DistrictId);
            
            //CreateTable(
            //    "dbo.Districts",
            //    c => new
            //        {
            //            DistrictId = c.Int(nullable: false, identity: true),
            //            ProvinceId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.DistrictId)
            //    .ForeignKey("dbo.Provinces", t => t.ProvinceId)
            //    .Index(t => t.ProvinceId);
            
            //CreateTable(
            //    "dbo.Cities",
            //    c => new
            //        {
            //            CityId = c.Int(nullable: false, identity: true),
            //            DistrictId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CityId)
            //    .ForeignKey("dbo.Districts", t => t.DistrictId)
            //    .Index(t => t.DistrictId);
            
            //CreateTable(
            //    "dbo.PoliceStations",
            //    c => new
            //        {
            //            PoliceStationId = c.Int(nullable: false, identity: true),
            //            DistrictId = c.Int(nullable: false),
            //            CityId = c.Int(),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PoliceStationId)
            //    .ForeignKey("dbo.Cities", t => t.CityId)
            //    .ForeignKey("dbo.Districts", t => t.DistrictId)
            //    .Index(t => t.DistrictId)
            //    .Index(t => t.CityId);
            
            //CreateTable(
            //    "dbo.Prisoners",
            //    c => new
            //        {
            //            PrisonerId = c.Int(nullable: false, identity: true),
            //            FMD1 = c.String(),
            //            JailId = c.Int(nullable: false),
            //            Status = c.Int(nullable: false),
            //            Category = c.Int(nullable: false),
            //            Class = c.Int(nullable: false),
            //            PrisonerTypeId = c.Int(nullable: false),
            //            PrisonerSubTypeId = c.Int(),
            //            CNIC = c.String(),
            //            Name = c.String(nullable: false),
            //            FatherOrHusbandName = c.String(nullable: false),
            //            PaternalGrandfatherName = c.String(nullable: false),
            //            MaternalGrandfatherName = c.String(),
            //            DateOfBirth = c.DateTime(),
            //            Gender = c.Int(nullable: false),
            //            MaritalStatus = c.Int(nullable: false),
            //            NarcoticsStatus = c.Int(nullable: false),
            //            ReligionId = c.Int(nullable: false),
            //            OccupationId = c.Int(nullable: false),
            //            NationalityCountryId = c.Int(nullable: false),
            //            Alias = c.String(),
            //            Caste = c.String(),
            //            PresentCountryId = c.Int(nullable: false),
            //            PresentProvinceId = c.Int(nullable: false),
            //            PresentDistrictId = c.Int(nullable: false),
            //            PresentCityId = c.Int(),
            //            PresentPoliceStationId = c.Int(),
            //            PresentMouza = c.String(),
            //            PresentHouseNumber = c.String(),
            //            PresentStreet = c.String(),
            //            PresentAreaOrColony = c.String(),
            //            PermanentCountryId = c.Int(nullable: false),
            //            PermanentProvinceId = c.Int(nullable: false),
            //            PermanentDistrictId = c.Int(nullable: false),
            //            PermanentCityId = c.Int(),
            //            PermanentPoliceStationId = c.Int(),
            //            PermanentMouza = c.String(),
            //            PermanentHouseNumber = c.String(),
            //            PermanentStreet = c.String(),
            //            PermanentAreaOrColony = c.String(),
            //            FormalEducationLevelId = c.Int(),
            //            TechnicalEducationLevelId = c.Int(),
            //            ReligiousEducationLevelId = c.Int(),
            //            NextOfKinName = c.String(),
            //            NextOfKinRelationTypeId = c.Int(),
            //            NextOfKinContact = c.String(),
            //            MoveableAssets = c.String(),
            //            ImmoveableAssets = c.String(),
            //            Remarks = c.String(),
            //            IdentificationMark1 = c.String(),
            //            IdentificationMark2 = c.String(),
            //            Age = c.String(),
            //            Scar = c.String(),
            //            Height = c.String(),
            //            Weight = c.String(),
            //            BloodGroup = c.String(),
            //            ApprovalStatus = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            EducationLevel_EducationLevelId = c.Int(),
            //            Country_CountryId = c.Int(),
            //            Province_ProvinceId = c.Int(),
            //            PoliceStation_PoliceStationId = c.Int(),
            //            City_CityId = c.Int(),
            //            District_DistrictId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.PrisonerId)
            //    .ForeignKey("dbo.EducationLevels", t => t.EducationLevel_EducationLevelId)
            //    .ForeignKey("dbo.EducationLevels", t => t.FormalEducationLevelId)
            //    .ForeignKey("dbo.Jails", t => t.JailId)
            //    .ForeignKey("dbo.Countries", t => t.Country_CountryId)
            //    .ForeignKey("dbo.Provinces", t => t.Province_ProvinceId)
            //    .ForeignKey("dbo.Countries", t => t.NationalityCountryId)
            //    .ForeignKey("dbo.RelationTypes", t => t.NextOfKinRelationTypeId)
            //    .ForeignKey("dbo.Occupations", t => t.OccupationId)
            //    .ForeignKey("dbo.Cities", t => t.PermanentCityId)
            //    .ForeignKey("dbo.Countries", t => t.PermanentCountryId)
            //    .ForeignKey("dbo.Districts", t => t.PermanentDistrictId)
            //    .ForeignKey("dbo.PoliceStations", t => t.PermanentPoliceStationId)
            //    .ForeignKey("dbo.Provinces", t => t.PermanentProvinceId)
            //    .ForeignKey("dbo.Cities", t => t.PresentCityId)
            //    .ForeignKey("dbo.Countries", t => t.PresentCountryId)
            //    .ForeignKey("dbo.Districts", t => t.PresentDistrictId)
            //    .ForeignKey("dbo.PoliceStations", t => t.PresentPoliceStationId)
            //    .ForeignKey("dbo.Provinces", t => t.PresentProvinceId)
            //    .ForeignKey("dbo.PrisonerSubTypes", t => t.PrisonerSubTypeId)
            //    .ForeignKey("dbo.PrisonerTypes", t => t.PrisonerTypeId)
            //    .ForeignKey("dbo.Religions", t => t.ReligionId)
            //    .ForeignKey("dbo.EducationLevels", t => t.ReligiousEducationLevelId)
            //    .ForeignKey("dbo.EducationLevels", t => t.TechnicalEducationLevelId)
            //    .ForeignKey("dbo.PoliceStations", t => t.PoliceStation_PoliceStationId)
            //    .ForeignKey("dbo.Cities", t => t.City_CityId)
            //    .ForeignKey("dbo.Districts", t => t.District_DistrictId)
            //    .Index(t => t.JailId)
            //    .Index(t => t.PrisonerTypeId)
            //    .Index(t => t.PrisonerSubTypeId)
            //    .Index(t => t.ReligionId)
            //    .Index(t => t.OccupationId)
            //    .Index(t => t.NationalityCountryId)
            //    .Index(t => t.PresentCountryId)
            //    .Index(t => t.PresentProvinceId)
            //    .Index(t => t.PresentDistrictId)
            //    .Index(t => t.PresentCityId)
            //    .Index(t => t.PresentPoliceStationId)
            //    .Index(t => t.PermanentCountryId)
            //    .Index(t => t.PermanentProvinceId)
            //    .Index(t => t.PermanentDistrictId)
            //    .Index(t => t.PermanentCityId)
            //    .Index(t => t.PermanentPoliceStationId)
            //    .Index(t => t.FormalEducationLevelId)
            //    .Index(t => t.TechnicalEducationLevelId)
            //    .Index(t => t.ReligiousEducationLevelId)
            //    .Index(t => t.NextOfKinRelationTypeId)
            //    .Index(t => t.EducationLevel_EducationLevelId)
            //    .Index(t => t.Country_CountryId)
            //    .Index(t => t.Province_ProvinceId)
            //    .Index(t => t.PoliceStation_PoliceStationId)
            //    .Index(t => t.City_CityId)
            //    .Index(t => t.District_DistrictId);
            
            //CreateTable(
            //    "dbo.EducationLevels",
            //    c => new
            //        {
            //            EducationLevelId = c.Int(nullable: false, identity: true),
            //            EducationType = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.EducationLevelId);
            
            //CreateTable(
            //    "dbo.Countries",
            //    c => new
            //        {
            //            CountryId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CountryId);
            
            //CreateTable(
            //    "dbo.Provinces",
            //    c => new
            //        {
            //            ProvinceId = c.Int(nullable: false, identity: true),
            //            CountryId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ProvinceId)
            //    .ForeignKey("dbo.Countries", t => t.CountryId)
            //    .Index(t => t.CountryId);
            
            //CreateTable(
            //    "dbo.RelationTypes",
            //    c => new
            //        {
            //            RelationTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.RelationTypeId);
            
            //CreateTable(
            //    "dbo.Occupations",
            //    c => new
            //        {
            //            OccupationId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.OccupationId);
            
            //CreateTable(
            //    "dbo.PrisonerSubTypes",
            //    c => new
            //        {
            //            PrisonerSubTypeId = c.Int(nullable: false, identity: true),
            //            PrisonerTypeId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.PrisonerSubTypeId)
            //    .ForeignKey("dbo.PrisonerTypes", t => t.PrisonerTypeId)
            //    .Index(t => t.PrisonerTypeId);
            
            //CreateTable(
            //    "dbo.PrisonerTypes",
            //    c => new
            //        {
            //            PrisonerTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.PrisonerTypeId);
            
            //CreateTable(
            //    "dbo.Religions",
            //    c => new
            //        {
            //            ReligionId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ReligionId);
            
            //CreateTable(
            //    "dbo.Children",
            //    c => new
            //        {
            //            ChildId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Gender = c.Int(nullable: false),
            //            DateOfBirth = c.DateTime(),
            //            Age = c.Int(nullable: false),
            //            AdmissionId = c.Int(nullable: false),
            //            PrisonerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ChildId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.AdmissionId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.Diseases",
            //    c => new
            //        {
            //            DiseaseId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //            IsCommunicable = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.DiseaseId);
            
            //CreateTable(
            //    "dbo.FIRs",
            //    c => new
            //        {
            //            FIRId = c.Int(nullable: false, identity: true),
            //            FIRNumber = c.String(nullable: false),
            //            FIRDate = c.DateTime(nullable: false),
            //            PoliceStationId = c.Int(nullable: false),
            //            DecisionStatus = c.Int(nullable: false),
            //            JudgeTypeId = c.Int(),
            //            DecisionAuthority = c.String(),
            //            DecisionDate = c.DateTime(),
            //            Victims = c.String(),
            //            DamagesCaused = c.String(),
            //            AdmissionId = c.Int(nullable: false),
            //            ApprovalStatus = c.Int(nullable: false),
            //            IsComplete = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.FIRId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.JudgeTypes", t => t.JudgeTypeId)
            //    .ForeignKey("dbo.PoliceStations", t => t.PoliceStationId)
            //    .Index(t => t.PoliceStationId)
            //    .Index(t => t.JudgeTypeId)
            //    .Index(t => t.AdmissionId);
            
            //CreateTable(
            //    "dbo.JudgeTypes",
            //    c => new
            //        {
            //            JudgeTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.JudgeTypeId);
            
            //CreateTable(
            //    "dbo.CourtHearings",
            //    c => new
            //        {
            //            CourtHearingId = c.Int(nullable: false, identity: true),
            //            FIRId = c.Int(nullable: false),
            //            CourtId = c.Int(nullable: false),
            //            JudgeTypeId = c.Int(),
            //            JudgeId = c.Int(nullable: false),
            //            DateOfCourtOrder = c.DateTime(nullable: false),
            //            DateOfHearing = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            Remarks = c.String(),
            //            AdmissionId = c.Int(nullable: false),
            //            PrisonerId = c.Int(nullable: false),
            //            ApprovalStatus = c.Int(nullable: false),
            //            IsComplete = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CourtHearingId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.Courts", t => t.CourtId)
            //    .ForeignKey("dbo.FIRs", t => t.FIRId)
            //    .ForeignKey("dbo.Judges", t => t.JudgeId)
            //    .ForeignKey("dbo.JudgeTypes", t => t.JudgeTypeId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.FIRId)
            //    .Index(t => t.CourtId)
            //    .Index(t => t.JudgeTypeId)
            //    .Index(t => t.JudgeId)
            //    .Index(t => t.AdmissionId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.Courts",
            //    c => new
            //        {
            //            CourtId = c.Int(nullable: false, identity: true),
            //            CourtTypeId = c.Int(nullable: false),
            //            DistrictId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //            Address = c.String(),
            //        })
            //    .PrimaryKey(t => t.CourtId)
            //    .ForeignKey("dbo.CourtTypes", t => t.CourtTypeId)
            //    .ForeignKey("dbo.Districts", t => t.DistrictId)
            //    .Index(t => t.CourtTypeId)
            //    .Index(t => t.DistrictId);
            
            //CreateTable(
            //    "dbo.CourtTypes",
            //    c => new
            //        {
            //            CourtTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.CourtTypeId);
            
            //CreateTable(
            //    "dbo.Judges",
            //    c => new
            //        {
            //            JudgeId = c.Int(nullable: false, identity: true),
            //            CourtId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.JudgeId)
            //    .ForeignKey("dbo.Courts", t => t.CourtId)
            //    .Index(t => t.CourtId);
            
            //CreateTable(
            //    "dbo.Sections",
            //    c => new
            //        {
            //            SectionId = c.Int(nullable: false, identity: true),
            //            ActId = c.Int(nullable: false),
            //            CrimeTypeId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.SectionId)
            //    .ForeignKey("dbo.Acts", t => t.ActId)
            //    .ForeignKey("dbo.CrimeTypes", t => t.CrimeTypeId)
            //    .Index(t => t.ActId)
            //    .Index(t => t.CrimeTypeId);
            
            //CreateTable(
            //    "dbo.CrimeTypes",
            //    c => new
            //        {
            //            CrimeTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.CrimeTypeId);
            
            //CreateTable(
            //    "dbo.AuditLogs",
            //    c => new
            //        {
            //            AuditLogId = c.Long(nullable: false, identity: true),
            //            Operation = c.String(),
            //            Entity = c.String(),
            //            EntityKey = c.Long(nullable: false),
            //            UserName = c.String(),
            //            OperationDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AuditLogId);
            
            //CreateTable(
            //    "dbo.CheckInOuts",
            //    c => new
            //        {
            //            CheckInOutId = c.Int(nullable: false, identity: true),
            //            Status = c.Int(nullable: false),
            //            Type = c.Int(nullable: false),
            //            DateOfCheckInOut = c.DateTime(nullable: false),
            //            Authority = c.String(),
            //            Description = c.String(),
            //            PrisonerNumber = c.String(),
            //            PrisonerId = c.Int(nullable: false),
            //            AdmissionId = c.Int(),
            //            JudgeTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.CheckInOutId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.JudgeTypes", t => t.JudgeTypeId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.PrisonerId)
            //    .Index(t => t.AdmissionId)
            //    .Index(t => t.JudgeTypeId);
            
            //CreateTable(
            //    "dbo.ExternalContacts",
            //    c => new
            //        {
            //            ExternalContactId = c.Int(nullable: false, identity: true),
            //            PrisonerId = c.Int(nullable: false),
            //            ContactPerson = c.Int(nullable: false),
            //            TypeOfContact = c.Int(nullable: false),
            //            DateOfContact = c.DateTime(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.ExternalContactId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.MedicalCheckups",
            //    c => new
            //        {
            //            MedicalCheckupId = c.Int(nullable: false, identity: true),
            //            DateOfCheckup = c.DateTime(nullable: false),
            //            BloodPressure = c.String(nullable: false),
            //            Pulse = c.Int(nullable: false),
            //            Temperature = c.Single(nullable: false),
            //            Prescription = c.String(nullable: false),
            //            MedicalTreatmentId = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MedicalCheckupId)
            //    .ForeignKey("dbo.MedicalTreatments", t => t.MedicalTreatmentId)
            //    .Index(t => t.MedicalTreatmentId);
            
            //CreateTable(
            //    "dbo.MedicalTreatments",
            //    c => new
            //        {
            //            MedicalTreatmentId = c.Int(nullable: false, identity: true),
            //            DateOfAdmission = c.DateTime(nullable: false),
            //            DateOfDischarge = c.DateTime(),
            //            Diagnosis = c.String(nullable: false),
            //            DiseaseId = c.Int(),
            //            Treatment = c.String(),
            //            DietaryRequirements = c.String(),
            //            MedicalOfficerId = c.Int(nullable: false),
            //            AdmissionId = c.Int(nullable: false),
            //            PrisonerId = c.Int(nullable: false),
            //            ApprovalStatus = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MedicalTreatmentId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.Diseases", t => t.DiseaseId)
            //    .ForeignKey("dbo.MedicalOfficers", t => t.MedicalOfficerId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.DiseaseId)
            //    .Index(t => t.MedicalOfficerId)
            //    .Index(t => t.AdmissionId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.MedicalOfficers",
            //    c => new
            //        {
            //            MedicalOfficerId = c.Int(nullable: false, identity: true),
            //            JailId = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MedicalOfficerId)
            //    .ForeignKey("dbo.Jails", t => t.JailId)
            //    .Index(t => t.JailId);
            
            //CreateTable(
            //    "dbo.PrescribedTests",
            //    c => new
            //        {
            //            PrescribedTestId = c.Int(nullable: false, identity: true),
            //            DateOfTest = c.DateTime(nullable: false),
            //            MedicalTestId = c.Int(nullable: false),
            //            TestType = c.String(),
            //            TestResults = c.String(),
            //            MedicalTreatmentId = c.Int(),
            //            AdmissionId = c.Int(nullable: false),
            //            PrisonerId = c.Int(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PrescribedTestId)
            //    .ForeignKey("dbo.Admissions", t => t.AdmissionId)
            //    .ForeignKey("dbo.MedicalTests", t => t.MedicalTestId)
            //    .ForeignKey("dbo.MedicalTreatments", t => t.MedicalTreatmentId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.MedicalTestId)
            //    .Index(t => t.MedicalTreatmentId)
            //    .Index(t => t.AdmissionId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.MedicalTests",
            //    c => new
            //        {
            //            MedicalTestId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.MedicalTestId);
            
            //CreateTable(
            //    "dbo.PrisonerProperties",
            //    c => new
            //        {
            //            PrisonerPropertyId = c.Int(nullable: false, identity: true),
            //            PrisonerId = c.Int(nullable: false),
            //            Description = c.String(nullable: false),
            //            DateOfDeposit = c.DateTime(),
            //            DateOfReturn = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.PrisonerPropertyId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.PrisonerTransactions",
            //    c => new
            //        {
            //            PrisonerTransactionId = c.Int(nullable: false, identity: true),
            //            PrisonerId = c.Int(nullable: false),
            //            DateOfTransaction = c.DateTime(nullable: false),
            //            TransactionType = c.Int(nullable: false),
            //            Amount = c.Int(nullable: false),
            //            Reference = c.String(nullable: false),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.PrisonerTransactionId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.PrisonOffences",
            //    c => new
            //        {
            //            PrisonOffenceId = c.Int(nullable: false, identity: true),
            //            PrisonerId = c.Int(nullable: false),
            //            DateOfOffence = c.DateTime(nullable: false),
            //            Offence = c.String(nullable: false),
            //            Witnesses = c.String(nullable: false),
            //            NameAndRankOfReportingPerson = c.String(nullable: false),
            //            Evidence = c.String(nullable: false),
            //            DefenceOfPrisoner = c.String(nullable: false),
            //            FindingsOfSuperintendent = c.String(nullable: false),
            //            PunishmentAwarded = c.String(nullable: false),
            //            DateOfInfliction = c.DateTime(),
            //            MedicalOfficerCertificate = c.String(),
            //            Remarks = c.String(),
            //        })
            //    .PrimaryKey(t => t.PrisonOffenceId)
            //    .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
            //    .Index(t => t.PrisonerId);
            
            //CreateTable(
            //    "dbo.Settings",
            //    c => new
            //        {
            //            SettingsId = c.Int(nullable: false, identity: true),
            //            JailId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SettingsId)
            //    .ForeignKey("dbo.Jails", t => t.JailId)
            //    .Index(t => t.JailId);
            
            //CreateTable(
            //    "dbo.SectionFIRs",
            //    c => new
            //        {
            //            Section_SectionId = c.Int(nullable: false),
            //            FIR_FIRId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Section_SectionId, t.FIR_FIRId })
            //    .ForeignKey("dbo.Sections", t => t.Section_SectionId, cascadeDelete: true)
            //    .ForeignKey("dbo.FIRs", t => t.FIR_FIRId, cascadeDelete: true)
            //    .Index(t => t.Section_SectionId)
            //    .Index(t => t.FIR_FIRId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Settings", "JailId", "dbo.Jails");
            //DropForeignKey("dbo.PrisonOffences", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.PrisonerTransactions", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.PrisonerProperties", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.MedicalTreatments", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.PrescribedTests", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.PrescribedTests", "MedicalTreatmentId", "dbo.MedicalTreatments");
            //DropForeignKey("dbo.PrescribedTests", "MedicalTestId", "dbo.MedicalTests");
            //DropForeignKey("dbo.PrescribedTests", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.MedicalTreatments", "MedicalOfficerId", "dbo.MedicalOfficers");
            //DropForeignKey("dbo.MedicalOfficers", "JailId", "dbo.Jails");
            //DropForeignKey("dbo.MedicalCheckups", "MedicalTreatmentId", "dbo.MedicalTreatments");
            //DropForeignKey("dbo.MedicalTreatments", "DiseaseId", "dbo.Diseases");
            //DropForeignKey("dbo.MedicalTreatments", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.ExternalContacts", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.CheckInOuts", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.CheckInOuts", "JudgeTypeId", "dbo.JudgeTypes");
            //DropForeignKey("dbo.CheckInOuts", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.Admissions", "Act_ActId", "dbo.Acts");
            //DropForeignKey("dbo.Admissions", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.Admissions", "JudgeTypeId", "dbo.JudgeTypes");
            //DropForeignKey("dbo.SectionFIRs", "FIR_FIRId", "dbo.FIRs");
            //DropForeignKey("dbo.SectionFIRs", "Section_SectionId", "dbo.Sections");
            //DropForeignKey("dbo.Sections", "CrimeTypeId", "dbo.CrimeTypes");
            //DropForeignKey("dbo.Sections", "ActId", "dbo.Acts");
            //DropForeignKey("dbo.FIRs", "PoliceStationId", "dbo.PoliceStations");
            //DropForeignKey("dbo.FIRs", "JudgeTypeId", "dbo.JudgeTypes");
            //DropForeignKey("dbo.CourtHearings", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.CourtHearings", "JudgeTypeId", "dbo.JudgeTypes");
            //DropForeignKey("dbo.CourtHearings", "JudgeId", "dbo.Judges");
            //DropForeignKey("dbo.CourtHearings", "FIRId", "dbo.FIRs");
            //DropForeignKey("dbo.CourtHearings", "CourtId", "dbo.Courts");
            //DropForeignKey("dbo.Judges", "CourtId", "dbo.Courts");
            //DropForeignKey("dbo.Courts", "DistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Courts", "CourtTypeId", "dbo.CourtTypes");
            //DropForeignKey("dbo.CourtHearings", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.FIRs", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.Admissions", "DiseaseId", "dbo.Diseases");
            //DropForeignKey("dbo.Children", "PrisonerId", "dbo.Prisoners");
            //DropForeignKey("dbo.Children", "AdmissionId", "dbo.Admissions");
            //DropForeignKey("dbo.Barracks", "JailId", "dbo.Jails");
            //DropForeignKey("dbo.Jails", "DistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Districts", "ProvinceId", "dbo.Provinces");
            //DropForeignKey("dbo.Prisoners", "District_DistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Prisoners", "City_CityId", "dbo.Cities");
            //DropForeignKey("dbo.Prisoners", "PoliceStation_PoliceStationId", "dbo.PoliceStations");
            //DropForeignKey("dbo.Prisoners", "TechnicalEducationLevelId", "dbo.EducationLevels");
            //DropForeignKey("dbo.Prisoners", "ReligiousEducationLevelId", "dbo.EducationLevels");
            //DropForeignKey("dbo.Prisoners", "ReligionId", "dbo.Religions");
            //DropForeignKey("dbo.Prisoners", "PrisonerTypeId", "dbo.PrisonerTypes");
            //DropForeignKey("dbo.Prisoners", "PrisonerSubTypeId", "dbo.PrisonerSubTypes");
            //DropForeignKey("dbo.PrisonerSubTypes", "PrisonerTypeId", "dbo.PrisonerTypes");
            //DropForeignKey("dbo.Prisoners", "PresentProvinceId", "dbo.Provinces");
            //DropForeignKey("dbo.Prisoners", "PresentPoliceStationId", "dbo.PoliceStations");
            //DropForeignKey("dbo.Prisoners", "PresentDistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Prisoners", "PresentCountryId", "dbo.Countries");
            //DropForeignKey("dbo.Prisoners", "PresentCityId", "dbo.Cities");
            //DropForeignKey("dbo.Prisoners", "PermanentProvinceId", "dbo.Provinces");
            //DropForeignKey("dbo.Prisoners", "PermanentPoliceStationId", "dbo.PoliceStations");
            //DropForeignKey("dbo.Prisoners", "PermanentDistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Prisoners", "PermanentCountryId", "dbo.Countries");
            //DropForeignKey("dbo.Prisoners", "PermanentCityId", "dbo.Cities");
            //DropForeignKey("dbo.Prisoners", "OccupationId", "dbo.Occupations");
            //DropForeignKey("dbo.Prisoners", "NextOfKinRelationTypeId", "dbo.RelationTypes");
            //DropForeignKey("dbo.Prisoners", "NationalityCountryId", "dbo.Countries");
            //DropForeignKey("dbo.Prisoners", "Province_ProvinceId", "dbo.Provinces");
            //DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            //DropForeignKey("dbo.Prisoners", "Country_CountryId", "dbo.Countries");
            //DropForeignKey("dbo.Prisoners", "JailId", "dbo.Jails");
            //DropForeignKey("dbo.Prisoners", "FormalEducationLevelId", "dbo.EducationLevels");
            //DropForeignKey("dbo.Prisoners", "EducationLevel_EducationLevelId", "dbo.EducationLevels");
            //DropForeignKey("dbo.PoliceStations", "DistrictId", "dbo.Districts");
            //DropForeignKey("dbo.PoliceStations", "CityId", "dbo.Cities");
            //DropForeignKey("dbo.Cities", "DistrictId", "dbo.Districts");
            //DropForeignKey("dbo.Admissions", "BarrackId", "dbo.Barracks");
            //DropIndex("dbo.SectionFIRs", new[] { "FIR_FIRId" });
            //DropIndex("dbo.SectionFIRs", new[] { "Section_SectionId" });
            //DropIndex("dbo.Settings", new[] { "JailId" });
            //DropIndex("dbo.PrisonOffences", new[] { "PrisonerId" });
            //DropIndex("dbo.PrisonerTransactions", new[] { "PrisonerId" });
            //DropIndex("dbo.PrisonerProperties", new[] { "PrisonerId" });
            //DropIndex("dbo.PrescribedTests", new[] { "PrisonerId" });
            //DropIndex("dbo.PrescribedTests", new[] { "AdmissionId" });
            //DropIndex("dbo.PrescribedTests", new[] { "MedicalTreatmentId" });
            //DropIndex("dbo.PrescribedTests", new[] { "MedicalTestId" });
            //DropIndex("dbo.MedicalOfficers", new[] { "JailId" });
            //DropIndex("dbo.MedicalTreatments", new[] { "PrisonerId" });
            //DropIndex("dbo.MedicalTreatments", new[] { "AdmissionId" });
            //DropIndex("dbo.MedicalTreatments", new[] { "MedicalOfficerId" });
            //DropIndex("dbo.MedicalTreatments", new[] { "DiseaseId" });
            //DropIndex("dbo.MedicalCheckups", new[] { "MedicalTreatmentId" });
            //DropIndex("dbo.ExternalContacts", new[] { "PrisonerId" });
            //DropIndex("dbo.CheckInOuts", new[] { "JudgeTypeId" });
            //DropIndex("dbo.CheckInOuts", new[] { "AdmissionId" });
            //DropIndex("dbo.CheckInOuts", new[] { "PrisonerId" });
            //DropIndex("dbo.Sections", new[] { "CrimeTypeId" });
            //DropIndex("dbo.Sections", new[] { "ActId" });
            //DropIndex("dbo.Judges", new[] { "CourtId" });
            //DropIndex("dbo.Courts", new[] { "DistrictId" });
            //DropIndex("dbo.Courts", new[] { "CourtTypeId" });
            //DropIndex("dbo.CourtHearings", new[] { "PrisonerId" });
            //DropIndex("dbo.CourtHearings", new[] { "AdmissionId" });
            //DropIndex("dbo.CourtHearings", new[] { "JudgeId" });
            //DropIndex("dbo.CourtHearings", new[] { "JudgeTypeId" });
            //DropIndex("dbo.CourtHearings", new[] { "CourtId" });
            //DropIndex("dbo.CourtHearings", new[] { "FIRId" });
            //DropIndex("dbo.FIRs", new[] { "AdmissionId" });
            //DropIndex("dbo.FIRs", new[] { "JudgeTypeId" });
            //DropIndex("dbo.FIRs", new[] { "PoliceStationId" });
            //DropIndex("dbo.Children", new[] { "PrisonerId" });
            //DropIndex("dbo.Children", new[] { "AdmissionId" });
            //DropIndex("dbo.PrisonerSubTypes", new[] { "PrisonerTypeId" });
            //DropIndex("dbo.Provinces", new[] { "CountryId" });
            //DropIndex("dbo.Prisoners", new[] { "District_DistrictId" });
            //DropIndex("dbo.Prisoners", new[] { "City_CityId" });
            //DropIndex("dbo.Prisoners", new[] { "PoliceStation_PoliceStationId" });
            //DropIndex("dbo.Prisoners", new[] { "Province_ProvinceId" });
            //DropIndex("dbo.Prisoners", new[] { "Country_CountryId" });
            //DropIndex("dbo.Prisoners", new[] { "EducationLevel_EducationLevelId" });
            //DropIndex("dbo.Prisoners", new[] { "NextOfKinRelationTypeId" });
            //DropIndex("dbo.Prisoners", new[] { "ReligiousEducationLevelId" });
            //DropIndex("dbo.Prisoners", new[] { "TechnicalEducationLevelId" });
            //DropIndex("dbo.Prisoners", new[] { "FormalEducationLevelId" });
            //DropIndex("dbo.Prisoners", new[] { "PermanentPoliceStationId" });
            //DropIndex("dbo.Prisoners", new[] { "PermanentCityId" });
            //DropIndex("dbo.Prisoners", new[] { "PermanentDistrictId" });
            //DropIndex("dbo.Prisoners", new[] { "PermanentProvinceId" });
            //DropIndex("dbo.Prisoners", new[] { "PermanentCountryId" });
            //DropIndex("dbo.Prisoners", new[] { "PresentPoliceStationId" });
            //DropIndex("dbo.Prisoners", new[] { "PresentCityId" });
            //DropIndex("dbo.Prisoners", new[] { "PresentDistrictId" });
            //DropIndex("dbo.Prisoners", new[] { "PresentProvinceId" });
            //DropIndex("dbo.Prisoners", new[] { "PresentCountryId" });
            //DropIndex("dbo.Prisoners", new[] { "NationalityCountryId" });
            //DropIndex("dbo.Prisoners", new[] { "OccupationId" });
            //DropIndex("dbo.Prisoners", new[] { "ReligionId" });
            //DropIndex("dbo.Prisoners", new[] { "PrisonerSubTypeId" });
            //DropIndex("dbo.Prisoners", new[] { "PrisonerTypeId" });
            //DropIndex("dbo.Prisoners", new[] { "JailId" });
            //DropIndex("dbo.PoliceStations", new[] { "CityId" });
            //DropIndex("dbo.PoliceStations", new[] { "DistrictId" });
            //DropIndex("dbo.Cities", new[] { "DistrictId" });
            //DropIndex("dbo.Districts", new[] { "ProvinceId" });
            //DropIndex("dbo.Jails", new[] { "DistrictId" });
            //DropIndex("dbo.Barracks", new[] { "JailId" });
            //DropIndex("dbo.Admissions", new[] { "Act_ActId" });
            //DropIndex("dbo.Admissions", new[] { "PrisonerId" });
            //DropIndex("dbo.Admissions", new[] { "JudgeTypeId" });
            //DropIndex("dbo.Admissions", new[] { "DiseaseId" });
            //DropIndex("dbo.Admissions", new[] { "BarrackId" });
            //DropTable("dbo.SectionFIRs");
            //DropTable("dbo.Settings");
            //DropTable("dbo.PrisonOffences");
            //DropTable("dbo.PrisonerTransactions");
            //DropTable("dbo.PrisonerProperties");
            //DropTable("dbo.MedicalTests");
            //DropTable("dbo.PrescribedTests");
            //DropTable("dbo.MedicalOfficers");
            //DropTable("dbo.MedicalTreatments");
            //DropTable("dbo.MedicalCheckups");
            //DropTable("dbo.ExternalContacts");
            //DropTable("dbo.CheckInOuts");
            //DropTable("dbo.AuditLogs");
            //DropTable("dbo.CrimeTypes");
            //DropTable("dbo.Sections");
            //DropTable("dbo.Judges");
            //DropTable("dbo.CourtTypes");
            //DropTable("dbo.Courts");
            //DropTable("dbo.CourtHearings");
            //DropTable("dbo.JudgeTypes");
            //DropTable("dbo.FIRs");
            //DropTable("dbo.Diseases");
            //DropTable("dbo.Children");
            //DropTable("dbo.Religions");
            //DropTable("dbo.PrisonerTypes");
            //DropTable("dbo.PrisonerSubTypes");
            //DropTable("dbo.Occupations");
            //DropTable("dbo.RelationTypes");
            //DropTable("dbo.Provinces");
            //DropTable("dbo.Countries");
            //DropTable("dbo.EducationLevels");
            //DropTable("dbo.Prisoners");
            //DropTable("dbo.PoliceStations");
            //DropTable("dbo.Cities");
            //DropTable("dbo.Districts");
            //DropTable("dbo.Jails");
            //DropTable("dbo.Barracks");
            //DropTable("dbo.Admissions");
            //DropTable("dbo.Acts");
        }
    }
}
