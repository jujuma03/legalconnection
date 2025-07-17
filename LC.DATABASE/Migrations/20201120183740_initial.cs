using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LC.DATABASE.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Headline = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    RouteType = table.Column<byte>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true),
                    SequenceOrder = table.Column<byte>(nullable: true),
                    UrlDirection = table.Column<string>(nullable: true),
                    StatusDirection = table.Column<byte>(nullable: false),
                    NameDirection = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true),
                    LawyerFullName = table.Column<string>(nullable: true),
                    LawyerPhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalPublications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrequentQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Icon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HowItWorksSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Order = table.Column<byte>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowItWorksSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionVisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UrlImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionVisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DescriptionLC = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    IntervalCount = table.Column<int>(nullable: false),
                    Interval = table.Column<byte>(nullable: false),
                    TrialDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true),
                    HeadLine = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shortcuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UrlDirection = table.Column<string>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    ParentShortcutId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shortcuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shortcuts_Shortcuts_ParentShortcutId",
                        column: x => x.ParentShortcutId,
                        principalTable: "Shortcuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UrlDirection = table.Column<string>(nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    OfficialName = table.Column<string>(nullable: true),
                    ColloquialName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    Ubigeo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanBenefits",
                columns: table => new
                {
                    PlanId = table.Column<string>(nullable: false),
                    BenefitId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanBenefits", x => new { x.PlanId, x.BenefitId });
                    table.ForeignKey(
                        name: "FK_PlanBenefits_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Benefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanBenefits_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialityThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    OfficialName = table.Column<string>(nullable: true),
                    ColloquialName = table.Column<string>(nullable: true),
                    SpecialityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialityThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialityThemes_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surnames = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<byte>(nullable: false, defaultValue: (byte)3),
                    Picture = table.Column<string>(nullable: true),
                    DistrictId = table.Column<Guid>(nullable: true),
                    LastConnection = table.Column<DateTime>(nullable: false),
                    RegisterBy = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    DocumentType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    ApplicationRoleId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lawyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CAL = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    FreeFirst = table.Column<bool>(nullable: false),
                    AboutMe = table.Column<string>(nullable: true),
                    ProfileWithChanges = table.Column<bool>(nullable: false),
                    FreeLegalCases = table.Column<int>(nullable: false),
                    FreeUser = table.Column<bool>(nullable: false, defaultValue: true),
                    PublicProfile = table.Column<bool>(nullable: false, defaultValue: true),
                    TermsAndConditions = table.Column<bool>(nullable: false),
                    ValidationDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawyers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lawyers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ValidationDate = table.Column<DateTime>(nullable: true),
                    DerivedDate = table.Column<DateTime>(nullable: true),
                    SelectionLawyerStartDate = table.Column<DateTime>(nullable: true),
                    Reviewed = table.Column<bool>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    ProvinceId = table.Column<Guid>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UrlFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalCases_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCases_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerCards",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    CardBrand = table.Column<string>(nullable: true),
                    LastCardDigits = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerCards_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerExperiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    WorkArea = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    LawyerId = table.Column<Guid>(nullable: false),
                    TemporalStatus = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerExperiences_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerInterviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Selected = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StartRange = table.Column<TimeSpan>(nullable: false),
                    EndRange = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerInterviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerInterviews_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    TemporalStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerLanguages_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    Process = table.Column<byte>(nullable: false),
                    HasBeenCorrected = table.Column<bool>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerObservations_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerPlanHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlanId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerPlanHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerPlanHistories_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerPlanHistories_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    AnswerDate = table.Column<DateTime>(nullable: true),
                    LawyerId = table.Column<Guid>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerPublications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerPublications_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerSpecialityThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SpecialityThemeId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerSpecialityThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerSpecialityThemes_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerSpecialityThemes_SpecialityThemes_SpecialityThemeId",
                        column: x => x.SpecialityThemeId,
                        principalTable: "SpecialityThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerStudies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Grade = table.Column<byte>(nullable: false),
                    Mention = table.Column<string>(nullable: true),
                    Ubication = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    TemporalStatus = table.Column<byte>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerStudies_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerWithdrawalInfos",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    FinancialEntity = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    BankAccount = table.Column<string>(nullable: true),
                    InterbankAccount = table.Column<string>(nullable: true),
                    Dni = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerWithdrawalInfos", x => x.LawyerId);
                    table.ForeignKey(
                        name: "FK_LawyerWithdrawalInfos_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerWithdrawalRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    UlrReceiptFileForFees = table.Column<string>(nullable: true),
                    Status = table.Column<byte>(nullable: false, defaultValue: (byte)1),
                    Observation = table.Column<string>(nullable: true),
                    UrlDepositReceipt = table.Column<string>(nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: true),
                    BankAccount = table.Column<string>(nullable: true),
                    InterbankAccount = table.Column<string>(nullable: true),
                    Dni = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    FinancialEntity = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerWithdrawalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerWithdrawalRequests_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporalLawyers",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    CAL = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    FreeFirst = table.Column<bool>(nullable: false),
                    AboutMe = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surnames = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<byte>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    DistrictId = table.Column<Guid>(nullable: true),
                    DocumentType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalLawyers", x => x.LawyerId);
                    table.ForeignKey(
                        name: "FK_TemporalLawyers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerQualifications",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Commentary = table.Column<string>(nullable: true),
                    Qualification = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerQualifications", x => new { x.LawyerId, x.LegalCaseId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerQualifications_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseApplicantLawyers",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    ApplicationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    ResponseTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseApplicantLawyers", x => new { x.LawyerId, x.LegalCaseId });
                    table.ForeignKey(
                        name: "FK_LegalCaseApplicantLawyers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseApplicantLawyers_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseDelayedTasks",
                columns: table => new
                {
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    Task = table.Column<byte>(nullable: false),
                    HangfireJobId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseDelayedTasks", x => new { x.LegalCaseId, x.Task });
                    table.ForeignKey(
                        name: "FK_LegalCaseDelayedTasks_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseFiledLawyers",
                columns: table => new
                {
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EndDateTimeAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseFiledLawyers", x => new { x.LawyerId, x.LegalCaseId });
                    table.ForeignKey(
                        name: "FK_LegalCaseFiledLawyers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseFiledLawyers_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseLawyers",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    ResponseTime = table.Column<int>(nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseLawyers", x => new { x.LawyerId, x.LegalCaseId });
                    table.ForeignKey(
                        name: "FK_LegalCaseLawyers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseLawyers_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseObservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Observation = table.Column<string>(nullable: true),
                    Process = table.Column<byte>(nullable: false),
                    HasBeenCorrected = table.Column<bool>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    CreatorRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalCaseObservations_AspNetRoles_CreatorRoleId",
                        column: x => x.CreatorRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseObservations_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseObservations_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LegalCaseQuestionId = table.Column<Guid>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalCaseResponses_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseResponses_LegalCaseQuestions_LegalCaseQuestionId",
                        column: x => x.LegalCaseQuestionId,
                        principalTable: "LegalCaseQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalCaseSpecialityThemes",
                columns: table => new
                {
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    SpecialityThemeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCaseSpecialityThemes", x => new { x.LegalCaseId, x.SpecialityThemeId });
                    table.ForeignKey(
                        name: "FK_LegalCaseSpecialityThemes_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalCaseSpecialityThemes_SpecialityThemes_SpecialityThemeId",
                        column: x => x.SpecialityThemeId,
                        principalTable: "SpecialityThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LegalCaseId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    OnlinePaymentId = table.Column<string>(nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    IgvAmount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    LawyerAmount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_LegalCases_LegalCaseId",
                        column: x => x.LegalCaseId,
                        principalTable: "LegalCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerPlanDetails",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(nullable: false),
                    PlanId = table.Column<string>(nullable: true),
                    SubscriptionId = table.Column<string>(nullable: true),
                    LawyerCardId = table.Column<string>(nullable: true),
                    TempStartDate = table.Column<DateTime>(nullable: true),
                    TempEndDate = table.Column<DateTime>(nullable: true),
                    Canceled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerPlanDetails", x => x.LawyerId);
                    table.ForeignKey(
                        name: "FK_LawyerPlanDetails_LawyerCards_LawyerCardId",
                        column: x => x.LawyerCardId,
                        principalTable: "LawyerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerPlanDetails_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerPlanDetails_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporalLawyerExperiences",
                columns: table => new
                {
                    LawyerExperienceId = table.Column<Guid>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    WorkArea = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalLawyerExperiences", x => x.LawyerExperienceId);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerExperiences_LawyerExperiences_LawyerExperienceId",
                        column: x => x.LawyerExperienceId,
                        principalTable: "LawyerExperiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerExperiences_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporalLawyerLanguages",
                columns: table => new
                {
                    LawyerLanguageId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Level = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalLawyerLanguages", x => x.LawyerLanguageId);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerLanguages_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerLanguages_LawyerLanguages_LawyerLanguageId",
                        column: x => x.LawyerLanguageId,
                        principalTable: "LawyerLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    ExternalPublicationId = table.Column<Guid>(nullable: true),
                    LawyerPublicationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_ExternalPublications_ExternalPublicationId",
                        column: x => x.ExternalPublicationId,
                        principalTable: "ExternalPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_LawyerPublications_LawyerPublicationId",
                        column: x => x.LawyerPublicationId,
                        principalTable: "LawyerPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporalLawyerSpecialityThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LawyerSpecialityThemeId = table.Column<Guid>(nullable: true),
                    SpecialityThemeId = table.Column<Guid>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalLawyerSpecialityThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerSpecialityThemes_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerSpecialityThemes_LawyerSpecialityThemes_LawyerSpecialityThemeId",
                        column: x => x.LawyerSpecialityThemeId,
                        principalTable: "LawyerSpecialityThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerSpecialityThemes_SpecialityThemes_SpecialityThemeId",
                        column: x => x.SpecialityThemeId,
                        principalTable: "SpecialityThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemporalLawyerStudies",
                columns: table => new
                {
                    LawyerStudyId = table.Column<Guid>(nullable: false),
                    Grade = table.Column<byte>(nullable: false),
                    Mention = table.Column<string>(nullable: true),
                    Ubication = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    LawyerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalLawyerStudies", x => x.LawyerStudyId);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerStudies_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalLawyerStudies_LawyerStudies_LawyerStudyId",
                        column: x => x.LawyerStudyId,
                        principalTable: "LawyerStudies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LawyerWithdrawals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LawyerId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    WithdrawalRequestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerWithdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerWithdrawals_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LawyerWithdrawals_LawyerWithdrawalRequests_WithdrawalRequestId",
                        column: x => x.WithdrawalRequestId,
                        principalTable: "LawyerWithdrawalRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationRoleId",
                table: "AspNetUserRoles",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DistrictId",
                table: "AspNetUsers",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ExternalPublicationId",
                table: "Blogs",
                column: "ExternalPublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_LawyerPublicationId",
                table: "Blogs",
                column: "LawyerPublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerCards_LawyerId",
                table: "LawyerCards",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerExperiences_LawyerId",
                table: "LawyerExperiences",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerInterviews_LawyerId",
                table: "LawyerInterviews",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerLanguages_LanguageId",
                table: "LawyerLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerLanguages_LawyerId",
                table: "LawyerLanguages",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerObservations_LawyerId",
                table: "LawyerObservations",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerPlanDetails_LawyerCardId",
                table: "LawyerPlanDetails",
                column: "LawyerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerPlanDetails_PlanId",
                table: "LawyerPlanDetails",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerPlanHistories_LawyerId",
                table: "LawyerPlanHistories",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerPlanHistories_PlanId",
                table: "LawyerPlanHistories",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerPublications_LawyerId",
                table: "LawyerPublications",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerQualifications_ClientId",
                table: "LawyerQualifications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerQualifications_LegalCaseId",
                table: "LawyerQualifications",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_UserId",
                table: "Lawyers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerSpecialityThemes_LawyerId",
                table: "LawyerSpecialityThemes",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerSpecialityThemes_SpecialityThemeId",
                table: "LawyerSpecialityThemes",
                column: "SpecialityThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerStudies_LawyerId",
                table: "LawyerStudies",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerWithdrawalRequests_LawyerId",
                table: "LawyerWithdrawalRequests",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerWithdrawals_LawyerId",
                table: "LawyerWithdrawals",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerWithdrawals_WithdrawalRequestId",
                table: "LawyerWithdrawals",
                column: "WithdrawalRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseApplicantLawyers_LegalCaseId",
                table: "LegalCaseApplicantLawyers",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseFiledLawyers_LegalCaseId",
                table: "LegalCaseFiledLawyers",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseLawyers_LegalCaseId",
                table: "LegalCaseLawyers",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseObservations_CreatorRoleId",
                table: "LegalCaseObservations",
                column: "CreatorRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseObservations_CreatorUserId",
                table: "LegalCaseObservations",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseObservations_LegalCaseId",
                table: "LegalCaseObservations",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseResponses_LegalCaseId",
                table: "LegalCaseResponses",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseResponses_LegalCaseQuestionId",
                table: "LegalCaseResponses",
                column: "LegalCaseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCases_ClientId",
                table: "LegalCases",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCases_ProvinceId",
                table: "LegalCases",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalCaseSpecialityThemes_SpecialityThemeId",
                table: "LegalCaseSpecialityThemes",
                column: "SpecialityThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LawyerId",
                table: "Payments",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LegalCaseId",
                table: "Payments",
                column: "LegalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanBenefits_BenefitId",
                table: "PlanBenefits",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_DepartmentId",
                table: "Provinces",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortcuts_ParentShortcutId",
                table: "Shortcuts",
                column: "ParentShortcutId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialityThemes_SpecialityId",
                table: "SpecialityThemes",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerExperiences_LawyerId",
                table: "TemporalLawyerExperiences",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerLanguages_LanguageId",
                table: "TemporalLawyerLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerLanguages_LawyerId",
                table: "TemporalLawyerLanguages",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerSpecialityThemes_LawyerId",
                table: "TemporalLawyerSpecialityThemes",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerSpecialityThemes_LawyerSpecialityThemeId",
                table: "TemporalLawyerSpecialityThemes",
                column: "LawyerSpecialityThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerSpecialityThemes_SpecialityThemeId",
                table: "TemporalLawyerSpecialityThemes",
                column: "SpecialityThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalLawyerStudies_LawyerId",
                table: "TemporalLawyerStudies",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UserId",
                table: "UserNotifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "FrequentQuestions");

            migrationBuilder.DropTable(
                name: "HowItWorksSteps");

            migrationBuilder.DropTable(
                name: "LawyerInterviews");

            migrationBuilder.DropTable(
                name: "LawyerObservations");

            migrationBuilder.DropTable(
                name: "LawyerPlanDetails");

            migrationBuilder.DropTable(
                name: "LawyerPlanHistories");

            migrationBuilder.DropTable(
                name: "LawyerQualifications");

            migrationBuilder.DropTable(
                name: "LawyerWithdrawalInfos");

            migrationBuilder.DropTable(
                name: "LawyerWithdrawals");

            migrationBuilder.DropTable(
                name: "LegalCaseApplicantLawyers");

            migrationBuilder.DropTable(
                name: "LegalCaseDelayedTasks");

            migrationBuilder.DropTable(
                name: "LegalCaseFiledLawyers");

            migrationBuilder.DropTable(
                name: "LegalCaseLawyers");

            migrationBuilder.DropTable(
                name: "LegalCaseObservations");

            migrationBuilder.DropTable(
                name: "LegalCaseResponses");

            migrationBuilder.DropTable(
                name: "LegalCaseSpecialityThemes");

            migrationBuilder.DropTable(
                name: "MissionVisions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PlanBenefits");

            migrationBuilder.DropTable(
                name: "SectionItems");

            migrationBuilder.DropTable(
                name: "Shortcuts");

            migrationBuilder.DropTable(
                name: "SocialNetworks");

            migrationBuilder.DropTable(
                name: "TemporalLawyerExperiences");

            migrationBuilder.DropTable(
                name: "TemporalLawyerLanguages");

            migrationBuilder.DropTable(
                name: "TemporalLawyers");

            migrationBuilder.DropTable(
                name: "TemporalLawyerSpecialityThemes");

            migrationBuilder.DropTable(
                name: "TemporalLawyerStudies");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "ExternalPublications");

            migrationBuilder.DropTable(
                name: "LawyerPublications");

            migrationBuilder.DropTable(
                name: "LawyerCards");

            migrationBuilder.DropTable(
                name: "LawyerWithdrawalRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LegalCaseQuestions");

            migrationBuilder.DropTable(
                name: "LegalCases");

            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "LawyerExperiences");

            migrationBuilder.DropTable(
                name: "LawyerLanguages");

            migrationBuilder.DropTable(
                name: "LawyerSpecialityThemes");

            migrationBuilder.DropTable(
                name: "LawyerStudies");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "SpecialityThemes");

            migrationBuilder.DropTable(
                name: "Lawyers");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
