using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreGymClub.Presentation.Migrations
{
    /// <inheritdoc />
    public partial class editmember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipEnd",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipStart",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipTypeId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members",
                column: "MembershipTypeId",
                principalTable: "MembershipTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipTypes_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MembershipTypeId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipEnd",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipStart",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                table: "Members");
        }
    }
}
