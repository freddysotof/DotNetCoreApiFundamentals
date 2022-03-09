using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityFundamentals.Migrations
{
    public partial class AdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8ed172e-f5c8-489d-adc2-8986e6a04921", "9cf2dccc-d646-4587-875a-41390143d13c", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8ed172e-f5c8-489d-adc2-8986e6a04921");
        }
    }
}
