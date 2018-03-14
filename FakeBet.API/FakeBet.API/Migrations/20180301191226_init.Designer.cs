﻿// <auto-generated />
using FakeBet.API.Models;
using FakeBet.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FakeBet.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180301191226_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("FakeBet.API.Models.Match", b =>
                {
                    b.Property<string>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<DateTime>("MatchTime");

                    b.Property<float>("PointsRatio");

                    b.Property<int?>("Status");

                    b.Property<string>("TeamAName");

                    b.Property<int>("TeamAPoints");

                    b.Property<string>("TeamBName");

                    b.Property<int>("TeamBPoints");

                    b.HasKey("MatchId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FakeBet.API.Models.User", b =>
                {
                    b.Property<string>("NickName")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("Points");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Status");

                    b.HasKey("NickName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FakeBet.API.Models.Bets", b =>
                {
                    b.Property<Guid>("BetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MatchId");

                    b.Property<string>("UserId");

                    b.Property<int>("UserPick");

                    b.Property<int>("BetOnTeamA");

                    b.HasKey("BetId");

                    b.HasIndex("MatchId");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("FakeBet.API.Models.Bets", b =>
                {
                    b.HasOne("FakeBet.API.Models.Match", "Match")
                        .WithMany("Bets")
                        .HasForeignKey("MatchId");

                    b.HasOne("FakeBet.API.Models.User", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
