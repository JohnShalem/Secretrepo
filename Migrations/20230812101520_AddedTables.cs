using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    FlowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Probability = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.FlowId);
                });

            migrationBuilder.CreateTable(
                name: "UserPrompts",
                columns: table => new
                {
                    UserPromptsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button1Id = table.Column<int>(type: "int", nullable: true),
                    Button1ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button1SkipToPromptId = table.Column<int>(type: "int", nullable: true),
                    Button2Id = table.Column<int>(type: "int", nullable: true),
                    Button2ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button2SkipToPromptId = table.Column<int>(type: "int", nullable: true),
                    Button3Id = table.Column<int>(type: "int", nullable: true),
                    Button3ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button3SkipToPromptId = table.Column<int>(type: "int", nullable: true),
                    ListIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEndOFFlow = table.Column<bool>(type: "bit", nullable: true),
                    AnswerShouldBeAttachment = table.Column<bool>(type: "bit", nullable: true),
                    PromptNeedsReformatting = table.Column<bool>(type: "bit", nullable: true),
                    IsModifyable = table.Column<bool>(type: "bit", nullable: true),
                    ReConfirmationPromptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrompts", x => x.UserPromptsId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadharNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "FlowId");
                });

            migrationBuilder.CreateTable(
                name: "FlowDetails",
                columns: table => new
                {
                    FlowDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: true),
                    UserPromptsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowDetails", x => x.FlowDetailsId);
                    table.ForeignKey(
                        name: "FK_FlowDetails_Flows_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "FlowId");
                    table.ForeignKey(
                        name: "FK_FlowDetails_UserPrompts_UserPromptsId",
                        column: x => x.UserPromptsId,
                        principalTable: "UserPrompts",
                        principalColumn: "UserPromptsId");
                });

            migrationBuilder.CreateTable(
                name: "PromptText",
                columns: table => new
                {
                    PromptTextId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button1Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button2Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Button3Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListButtonText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPromptsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptText", x => x.PromptTextId);
                    table.ForeignKey(
                        name: "FK_PromptText_UserPrompts_UserPromptsId",
                        column: x => x.UserPromptsId,
                        principalTable: "UserPrompts",
                        principalColumn: "UserPromptsId");
                });

            migrationBuilder.CreateTable(
                name: "Communication",
                columns: table => new
                {
                    CommunicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveredDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRePrompt = table.Column<bool>(type: "bit", nullable: true),
                    UserPromptsId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communication", x => x.CommunicationId);
                    table.ForeignKey(
                        name: "FK_Communication_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Communication_UserPrompts_UserPromptsId",
                        column: x => x.UserPromptsId,
                        principalTable: "UserPrompts",
                        principalColumn: "UserPromptsId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerBank",
                columns: table => new
                {
                    CustomerBankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBank", x => x.CustomerBankId);
                    table.ForeignKey(
                        name: "FK_CustomerBank_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    CustomerContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContact", x => x.CustomerContactId);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AadharDocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanDocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerStatus",
                columns: table => new
                {
                    CustomerStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStatus", x => x.CustomerStatusId);
                    table.ForeignKey(
                        name: "FK_CustomerStatus_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommunicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Communication_CommunicationId",
                        column: x => x.CommunicationId,
                        principalTable: "Communication",
                        principalColumn: "CommunicationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Communication_CustomerId",
                table: "Communication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Communication_UserPromptsId",
                table: "Communication",
                column: "UserPromptsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_FlowId",
                table: "Customer",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBank_CustomerId",
                table: "CustomerBank",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                table: "CustomerContact",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CustomerId",
                table: "CustomerDocuments",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStatus_CustomerId",
                table: "CustomerStatus",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDetails_FlowId",
                table: "FlowDetails",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowDetails_UserPromptsId",
                table: "FlowDetails",
                column: "UserPromptsId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptText_UserPromptsId",
                table: "PromptText",
                column: "UserPromptsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_CommunicationId",
                table: "UserAnswers",
                column: "CommunicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBank");

            migrationBuilder.DropTable(
                name: "CustomerContact");

            migrationBuilder.DropTable(
                name: "CustomerDocuments");

            migrationBuilder.DropTable(
                name: "CustomerStatus");

            migrationBuilder.DropTable(
                name: "FlowDetails");

            migrationBuilder.DropTable(
                name: "PromptText");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "Communication");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "UserPrompts");

            migrationBuilder.DropTable(
                name: "Flows");
        }
    }
}
