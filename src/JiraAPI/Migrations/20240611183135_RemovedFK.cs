﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraAPI.Migrations
{
    public partial class RemovedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_ProjectId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProjectId",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_ProjectId",
                table: "User",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_ProjectId",
                table: "User",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
