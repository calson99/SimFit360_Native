using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimFit360.Migrations
{
    /// <inheritdoc />
    public partial class updatePincode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pincode",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pincode",
                value: "C1ejnY0OM9pyOZ2QDLK7CQy9qLEuhbFLX4tAd2AQZ9M=:FhZn/dOHXTiSKBq1V50b9Q==:10000:SHA512");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pincode",
                value: "K4mhK658t9oMBYk4IsxprXmUyMyXc+Ef8LmTr6dqG7o=:OBIXhAoVYRLMdaho7itZwQ==:10000:SHA512");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Pincode",
                value: "phaxTj+F6KxgM24DVZ4xKtz0gOPZtn+4OXm5gdhJbEs=:C8wKYsmDLgXWOUnFKTP4/A==:10000:SHA512");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Pincode",
                value: "4RcG6dSKrKuCuWzfTY3SZmSTHjkgckW9+IjQoI2UGzg=:t5INc8G3zBZCCz88kRpkEQ==:10000:SHA512");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Pincode",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pincode",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pincode",
                value: 1111);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Pincode",
                value: 2222);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Pincode",
                value: 3333);
        }
    }
}
