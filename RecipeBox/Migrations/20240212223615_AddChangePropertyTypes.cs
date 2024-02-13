using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBox.Migrations
{
    public partial class AddChangePropertyTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServingSize",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CookTime",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ServingSize",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "CookTime",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
