﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhatsAppAPI.Data;

#nullable disable

namespace WhatsAppAPI.Migrations
{
    [DbContext(typeof(RegistrationDbContext))]
    [Migration("20230813141719_removedListButtonText")]
    partial class removedListButtonText
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Communication", b =>
                {
                    b.Property<int>("CommunicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunicationId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DeliveredDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsRePrompt")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReadDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserPromptsId")
                        .HasColumnType("int");

                    b.Property<string>("WhatsAppId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommunicationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserPromptsId");

                    b.ToTable("Communication");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Customer", b =>
                {
                    b.Property<int?>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("CustomerId"));

                    b.Property<string>("AadharNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FlowId")
                        .HasColumnType("int");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PANNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("FlowId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerBank", b =>
                {
                    b.Property<int>("CustomerBankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerBankId"));

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Branch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("IFSC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerBankId");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.ToTable("CustomerBank");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerContact", b =>
                {
                    b.Property<int>("CustomerContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerContactId"));

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pincode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerContactId");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.ToTable("CustomerContact");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerDocuments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AadharDocumentPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("PanDocumentPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.ToTable("CustomerDocuments");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerStatus", b =>
                {
                    b.Property<int>("CustomerStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerStatusId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("CustomerStatusId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerStatus");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Flow", b =>
                {
                    b.Property<int>("FlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlowId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Probability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FlowId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.FlowDetails", b =>
                {
                    b.Property<int>("FlowDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlowDetailsId"));

                    b.Property<int?>("FlowId")
                        .HasColumnType("int");

                    b.Property<int?>("UserPromptsId")
                        .HasColumnType("int");

                    b.HasKey("FlowDetailsId");

                    b.HasIndex("FlowId");

                    b.HasIndex("UserPromptsId");

                    b.ToTable("FlowDetails");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.PromptText", b =>
                {
                    b.Property<int>("PromptTextId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromptTextId"));

                    b.Property<string>("BodyText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Button1Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Button2Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Button3Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FooterText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserPromptsId")
                        .HasColumnType("int");

                    b.HasKey("PromptTextId");

                    b.HasIndex("UserPromptsId");

                    b.ToTable("PromptText");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.UserAnswers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CommunicationId")
                        .HasColumnType("int");

                    b.Property<string>("UserAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsAppId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommunicationId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.UserPrompts", b =>
                {
                    b.Property<int>("UserPromptsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserPromptsId"));

                    b.Property<bool?>("AnswerShouldBeAttachment")
                        .HasColumnType("bit");

                    b.Property<string>("Button1ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Button1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Button1SkipToPromptId")
                        .HasColumnType("int");

                    b.Property<string>("Button2ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Button2Id")
                        .HasColumnType("int");

                    b.Property<int?>("Button2SkipToPromptId")
                        .HasColumnType("int");

                    b.Property<string>("Button3ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Button3Id")
                        .HasColumnType("int");

                    b.Property<int?>("Button3SkipToPromptId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsEndOFFlow")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsModifyable")
                        .HasColumnType("bit");

                    b.Property<string>("ListIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("PromptNeedsReformatting")
                        .HasColumnType("bit");

                    b.Property<string>("PromptType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReConfirmationPromptId")
                        .HasColumnType("int");

                    b.HasKey("UserPromptsId");

                    b.ToTable("UserPrompts");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Communication", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("WhatsAppAPI.Models.Registration.UserPrompts", "UserPrompts")
                        .WithMany()
                        .HasForeignKey("UserPromptsId");

                    b.Navigation("Customer");

                    b.Navigation("UserPrompts");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Customer", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Flow", "Flow")
                        .WithMany()
                        .HasForeignKey("FlowId");

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerBank", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Customer", "Customer")
                        .WithOne("CustomerBank")
                        .HasForeignKey("WhatsAppAPI.Models.Registration.CustomerBank", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerContact", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Customer", "Customer")
                        .WithOne("CustomerContact")
                        .HasForeignKey("WhatsAppAPI.Models.Registration.CustomerContact", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerDocuments", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Customer", "Customer")
                        .WithOne("CustomerDocuments")
                        .HasForeignKey("WhatsAppAPI.Models.Registration.CustomerDocuments", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.CustomerStatus", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.FlowDetails", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Flow", "Flow")
                        .WithMany()
                        .HasForeignKey("FlowId");

                    b.HasOne("WhatsAppAPI.Models.Registration.UserPrompts", "UserPrompts")
                        .WithMany()
                        .HasForeignKey("UserPromptsId");

                    b.Navigation("Flow");

                    b.Navigation("UserPrompts");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.PromptText", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.UserPrompts", "UserPrompts")
                        .WithMany()
                        .HasForeignKey("UserPromptsId");

                    b.Navigation("UserPrompts");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.UserAnswers", b =>
                {
                    b.HasOne("WhatsAppAPI.Models.Registration.Communication", "Communication")
                        .WithMany()
                        .HasForeignKey("CommunicationId");

                    b.Navigation("Communication");
                });

            modelBuilder.Entity("WhatsAppAPI.Models.Registration.Customer", b =>
                {
                    b.Navigation("CustomerBank")
                        .IsRequired();

                    b.Navigation("CustomerContact")
                        .IsRequired();

                    b.Navigation("CustomerDocuments")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
