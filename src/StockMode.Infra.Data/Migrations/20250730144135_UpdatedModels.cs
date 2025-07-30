using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMode.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItem_Sales_SaleId",
                table: "SaleItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItem_Variation_VariationId",
                table: "SaleItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Variation_VariationId",
                table: "StockMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_Variation_Products_ProductId",
                table: "Variation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variation",
                table: "Variation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleItem",
                table: "SaleItem");

            migrationBuilder.RenameTable(
                name: "Variation",
                newName: "Variations");

            migrationBuilder.RenameTable(
                name: "SaleItem",
                newName: "SaleItems");

            migrationBuilder.RenameIndex(
                name: "IX_Variation_ProductId",
                table: "Variations",
                newName: "IX_Variations_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItem_VariationId",
                table: "SaleItems",
                newName: "IX_SaleItems_VariationId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItems",
                newName: "IX_SaleItems_SaleId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "StockMovements",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "StockMovements",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StockMovements",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Sales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "Sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "Variations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Variations",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Variations",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPrice",
                table: "Variations",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceAtSale",
                table: "SaleItems",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variations",
                table: "Variations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Variations_Sku",
                table: "Variations",
                column: "Sku",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Sales_SaleId",
                table: "SaleItems",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Variations_VariationId",
                table: "SaleItems",
                column: "VariationId",
                principalTable: "Variations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Variations_VariationId",
                table: "StockMovements",
                column: "VariationId",
                principalTable: "Variations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variations_Products_ProductId",
                table: "Variations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Sales_SaleId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Variations_VariationId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Variations_VariationId",
                table: "StockMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_Variations_Products_ProductId",
                table: "Variations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variations",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Variations_Sku",
                table: "Variations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleItems",
                table: "SaleItems");

            migrationBuilder.RenameTable(
                name: "Variations",
                newName: "Variation");

            migrationBuilder.RenameTable(
                name: "SaleItems",
                newName: "SaleItem");

            migrationBuilder.RenameIndex(
                name: "IX_Variations_ProductId",
                table: "Variation",
                newName: "IX_Variation_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_VariationId",
                table: "SaleItem",
                newName: "IX_SaleItem_VariationId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItem",
                newName: "IX_SaleItem_SaleId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "StockMovements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "StockMovements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StockMovements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Sales",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Sales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Sales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "Sales",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "Variation",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Variation",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Variation",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPrice",
                table: "Variation",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceAtSale",
                table: "SaleItem",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variation",
                table: "Variation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleItem",
                table: "SaleItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItem_Sales_SaleId",
                table: "SaleItem",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItem_Variation_VariationId",
                table: "SaleItem",
                column: "VariationId",
                principalTable: "Variation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Variation_VariationId",
                table: "StockMovements",
                column: "VariationId",
                principalTable: "Variation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Variation_Products_ProductId",
                table: "Variation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
