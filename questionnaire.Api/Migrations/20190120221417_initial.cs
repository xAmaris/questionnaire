using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace questionnaire.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionPosition = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Select = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionPosition = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Select = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false),
                    SurveyTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTemplates_SurveyTemplates_SurveyTemplateId",
                        column: x => x.SurveyTemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MinValue = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    MinLabel = table.Column<string>(nullable: true),
                    MaxLabel = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldData_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldDataTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MinValue = table.Column<int>(nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    MinLabel = table.Column<string>(nullable: true),
                    MaxLabel = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    QuestionTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDataTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldDataTemplates_QuestionTemplates_QuestionTemplateId",
                        column: x => x.QuestionTemplateId,
                        principalTable: "QuestionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionPosition = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    ViewValue = table.Column<string>(nullable: true),
                    FieldDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceOptions_FieldData_FieldDataId",
                        column: x => x.FieldDataId,
                        principalTable: "FieldData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowPosition = table.Column<int>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    FieldDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rows_FieldData_FieldDataId",
                        column: x => x.FieldDataId,
                        principalTable: "FieldData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceOptionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionPosition = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    ViewValue = table.Column<string>(nullable: true),
                    FieldDataTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceOptionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceOptionTemplates_FieldDataTemplates_FieldDataTemplateId",
                        column: x => x.FieldDataTemplateId,
                        principalTable: "FieldDataTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RowTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowPosition = table.Column<int>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    FieldDataTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowTemplates_FieldDataTemplates_FieldDataTemplateId",
                        column: x => x.FieldDataTemplateId,
                        principalTable: "FieldDataTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOptions_FieldDataId",
                table: "ChoiceOptions",
                column: "FieldDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOptionTemplates_FieldDataTemplateId",
                table: "ChoiceOptionTemplates",
                column: "FieldDataTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldData_QuestionId",
                table: "FieldData",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldDataTemplates_QuestionTemplateId",
                table: "FieldDataTemplates",
                column: "QuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTemplates_SurveyTemplateId",
                table: "QuestionTemplates",
                column: "SurveyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_FieldDataId",
                table: "Rows",
                column: "FieldDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RowTemplates_FieldDataTemplateId",
                table: "RowTemplates",
                column: "FieldDataTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceOptions");

            migrationBuilder.DropTable(
                name: "ChoiceOptionTemplates");

            migrationBuilder.DropTable(
                name: "Rows");

            migrationBuilder.DropTable(
                name: "RowTemplates");

            migrationBuilder.DropTable(
                name: "FieldData");

            migrationBuilder.DropTable(
                name: "FieldDataTemplates");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionTemplates");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyTemplates");
        }
    }
}
