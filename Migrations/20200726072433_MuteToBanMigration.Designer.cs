﻿// <auto-generated />
using System;
using Discord_Bot.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Discord_Bot.Migrations
{
    [DbContext(typeof(SqliteContext))]
    [Migration("20200726072433_MuteToBanMigration")]
    partial class MuteToBanMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.7.20365.15");

            modelBuilder.Entity("Discord_Bot.Models.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("banReason")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("banTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("unbanTime")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("userID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("userName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Bans");
                });
#pragma warning restore 612, 618
        }
    }
}