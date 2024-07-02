using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class chant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32afe8c6-80e5-4bf2-8e2a-319c2858e7d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3773b28b-dc4e-4083-99a3-8566e1c47110");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "817e5a97-57f2-4a61-a3ff-51d2769a7557");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "Government",
                table: "Merchants");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Merchants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GovernmentId",
                table: "Merchants",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "7/2/2024 8:24:42 AM");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14bd1c1b-67a3-4232-82b0-1f0c8e8bb5ca", null, "7/2/2024 8:24:42 AM", "التجار", "التجار" },
                    { "597b180b-bb25-4b4d-a962-5cf49aaa38fc", null, "7/2/2024 8:24:42 AM", "المناديب", "المناديب" },
                    { "f6f25a99-f05f-4b50-b63d-51adcc76b8f3", null, "7/2/2024 8:24:42 AM", "الموظفين", "الموظفين" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffea3d33-23c5-4718-bad8-d3f8e7e87a60", "AQAAAAIAAYagAAAAEKz+cNKevTybCiPQgFKUC2yhSDuo+EMePiF+djJoi3pnsXDzeO9khSTVlELf33bV+Q==", "6321c4c9-88cb-4053-9739-dcbd300b8682" });

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_CityId",
                table: "Merchants",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_GovernmentId",
                table: "Merchants",
                column: "GovernmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Cities_CityId",
                table: "Merchants",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Governments_GovernmentId",
                table: "Merchants",
                column: "GovernmentId",
                principalTable: "Governments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Cities_CityId",
                table: "Merchants");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Governments_GovernmentId",
                table: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_CityId",
                table: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_GovernmentId",
                table: "Merchants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14bd1c1b-67a3-4232-82b0-1f0c8e8bb5ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "597b180b-bb25-4b4d-a962-5cf49aaa38fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6f25a99-f05f-4b50-b63d-51adcc76b8f3");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "GovernmentId",
                table: "Merchants");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Government",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "18/06/2024 01:21:37 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32afe8c6-80e5-4bf2-8e2a-319c2858e7d1", null, "6/19/2024 8:24:57 PM", "التجار", "التجار" },
                    { "3773b28b-dc4e-4083-99a3-8566e1c47110", null, "18/06/2024 01:21:37 م", "المناديب", "المناديب" },
                    { "817e5a97-57f2-4a61-a3ff-51d2769a7557", null, "6/19/2024 8:24:57 PM", "الموظفين", "الموظفين" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26ec50df-a61e-466e-a27f-8c6cca53868a", "AQAAAAIAAYagAAAAECZCEoVe62rEO4NE15dYYEMHvr8oEaNXj5gAh+gH1Gj33tBZxRpPRNKZQNwbLIavNg==", "53049d19-5fae-4e5d-b4c9-057b974571fd" });
        }
    }
}
