using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class changeWeightSetting : Migration
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

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "weightSettings",
                newName: "StandaredWeight");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "19/06/2024 02:23:20 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73bb8ded-c118-46bf-86d0-3ceb9c5ae6bf", null, "19/06/2024 02:23:20 م", "الموظفين", "الموظفين" },
                    { "ce4b7061-a5a8-449a-ae42-42adc00fb2ba", null, "19/06/2024 02:23:20 م", "التجار", "التجار" },
                    { "dd044f6c-bc05-415a-bda0-e0800f204ab4", null, "19/06/2024 02:23:20 م", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48d3e007-7aa7-4d92-976e-8132ed2f8c52", "AQAAAAIAAYagAAAAEOwZw/egTZrE7XlmLC+fLwTBuNL9pWtUAzhdhUZLo8LlMpdepXSmjPcek4SgwZ4HoA==", "2b2cca39-7be6-4b51-874f-fabfceab962a" });

            migrationBuilder.UpdateData(
                table: "weightSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Addition_Cost",
                value: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73bb8ded-c118-46bf-86d0-3ceb9c5ae6bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4b7061-a5a8-449a-ae42-42adc00fb2ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd044f6c-bc05-415a-bda0-e0800f204ab4");

            migrationBuilder.RenameColumn(
                name: "StandaredWeight",
                table: "weightSettings",
                newName: "Cost");

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

            migrationBuilder.UpdateData(
                table: "weightSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Addition_Cost",
                value: 100);
        }
    }
}
