﻿// <auto-generated />
using System;
using FizzBuzzLightYearAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FizzBuzzLightYearAPI.Migrations
{
    [DbContext(typeof(APIDbContext))]
    [Migration("20241018070707_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.GameSession", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Player")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("SessionId");

                    b.HasIndex("GameId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerAnswer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("TEXT");

                    b.HasKey("QuestionId");

                    b.HasIndex("SessionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Rule", b =>
                {
                    b.Property<Guid>("RuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DivisibleBy")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReplaceWith")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RuleId");

                    b.HasIndex("GameId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.GameSession", b =>
                {
                    b.HasOne("FizzBuzzLightYearAPI.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Question", b =>
                {
                    b.HasOne("FizzBuzzLightYearAPI.Models.GameSession", "GameSession")
                        .WithMany("Questions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Rule", b =>
                {
                    b.HasOne("FizzBuzzLightYearAPI.Models.Game", "Game")
                        .WithMany("Rules")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.Game", b =>
                {
                    b.Navigation("Rules");
                });

            modelBuilder.Entity("FizzBuzzLightYearAPI.Models.GameSession", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}