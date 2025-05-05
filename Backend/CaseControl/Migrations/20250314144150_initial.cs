using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CaseStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CaseTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EvidenceClassifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceClassifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FaultTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IntervieweeTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervieweeTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LinkTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionMedia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionMedia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VwCostCenters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Center = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Full_Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VwCostCenters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "vwEmployees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre_Completo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_de_Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_de_Empleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gerencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_de_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_de_Ingreso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwEmployees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VwOficinas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oficina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oficina_Parsed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre_Oficina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_Oficina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oficina_Completa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VwOficinas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkingGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Faults_FaultTypes_FaultTypeID",
                        column: x => x.FaultTypeID,
                        principalTable: "FaultTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Access_Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    AccessID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Access_Roles_Accesses_AccessID",
                        column: x => x.AccessID,
                        principalTable: "Accesses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Access_Roles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLevels_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    WorkingGroupID = table.Column<int>(type: "int", nullable: false),
                    UserLevelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserLevels_UserLevelID",
                        column: x => x.UserLevelID,
                        principalTable: "UserLevels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_WorkingGroups_WorkingGroupID",
                        column: x => x.WorkingGroupID,
                        principalTable: "WorkingGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transmitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommunicationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCommunication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommunicationReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountDetected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInvestigated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountRecovered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountLost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AffectedAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserNameRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceptionMediumID = table.Column<int>(type: "int", nullable: false),
                    CaseTypeID = table.Column<int>(type: "int", nullable: false),
                    CaseStatusID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cases_CaseStatuses_CaseStatusID",
                        column: x => x.CaseStatusID,
                        principalTable: "CaseStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_CaseTypes_CaseTypeID",
                        column: x => x.CaseTypeID,
                        principalTable: "CaseTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_ReceptionMedia_ReceptionMediumID",
                        column: x => x.ReceptionMediumID,
                        principalTable: "ReceptionMedia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Binnacles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binnacles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Binnacles_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Binnacles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseAssignments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userNameRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseAssignments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CaseAssignments_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseAssignments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseStatusChanges",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userNameRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    CaseStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatusChanges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CaseStatusChanges_CaseStatuses_CaseStatusID",
                        column: x => x.CaseStatusID,
                        principalTable: "CaseStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseStatusChanges_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evidences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    EvidenceClassificationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evidences_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evidences_EvidenceClassifications_EvidenceClassificationID",
                        column: x => x.EvidenceClassificationID,
                        principalTable: "EvidenceClassifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linkeds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTypeID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linkeds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Linkeds_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Linkeds_LinkTypes_LinkTypeID",
                        column: x => x.LinkTypeID,
                        principalTable: "LinkTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitToWhichItIsAddressed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecommendationStatusID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RecommendationTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recommendations_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_RecommendationStatuses_RecommendationStatusID",
                        column: x => x.RecommendationStatusID,
                        principalTable: "RecommendationStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_RecommendationTypes_RecommendationTypeID",
                        column: x => x.RecommendationTypeID,
                        principalTable: "RecommendationTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecoveryHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountRecovery = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRecovery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecoveryHistories_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecoveryHistories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntervieweeTypeID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LinkedID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Interviews_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interviews_IntervieweeTypes_IntervieweeTypeID",
                        column: x => x.IntervieweeTypeID,
                        principalTable: "IntervieweeTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interviews_Linkeds_LinkedID",
                        column: x => x.LinkedID,
                        principalTable: "Linkeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelLinkedFaults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFault = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNameRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FaultID = table.Column<int>(type: "int", nullable: false),
                    LinkedID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelLinkedFaults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelLinkedFaults_Faults_FaultID",
                        column: x => x.FaultID,
                        principalTable: "Faults",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelLinkedFaults_Linkeds_LinkedID",
                        column: x => x.LinkedID,
                        principalTable: "Linkeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Access_Roles_AccessID",
                table: "Access_Roles",
                column: "AccessID");

            migrationBuilder.CreateIndex(
                name: "IX_Access_Roles_RoleID",
                table: "Access_Roles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_Name",
                table: "Accesses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Binnacles_CaseID",
                table: "Binnacles",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Binnacles_Name",
                table: "Binnacles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Binnacles_UserID",
                table: "Binnacles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAssignments_CaseID",
                table: "CaseAssignments",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAssignments_UserID",
                table: "CaseAssignments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseStatusID",
                table: "Cases",
                column: "CaseStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseTypeID",
                table: "Cases",
                column: "CaseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ReceptionMediumID",
                table: "Cases",
                column: "ReceptionMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_UserID",
                table: "Cases",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStatusChanges_CaseID",
                table: "CaseStatusChanges",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStatusChanges_CaseStatusID",
                table: "CaseStatusChanges",
                column: "CaseStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStatuses_Name",
                table: "CaseStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseTypes_Name",
                table: "CaseTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceClassifications_Name",
                table: "EvidenceClassifications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_CaseID",
                table: "Evidences",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_EvidenceClassificationID",
                table: "Evidences",
                column: "EvidenceClassificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_FaultTypeID",
                table: "Faults",
                column: "FaultTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_CaseID",
                table: "Interviews",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_IntervieweeTypeID",
                table: "Interviews",
                column: "IntervieweeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_LinkedID",
                table: "Interviews",
                column: "LinkedID");

            migrationBuilder.CreateIndex(
                name: "IX_Linkeds_CaseID",
                table: "Linkeds",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Linkeds_LinkTypeID",
                table: "Linkeds",
                column: "LinkTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LinkTypes_Name",
                table: "LinkTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedia_Name",
                table: "ReceptionMedia",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_CaseID",
                table: "Recommendations",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_RecommendationStatusID",
                table: "Recommendations",
                column: "RecommendationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_RecommendationTypeID",
                table: "Recommendations",
                column: "RecommendationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_Title",
                table: "Recommendations",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserID",
                table: "Recommendations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationStatuses_Name",
                table: "RecommendationStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationTypes_Name",
                table: "RecommendationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryHistories_CaseID",
                table: "RecoveryHistories",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryHistories_UserID",
                table: "RecoveryHistories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RelLinkedFaults_FaultID",
                table: "RelLinkedFaults",
                column: "FaultID");

            migrationBuilder.CreateIndex(
                name: "IX_RelLinkedFaults_LinkedID",
                table: "RelLinkedFaults",
                column: "LinkedID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_Name",
                table: "UserLevels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_RoleID",
                table: "UserLevels",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserLevelID",
                table: "Users",
                column: "UserLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkingGroupID",
                table: "Users",
                column: "WorkingGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingGroups_Name",
                table: "WorkingGroups",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Access_Roles");

            migrationBuilder.DropTable(
                name: "Binnacles");

            migrationBuilder.DropTable(
                name: "CaseAssignments");

            migrationBuilder.DropTable(
                name: "CaseStatusChanges");

            migrationBuilder.DropTable(
                name: "Evidences");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "RecoveryHistories");

            migrationBuilder.DropTable(
                name: "RelLinkedFaults");

            migrationBuilder.DropTable(
                name: "VwCostCenters");

            migrationBuilder.DropTable(
                name: "vwEmployees");

            migrationBuilder.DropTable(
                name: "VwOficinas");

            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "EvidenceClassifications");

            migrationBuilder.DropTable(
                name: "IntervieweeTypes");

            migrationBuilder.DropTable(
                name: "RecommendationStatuses");

            migrationBuilder.DropTable(
                name: "RecommendationTypes");

            migrationBuilder.DropTable(
                name: "Faults");

            migrationBuilder.DropTable(
                name: "Linkeds");

            migrationBuilder.DropTable(
                name: "FaultTypes");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "LinkTypes");

            migrationBuilder.DropTable(
                name: "CaseStatuses");

            migrationBuilder.DropTable(
                name: "CaseTypes");

            migrationBuilder.DropTable(
                name: "ReceptionMedia");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.DropTable(
                name: "WorkingGroups");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
