using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3773b28b-dc4e-4083-99a3-8566e1c47110");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94c1b123-deec-4cc4-868a-d9be7a448b52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2ac875-4170-4b7a-bf96-9acc831f30bb");

            migrationBuilder.AddColumn<int>(
                name: "GovernmentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "6/19/2024 8:24:57 PM");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32afe8c6-80e5-4bf2-8e2a-319c2858e7d1", null, "6/19/2024 8:24:57 PM", "التجار", "التجار" },
                    { "817e5a97-57f2-4a61-a3ff-51d2769a7557", null, "6/19/2024 8:24:57 PM", "الموظفين", "الموظفين" },
                    { "d8d70001-b16f-4c42-8f4f-ecb094ba7b7e", null, "6/19/2024 8:24:57 PM", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26ec50df-a61e-466e-a27f-8c6cca53868a", "AQAAAAIAAYagAAAAECZCEoVe62rEO4NE15dYYEMHvr8oEaNXj5gAh+gH1Gj33tBZxRpPRNKZQNwbLIavNg==", "53049d19-5fae-4e5d-b4c9-057b974571fd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32afe8c6-80e5-4bf2-8e2a-319c2858e7d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "817e5a97-57f2-4a61-a3ff-51d2769a7557");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8d70001-b16f-4c42-8f4f-ecb094ba7b7e");

            migrationBuilder.DropColumn(
                name: "GovernmentId",
                table: "Orders");

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
                    { "3773b28b-dc4e-4083-99a3-8566e1c47110", null, "18/06/2024 01:21:37 م", "المناديب", "المناديب" },
                    { "94c1b123-deec-4cc4-868a-d9be7a448b52", null, "18/06/2024 01:21:37 م", "التجار", "التجار" },
                    { "aa2ac875-4170-4b7a-bf96-9acc831f30bb", null, "18/06/2024 01:21:37 م", "الموظفين", "الموظفين" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1224400-4280-419a-bf0d-66b02447bbd0", "AQAAAAIAAYagAAAAEDlQ3h2vMd8KovLNy13Sm55tQ8CH3G09nfvznFZV1KCGrHJRtBl7MK+8WljlRUNwvg==", "df4a6d9f-d6fd-4476-af5b-4d805ff1213d" });
        }
    }
}
