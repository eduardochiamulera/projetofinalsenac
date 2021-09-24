using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evian.Repository.Migrations
{
    public partial class AlterTableContaPagarReceber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContaPagarParcelaPaiId",
                table: "conta_pagar",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContaPagarRepeticaoPaiId",
                table: "conta_pagar",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaPagarParcelaPaiId",
                table: "conta_pagar");

            migrationBuilder.DropColumn(
                name: "ContaPagarRepeticaoPaiId",
                table: "conta_pagar");
        }
    }
}
