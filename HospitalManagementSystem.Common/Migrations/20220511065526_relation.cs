using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManagementSystem.Common.Migrations
{
    public partial class relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "NumberOfBed",
                table: "Bed");

            migrationBuilder.AddColumn<int>(
                name: "BedId",
                table: "Bill",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicinesId",
                table: "Bill",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientRegistrationid",
                table: "Bill",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_BedId",
                table: "Bill",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_MedicinesId",
                table: "Bill",
                column: "MedicinesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_PatientRegistrationid",
                table: "Bill",
                column: "PatientRegistrationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Bed_BedId",
                table: "Bill",
                column: "BedId",
                principalTable: "Bed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Medicines_MedicinesId",
                table: "Bill",
                column: "MedicinesId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_PatientRegistration_PatientRegistrationid",
                table: "Bill",
                column: "PatientRegistrationid",
                principalTable: "PatientRegistration",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Bed_BedId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Medicines_MedicinesId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_PatientRegistration_PatientRegistrationid",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_BedId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_MedicinesId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_PatientRegistrationid",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "BedId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "MedicinesId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "PatientRegistrationid",
                table: "Bill");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Bill",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBed",
                table: "Bed",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
