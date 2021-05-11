﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Migrations.Migrations
{
    [DbContext(typeof(TaskMigrationContext))]
    partial class TaskMigrationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DatabaseEntity.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastSavedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastSavedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("task");
                });

            modelBuilder.Entity("DatabaseEntity.TaskText", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastSavedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastSavedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TextId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WordId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.HasIndex("TaskId", "TextId", "WordId")
                        .IsUnique();

                    b.ToTable("tasks_texts");
                });

            modelBuilder.Entity("DatabaseEntity.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LastSavedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastSavedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("TaskId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TaskId1");

                    b.ToTable("word");
                });

            modelBuilder.Entity("DatabaseEntity.TaskText", b =>
                {
                    b.HasOne("DatabaseEntity.Task", "Task")
                        .WithMany("TasksTexts")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseEntity.Word", "Word")
                        .WithMany("TasksTexts")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("DatabaseEntity.Word", b =>
                {
                    b.HasOne("DatabaseEntity.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId1");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DatabaseEntity.Task", b =>
                {
                    b.Navigation("TasksTexts");
                });

            modelBuilder.Entity("DatabaseEntity.Word", b =>
                {
                    b.Navigation("TasksTexts");
                });
#pragma warning restore 612, 618
        }
    }
}
