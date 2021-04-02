﻿// <auto-generated />
using System;
using Bookish.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Bookish.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210401023212_AddedMessages")]
    partial class AddedMessages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Bookish.Data.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("Commented_At")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("Commented_ById")
                        .HasColumnType("integer");

                    b.Property<int>("Commented_OnId")
                        .HasColumnType("integer");

                    b.Property<int?>("Commented_UnderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Commented_ById");

                    b.HasIndex("Commented_OnId");

                    b.HasIndex("Commented_UnderId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Bookish.Data.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AboutComment_Id")
                        .HasColumnType("integer");

                    b.Property<int>("ForUser_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AboutComment_Id");

                    b.HasIndex("ForUser_Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Bookish.Data.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<string>("BookTitle")
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .HasColumnType("character varying(13)")
                        .HasMaxLength(13);

                    b.Property<DateTime>("Posted_At")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Posted_ById")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("WorksId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Posted_ById");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Bookish.Data.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Comment_Id")
                        .HasColumnType("integer");

                    b.Property<bool>("IsUpvoted")
                        .HasColumnType("boolean");

                    b.Property<int?>("Post_Id")
                        .HasColumnType("integer");

                    b.Property<int>("User_Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Comment_Id");

                    b.HasIndex("Post_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Bookish.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bookish.Data.Comment", b =>
                {
                    b.HasOne("Bookish.Data.User", "Commented_By")
                        .WithMany()
                        .HasForeignKey("Commented_ById");

                    b.HasOne("Bookish.Data.Post", "Commented_On")
                        .WithMany("Comments")
                        .HasForeignKey("Commented_OnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookish.Data.Comment", "Commented_Under")
                        .WithMany()
                        .HasForeignKey("Commented_UnderId");
                });

            modelBuilder.Entity("Bookish.Data.Message", b =>
                {
                    b.HasOne("Bookish.Data.Comment", "AboutComment")
                        .WithMany()
                        .HasForeignKey("AboutComment_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookish.Data.User", "ForUser")
                        .WithMany()
                        .HasForeignKey("ForUser_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bookish.Data.Post", b =>
                {
                    b.HasOne("Bookish.Data.User", "Posted_By")
                        .WithMany()
                        .HasForeignKey("Posted_ById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bookish.Data.Rating", b =>
                {
                    b.HasOne("Bookish.Data.Comment", "Comment")
                        .WithMany("Ratings")
                        .HasForeignKey("Comment_Id");

                    b.HasOne("Bookish.Data.Post", "Post")
                        .WithMany("Ratings")
                        .HasForeignKey("Post_Id");

                    b.HasOne("Bookish.Data.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}