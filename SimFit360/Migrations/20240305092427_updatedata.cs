using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimFit360.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 11111111, "i4If1sCeoKM9XdG+oCdL5IYJQk7tB3Sk6+BIalrQkLw=:E/XIEKrA8AQlcUjWyEsNuQ==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 22222222, "3B/AJPjJnpkZuuXqhfOzgJj8Kr2/h5T2e5mv+y3Czds=:wJ5bW+0ZkrswwzY7KX0oDw==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 33333333, "8tzEeHPv4rUJtR5sx5K00RbHcu8gxE2YZgxFpm6j4Jc=:c932ueec36uEuqoSwZA8iQ==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 44444444, "W7rkeW2t/dH9clWNmUJIzIH/xQz7+YUS6HV3i6PhDwc=:JFFzdjaQSFWW5ZrpIb+oFg==:10000:SHA512" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 1010101, "C1ejnY0OM9pyOZ2QDLK7CQy9qLEuhbFLX4tAd2AQZ9M=:FhZn/dOHXTiSKBq1V50b9Q==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 12121212, "K4mhK658t9oMBYk4IsxprXmUyMyXc+Ef8LmTr6dqG7o=:OBIXhAoVYRLMdaho7itZwQ==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 23232323, "phaxTj+F6KxgM24DVZ4xKtz0gOPZtn+4OXm5gdhJbEs=:C8wKYsmDLgXWOUnFKTP4/A==:10000:SHA512" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Barcode", "Pincode" },
                values: new object[] { 34343434, "4RcG6dSKrKuCuWzfTY3SZmSTHjkgckW9+IjQoI2UGzg=:t5INc8G3zBZCCz88kRpkEQ==:10000:SHA512" });
        }
    }
}
