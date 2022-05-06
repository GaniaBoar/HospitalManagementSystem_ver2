using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace HospitalManagementSystem.Common.Migrations
{
    public partial class PatientRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "patientRegistration",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    firstname = table.Column<string>(nullable: true),
                    middlename = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    dob = table.Column<DateTime>(nullable: false),
                    bloodgroup = table.Column<string>(nullable: true),
                    maritalstatus = table.Column<string>(nullable: true),
                    phoneno = table.Column<int>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    diagnosis = table.Column<string>(nullable: true),
                    complaints = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientRegistration", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patientRegistration");

            
                
        }
    }
}
