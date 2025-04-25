using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Contexts.IdentityMigrations
{
    /// <inheritdoc />
    public partial class ExtendAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1c2b730a-226c-49a4-aa68-ce046cc98ca4", "58520f1c-4acb-4912-adb6-af36a44687c3" });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d73c44b-9a12-4e6c-926e-0db03f4dc908", "58520f1c-4acb-4912-adb6-af36a44687c3" });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "58520f1c-4acb-4912-adb6-af36a44687c3");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2543961c-02d6-420f-915f-30c5f70759c4", 0, "da9aa2f8-56a3-45f0-9ebb-1c03f72b07c0", "admin@local.com", false, "", "", "", false, null, "ADMIN@LOCAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKvt8gICEfvWDa3NmQLS8dOv3Dix0XEGRrzlpMgouXXi4CptFDQQvzJPWUUYhVU4gQ==", null, false, "6bf15551-2a5d-4e5e-84fd-0cfbb2daf4c1", false, "admin" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1c2b730a-226c-49a4-aa68-ce046cc98ca4", "2543961c-02d6-420f-915f-30c5f70759c4" },
                    { "3d73c44b-9a12-4e6c-926e-0db03f4dc908", "2543961c-02d6-420f-915f-30c5f70759c4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1c2b730a-226c-49a4-aa68-ce046cc98ca4", "2543961c-02d6-420f-915f-30c5f70759c4" });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d73c44b-9a12-4e6c-926e-0db03f4dc908", "2543961c-02d6-420f-915f-30c5f70759c4" });

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "2543961c-02d6-420f-915f-30c5f70759c4");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Security",
                table: "Users");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "58520f1c-4acb-4912-adb6-af36a44687c3", 0, "8d111e32-7030-425c-9652-3b2b6d1d1a38", "admin@local.com", false, false, null, "ADMIN@LOCAL.COM", "ADMIN", "AQAAAAIAAYagAAAAECNm4QmK/yzaLtLQaDyH7xbu98IqQV/oNoV8aKZKvmcC/GKJF3c4EDcn2HIV8cx2fQ==", null, false, "85f0ab4e-a5b3-4f1b-8858-2634fb90a59f", false, "admin" });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1c2b730a-226c-49a4-aa68-ce046cc98ca4", "58520f1c-4acb-4912-adb6-af36a44687c3" },
                    { "3d73c44b-9a12-4e6c-926e-0db03f4dc908", "58520f1c-4acb-4912-adb6-af36a44687c3" }
                });
        }
    }
}
