﻿// <auto-generated />
using DiscordBackup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiscordBackup.Migrations
{
    [DbContext(typeof(MessageContext))]
    [Migration("20181029191142_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("DiscordBackup.Attachment", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("ChannelId");

                    b.Property<string>("FileName");

                    b.Property<uint>("Height");

                    b.Property<string>("ProxyUrl");

                    b.Property<string>("Url");

                    b.Property<uint>("Width");

                    b.HasKey("Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("DiscordBackup.Message", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("ChannelId");

                    b.Property<string>("ChannelName");

                    b.Property<string>("Content");

                    b.Property<bool>("IsPinned");

                    b.Property<bool>("IsTTS");

                    b.Property<ulong>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
