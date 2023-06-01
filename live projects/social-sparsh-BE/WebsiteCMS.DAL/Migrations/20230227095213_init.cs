using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteCMS.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTAPILogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTAPILogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTAvatar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTAvatar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTComponent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTVisitor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorUUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTVisitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTWhatsApptemplateRegisterIssus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTWhatsApptemplateRegisterIssus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTWhatsAppTemplatesStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhatsAppTemplateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTWhatsAppTemplatesStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCountry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMAlign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMAlign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMColor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMFontFamily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMFontFamily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TagImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateFieldType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateFieldType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSocialPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSocialPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FBProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityToSell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemGroupId = table.Column<long>(type: "bigint", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingWeight = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSFieldType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSFieldType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSMasterType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSMasterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSTemplatePageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSTemplatePageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredPathURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetRefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "AspNetUserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserIdId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserProfile_AspNetUsers_AppUserIdId",
                        column: x => x.AppUserIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "tblBOTChatBot",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTChatBot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTChatBot_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsUserDefined = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSProductCategories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTWebHookResponse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BussinessId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BOTVisitorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTWebHookResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTWebHookResponse_tblBOTVisitor_BOTVisitorsId",
                        column: x => x.BOTVisitorsId,
                        principalTable: "tblBOTVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessContactInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBusinessContactInfo_tblBusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "tblBusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessInfoCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessInfoId = table.Column<int>(type: "int", nullable: false),
                    BusinessCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessInfoCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBusinessInfoCategorys_tblBusinessCategorys_BusinessCategoryId",
                        column: x => x.BusinessCategoryId,
                        principalTable: "tblBusinessCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBusinessInfoCategorys_tblBusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "tblBusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessLocationInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessInfoId = table.Column<int>(type: "int", nullable: false),
                    StreetAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessLocationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBusinessLocationInfo_tblBusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "tblBusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessServiceArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessServiceArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBusinessServiceArea_tblBusinessInfo_BusinessInfoId",
                        column: x => x.BusinessInfoId,
                        principalTable: "tblBusinessInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblState_tblCountry_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tblCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMSubCategory_tblSCRMCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblSCRMCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicTemplateImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiumTemplateImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicTemplatePreviewImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiumTemplatePreviewImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplate_tblSCRMColor_ColorId",
                        column: x => x.ColorId,
                        principalTable: "tblSCRMColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplate_tblSCRMLanguage_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "tblSCRMLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateFieldTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateField_tblSCRMTemplateFieldType_TemplateFieldTypeId",
                        column: x => x.TemplateFieldTypeId,
                        principalTable: "tblSCRMTemplateFieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblFacebookPagesTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialId = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Access_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowersCount = table.Column<int>(type: "int", nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectedInstagramAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleLineAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialPlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFacebookPagesTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFacebookPagesTokens_tblSocialPlatforms_SocialPlatformsId",
                        column: x => x.SocialPlatformsId,
                        principalTable: "tblSocialPlatforms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSProductFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSProductFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSProductFields_tblWCMSFieldType_FieldTypeId",
                        column: x => x.FieldTypeId,
                        principalTable: "tblWCMSFieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSFieldsMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterTypeId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldtypeId = table.Column<int>(type: "int", nullable: false),
                    Syntax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Selector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewSelector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    IsUserVisible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSFieldsMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSFieldsMasters_tblWCMSFieldType_FieldtypeId",
                        column: x => x.FieldtypeId,
                        principalTable: "tblWCMSFieldType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSFieldsMasters_tblWCMSMasterType_MasterTypeId",
                        column: x => x.MasterTypeId,
                        principalTable: "tblWCMSMasterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSTemplatePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    DisplayPageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplatePageTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSTemplatePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSTemplatePages_tblWCMSTemplatePageType_TemplatePageTypeId",
                        column: x => x.TemplatePageTypeId,
                        principalTable: "tblWCMSTemplatePageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSTemplatePages_tblWCMSTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblWCMSTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSUserTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsPreview = table.Column<int>(type: "int", nullable: false),
                    ColorGroupId = table.Column<int>(type: "int", nullable: true),
                    FontGroupId = table.Column<int>(type: "int", nullable: true),
                    GATagId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookPixelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSUserTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplates_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplates_tblWCMSTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblWCMSTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTPlatform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTPlatform", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTPlatform_tblBOTChatBot_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "tblBOTChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<long>(type: "bigint", nullable: false),
                    FrontendId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSkippable = table.Column<bool>(type: "bit", nullable: false),
                    IsLeadMessage = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTQuestion_tblBOTChatBot_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "tblBOTChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBOTQuestion_tblBOTComponent_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "tblBOTComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTWhatsAppBusinessData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhNoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WAToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false),
                    errorMessageID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTWhatsAppBusinessData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTWhatsAppBusinessData_tblBOTChatBot_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "tblBOTChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSCategoryWiseProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSCategoryWiseProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSCategoryWiseProducts_tblWCMSProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "tblWCMSProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBusinessPhoneNos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactInfoId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBusinessPhoneNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBusinessPhoneNos_tblBusinessContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "tblBusinessContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false),
                    CountriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCity_tblCountry_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "tblCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCity_tblState_StatesId",
                        column: x => x.StatesId,
                        principalTable: "tblState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMCaptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SCRMCategoryID = table.Column<int>(type: "int", nullable: true),
                    SCRMSubCategoryId = table.Column<int>(type: "int", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMCaptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMCaptions_tblSCRMCategory_SCRMCategoryID",
                        column: x => x.SCRMCategoryID,
                        principalTable: "tblSCRMCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblSCRMCaptions_tblSCRMSubCategory_SCRMSubCategoryId",
                        column: x => x.SCRMSubCategoryId,
                        principalTable: "tblSCRMSubCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateCategory_tblSCRMCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblSCRMCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateCategory_tblSCRMTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblSCRMTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateSubCategory_tblSCRMSubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "tblSCRMSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateSubCategory_tblSCRMTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblSCRMTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateTag_tblSCRMTag_TagId",
                        column: x => x.TagId,
                        principalTable: "tblSCRMTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateTag_tblSCRMTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblSCRMTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateFieldImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    TemplateFieldId = table.Column<int>(type: "int", nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateFieldImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldImage_tblSCRMTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblSCRMTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldImage_tblSCRMTemplateField_TemplateFieldId",
                        column: x => x.TemplateFieldId,
                        principalTable: "tblSCRMTemplateField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMTemplateFieldText",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    TemplateFieldId = table.Column<int>(type: "int", nullable: false),
                    FontFamilyId = table.Column<int>(type: "int", nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    AlignId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMTemplateFieldText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldText_tblSCRMAlign_AlignId",
                        column: x => x.AlignId,
                        principalTable: "tblSCRMAlign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldText_tblSCRMFontFamily_FontFamilyId",
                        column: x => x.FontFamilyId,
                        principalTable: "tblSCRMFontFamily",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldText_tblSCRMTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblSCRMTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMTemplateFieldText_tblSCRMTemplateField_TemplateFieldId",
                        column: x => x.TemplateFieldId,
                        principalTable: "tblSCRMTemplateField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMUserMetaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TemplateFieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMUserMetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMUserMetaData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSCRMUserMetaData_tblSCRMTemplateField_TemplateFieldId",
                        column: x => x.TemplateFieldId,
                        principalTable: "tblSCRMTemplateField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSCRMUserCategoryList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FBTokenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSCRMUserCategoryList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSCRMUserCategoryList_tblFacebookPagesTokens_FBTokenId",
                        column: x => x.FBTokenId,
                        principalTable: "tblFacebookPagesTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSociaMediaPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacebookPagesTokensId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSociaMediaPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSociaMediaPost_tblFacebookPagesTokens_FacebookPagesTokensId",
                        column: x => x.FacebookPagesTokensId,
                        principalTable: "tblFacebookPagesTokens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSFieldsMasterChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    FieldsMasterId = table.Column<int>(type: "int", nullable: false),
                    MasterTypeId = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSFieldsMasterChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSFieldsMasterChild_tblWCMSFieldsMasters_FieldsMasterId",
                        column: x => x.FieldsMasterId,
                        principalTable: "tblWCMSFieldsMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSFieldsMasterChild_tblWCMSMasterType_MasterTypeId",
                        column: x => x.MasterTypeId,
                        principalTable: "tblWCMSMasterType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblWCMSFieldsMasterChild_tblWCMSTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "tblWCMSTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSTemplatePageFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplatePageId = table.Column<int>(type: "int", nullable: false),
                    FieldsMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSTemplatePageFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSTemplatePageFields_tblWCMSFieldsMasters_FieldsMasterId",
                        column: x => x.FieldsMasterId,
                        principalTable: "tblWCMSFieldsMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSTemplatePageFields_tblWCMSTemplatePages_TemplatePageId",
                        column: x => x.TemplatePageId,
                        principalTable: "tblWCMSTemplatePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSUserTemplatesChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTemplateId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSUserTemplatesChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplatesChild_tblSocialPlatforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "tblSocialPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplatesChild_tblWCMSUserTemplates_UserTemplateId",
                        column: x => x.UserTemplateId,
                        principalTable: "tblWCMSUserTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBotMessage = table.Column<bool>(type: "bit", nullable: false),
                    VisitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTHistory_tblBOTChatBot_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "tblBOTChatBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBOTHistory_tblBOTQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tblBOTQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblBOTHistory_tblBOTVisitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "tblBOTVisitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTImageOrFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontendId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageOrFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BotQuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTImageOrFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTImageOrFile_tblBOTQuestion_BotQuestionId",
                        column: x => x.BotQuestionId,
                        principalTable: "tblBOTQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTOption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTOption_tblBOTQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tblBOTQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblBOTWhatsAppTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhatsAppTemplateId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsAppTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTWhatsAppTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTWhatsAppTemplates_tblBOTQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tblBOTQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSUserProductFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ProductFieldsId = table.Column<int>(type: "int", nullable: false),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBannerField = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSUserProductFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserProductFields_tblWCMSCategoryWiseProducts_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "tblWCMSCategoryWiseProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserProductFields_tblWCMSProductFields_ProductFieldsId",
                        column: x => x.ProductFieldsId,
                        principalTable: "tblWCMSProductFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSocialPlateformWisePosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    Plateformid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSocialPlateformWisePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSocialPlateformWisePosts_tblFacebookPagesTokens_PageId",
                        column: x => x.PageId,
                        principalTable: "tblFacebookPagesTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSocialPlateformWisePosts_tblSocialPlatforms_Plateformid",
                        column: x => x.Plateformid,
                        principalTable: "tblSocialPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblSocialPlateformWisePosts_tblSociaMediaPost_PostId",
                        column: x => x.PostId,
                        principalTable: "tblSociaMediaPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSUserTemplateDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTemplateId = table.Column<int>(type: "int", nullable: false),
                    TemplatePageFieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasChilds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSUserTemplateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplateDetails_tblWCMSTemplatePageFields_TemplatePageFieldId",
                        column: x => x.TemplatePageFieldId,
                        principalTable: "tblWCMSTemplatePageFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplateDetails_tblWCMSUserTemplates_UserTemplateId",
                        column: x => x.UserTemplateId,
                        principalTable: "tblWCMSUserTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblWCMSUserTemplateDetailsChilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTemplateDetailsId = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false),
                    TemplatePageFieldsId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWCMSUserTemplateDetailsChilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplateDetailsChilds_tblWCMSTemplatePageFields_TemplatePageFieldsId",
                        column: x => x.TemplatePageFieldsId,
                        principalTable: "tblWCMSTemplatePageFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWCMSUserTemplateDetailsChilds_tblWCMSUserTemplateDetails_UserTemplateDetailsId",
                        column: x => x.UserTemplateDetailsId,
                        principalTable: "tblWCMSUserTemplateDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRefreshToken_ApplicationUserId",
                table: "AspNetRefreshToken",
                column: "ApplicationUserId");

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
                name: "IX_AspNetUserProfile_AppUserIdId",
                table: "AspNetUserProfile",
                column: "AppUserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

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
                name: "IX_tblBOTChatBot_ApplicationUserId",
                table: "tblBOTChatBot",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTHistory_ChatBotId",
                table: "tblBOTHistory",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTHistory_QuestionId",
                table: "tblBOTHistory",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTHistory_VisitorId",
                table: "tblBOTHistory",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTImageOrFile_BotQuestionId",
                table: "tblBOTImageOrFile",
                column: "BotQuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTOption_QuestionId",
                table: "tblBOTOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTPlatform_ChatBotId",
                table: "tblBOTPlatform",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTQuestion_ChatBotId",
                table: "tblBOTQuestion",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTQuestion_ComponentTypeId",
                table: "tblBOTQuestion",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTVisitor_VisitorUUId",
                table: "tblBOTVisitor",
                column: "VisitorUUId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTWebHookResponse_BOTVisitorsId",
                table: "tblBOTWebHookResponse",
                column: "BOTVisitorsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTWhatsAppBusinessData_ChatBotId",
                table: "tblBOTWhatsAppBusinessData",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTWhatsAppTemplates_QuestionId",
                table: "tblBOTWhatsAppTemplates",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessContactInfo_BusinessInfoId",
                table: "tblBusinessContactInfo",
                column: "BusinessInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessInfoCategorys_BusinessCategoryId",
                table: "tblBusinessInfoCategorys",
                column: "BusinessCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessInfoCategorys_BusinessInfoId",
                table: "tblBusinessInfoCategorys",
                column: "BusinessInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessLocationInfo_BusinessInfoId",
                table: "tblBusinessLocationInfo",
                column: "BusinessInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessPhoneNos_ContactInfoId",
                table: "tblBusinessPhoneNos",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBusinessServiceArea_BusinessInfoId",
                table: "tblBusinessServiceArea",
                column: "BusinessInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCity_CountriesId",
                table: "tblCity",
                column: "CountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCity_StatesId",
                table: "tblCity",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFacebookPagesTokens_SocialPlatformsId",
                table: "tblFacebookPagesTokens",
                column: "SocialPlatformsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMCaptions_SCRMCategoryID",
                table: "tblSCRMCaptions",
                column: "SCRMCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMCaptions_SCRMSubCategoryId",
                table: "tblSCRMCaptions",
                column: "SCRMSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMSubCategory_CategoryId",
                table: "tblSCRMSubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplate_ColorId",
                table: "tblSCRMTemplate",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplate_LanguageId",
                table: "tblSCRMTemplate",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateCategory_CategoryId",
                table: "tblSCRMTemplateCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateCategory_TemplateId",
                table: "tblSCRMTemplateCategory",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateField_TemplateFieldTypeId",
                table: "tblSCRMTemplateField",
                column: "TemplateFieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldImage_TemplateFieldId",
                table: "tblSCRMTemplateFieldImage",
                column: "TemplateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldImage_TemplateId",
                table: "tblSCRMTemplateFieldImage",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldText_AlignId",
                table: "tblSCRMTemplateFieldText",
                column: "AlignId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldText_FontFamilyId",
                table: "tblSCRMTemplateFieldText",
                column: "FontFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldText_TemplateFieldId",
                table: "tblSCRMTemplateFieldText",
                column: "TemplateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateFieldText_TemplateId",
                table: "tblSCRMTemplateFieldText",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateSubCategory_SubCategoryId",
                table: "tblSCRMTemplateSubCategory",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateSubCategory_TemplateId",
                table: "tblSCRMTemplateSubCategory",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateTag_TagId",
                table: "tblSCRMTemplateTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMTemplateTag_TemplateId",
                table: "tblSCRMTemplateTag",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMUserCategoryList_FBTokenId",
                table: "tblSCRMUserCategoryList",
                column: "FBTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMUserMetaData_ApplicationUserId",
                table: "tblSCRMUserMetaData",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSCRMUserMetaData_TemplateFieldId",
                table: "tblSCRMUserMetaData",
                column: "TemplateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocialPlateformWisePosts_PageId",
                table: "tblSocialPlateformWisePosts",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocialPlateformWisePosts_Plateformid",
                table: "tblSocialPlateformWisePosts",
                column: "Plateformid");

            migrationBuilder.CreateIndex(
                name: "IX_tblSocialPlateformWisePosts_PostId",
                table: "tblSocialPlateformWisePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSociaMediaPost_FacebookPagesTokensId",
                table: "tblSociaMediaPost",
                column: "FacebookPagesTokensId");

            migrationBuilder.CreateIndex(
                name: "IX_tblState_CountryId",
                table: "tblState",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSCategoryWiseProducts_ProductCategoryId",
                table: "tblWCMSCategoryWiseProducts",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSFieldsMasterChild_FieldsMasterId",
                table: "tblWCMSFieldsMasterChild",
                column: "FieldsMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSFieldsMasterChild_MasterTypeId",
                table: "tblWCMSFieldsMasterChild",
                column: "MasterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSFieldsMasterChild_TemplateId",
                table: "tblWCMSFieldsMasterChild",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSFieldsMasters_FieldtypeId",
                table: "tblWCMSFieldsMasters",
                column: "FieldtypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSFieldsMasters_MasterTypeId",
                table: "tblWCMSFieldsMasters",
                column: "MasterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSProductCategories_ApplicationUserId",
                table: "tblWCMSProductCategories",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSProductFields_FieldTypeId",
                table: "tblWCMSProductFields",
                column: "FieldTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSTemplatePageFields_FieldsMasterId",
                table: "tblWCMSTemplatePageFields",
                column: "FieldsMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSTemplatePageFields_TemplatePageId",
                table: "tblWCMSTemplatePageFields",
                column: "TemplatePageId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSTemplatePages_TemplateId",
                table: "tblWCMSTemplatePages",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSTemplatePages_TemplatePageTypeId",
                table: "tblWCMSTemplatePages",
                column: "TemplatePageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserProductFields_ProductFieldsId",
                table: "tblWCMSUserProductFields",
                column: "ProductFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserProductFields_ProductsId",
                table: "tblWCMSUserProductFields",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplateDetails_TemplatePageFieldId",
                table: "tblWCMSUserTemplateDetails",
                column: "TemplatePageFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplateDetails_UserTemplateId",
                table: "tblWCMSUserTemplateDetails",
                column: "UserTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplateDetailsChilds_TemplatePageFieldsId",
                table: "tblWCMSUserTemplateDetailsChilds",
                column: "TemplatePageFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplateDetailsChilds_UserTemplateDetailsId",
                table: "tblWCMSUserTemplateDetailsChilds",
                column: "UserTemplateDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplates_ApplicationUserId",
                table: "tblWCMSUserTemplates",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplates_TemplateId",
                table: "tblWCMSUserTemplates",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplatesChild_PlatformId",
                table: "tblWCMSUserTemplatesChild",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWCMSUserTemplatesChild_UserTemplateId",
                table: "tblWCMSUserTemplatesChild",
                column: "UserTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserProfile");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tblBOTAPILogs");

            migrationBuilder.DropTable(
                name: "tblBOTAvatar");

            migrationBuilder.DropTable(
                name: "tblBOTHistory");

            migrationBuilder.DropTable(
                name: "tblBOTImageOrFile");

            migrationBuilder.DropTable(
                name: "tblBOTOption");

            migrationBuilder.DropTable(
                name: "tblBOTPlatform");

            migrationBuilder.DropTable(
                name: "tblBOTWebHookResponse");

            migrationBuilder.DropTable(
                name: "tblBOTWhatsAppBusinessData");

            migrationBuilder.DropTable(
                name: "tblBOTWhatsApptemplateRegisterIssus");

            migrationBuilder.DropTable(
                name: "tblBOTWhatsAppTemplates");

            migrationBuilder.DropTable(
                name: "tblBOTWhatsAppTemplatesStatus");

            migrationBuilder.DropTable(
                name: "tblBusinessInfoCategorys");

            migrationBuilder.DropTable(
                name: "tblBusinessLocationInfo");

            migrationBuilder.DropTable(
                name: "tblBusinessPhoneNos");

            migrationBuilder.DropTable(
                name: "tblBusinessServiceArea");

            migrationBuilder.DropTable(
                name: "tblCity");

            migrationBuilder.DropTable(
                name: "tblSCRMCaptions");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateCategory");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateFieldImage");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateFieldText");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateSubCategory");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateTag");

            migrationBuilder.DropTable(
                name: "tblSCRMUserCategoryList");

            migrationBuilder.DropTable(
                name: "tblSCRMUserMetaData");

            migrationBuilder.DropTable(
                name: "tblSocialPlateformWisePosts");

            migrationBuilder.DropTable(
                name: "tblWCMSCatalog");

            migrationBuilder.DropTable(
                name: "tblWCMSFieldsMasterChild");

            migrationBuilder.DropTable(
                name: "tblWCMSUserProductFields");

            migrationBuilder.DropTable(
                name: "tblWCMSUserTemplateDetailsChilds");

            migrationBuilder.DropTable(
                name: "tblWCMSUserTemplatesChild");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "tblBOTVisitor");

            migrationBuilder.DropTable(
                name: "tblBOTQuestion");

            migrationBuilder.DropTable(
                name: "tblBusinessCategorys");

            migrationBuilder.DropTable(
                name: "tblBusinessContactInfo");

            migrationBuilder.DropTable(
                name: "tblState");

            migrationBuilder.DropTable(
                name: "tblSCRMAlign");

            migrationBuilder.DropTable(
                name: "tblSCRMFontFamily");

            migrationBuilder.DropTable(
                name: "tblSCRMSubCategory");

            migrationBuilder.DropTable(
                name: "tblSCRMTag");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplate");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateField");

            migrationBuilder.DropTable(
                name: "tblSociaMediaPost");

            migrationBuilder.DropTable(
                name: "tblWCMSCategoryWiseProducts");

            migrationBuilder.DropTable(
                name: "tblWCMSProductFields");

            migrationBuilder.DropTable(
                name: "tblWCMSUserTemplateDetails");

            migrationBuilder.DropTable(
                name: "tblBOTChatBot");

            migrationBuilder.DropTable(
                name: "tblBOTComponent");

            migrationBuilder.DropTable(
                name: "tblBusinessInfo");

            migrationBuilder.DropTable(
                name: "tblCountry");

            migrationBuilder.DropTable(
                name: "tblSCRMCategory");

            migrationBuilder.DropTable(
                name: "tblSCRMColor");

            migrationBuilder.DropTable(
                name: "tblSCRMLanguage");

            migrationBuilder.DropTable(
                name: "tblSCRMTemplateFieldType");

            migrationBuilder.DropTable(
                name: "tblFacebookPagesTokens");

            migrationBuilder.DropTable(
                name: "tblWCMSProductCategories");

            migrationBuilder.DropTable(
                name: "tblWCMSTemplatePageFields");

            migrationBuilder.DropTable(
                name: "tblWCMSUserTemplates");

            migrationBuilder.DropTable(
                name: "tblSocialPlatforms");

            migrationBuilder.DropTable(
                name: "tblWCMSFieldsMasters");

            migrationBuilder.DropTable(
                name: "tblWCMSTemplatePages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblWCMSFieldType");

            migrationBuilder.DropTable(
                name: "tblWCMSMasterType");

            migrationBuilder.DropTable(
                name: "tblWCMSTemplatePageType");

            migrationBuilder.DropTable(
                name: "tblWCMSTemplates");
        }
    }
}
