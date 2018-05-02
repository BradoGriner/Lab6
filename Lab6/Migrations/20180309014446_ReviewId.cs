using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lab6.Migrations
{
    public partial class ReviewId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MReviewID",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MReviewID",
                table: "Reviews",
                column: "MReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieID",
                table: "Reviews",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reviews_MReviewID",
                table: "Reviews",
                column: "MReviewID",
                principalTable: "Reviews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movie_MovieID",
                table: "Reviews",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reviews_MReviewID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movie_MovieID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MReviewID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MovieID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MReviewID",
                table: "Reviews");
        }
    }
}
