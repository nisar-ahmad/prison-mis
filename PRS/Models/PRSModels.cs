using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRS.Models
{
    #region ENUMS AND LISTS

    public static class Lists
    {
        public static string[] BloodGroups = { "Unknown", "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        public static string[] Heights = {  "3' 0\"", "3' 1\"", "3' 2\"", "3' 3\"", "3' 4\"", "3' 5\"", 
                                            "3' 6\"", "3' 7\"", "3' 8\"", "3' 9\"", "3' 10\"", "3' 11\"",
                                            "4' 0\"", "4' 1\"", "4' 2\"", "4' 3\"", "4' 4\"", "4' 5\"", 
                                            "4' 6\"", "4' 7\"", "4' 8\"", "4' 9\"", "4' 10\"", "4' 11\"",
                                            "5' 0\"", "5' 1\"", "5' 2\"", "5' 3\"", "5' 4\"", "5' 5\"", 
                                            "5' 6\"", "5' 7\"", "5' 8\"", "5' 9\"", "5' 10\"", "5' 11\"",
                                            "6' 0\"", "6' 1\"", "6' 2\"", "6' 3\"", "6' 4\"", "6' 5\"", 
                                            "6' 6\"", "6' 7\"", "6' 8\"", "6' 9\"", "6' 10\"", "6' 11\"",
                                            "7' 0\"", "7' 1\"", "7' 2\"", "7' 3\"", "7' 4\"", "7' 5\"", 
                                            "7' 6\"", "7' 7\"", "7' 8\"", "7' 9\"", "7' 10\"", "7' 11\"",
                                            "8' 0\""};

    }

    public enum Gender { Male, Female, Other }
    public enum MaritalStatus { Single, Married, Divorced, Widowed }

    public enum PrisonerCategory { UnderTrial, Convicted, Condemned, Internee, Detainee }

    public enum PrisonerStatus { Admitted, Released, Transferred, Expired, Escaped, Executed }
    public enum PrisonerClass { A, B, C }

    public enum VisitorType { Personal, Official, Legal, Embassy, Interpreter }

    public enum EducationType { Formal, Technical, Religious }
    public enum JailType { Central, District, SubJail, Special, Lockup }

    public enum NarcoticsStatus { None, Peddler, User, Trafficker }

    public enum FineType { Ordinary, Special }

    public enum CourtHearingStatus { In, Out }

    public enum ApprovalStatus { Pending, Approved, Disapproved } 

    public enum DecisionStatus { Pending = 0, Acquitted = 1, Bailed = 2, Disposed = 3, Sentenced = 4 }

    public enum SectionDecisionType { Convicted = 0, DeathPenalty = 1, FineOnly = 2, LifeImprisonment = 3, Released = 4 }

    public enum SentenceType { Simple = 0, Rigorous = 1 }

    public enum CourtDecisionType { NotApplicable = 0, Consecutive = 1, Concurrent = 2 }

    public enum TransactionType { Credit, Debit }

    public enum CheckInOutStatus { CheckIn, CheckOut }

    public enum CheckInOutType { Admission, ReAdmission, Release, PhysicalRemandIn, PhysicalRemandOut, TransferIn, TransferOut, CourtProductionIn, CourtProductionOut, ExternalTreatmentOut, ExternalTreatmentIn, Parole, Bail }

    public enum LabourType { None, Simple, Rigorous }

    public enum IdentityDocumentType { CNIC, Passport, AfghanID, Other }

    #endregion

    #region PRISONERS

    public class Prisoner
    {
        /// <summary>
        /// Constructor to initialize with Default Values for a new Prisoner
        /// </summary>
        public Prisoner()
        {
            ReligionId = 1;                                                     // Islam
            NationalityCountryId = PresentCountryId  = PermanentCountryId = 157;// Pakistan
            PresentProvinceId = PermanentProvinceId = 7;                        // Punjab
            PresentDistrictId = PermanentDistrictId = 104;                      // Lahore
            PresentCityId = PermanentCityId = 104;                              // Lahore
            PrisonerTypeId = 6;                                                 // Ordinary
            Class = PrisonerClass.C;
            Height = "5' 8\"";
            DateOfBirth = null;

            IsActive = true;
        }

        // BASIC INFO
        public string FMD1 { get; set; }

        public int PrisonerId { get; set; }
        
        public int JailId { get; set; }

        [ForeignKey("JailId")]
        [Display(Name = "Jail جیل")]
        public virtual Jail Jail { get; set; }

        [Display(Name = "Status کیفیت")]
        public PrisonerStatus Status { get; set; }

        [Display(Name = "Category قسم")]
        public PrisonerCategory Category { get; set; }

        [Display(Name = "Class کلاس")]
        public PrisonerClass Class { get; set; }

        [Display(Name = "Prisoner Type  ٹائپ")]
        public int PrisonerTypeId { get; set; }
        
        [ForeignKey("PrisonerTypeId")]
        [Display(Name = "Prisoner Type  ٹائپ")]
        public virtual PrisonerType PrisonerType { get; set; }

        public int? PrisonerSubTypeId { get; set; }

        [ForeignKey("PrisonerSubTypeId")]
        [Display(Name = "Prisoner Sub Type سب ٹائپ")]
        public virtual PrisonerSubType PrisonerSubType { get; set; }

        [Display(Name = "Identity Document شناختی دستاویز")]
        public IdentityDocumentType IdentityDocumentType { get; set; }

        //[Required]
        [Display(Name = "CNIC شناختی کارڈ")]
        [RegularExpression("[0-9+]{5}-[0-9+]{7}-[0-9]{1}")]
        public string CNIC { get; set; }

        [Display(Name = "Passport/Afghan ID/Other No. نمبر")]
        public string IdentityDocumentNumber { get; set; }

        [Required]
        [Display(Name = "Name نام")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Father/Husband Name والد/شوہرکانام")]
        public string FatherOrHusbandName { get; set; }

        [Required]
        [Display(Name = "Paternal Grandfather Name  دادا کانام")]
        public string PaternalGrandfatherName { get; set; }

        [Display(Name = "Maternal Grandfather Name  نانا کانام")]
        public string MaternalGrandfatherName { get; set; }

        //[Required]
        [Display(Name = "Date of Birth تاریخ پیدائش")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender جنس")]
        public Gender Gender { get; set; }

        [Display(Name = "Marital Status ازدواجی حیثیت")]
        public MaritalStatus MaritalStatus { get; set; }

        [Display(Name = "Narcotics Status منشیات")]
        public NarcoticsStatus NarcoticsStatus { get; set; }

        public int ReligionId { get; set; }
        
        [ForeignKey("ReligionId")]
        [Display(Name = "Religion مذہب")]
        public virtual Religion Religion { get; set; }

        public int OccupationId { get; set; }        

        [ForeignKey("OccupationId")]
        [Display(Name = "Occupation پیشہ")]
        public virtual Occupation Occupation { get; set; }

        public int NationalityCountryId { get; set; }

        [ForeignKey("NationalityCountryId")]
        [Display(Name = "Nationality شہریت")]
        public virtual Country Nationality { get; set; }

        [Display(Name = "Alias عرف")]
        public string Alias { get; set; }

        [Display(Name = "Caste ذات")]
        public string Caste { get; set; }

        // PRESENT ADDRESS

        [Display(Name = "Country ملک")]
        public int PresentCountryId { get; set; }

        [ForeignKey("PresentCountryId")]
        [Display(Name = "Country ملک")]
        public virtual Country PresentCountry { get; set; }

        [Display(Name = "Province صوبہ")]
        public int PresentProvinceId { get; set; }

        [ForeignKey("PresentProvinceId")]
        [Display(Name = "Province صوبہ")]
        public virtual Province PresentProvince { get; set; }

        [Display(Name = "District ضلع")]
        public int PresentDistrictId { get; set; }

        [ForeignKey("PresentDistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District PresentDistrict { get; set; }

        public int? PresentCityId { get; set; }

        [ForeignKey("PresentCityId")]
        [Display(Name = "City شہر")]
        public virtual City PresentCity { get; set; }

        public int? PresentPoliceStationId { get; set; }

        [ForeignKey("PresentPoliceStationId")]
        [Display(Name = "Police Station تھانہ")]
        public virtual PoliceStation PresentPoliceStation { get; set; }

        [Display(Name = "Mouza موضع")]
        public string PresentMouza { get; set; }

        [Display(Name = "House # مکان")]
        public string PresentHouseNumber { get; set; }

        [Display(Name = "Street گلی")]
        public string PresentStreet { get; set; }

        [Display(Name = "Area / Colony کالونی")]
        public string PresentAreaOrColony { get; set; }

        // PERMANENT ADDRESS

        public int PermanentCountryId { get; set; }

        [ForeignKey("PermanentCountryId")]
        [Display(Name = "Country ملک")]
        public virtual Country PermanentCountry { get; set; }

        public int PermanentProvinceId { get; set; }

        [ForeignKey("PermanentProvinceId")]
        [Display(Name = "Province صوبہ")]
        public virtual Province PermanentProvince { get; set; }

        public int PermanentDistrictId { get; set; }

        [ForeignKey("PermanentDistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District PermanentDistrict { get; set; }

        public int? PermanentCityId { get; set; }        

        [ForeignKey("PermanentCityId")]
        [Display(Name = "City شہر")]
        public virtual City PermanentCity { get; set; }

        public int? PermanentPoliceStationId { get; set; }

        [ForeignKey("PermanentPoliceStationId")]
        [Display(Name = "Police Station تھانہ")]
        public virtual PoliceStation PermanentPoliceStation { get; set; }

        [Display(Name = "Mouza موضع")]
        public string PermanentMouza { get; set; }

        [Display(Name = "House # مکان")]
        public string PermanentHouseNumber { get; set; }

        [Display(Name = "Street گلی")]
        public string PermanentStreet { get; set; }

        [Display(Name = "Area / Colony کالونی")]
        public string PermanentAreaOrColony { get; set; }

        // EDUCATION

        public int? FormalEducationLevelId { get; set; }             

        [ForeignKey("FormalEducationLevelId")]
        [Display(Name = "Formal Education تعلیم")]
        public virtual EducationLevel FormalEducation { get; set; }

        public int? TechnicalEducationLevelId { get; set; }

        [ForeignKey("TechnicalEducationLevelId")]
        [Display(Name = "Technical Education ٹیکنیکل تعلیم")]
        public virtual EducationLevel TechnicalEducation { get; set; }

        public int? ReligiousEducationLevelId { get; set; }

        [ForeignKey("ReligiousEducationLevelId")]
        [Display(Name = "Religious Education دینی تعلیم")]
        public virtual EducationLevel ReligiousEducation { get; set; }

        // NEXT OF KIN
        [Display(Name = "Next of Kin وارث کا نام")]
        public string NextOfKinName { get; set; }

        public int? NextOfKinRelationTypeId { get; set; }

        [ForeignKey("NextOfKinRelationTypeId")]
        [Display(Name = "Relationship نسبت")]
        public virtual RelationType NextOfKinRelationType { get; set; }

        [Display(Name = "Contact Information رابطہ نمبر")]
        public string NextOfKinContact { get; set; }

        // FINANCIAL STATUS

        [Display(Name = "Moveable Assets منقولہ جائداد")]
        public string MoveableAssets { get; set; }

        [Display(Name = "Immoveable Assets غیر منقولہ جائداد")]
        public string ImmoveableAssets { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        // MEDICAL INFO

        [Display(Name = "Identification Mark شناختی نشان 1 ")]
        public string IdentificationMark1 { get; set; }

        [Display(Name = "Identification Mark شناختی نشان 2 ")]
        public string IdentificationMark2 { get; set; }

        [Display(Name = "Age عمر")]
        public int Age { get; set; }

        [Display(Name = "Scar زخم کا نشان")]
        public string Scar { get; set; }

        [Display(Name = "Height قد")]
        public string Height { get; set; }

        [Display(Name = "Weight (kg) وزن")]
        public string Weight { get; set; }

        [Display(Name = "Blood Group بلڈ گروپ")]
        public string BloodGroup { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public bool IsActive { get; set; }

        //public virtual ICollection<Admission> Admissions { get; set; }
        //public virtual ICollection<CourtHearing> CourtHearings { get; set; }

        //public virtual ICollection<PrescribedTest> PrescribedTests { get; set; }
        //public virtual ICollection<MedicalTreatment> MedicalTreatments { get; set; }
        //public virtual ICollection<PrisonerProperty> PrisonerProperties { get; set; }
        //public virtual ICollection<PrisonerTransaction> PrisonerTransactions { get; set; }
        //public virtual ICollection<Child> PrisonerChildren { get; set; }
    }

    public class Admission
    {
        public Admission()
        {
            DateOfAdmission = DateTime.Now;
            IsActive = true;
            IsComplete = true;
        }

        public int AdmissionId { get; set; }
        
        // ADMISSION INFO

        [Display(Name = "Date of Admission تاریخ داخلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfAdmission { get; set; }

        [Required]
        [Display(Name = "Prisoner Number قیدی نمبر")]
        public string PrisonerNumber { get; set; }

        public int? BarrackId { get; set; }

        [ForeignKey("BarrackId")]
        [Display(Name = "Barrack بیرک")]
        public virtual Barrack Barrack { get; set; }

        [Display(Name = "Block Number بلاک نمبر")]
        public string BlockNumber { get; set; }

        [Display(Name = "Cell Number سیل نمبر")]
        public string CellNumber { get; set; }

        [Display(Name = "Date of Warrant Commitment تاریخ وارنٹ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfWarrantCommitment { get; set; }

        [Display(Name = "Date of Return from Remand تاریخ ریمانڈ واپسی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRemand { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        // MEDICAL INFO

        [Display(Name = "Health on Admission داخلہ پر صحت")]
        public string HealthOnAdmission { get; set; }

        [Display(Name = "Weight on Admission (kg) داخلہ پر وزن")]
        public string WeightOnAdmission { get; set; }

        [Display(Name = "Known Ailment معلوم بیماری")]
        public string KnownAilment { get; set; }

        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [Display(Name = "Communicable Disease چھوت کی بیماری")]
        public virtual Disease CommunicableDisease { get; set; }

        [Display(Name = "Explained Injuries معلوم زخم")]
        public string ExplainedInjuries { get; set; }

        [Display(Name = "Unexplained Injuries نامعلوم زخم")]
        public string UnexplainedInjuries { get; set; }

        [Display(Name = "Medical Remarks میڈیکل نوٹس")]
        public string MedicalRemarks { get; set; }

        // RELEASE INFO
       
        [Display(Name = "Date of Release تاریخ رہائی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }

        [Display(Name = "Date of Release with Full Sentence تاریخ رہائی مکمل سزا")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfReleaseWithFullSentence { get; set; }

        [Display(Name = "Decision Status کیفیت")]
        public DecisionStatus DecisionStatus { get; set; }

        public int? JudgeTypeId { get; set; }

        [ForeignKey("JudgeTypeId")]
        [Display(Name = "Judge Type جج ٹائپ")]
        public virtual JudgeType JudgeType { get; set; }

        [Display(Name = "Authority for Release رہائی اتھارٹی")]
        public string AuthorityForRelease { get; set; }

        [Display(Name = "Health on Release رہائی پر صحت")]
        public string HealthOnRelease { get; set; }

        [Display(Name = "Weight on Release (kg) رہائی پر وزن")]
        public string WeightOnRelease { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public virtual ICollection<FIR> FIRs { get; set; }
        public virtual ICollection<Child> Children { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
    }

    public class FIR
    {
        public FIR()
        {
            FIRDate = DateTime.Now;
            IsActive = true;
            IsComplete = true;
        }

        public int FIRId { get; set; }

        [Required]
        [Display(Name = "FIR Number ایف آئی آر نمبر")]
        public string FIRNumber { get; set; }

        [Display(Name = "FIR Date تاریخ ایف آئی آر")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime FIRDate { get; set; }

        public int PoliceStationId { get; set; }

        [ForeignKey("PoliceStationId")]
        [Display(Name = "Police Station تھانہ")]
        public virtual PoliceStation PoliceStation { get; set; }

        [Display(Name = "Decision Status فیصلہ")]
        public DecisionStatus DecisionStatus { get; set; }

        public int? JudgeTypeId { get; set; }

        [ForeignKey("JudgeTypeId")]
        [Display(Name = "Decision Judge Type جج ٹائپ")]
        public virtual JudgeType JudgeType { get; set; }

        [Display(Name = "Decision Authority اتھارٹی")]
        public string DecisionAuthority { get; set; }

        [Display(Name = "Decision Date تاریخ فیصلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DecisionDate { get; set; }

        [Display(Name = "Victims متاثرین")]
        public string Victims { get; set; }

        [Display(Name = "Damages Caused تقصانات")]
        public string DamagesCaused { get;set; }

        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<CourtDecision> CourtDecisions { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
    }

    public class CourtHearing
    {
        public CourtHearing()
        {
            DateOfCourtOrder = DateTime.Now;
            DateOfHearing = DateOfCourtOrder.AddDays(14);
            IsActive = true;
            IsComplete = true;
        }

        public int CourtHearingId { get; set; }
        
        public int FIRId { get; set; }

        [ForeignKey("FIRId")]
        [Display(Name = "FIR ایف آئی آر")]
        public virtual FIR FIR { get; set; }

        [Display(Name = "Court عدالت")]
        public int CourtId { get; set; }

        [ForeignKey("CourtId")]
        [Display(Name = "Court عدالت")]
        public virtual Court Court { get; set; }

        public int? JudgeTypeId { get; set; }

        [ForeignKey("JudgeTypeId")]
        [Display(Name = "Judge Type جج ٹائپ")]
        public virtual JudgeType JudgeType { get; set; }

        [Display(Name = "Judge جج")]
        public int JudgeId { get; set; }

        [ForeignKey("JudgeId")]
        [Display(Name = "Judge جج")]
        public virtual Judge Judge { get; set; }

        [Display(Name = "Date of Court Order تاریخ کورٹ آرڈر")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfCourtOrder { get; set; }

        [Display(Name = "Date of Next Hearing تاریخ پیشی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfHearing { get; set; }

        [Display(Name = "Status کیفیت")]
        public CourtHearingStatus Status { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        [Display(Name = "Prisoner قیدی")]
        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
    }

    public class PrescribedTest
    {
        public PrescribedTest()
        {
            IsActive = true;
        }

        public int PrescribedTestId { get; set; }

        [Display(Name = "Date of Test تاریخ ٹیسٹ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfTest { get; set; }

        public int MedicalTestId { get; set; }

        [ForeignKey("MedicalTestId")]
        [Display(Name = "Test Name ٹیسٹ")]
        public virtual MedicalTest MedicalTest { get; set; }

        [Display(Name = "Test Type ٹیسٹ کی قسم")]
        public string TestType { get; set; }

        [Display(Name = "Test Results ٹیسٹ کا نتیجہ")]
        public string TestResults { get; set; }

        [Display(Name = "Medical Treatment علاج")]
        public int? MedicalTreatmentId { get; set; }

        [ForeignKey("MedicalTreatmentId")]
        [Display(Name = "Medical Treatment علاج")]
        public virtual MedicalTreatment MedicalTreatment { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        [Display(Name = "Prisoner قیدی")]
        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public bool IsActive { get; set; }
    }

    public class MedicalCheckup
    {
        public MedicalCheckup()
        {
            DateOfCheckup = DateTime.Now;
            IsActive = true;
        }

        public int MedicalCheckupId { get; set; }

        [Display(Name = "Date of Checkup تاریخ معائنہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfCheckup { get; set; }

        [Required]
        [Display(Name = "Blood Pressure بلڈ پریشر")]
        public string BloodPressure { get; set; }

        [Required]
        [Display(Name = "Pulse نبض")]
        [Range(30, 150)]
        public int Pulse { get; set; }

        [Required]
        [Display(Name = "Temperature ('F) درجہ حرارت")]
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        [Range(90, 110)]
        public float Temperature { get; set; }

        [Required]
        [Display(Name ="Prescription / Remarks نسخہ")]
        public string Prescription { get; set; }
        
        public int MedicalTreatmentId { get; set; }     

        [ForeignKey("MedicalTreatmentId")]
        [Display(Name = "Medical Treatment علاج")]
        public virtual MedicalTreatment MedicalTreatment { get; set; }

        public bool IsActive { get; set; }
    }

    public class MedicalTreatment
    {
        public MedicalTreatment()
        {
            DateOfAdmission = DateTime.Now;
            DateOfDischarge = DateTime.Now;

            IsActive = true;
        }

        public int MedicalTreatmentId { get; set; }

        [Display(Name = "Date of Hospital Admission تاریخ داخلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfAdmission { get; set; }

        [Display(Name = "Date of Discharge تاریخ ڈسچارج")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfDischarge { get; set; }

        [Required]
        [Display(Name = "Diagnosis تشخیص")]
        public string Diagnosis { get; set; }

        [Display(Name = "Disease بیماری")]
        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [Display(Name = "Disease بیماری")]
        public virtual Disease Disease { get; set; }

        [Display(Name = "Treatment علاج")]
        public string Treatment { get; set; }

        [Display(Name = "Dietary Requirements پرہیز")]
        public string DietaryRequirements { get; set; }

        [Display(Name = "Medical Officer ڈاکٹر")]
        public int MedicalOfficerId { get; set; }

        [ForeignKey("MedicalOfficerId")]
        [Display(Name = "Medical Officer ڈاکٹر")]
        public MedicalOfficer MedicalOfficer { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public virtual ICollection<MedicalCheckup> MedicalCheckups { get; set; }
        public virtual ICollection<PrescribedTest> PrescribedTests { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public bool IsActive { get; set; }
    }

    public class PrisonOffence
    {
        public int PrisonOffenceId { get; set; }

        public int PrisonerId { get; set; }

        [Display(Name = "Date of Offence تاریخ جرم")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfOffence { get; set; }

        [Required]
        [Display(Name = "Offence جرم")]
        public string Offence { get; set; }

        [Required]
        [Display(Name = "Witnesses گواہ")]
        public string Witnesses { get; set; }

        [Required]
        [Display(Name = "Name & Rank of Reporting Person رپورٹ کرنے والے کا نام و عہدہ")]
        public string NameAndRankOfReportingPerson { get; set; }

        [Required]
        [Display(Name = "Evidence ثبوت")]
        public string Evidence { get; set; }

        [Required]
        [Display(Name = "Defence of Prisoner ملزم کابیان")]
        public string DefenceOfPrisoner { get; set; }

        [Required]
        [Display(Name = "Findings of Superintendent سپرنٹنڈنٹ کی تفتیش")]
        public string FindingsOfSuperintendent { get; set; }

        [Required]
        [Display(Name = "Punishment Awarded سزا")]
        public string PunishmentAwarded { get; set; }

        [Display(Name = "Date of Infliction تاریخ سزا")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfInfliction { get; set; }

        [Display(Name = "Medical Officer Certificate ڈاکٹر سرٹیفیکیٹ")]
        public string MedicalOfficerCertificate { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    public class PrisonerProperty
    {
        public int PrisonerPropertyId { get; set; }

        public int PrisonerId { get; set; }

        [Required]
        [Display(Name = "Description اشیاکی تفصیل")]
        public string Description { get; set; }

        [Display(Name = "Date of Deposit تاریخ جمع")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfDeposit { get; set; }

        [Display(Name = "Date of Return تاریخ واپسی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfReturn { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    public class PrisonerTransaction
    {
        public PrisonerTransaction()
        {
            DateOfTransaction = DateTime.Now;
        }

        public int PrisonerTransactionId { get; set; }

        public int PrisonerId { get; set; }

        [Display(Name = "Date of Transaction تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfTransaction { get; set; }

        [Display(Name = "Transaction Type نوعیت")]
        public TransactionType TransactionType { get; set; }

        [Range(1, 100000, ErrorMessage="Amount must be between 1 and 100,000")]
        [Display(Name = "Amount رقم")]
        public int Amount { get; set; }

        [Required]
        [Display(Name = "Reference No. حوالہ نمبر")]
        public string Reference { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    public class Child
    {
        public int ChildId { get; set; }

        [Required]
        [Display(Name = "Name نام")]
        public string Name { get; set; }

        [Display(Name = "Gender جنس")]
        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth تاریخ پیدائش")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Age عمر")]
        public int Age { get; set; }

        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    public class CheckInOut
    {
        public CheckInOut()
        {
            DateOfCheckInOut = DateTime.Now;
        }

        public int CheckInOutId { get; set; }

        public CheckInOutStatus Status { get; set; }

        public CheckInOutType Type { get; set; }

        [Display(Name = "Date تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfCheckInOut { get; set; }

        [Display(Name = "Decision Authority اتھارٹی")]
        public string Authority { get; set; }

        [Display(Name = "Description تفصیل")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string PrisonerNumber { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int? AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        public int? JudgeTypeId { get; set; }

        [ForeignKey("JudgeTypeId")]
        [Display(Name = "Judge Type جج ٹائپ")]
        public virtual JudgeType JudgeType { get; set; }
    }

    public class CourtDecision
    {
        public int CourtDecisionId { get; set; }       

        [Display(Name = "Decision Status فیصلہ")]
        public DecisionStatus DecisionStatus { get; set; }

        [Display(Name = "Sentence Start Date تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfSentenceStart { get; set; }

        [Display(Name = "Under Trial Start Date تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfUnderTrialStart { get; set; }

        [Display(Name = "Under Trial End Date تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfUnderTrialEnd { get; set; }        

        [Display(Name = "Court Decision Type")]
        public CourtDecisionType CourtDecisionType { get; set; }

        [Display(Name = "Is 382B Applied?")]
        public bool Is382BApplied { get; set; }

        [Display(Name = "Decision Authority اتھارٹی")]
        public string DecisionAuthority { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        public bool IsActive { get; set; }

        public int FIRId { get; set; }

        [ForeignKey("FIRId")]
        [Display(Name = "FIR ایف آئی آر")]
        public virtual FIR FIR { get; set; }

        public int CourtHearingId { get; set; }

        [ForeignKey("CourtHearingId")]
        [Display(Name = "Court Hearing")]
        public virtual CourtHearing CourtHearing { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        [Display(Name = "Prisoner قیدی")]
        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public virtual ICollection<CourtDecisionSection> CourtDecisionSections { get; set; }
    }

    public class CourtDecisionSection
    {
        public int CourtDecisionSectionId { get; set; }

        [Display(Name = "Section Decision Type")]
        public SectionDecisionType SectionDecisionType { get; set; }

        [Display(Name = "Sentence Type")]
        public SentenceType SentenceType { get; set; }

        [Display(Name = "Years سال")]
        public int SentenceYears { get; set; }

        [Display(Name = "Months مہینے")]
        public int SentenceMonths { get; set; }

        [Display(Name = "Days دن")]
        public int SentenceDays { get; set; }

        [Display(Name = "Further Sentence Type")]
        public SentenceType FurtherSentenceType { get; set; }

        [Display(Name = "Years سال")]
        public int FurtherSentenceYears { get; set; }

        [Display(Name = "Months مہینے")]
        public int FurtherSentenceMonths { get; set; }

        [Display(Name = "Days دن")]
        public int FurtherSentenceDays { get; set; }

        [Display(Name = "Fine Amount")]
        public int FineAmount { get; set; }

        [Display(Name = "Is Fine Paid?")]
        public bool IsFinePaid { get; set; }

        public int Stripes { get; set; }

        [Display(Name = "Is 382B Applied?")]
        public bool Is382BApplied { get; set; }

        [Display(Name = "Court Decision Type")]
        public CourtDecisionType CourtDecisionType { get; set; }

        public string Remarks { get; set; }

        public int FIRId { get; set; }

        [ForeignKey("FIRId")]
        [Display(Name = "FIR ایف آئی آر")]
        public virtual FIR FIR { get; set; }

        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        [Display(Name = "Section سیکشن")]
        public virtual Section Section { get; set; }

        public int CourtDecisionId { get; set; }

        [ForeignKey("CourtDecisionId")]
        [Display(Name = "Court Decision کورٹ فیصلہ")]
        public virtual CourtDecision CourtDecision { get; set; }
    }

    public class EarnedRemission
    {
        public EarnedRemission()
        {
            DateOfRemission = DateTime.Now;
        }

        public int EarnedRemissionId { get; set; }

        [Display(Name = "Date of Remission تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfRemission { get; set; }

        [Required]
        [Display(Name = "Remission By")]
        public string RemissionBy { get; set; }

        [Display(Name = "Labour Type")]
        public LabourType LabourType { get; set; }

        [Display(Name = "Labour Period From تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfLabourStart { get; set; }

        [Display(Name = "Labour Period To تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfLabourEnd { get; set; }

        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        [Display(Name = "Probable Date of Release (PDR) تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfProbableRelease { get; set; }

        [Display(Name = "Remarks تفصیل")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public int RemissionTypeId { get; set; }

        [ForeignKey("RemissionTypeId")]
        public virtual RemissionType RemissionType { get; set; }

        [Display(Name = "Admission ایڈمشن")]
        public int AdmissionId { get; set; }

        [ForeignKey("AdmissionId")]
        [Display(Name = "Admission ایڈمشن")]
        public virtual Admission Admission { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    public class Appeal
    {
        public Appeal()
        {
            DateOfAppeal = DateTime.Now;
        }

        [Key]
        public int AppealId { get; set; }

        [Display(Name = "Prisoner قیدی")]
        public int PrisonerId { get; set; }

        [Display(Name = "Date of Appeal تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfAppeal { get; set; }

        [Display(Name = "Court عدالت")]
        public int CourtId { get; set; }

        [Display(Name = "Status کیفیت")]
        public ApprovalStatus Status { get; set; }

        [Display(Name = "Date of Result تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfResult { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        [ForeignKey("CourtId")]
        [Display(Name = "Court عدالت")]
        public virtual Court Court { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }
    }

    #endregion

    #region VISITORS

    public class Visitor
    {
        public Visitor()
        {
            NationalityCountryId = PresentCountryId = 157;// Pakistan
            PresentProvinceId = 7;                        // Punjab
            PresentDistrictId = 104;                      // Lahore
            PresentCityId = 104;                          // Lahore
        }

        public int VisitorId { get; set; }

        [Display(Name = "Visitor Type ملاقاتی قسم")]
        public VisitorType VisitorType { get; set; }

        public int RelationTypeId { get; set; }

        [ForeignKey("RelationTypeId")]
        [Display(Name = "Relation قیدی سے نسبت")]
        public virtual RelationType RelationType { get; set; }

        [Required]
        [Display(Name = "Visitor CNIC شناختی کارڈ ملاقاتی")]
        [RegularExpression("[0-9+]{5}-[0-9+]{7}-[0-9]{1}", ErrorMessage="Invalid CNIC format!")]
        public string CNIC { get; set; }

        [Required]
        [Display(Name = "Visitor Name نام ملاقاتی")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Father/Husband Name والد/شوہرکانام")]
        public string FatherOrHusbandName { get; set; }

        //[Required]
        [Display(Name = "Date of Birth تاریخ پیدائش")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender جنس")]
        public Gender Gender { get; set; }

        [Display(Name = "Designation عہدہ")]
        public string Designation { get; set; }

        [Display(Name = "Department محکمہ")]
        public string Department { get; set; }

        [Display(Name = "Authority Letter اتھارٹی لیڑر")]
        public string AuthorityLetter { get; set; }

        [Display(Name = "Appointment Start Date تاریخ آٖغاز")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfStart { get; set; }

        [Display(Name = "Appointment Expiry Date تاریخ تنسیخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfExpiry { get; set; }

        public int? OccupationId { get; set; }

        [ForeignKey("OccupationId")]
        [Display(Name = "Occupation پیشہ")]
        public virtual Occupation Occupation { get; set; }

        public int NationalityCountryId { get; set; }

        [ForeignKey("NationalityCountryId")]
        [Display(Name = "Nationality شہریت")]
        public virtual Country Nationality { get; set; }

        // ADDRESS

        [Display(Name = "Country ملک")]
        public int PresentCountryId { get; set; }

        [ForeignKey("PresentCountryId")]
        [Display(Name = "Country ملک")]
        public virtual Country PresentCountry { get; set; }

        [Display(Name = "Province صوبہ")]
        public int PresentProvinceId { get; set; }

        [ForeignKey("PresentProvinceId")]
        [Display(Name = "Province صوبہ")]
        public virtual Province PresentProvince { get; set; }

        [Display(Name = "District ضلع")]
        public int PresentDistrictId { get; set; }

        [ForeignKey("PresentDistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District PresentDistrict { get; set; }

        public int? PresentCityId { get; set; }

        [ForeignKey("PresentCityId")]
        [Display(Name = "City شہر")]
        public virtual City PresentCity { get; set; }

        [Display(Name = "Mouza موضع")]
        public string PresentMouza { get; set; }

        [Display(Name = "House # مکان")]
        public string PresentHouseNumber { get; set; }

        [Display(Name = "Street گلی")]
        public string PresentStreet { get; set; }

        [Display(Name = "Area / Colony کالونی")]
        public string PresentAreaOrColony { get; set; }

        [Display(Name = "Mobile No. موبائل")]
        public string Mobile { get; set; }

        [Display(Name = "Landline No. فون نمبر")]
        public string Landline { get; set; }

        [Display(Name = "Remarks تفصیل")]
        public string Remarks { get; set; }

        public bool IsActive { get; set; }

        public int JailId { get; set; }

        [ForeignKey("JailId")]
        [Display(Name = "Jail جیل")]
        public virtual Jail Jail { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }

    public class Visit
    {
        public Visit()
        {
            DateOfVisit = DateTime.Now;
        }

        public int VisitId { get; set; }

        [Display(Name = "Date of Visit تاریخ ملاقات")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfVisit { get; set; }

        [Display(Name = "Serial No. نمبر")]
        public int SerialNumber { get; set; }

        [Display(Name = "Batch No. پرچہ نمبر")]
        public int BatchNumber { get; set; }

        [Display(Name = "Luggage Details تفصیل سامان")]
        public string LuggageDetails { get; set; }

        [Display(Name = "Purpose of Visit ملاقات کا مقصد")]
        public string PurposeOfVisit { get; set; }

        [Display(Name = "Mobile Token No. موبائل ٹوکن نمبر")]
        public int MobileTokenNumber { get; set; }

        [Display(Name = "Remarks تفصیل")]
        public string Remarks { get; set; }

        public int PrisonerId { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public int VisitorId { get; set; }

        [ForeignKey("VisitorId")]
        [Display(Name = "Visitor ملاقاتی")]
        public virtual Visitor Visitor { get; set; }
    }

    #endregion

    #region JAILS

    public class Jail
    {
        public int JailId { get; set; }
        public int DistrictId { get; set; }

        [Display(Name = "Jail Type نوعیت جیل")]
        public JailType JailType { get; set; }

        [Required]
        [Display(Name = "Jail Name  جیل کا نام")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Authorized Accomodation گنجائش")]
        public int Capacity { get; set; }

        [Display(Name = "Jail Address جیل پتہ")]
        public string Address { get; set; }

        [Display(Name = "Jail Telephone جیل فون")]
        public string Telephone { get; set; }

        [Display(Name = "Superintendent Name سپرانٹنڈنٹ")]
        public string SuperintendentName { get; set; }

        [Display(Name = "Superintendent Mobile No. سپرانٹنڈنٹ فون")]
        public string SuperintendentContactNumber { get; set; }

        [ForeignKey("DistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District District { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class MedicalOfficer
    {
        public int MedicalOfficerId { get; set; }
        public int JailId { get; set; }

        [Required]
        [Display(Name = "Medical Officer Name ڈاکٹر کا نام")]
        public string Name { get; set; }

        [ForeignKey("JailId")]
        [Display(Name = "Jail جیل")]
        public virtual Jail Jail { get; set; }
    }

    public class Barrack
    {
        public int BarrackId { get; set; }
        public int JailId { get; set; }

        [Required]
        [Display(Name = "Barrack بیرک")]
        public string Name { get; set; }

        [ForeignKey("JailId")]
        [Display(Name = "Jail جیل")]
        public virtual Jail Jail { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
    }

    #endregion

    #region SETUP CLASSES

    public class Settings
    {
        public int SettingsId { get; set; }

        public int JailId { get; set; }

        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }

        [ForeignKey("JailId")]
        [Display(Name = "This Jail جیل")]
        public virtual Jail Jail { get; set; }

        [ForeignKey("ProvinceId")]
        [Display(Name = "Province صوبہ")]
        public virtual Province Province { get; set; }

        [ForeignKey("DistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District District { get; set; }

        [ForeignKey("CityId")]
        [Display(Name = "City شہر")]
        public virtual City City { get; set; }
    }

    public class RemissionType
    {
        public int RemissionTypeId { get; set; }

        [Required]
        [Display(Name = "Remission Type نوعیت")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class RemissionOrder
    {
        public int RemissionOrderId { get; set; }

        [Display(Name = "Date of Order تاریخ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfOrder { get; set; }

        [Required]
        [Display(Name = "Order By")]
        public string OrderBy { get; set; }

        [Display(Name = "Occasion")]
        public string Occasion { get; set; }

        [Display(Name = "Letter Reference")]
        public string Reference { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public int RemissionTypeId { get; set; }

        [ForeignKey("RemissionTypeId")]
        public virtual RemissionType RemissionType { get; set; }
    }

    public class CrimeType
    {
        public int CrimeTypeId { get; set; }

        [Required]
        [Display(Name = "Crime Type نوعیت جرم")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }

    public class Act
    {
        public int ActId { get; set; }

        [Required]
        [Display(Name = "Act ایکٹ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Name مختصر")]
        public string ShortName { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Admission> Admissions { get; set; }
    }

    public class Section
    {
        public int SectionId { get; set; }
        public int ActId { get; set; }
        public int CrimeTypeId { get; set; }

        [ForeignKey("ActId")]
        [Display(Name = "Act ایکٹ")]
        public virtual Act Act { get; set; }

        [ForeignKey("CrimeTypeId")]
        [Display(Name = "Crime Type نوعیت جرم")]
        public virtual CrimeType CrimeType { get; set; }

        [Required]
        [Display(Name = "Section سیکشن")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public virtual ICollection<FIR> FIRs { get; set; }
    }

    public class EducationLevel
    {
        public int EducationLevelId { get; set; }

        [Display(Name = "Education Type نظام تعلیم")]
        public EducationType EducationType { get; set; }

        [Required]
        [Display(Name = "Education Level تعلیم")]
        public string Name { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class Occupation
    {
        public int OccupationId { get; set; }

        [Required]
        [Display(Name = "Occupation پیشہ")]
        public string Name { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class Religion
    {
        public int ReligionId { get; set; }

        [Required]
        [Display(Name = "Religion مذہب")]
        public string Name { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class Disease
    {
        public int DiseaseId { get; set; }

        [Required]
        [Display(Name = "Disease Name مرض")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        [Display(Name = "Is Communicable? چھوت کا مرض؟")]
        public bool IsCommunicable { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
    }

    public class MedicalTest
    {
        public int MedicalTestId { get; set; }

        [Required]
        [Display(Name = "Test Name میڈیکل ٹیسٹ")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Country ملک")]
        public string Name { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class Province
    {
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Province صوبہ")]
        public string Name { get; set; }

        [ForeignKey("CountryId")]
        [Display(Name = "Country ملک")]
        public virtual Country Country { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class District
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }

        [Required]
        [Display(Name = "District ضلع")]
        public string Name { get; set; }

        [ForeignKey("ProvinceId")]
        [Display(Name = "Province صوبہ")]
        public virtual Province Province { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<PoliceStation> PoliceStations { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        [Required]
        [Display(Name = "City شہر")]
        public string Name { get; set; }

        [ForeignKey("DistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District District { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
        public virtual ICollection<PoliceStation> PoliceStations { get; set; }
    }

    public class PoliceStation
    {
        public int PoliceStationId { get; set; }
        public int DistrictId { get; set; }
        public int? CityId { get; set; }

        [Required]
        [Display(Name = "Police Station تھانہ")]
        public string Name { get; set; }

        [ForeignKey("DistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District District { get; set; }

        [ForeignKey("CityId")]
        [Display(Name = "City شہر")]
        public virtual City City { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }

    public class PrisonerType
    {
        public int PrisonerTypeId { get; set; }

        [Required]
        [Display(Name = "Prisoner Type قیدی ٹائپ")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public virtual ICollection<PrisonerSubType> PrisonerSubTypes { get; set; }
    }

    public class PrisonerSubType
    {
        public int PrisonerSubTypeId { get; set; }
        public int PrisonerTypeId { get; set; }

        [Required]
        [Display(Name = "Prisoner Subtype قیدی سب ٹائپ")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        [ForeignKey("PrisonerTypeId")]
        [Display(Name = "Prisoner Type قیدی ٹائپ")]
        public virtual PrisonerType PrisonerType { get; set; }
    }

    public class CourtType
    {
        public int CourtTypeId { get; set; }

        [Required]
        [Display(Name = "Court Type عدالت قسم")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public virtual ICollection<Court> Courts { get; set; }
    }

    public class Court
    {
        public int CourtId { get; set; }
        public int CourtTypeId { get; set; }
        public int DistrictId { get; set; }

        [Required]
        [Display(Name = "Court Name عدالت")]
        public string Name { get; set; }

        [ForeignKey("CourtTypeId")]
        [Display(Name = "Court Type عدالت کی قسم")]
        public virtual CourtType CourtType { get; set; }

        [Display(Name = "Court Address عدالت کا پتہ")]
        public string Address { get; set; }

        [ForeignKey("DistrictId")]
        [Display(Name = "District ضلع")]
        public virtual District District { get; set; }

        public virtual ICollection<Judge> Judges { get; set; }
    }

    public class JudgeType
    {
        public int JudgeTypeId { get; set; }

        [Required]
        [Display(Name = "Judge Type جج ٹائپ")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }

        public virtual ICollection<CourtHearing> CourtHearings { get; set; }
        public virtual ICollection<FIR> FIRs { get; set; }
    }

    public class Judge
    {
        public int JudgeId { get; set; }
        public int CourtId { get; set; }

        [Required]
        [Display(Name = "Judge Name جج کا نام")]
        public string Name { get; set; }

        [ForeignKey("CourtId")]
        [Display(Name = "Court عدالت")]
        public virtual Court Court { get; set; }
    }

    public class RelationType
    {
        public int RelationTypeId { get; set; }

        [Required]
        [Display(Name = "Relation Type نسبت")]
        public string Name { get; set; }

        [Display(Name = "Description تفصیل")]
        public string Description { get; set; }
    }

    public class AuditLog
    {
        public long AuditLogId { get; set; }

        [Display(Name = "Operation نوعیت")]
        public string Operation { get; set; }

        [Display(Name = "Record ریکارڈ")]
        public string Entity { get; set; }

        [Display(Name = "Record ID ریکارڈنمبر")]
        public long EntityKey { get; set; }

        [Display(Name = "User کرنے والا")]
        public string UserName { get; set; }

        [Display(Name = "Date/Time تاریخ/وقت")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm:ss tt}")]
        public DateTime OperationDate { get; set; }
    }
    
    #endregion

    #region DEPRECATED / OLD

    public class PrisonerAdmission
    {
        public int PrisonerAdmissionId { get; set; }

        public int PrisonerId { get; set; }

        [Display(Name = "Date of Admission تاریخ داخلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfAdmission { get; set; }

        [Required]
        [Display(Name = "Prisoner Number قیدی نمبر")]
        public string PrisonerNumber { get; set; }

        [Display(Name = "Previous Occupation پیشہ")]
        public string PreviousOccupation { get; set; }

        [Display(Name = "Residence رہائش")]
        public string Residence { get; set; }

        [Display(Name = "Village گاوں")]
        public string Village { get; set; }

        [Display(Name = "Police Station تھانہ")]
        public string PoliceStation { get; set; }

        [Display(Name = "District ضلع")]
        public string District { get; set; }

        [Display(Name = "Personal Description & Height شناخت و قد")]
        public string PersonalDescriptionAndHeight { get; set; }

        [Display(Name = "Transfer Jail or District تبادلہ جیل و ضلع")]
        public string TransferJailOrDistrict { get; set; }

        [Display(Name = "Charged with Act & Section فرد جرم سیکشن")]
        public string ChargedWithActAndSection { get; set; }

        [Display(Name = "Health on Admission داخلہ پر صحت")]
        public string HealthOnAdmission { get; set; }

        [Display(Name = "Health on Release رہائی پر صحت")]
        public string HealthOnRelease { get; set; }

        [Display(Name = "Weight on Admission داخلہ پر وزن")]
        public string WeightOnAdmission { get; set; }

        [Display(Name = "Weight on Release رہائی پر وزن")]
        public string WeightOnRelease { get; set; }

        [Display(Name = "Education on Admission داخلہ پر تعلیم")]
        public string EducationOnAdmission { get; set; }

        [Display(Name = "Education on Release رہائی پر تعلیم")]
        public string EducationOnRelease { get; set; }

        [Display(Name = "Date of Warrant Commitment تاریخ وارنٹ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfWarrantCommitment { get; set; }

        [Display(Name = "Date to Produce Prisoner تاریخ پیشی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateToProducePrisoner { get; set; }

        [Display(Name = "Date of Remand تاریخ ریمانڈ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRemand { get; set; }

        [Display(Name = "Date of Sentence تاریخ سزا")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfSentence { get; set; }

        [Display(Name = "Date of Release with Full Sentence تاریخ رہائی مکمل سزا")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfReleaseWithFullSentence { get; set; }

        [Display(Name = "Sentencing Court عدالت")]
        public string SentencingCourt { get; set; }

        [Display(Name = "Convicted under Section قانونی شق")]
        public string ConvictedUnderSection { get; set; }

        [Display(Name = "Sentence فرد جرم")]
        public string Sentence { get; set; }

        public int ClassificationId { get; set; }

        [Display(Name = "Previous Conviction سابقہ جرم")]
        public string PreviousConviction { get; set; }

        [Display(Name = "Date of Payment of Fine تاریخ فائن")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfPaymentOfFine { get; set; }

        [Display(Name = "Date of Appeal تاریخ اپیل")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfAppeal { get; set; }

        [Display(Name = "Result of Appeal اپیل نتیجہ")]
        public string ResultOfAppeal { get; set; }

        [Display(Name = "Date of Imposition تاریخ آغاز")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfImposition { get; set; }

        [Display(Name = "Date of Removal تاریخ اختتام")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRemoval { get; set; }

        [Display(Name = "Date of Release تاریخ رہائی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }

        [Display(Name = "Authority for Release رہائی حکم نامہ")]
        public string AuthorityForRelease { get; set; }

        [Display(Name = "Date of Disposal تاریخ خاتمہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfDisposal { get; set; }

        [Display(Name = "Barrack / Cell بیرک")]
        public string BarrackOrCell { get; set; }

        [Display(Name = "Remarks نوٹس")]
        public string Remarks { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        [ForeignKey("ClassificationId")]
        [Display(Name = "Classification قسم بندی")]
        public virtual Classification Classification { get; set; }
    }

    public class Classification
    {
        public int ClassificationId { get; set; }
        public string Name { get; set; }
    }

    public class MedicalExam
    {
        public int MedicalExamId { get; set; }

        public int PrisonerId { get; set; }

        [Display(Name = "Date of Examination تاریخ معائنہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfExamination { get; set; }

        [Required]
        [Display(Name = "Medical Officer ڈاکٹر")]
        public string MedicalOfficer { get; set; }

        [Required]
        [Display(Name = "Diagnosis تشخیص")]
        public string Diagnosis { get; set; }

        [Display(Name = "Treatment علاج")]
        public string Treatment { get; set; }

        [Display(Name = "Dietary Requirements پرہیز")]
        public string DietaryRequirements { get; set; }

        [ForeignKey("PrisonerId")]
        [Display(Name = "Prisoner قیدی")]
        public virtual Prisoner Prisoner { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }
    }

    public class Prison
    {
        public int PrisonId { get; set; }

        [Required]
        public string Name { get; set; }

        public string District { get; set; }

        public string Province { get; set; }

        [Required]
        public int Capacity { get; set; }

    } 

    #endregion

    #region VIEW MODELS

    public class BaseFilter
    {
        public int? Page
        {
            get
            {
                if (_page == null)
                    _page = 1;

                return _page;
            }
            set
            {
                _page = value;
            }
        }
        private int? _page = 1;
    }

    public class PrisonerFilter : BaseFilter
    {
        public PrisonerCategory? Category { get; set; }
        public PrisonerStatus? Status { get; set; }
        public PrisonerClass? Class { get; set; }
        public Gender? Gender { get; set; }
        
        public string Name { get; set; }
        public string PrisonerNumber { get; set; }
        public string Parentage { get; set; }

        public string District { get; set; }
        public string FIRNumber { get; set; }
        public string Section { get; set; }
        public string TrialCourt { get; set; }
        public string PoliceStation { get; set; }

        public DateTime? AdmissionFrom { get; set; }
        public DateTime? AdmissionTo { get; set; }
        public DateTime? ReleaseFrom { get; set; }
        public DateTime? ReleaseTo { get; set; }
        public DateTime? HearingFrom { get; set; }
        public DateTime? HearingTo { get; set; }

        // MEDICAL INFO

        public int? Age { get; set; }
        public string IdentificationMark1 { get; set; }
        public string IdentificationMark2 { get; set; }

        public string Scar { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodGroup { get; set; }
        public string CommunicableDisease { get; set; }
        public string HealthOnAdmission { get; set; }
        public string HealthOnRelease { get; set; }
        public string WeightOnAdmission { get; set; }
        public string WeightOnRelease { get; set; }
    }

    public class PrisonerReport
    {
        public IPagedList<PrisonerViewModel> Prisoners { get; set; }
        public PrisonerFilter Filter { get; set; }

        public PrisonerReport()
        {
            Filter = new PrisonerFilter();
        }

        public PrisonerReport(IPagedList<PrisonerViewModel> prisoners, PrisonerFilter filter)
        {
            Prisoners = prisoners;
            Filter = filter;
        }
    }

    public class PrisonerViewModel
    {
        public int PrisonerId { get; set; }

        [Display(Name = "Computer No. کمپیوٹر نمبر")]
        public int? AdmissionId { get; set; }

        [Display(Name = "Prisoner Number قیدی نمبر")]
        public string PrisonerNumber { get; set; }

        [Display(Name = "CNIC شناختی کارڈ")]
        [RegularExpression("[0-9+]{5}-[0-9+]{7}-[0-9]{1}")]
        public string CNIC { get; set; }

        [Display(Name = "Name نام")]
        public string Name { get; set; }

        [Display(Name = "Parentage ولدیت")]
        public string Parentage { get; set; }

        [Display(Name = "Category قسم")]
        public PrisonerCategory Category { get; set; }

        [Display(Name = "Status کیفیت")]
        public PrisonerStatus Status { get; set; }

        [Display(Name = "Class کلاس")]
        public PrisonerClass Class { get; set; }

        [Display(Name = "Gender جنس")]
        public Gender Gender { get; set; }

        [Display(Name = "District ضلع")]
        public string District { get; set; }

        [Display(Name = "FIRs ایف آئی آر نمبر")]
        public string FIRs { get; set; }

        [Display(Name = "Sections سیکشنز")]
        public string UnderSections { get; set; }

        [Display(Name = "Police Stations تھانہ")]
        public string PoliceStation { get; set; }

        [Display(Name = "Date of Admission تاریخ داخلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfAdmission { get; set; }

        [Display(Name = "Date of Release تاریخ رہائی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }

        [Display(Name = "Years سال")]
        public int Years { get; set; }

        [Display(Name = "Months مہینے")]
        public int Months { get; set; }

        [Display(Name = "Days دن")]
        public int Days { get; set; }

        [Display(Name = "Trial Court عدالت")]
        public string TrialCourt { get; set; }

        [Display(Name = "Date of Next Hearing تاریخ پیشی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfHearing { get; set; }

        public int Children { get; set; }

        // MEDICAL INFO

        [Display(Name = "Identification Mark شناختی نشان 1 ")]
        public string IdentificationMark1 { get; set; }

        [Display(Name = "Identification Mark شناختی نشان 2 ")]
        public string IdentificationMark2 { get; set; }

        [Display(Name = "Age عمر")]
        public int Age { get; set; }

        [Display(Name = "Scar زخم کا نشان")]
        public string Scar { get; set; }

        [Display(Name = "Height قد")]
        public string Height { get; set; }

        [Display(Name = "Weight (kg) وزن")]
        public string Weight { get; set; }

        [Display(Name = "Blood Group بلڈ گروپ")]
        public string BloodGroup { get; set; }

        [Display(Name = "Communicable Disease چھوت کی بیماری")]
        public string CommunicableDisease { get; set; }

        [Display(Name = "Health on Admission داخلہ پر صحت")]
        public string HealthOnAdmission { get; set; }

        [Display(Name = "Health on Release رہائی پر صحت")]
        public string HealthOnRelease { get; set; }

        [Display(Name = "Weight on Admission داخلہ پر وزن")]
        public string WeightOnAdmission { get; set; }

        [Display(Name = "Weight on Release رہائی پر وزن")]
        public string WeightOnRelease { get; set; }

        public int FIRCount { get; set; }
    }

    public class AdmissionFIR
    {
        public ICollection<FIR> FIRs { get; set; }

        public Admission Admission { get; set; }
        public FIR FIR { get; set; }
        public CourtHearing CourtHearing { get; set; }

        public AdmissionFIR() :this(0, 0)
        {
                
        }

        public AdmissionFIR(int prisonerId, int admissionId)
        {
            Admission = new Admission();
            Admission.AdmissionId = admissionId;

            FIR = new FIR();
            FIR.AdmissionId = admissionId;

            CourtHearing = new CourtHearing();
            CourtHearing.PrisonerId = prisonerId;
            CourtHearing.AdmissionId = admissionId;
        }
    }

    public class VisitViewModel
    {
        public Visit Visit { get; set; }
        public Visitor Visitor { get; set; }

        public VisitViewModel()
        {
            Visit = new Visit();
            Visitor = new Visitor();

            Visitor.Visits = new List<Visit>();
        }
    }

    public class ReleasedPrisoner
    {
        public int PrisonerId { get; set; }

        [Display(Name = "No. نمبر شمار")]
        public int SerialNumber { get; set; }

        [Display(Name = "Computer No. کمپیوٹر نمبر")]
        public int ComputerNumber { get; set; }

        [Display(Name = "Prisoner No. قیدی نمبر")]
        public string PrisonerNumber { get; set; }

        [Display(Name = "Name نام")]
        public string Name { get; set; }

        [Display(Name = "Father/Husband Name والد/شوہرکانام")]
        public string FatherOrHusbandName { get; set; }

        [Display(Name = "Date of Admission تاریخ داخلہ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateOfAdmission { get; set; }

        [Display(Name = "Date of Release تاریخ ریائی")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }

        [Display(Name = "Years سال")]
        public int Years { get; set; }

        [Display(Name = "Months مہینے")]
        public int Months { get; set; }

        [Display(Name = "Days دن")]
        public int Days { get; set; }

        [Display(Name = "Health on Admission داخلہ پر صحت")]
        public string HealthOnAdmission { get; set; }

        [Display(Name = "Health on Release رہائی پر صحت")]
        public string HealthOnRelease { get; set; }

        [Display(Name = "Weight on Admission داخلہ پر وزن")]
        public string WeightOnAdmission { get; set; }

        [Display(Name = "Weight on Release رہائی پر وزن")]
        public string WeightOnRelease { get; set; }
    }

    #endregion
}