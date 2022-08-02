using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Layer.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidatesAndSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatesAndSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });


            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "ContactNumber", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { new Guid("e9006a52-ed6c-42f2-96c9-b7b61ab37fa6"), "1", new DateTime(2022, 3, 31, 18, 41, 26, 177, DateTimeKind.Utc).AddTicks(6246), "email", "John Johnson" },
                    { new Guid("f2ba6cda-1b3e-44f2-80f8-9862e3dfb199"), "2", new DateTime(2022, 3, 31, 18, 41, 26, 177, DateTimeKind.Utc).AddTicks(7148), "email2", "Jack Jackson" },
                    { new Guid("d0b2fecb-99c0-4d27-84c6-c53a40d08b20"), "3", new DateTime(2022, 3, 31, 18, 41, 26, 177, DateTimeKind.Utc).AddTicks(7169), "email3", "Johnny Joe" }
                });

            migrationBuilder.InsertData(
                table: "CandidatesAndSkills",
                columns: new[] { "Id", "CandidateId", "SkillId" },
                values: new object[] { new Guid("b56b0a9e-062c-4ea5-840b-54cb05a076eb"), new Guid("e9006a52-ed6c-42f2-96c9-b7b61ab37fa6"), new Guid("466b382c-c0f7-4cdc-a4bd-ea911e6afcc4") });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("466b382c-c0f7-4cdc-a4bd-ea911e6afcc4"), "Skill A" },
                    { new Guid("bc28d359-2af6-4b3c-bab2-1bb9fa74cb75"), "Skill B" },
                    { new Guid("1de38ee1-0f6d-4d59-8065-f1cb04161a87"), "Skill C" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatesAndSkills");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
