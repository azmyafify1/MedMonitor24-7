using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedMonitor24_7.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Beds_BedID",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_ReportCategories_CategoryID",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_SystemUsers_UploadedByUserID",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsers_Roles_RoleID",
                table: "SystemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VitalSignReadings_VitalSignTypes_VitalSignTypeID",
                table: "VitalSignReadings");

            migrationBuilder.DropTable(
                name: "User_Unit");

            migrationBuilder.DropIndex(
                name: "IX_Units_Name",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Name",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "SystemUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "SystemUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "SystemUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PatientIdentifier",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Admissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NurseID",
                table: "Admissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AdmissionRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedByDoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PatientIdentifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    CompanionPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewedByAdminID = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_AdmissionRequests_SystemUsers_RequestedByDoctorID",
                        column: x => x.RequestedByDoctorID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionRequests_SystemUsers_ReviewedByAdminID",
                        column: x => x.ReviewedByAdminID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserUnits",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    RoleInUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnits", x => new { x.UserID, x.UnitID });
                    table.ForeignKey(
                        name: "FK_UserUnits_SystemUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "SystemUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnits_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionRequests_RequestedByDoctorID",
                table: "AdmissionRequests",
                column: "RequestedByDoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionRequests_ReviewedByAdminID",
                table: "AdmissionRequests",
                column: "ReviewedByAdminID");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnits_UnitID",
                table: "UserUnits",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Beds_BedID",
                table: "Admissions",
                column: "BedID",
                principalTable: "Beds",
                principalColumn: "BedID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_ReportCategories_CategoryID",
                table: "PatientReports",
                column: "CategoryID",
                principalTable: "ReportCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_SystemUsers_UploadedByUserID",
                table: "PatientReports",
                column: "UploadedByUserID",
                principalTable: "SystemUsers",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsers_Roles_RoleID",
                table: "SystemUsers",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VitalSignReadings_VitalSignTypes_VitalSignTypeID",
                table: "VitalSignReadings",
                column: "VitalSignTypeID",
                principalTable: "VitalSignTypes",
                principalColumn: "VitalSignTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Beds_BedID",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_ReportCategories_CategoryID",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_SystemUsers_UploadedByUserID",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsers_Roles_RoleID",
                table: "SystemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VitalSignReadings_VitalSignTypes_VitalSignTypeID",
                table: "VitalSignReadings");

            migrationBuilder.DropTable(
                name: "AdmissionRequests");

            migrationBuilder.DropTable(
                name: "UserUnits");

            migrationBuilder.DropIndex(
                name: "IX_Units_Name",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "PatientIdentifier",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "SystemUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "SystemUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "SystemUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Patients",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Admissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "NurseID",
                table: "Admissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name",
                table: "Patients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_Unit_UnitID",
                table: "User_Unit",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Beds_BedID",
                table: "Admissions",
                column: "BedID",
                principalTable: "Beds",
                principalColumn: "BedID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_ReportCategories_CategoryID",
                table: "PatientReports",
                column: "CategoryID",
                principalTable: "ReportCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_SystemUsers_UploadedByUserID",
                table: "PatientReports",
                column: "UploadedByUserID",
                principalTable: "SystemUsers",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsers_Roles_RoleID",
                table: "SystemUsers",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VitalSignReadings_VitalSignTypes_VitalSignTypeID",
                table: "VitalSignReadings",
                column: "VitalSignTypeID",
                principalTable: "VitalSignTypes",
                principalColumn: "VitalSignTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
