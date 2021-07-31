using Microsoft.EntityFrameworkCore.Migrations;

namespace Aluraflix.API.Migrations
{
    public partial class CategoriaID_em_Videos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey( name: "FK_Videos_Categorias_CategoriaId", table: "Videos");
            // o mysql tem um bug que nao aceita o comando acima com o 'drop constraint' padrao do EF Core
            migrationBuilder.Sql("ALTER TABLE Videos DROP FOREIGN KEY FK_Videos_Categorias_CategoriaId;");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categorias_CategoriaId",
                table: "Videos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categorias_CategoriaId",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Videos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categorias_CategoriaId",
                table: "Videos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
