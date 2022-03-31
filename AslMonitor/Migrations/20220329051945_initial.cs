using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AslMonitor.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLogs",
                columns: table => new
                {
                    ClogID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TableName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LogType = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    LogData = table.Column<string>(type: "TEXT", nullable: false),
                    LogTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    Ltude = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLogs", x => x.ClogID);
                });

            migrationBuilder.CreateTable(
                name: "LoginTokens",
                columns: table => new
                {
                    LoginTokenID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginTokens", x => x.LoginTokenID);
                });

            migrationBuilder.CreateTable(
                name: "ScreenShots",
                columns: table => new
                {
                    ScreenShotID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DirPath = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    FileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    InUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    UpUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    InTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UpTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    InLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UpLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    InIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenShots", x => x.ScreenShotID);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserInfoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    MobNo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    EmailID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LoginID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TimeFr = table.Column<TimeSpan>(type: "time", maxLength: 10, nullable: false),
                    TimeTo = table.Column<TimeSpan>(type: "time", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    LoginBy = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    LoginPW = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    InUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    UpUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    InTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UpTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    InLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UpLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    InIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserInfoID);
                });

            migrationBuilder.CreateTable(
                name: "UserStates",
                columns: table => new
                {
                    UserStateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentState = table.Column<string>(type: "varchar(50)", maxLength: 5, nullable: false),
                    TimeFrom = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    TimeTo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    InUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    UpUserID = table.Column<int>(type: "INTEGER", nullable: true),
                    InTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UpTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    InLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UpLtude = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpUserPC = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    InIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    UpIPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStates", x => x.UserStateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_EmailID",
                table: "UserInfos",
                column: "EmailID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_LoginID",
                table: "UserInfos",
                column: "LoginID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_MobNo",
                table: "UserInfos",
                column: "MobNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStates_UserID",
                table: "UserStates",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLogs");

            migrationBuilder.DropTable(
                name: "LoginTokens");

            migrationBuilder.DropTable(
                name: "ScreenShots");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "UserStates");
        }
    }
}
