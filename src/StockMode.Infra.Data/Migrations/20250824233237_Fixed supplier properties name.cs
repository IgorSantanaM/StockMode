using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMode.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixedsupplierpropertiesname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContatctPerson",
                table: "Suppliers",
                newName: "ContactPerson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactPerson",
                table: "Suppliers",
                newName: "ContatctPerson");
        }
    }
}
