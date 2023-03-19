using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScaffoldingDB.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Stake = table.Column<decimal>(type: "decimal(18,17)", nullable: true, defaultValueSql: "((0))"),
                    Weight = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    LocalStake = table.Column<decimal>(type: "decimal(18,17)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Containe__3214EC2708AE6587", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Container__Paren__398D8EEE",
                        column: x => x.ParentID,
                        principalTable: "Container",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Staker",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staker__3214EC278ADB90EC", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContainerMembership",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false),
                    StakerID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    LocalStake = table.Column<decimal>(type: "decimal(18,17)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Containe__5D8C1B7B0678ED0E", x => new { x.ContainerID, x.StakerID });
                    table.ForeignKey(
                        name: "FK__Container__Conta__412EB0B6",
                        column: x => x.ContainerID,
                        principalTable: "Container",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Container__Stake__4222D4EF",
                        column: x => x.StakerID,
                        principalTable: "Staker",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StakerDailyStake",
                columns: table => new
                {
                    StakerID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Stake = table.Column<decimal>(type: "decimal(18,17)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StakerDa__982439D490A019AD", x => new { x.StakerID, x.Date });
                    table.ForeignKey(
                        name: "FK__StakerDai__Stake__47DBAE45",
                        column: x => x.StakerID,
                        principalTable: "Staker",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Container_Name",
                table: "Container",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Container_ParentID",
                table: "Container",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerMembership_StakerID",
                table: "ContainerMembership",
                column: "StakerID");

            migrationBuilder.CreateIndex(
                name: "IX_StakerDailyStake_Date",
                table: "StakerDailyStake",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerMembership");

            migrationBuilder.DropTable(
                name: "StakerDailyStake");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "Staker");
        }
    }
}
