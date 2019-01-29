using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace questionnaire.Api.Migrations
{
    public partial class pokfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Activated = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IndexNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SurveyTitle = table.Column<string>(nullable: true),
                    SurveyId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SurveyTitle = table.Column<string>(nullable: true),
                    AnswersNumber = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyReports", x => x.Id);
                });

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
                name: "SurveyUserIdentifiers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserEmailHash = table.Column<string>(nullable: true),
                    SurveyId = table.Column<int>(nullable: false),
                    Answered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyUserIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnregisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Course = table.Column<string>(nullable: true),
                    DateOfCompletion = table.Column<DateTime>(nullable: false),
                    TypeOfStudy = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnregisteredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountActivation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivationKey = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ActivatedAt = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountActivation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountActivation_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRestoringPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RestoredAt = table.Column<DateTime>(nullable: false),
                    Token = table.Column<Guid>(nullable: false),
                    Restored = table.Column<bool>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRestoringPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRestoringPasswords_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionPosition = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Select = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false),
                    SurveyAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsAnswers_SurveyAnswers_SurveyAnswerId",
                        column: x => x.SurveyAnswerId,
                        principalTable: "SurveyAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Select = table.Column<string>(nullable: true),
                    AnswersNumber = table.Column<int>(nullable: false),
                    QuestionPosition = table.Column<int>(nullable: false),
                    SurveyReportId = table.Column<int>(nullable: false),
                    LabelsList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionReports_SurveyReports_SurveyReportId",
                        column: x => x.SurveyReportId,
                        principalTable: "SurveyReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "FieldDataAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MinLabel = table.Column<string>(nullable: true),
                    MaxLabel = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    QuestionAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDataAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldDataAnswers_QuestionsAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionsAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    QuestionReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSets_QuestionReports_QuestionReportId",
                        column: x => x.QuestionReportId,
                        principalTable: "QuestionReports",
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
                name: "ChoiceOptionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionPosition = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    ViewValue = table.Column<string>(nullable: true),
                    FieldDataAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceOptionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceOptionsAnswers_FieldDataAnswers_FieldDataAnswerId",
                        column: x => x.FieldDataAnswerId,
                        principalTable: "FieldDataAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RowAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowPosition = table.Column<int>(nullable: false),
                    Input = table.Column<string>(nullable: true),
                    FieldDataAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowAnswers_FieldDataAnswers_FieldDataAnswerId",
                        column: x => x.FieldDataAnswerId,
                        principalTable: "FieldDataAnswers",
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

            migrationBuilder.CreateTable(
                name: "RowChoiceOptionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionPosition = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    ViewValue = table.Column<string>(nullable: true),
                    RowAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowChoiceOptionsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowChoiceOptionsAnswers_RowAnswers_RowAnswerId",
                        column: x => x.RowAnswerId,
                        principalTable: "RowAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountActivation_AccountId",
                table: "AccountActivation",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRestoringPasswords_AccountId",
                table: "AccountRestoringPasswords",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOptions_FieldDataId",
                table: "ChoiceOptions",
                column: "FieldDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOptionsAnswers_FieldDataAnswerId",
                table: "ChoiceOptionsAnswers",
                column: "FieldDataAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOptionTemplates_FieldDataTemplateId",
                table: "ChoiceOptionTemplates",
                column: "FieldDataTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSets_QuestionReportId",
                table: "DataSets",
                column: "QuestionReportId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldData_QuestionId",
                table: "FieldData",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldDataAnswers_QuestionAnswerId",
                table: "FieldDataAnswers",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldDataTemplates_QuestionTemplateId",
                table: "FieldDataTemplates",
                column: "QuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReports_SurveyReportId",
                table: "QuestionReports",
                column: "SurveyReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsAnswers_SurveyAnswerId",
                table: "QuestionsAnswers",
                column: "SurveyAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTemplates_SurveyTemplateId",
                table: "QuestionTemplates",
                column: "SurveyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RowAnswers_FieldDataAnswerId",
                table: "RowAnswers",
                column: "FieldDataAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_RowChoiceOptionsAnswers_RowAnswerId",
                table: "RowChoiceOptionsAnswers",
                column: "RowAnswerId");

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
                name: "AccountActivation");

            migrationBuilder.DropTable(
                name: "AccountRestoringPasswords");

            migrationBuilder.DropTable(
                name: "ChoiceOptions");

            migrationBuilder.DropTable(
                name: "ChoiceOptionsAnswers");

            migrationBuilder.DropTable(
                name: "ChoiceOptionTemplates");

            migrationBuilder.DropTable(
                name: "DataSets");

            migrationBuilder.DropTable(
                name: "RowChoiceOptionsAnswers");

            migrationBuilder.DropTable(
                name: "Rows");

            migrationBuilder.DropTable(
                name: "RowTemplates");

            migrationBuilder.DropTable(
                name: "SurveyUserIdentifiers");

            migrationBuilder.DropTable(
                name: "UnregisteredUsers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "QuestionReports");

            migrationBuilder.DropTable(
                name: "RowAnswers");

            migrationBuilder.DropTable(
                name: "FieldData");

            migrationBuilder.DropTable(
                name: "FieldDataTemplates");

            migrationBuilder.DropTable(
                name: "SurveyReports");

            migrationBuilder.DropTable(
                name: "FieldDataAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionTemplates");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyTemplates");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");
        }
    }
}
