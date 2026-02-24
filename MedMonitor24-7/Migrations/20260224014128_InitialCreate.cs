using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMonitor24_7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    CompanionPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaxBeds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitID);
                });

            migrationBuilder.CreateTable(
                name: "VitalSignTypes",
                columns: table => new
                {
                    VitalSignTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalMin = table.Column<float>(type: "real", nullable: false),
                    NormalMax = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSignTypes", x => x.VitalSignTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_SystemUsers_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    BedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    BedCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.BedID);
                    table.ForeignKey(
                        name: "FK_Beds_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserID = table.Column<int>(type: "int", nullable: false),
                    ReceiverUserID = table.Column<int>(type: "int", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFromBot = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_ChatMessages_SystemUsers_ReceiverUserID",
                        column: x => x.ReceiverUserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_SystemUsers_SenderUserID",
                        column: x => x.SenderUserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "bit", nullable: false),
                    HeartRateAlert = table.Column<bool>(type: "bit", nullable: false),
                    OxygenAlert = table.Column<bool>(type: "bit", nullable: false),
                    TaskDelayAlert = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_SystemUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Unit",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    RoleInUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Unit", x => new { x.UserID, x.UnitID });
                    table.ForeignKey(
                        name: "FK_User_Unit_SystemUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Unit_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    AdmissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    NurseID = table.Column<int>(type: "int", nullable: false),
                    AdmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.AdmissionID);
                    table.ForeignKey(
                        name: "FK_Admissions_Beds_BedID",
                        column: x => x.BedID,
                        principalTable: "Beds",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_SystemUsers_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_SystemUsers_NurseID",
                        column: x => x.NurseID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedID = table.Column<int>(type: "int", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Devices_Beds_BedID",
                        column: x => x.BedID,
                        principalTable: "Beds",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionVitalThresholds",
                columns: table => new
                {
                    ThresholdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionID = table.Column<int>(type: "int", nullable: false),
                    VitalSignTypeID = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<float>(type: "real", nullable: true),
                    MaxValue = table.Column<float>(type: "real", nullable: true),
                    Severity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionVitalThresholds", x => x.ThresholdID);
                    table.ForeignKey(
                        name: "FK_AdmissionVitalThresholds_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionVitalThresholds_VitalSignTypes_VitalSignTypeID",
                        column: x => x.VitalSignTypeID,
                        principalTable: "VitalSignTypes",
                        principalColumn: "VitalSignTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionID = table.Column<int>(type: "int", nullable: false),
                    VitalSignTypeID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RaisedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_VitalSignTypes_VitalSignTypeID",
                        column: x => x.VitalSignTypeID,
                        principalTable: "VitalSignTypes",
                        principalColumn: "VitalSignTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionID = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: false),
                    AssignedNurseID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reminder = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RepeatRule = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_ClinicalTasks_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalTasks_SystemUsers_AssignedNurseID",
                        column: x => x.AssignedNurseID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClinicalTasks_SystemUsers_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UploadedByUserID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_PatientReports_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientReports_ReportCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ReportCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientReports_SystemUsers_UploadedByUserID",
                        column: x => x.UploadedByUserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSignReadings",
                columns: table => new
                {
                    ReadingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionID = table.Column<int>(type: "int", nullable: false),
                    VitalSignTypeID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    ReadingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false),
                    IsAbnormal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSignReadings", x => x.ReadingID);
                    table.ForeignKey(
                        name: "FK_VitalSignReadings_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VitalSignReadings_VitalSignTypes_VitalSignTypeID",
                        column: x => x.VitalSignTypeID,
                        principalTable: "VitalSignTypes",
                        principalColumn: "VitalSignTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHeartbeats",
                columns: table => new
                {
                    HeartbeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceID = table.Column<int>(type: "int", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Meta = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHeartbeats", x => x.HeartbeatID);
                    table.ForeignKey(
                        name: "FK_DeviceHeartbeats_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertAcknowledgements",
                columns: table => new
                {
                    AckID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AckAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertAcknowledgements", x => x.AckID);
                    table.ForeignKey(
                        name: "FK_AlertAcknowledgements_Alerts_AlertID",
                        column: x => x.AlertID,
                        principalTable: "Alerts",
                        principalColumn: "AlertID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertAcknowledgements_SystemUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskEvents",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    EventAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEvents", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_TaskEvents_ClinicalTasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "ClinicalTasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEvents_SystemUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportFiles",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFiles", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_ReportFiles_PatientReports_ReportID",
                        column: x => x.ReportID,
                        principalTable: "PatientReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_BedID",
                table: "Admissions",
                column: "BedID",
                unique: true,
                filter: "[DischargeDate] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_DoctorID",
                table: "Admissions",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_NurseID",
                table: "Admissions",
                column: "NurseID");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientID",
                table: "Admissions",
                column: "PatientID",
                unique: true,
                filter: "[DischargeDate] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionVitalThresholds_AdmissionID_VitalSignTypeID",
                table: "AdmissionVitalThresholds",
                columns: new[] { "AdmissionID", "VitalSignTypeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionVitalThresholds_VitalSignTypeID",
                table: "AdmissionVitalThresholds",
                column: "VitalSignTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AlertAcknowledgements_AlertID",
                table: "AlertAcknowledgements",
                column: "AlertID");

            migrationBuilder.CreateIndex(
                name: "IX_AlertAcknowledgements_UserID",
                table: "AlertAcknowledgements",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AdmissionID_Status_RaisedAt",
                table: "Alerts",
                columns: new[] { "AdmissionID", "Status", "RaisedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_VitalSignTypeID",
                table: "Alerts",
                column: "VitalSignTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_UnitID_BedCode",
                table: "Beds",
                columns: new[] { "UnitID", "BedCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverUserID",
                table: "ChatMessages",
                column: "ReceiverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderUserID_SentAt",
                table: "ChatMessages",
                columns: new[] { "SenderUserID", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTasks_AdmissionID",
                table: "ClinicalTasks",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTasks_AssignedNurseID_Status_StartDateTime",
                table: "ClinicalTasks",
                columns: new[] { "AssignedNurseID", "Status", "StartDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTasks_CreatedByUserID",
                table: "ClinicalTasks",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHeartbeats_DeviceID",
                table: "DeviceHeartbeats",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BedID",
                table: "Devices",
                column: "BedID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Identifier",
                table: "Devices",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_AdmissionID",
                table: "PatientReports",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_CategoryID",
                table: "PatientReports",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_UploadedByUserID",
                table: "PatientReports",
                column: "UploadedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name",
                table: "Patients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_Name",
                table: "ReportCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_ReportID",
                table: "ReportFiles",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_Email",
                table: "SystemUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_RoleID",
                table: "SystemUsers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_TaskID",
                table: "TaskEvents",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_UserID",
                table: "TaskEvents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_Unit_UnitID",
                table: "User_Unit",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSignReadings_AdmissionID_VitalSignTypeID_ReadingTime",
                table: "VitalSignReadings",
                columns: new[] { "AdmissionID", "VitalSignTypeID", "ReadingTime" });

            migrationBuilder.CreateIndex(
                name: "IX_VitalSignReadings_VitalSignTypeID",
                table: "VitalSignReadings",
                column: "VitalSignTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSignTypes_Name",
                table: "VitalSignTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionVitalThresholds");

            migrationBuilder.DropTable(
                name: "AlertAcknowledgements");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "DeviceHeartbeats");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "ReportFiles");

            migrationBuilder.DropTable(
                name: "TaskEvents");

            migrationBuilder.DropTable(
                name: "User_Unit");

            migrationBuilder.DropTable(
                name: "VitalSignReadings");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "PatientReports");

            migrationBuilder.DropTable(
                name: "ClinicalTasks");

            migrationBuilder.DropTable(
                name: "VitalSignTypes");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
