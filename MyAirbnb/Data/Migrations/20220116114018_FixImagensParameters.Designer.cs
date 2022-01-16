﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyAirbnb.Data;

namespace MyAirbnb.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220116114018_FixImagensParameters")]
    partial class FixImagensParameters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyAirbnb.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("CC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Morada")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NIF")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("NumeroTelemovel")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MyAirbnb.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("MyAirbnb.Models.Checklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("MomentoEntrega")
                        .HasColumnType("bit");

                    b.Property<bool>("MomentoPreparacao")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("DonoId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("MyAirbnb.Models.Classificacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estrelas")
                        .HasColumnType("int");

                    b.Property<int?>("ImovelId")
                        .HasColumnType("int");

                    b.Property<string>("UtilizadorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ImovelId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Classificacaos");
                });

            modelBuilder.Entity("MyAirbnb.Models.DoneChecklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChecklistId")
                        .HasColumnType("int");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<string>("ResponsavelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("ReservaId");

                    b.HasIndex("ResponsavelId");

                    b.ToTable("DoneChecklists");
                });

            modelBuilder.Entity("MyAirbnb.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DonoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DonoId")
                        .IsUnique()
                        .HasFilter("[DonoId] IS NOT NULL");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("MyAirbnb.Models.Imagens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DoneChecklistId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImovelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoneChecklistId");

                    b.HasIndex("ImovelId");

                    b.ToTable("Imagens");
                });

            modelBuilder.Entity("MyAirbnb.Models.Imovel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Entregue")
                        .HasColumnType("bit");

                    b.Property<float>("EspacoM2")
                        .HasColumnType("real");

                    b.Property<DateTime>("HoraCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Listado")
                        .HasColumnType("bit");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroCamas")
                        .HasColumnType("int");

                    b.Property<int>("NumeroPessoas")
                        .HasColumnType("int");

                    b.Property<float>("PrecoPorNoite")
                        .HasColumnType("real");

                    b.Property<string>("ResponsavelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TemCozinha")
                        .HasColumnType("bit");

                    b.Property<bool>("TemJacuzzi")
                        .HasColumnType("bit");

                    b.Property<bool>("TemPiscina")
                        .HasColumnType("bit");

                    b.Property<int>("TipoImovelId")
                        .HasColumnType("int");

                    b.Property<int>("numeroWC")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonoId");

                    b.HasIndex("ResponsavelId");

                    b.HasIndex("TipoImovelId");

                    b.ToTable("Imoveis");
                });

            modelBuilder.Entity("MyAirbnb.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCheckin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCheckout")
                        .HasColumnType("datetime2");

                    b.Property<int>("ImovelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ImovelId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyAirbnb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyAirbnb.Models.Checklist", b =>
                {
                    b.HasOne("MyAirbnb.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Dono")
                        .WithMany()
                        .HasForeignKey("DonoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyAirbnb.Models.Classificacao", b =>
                {
                    b.HasOne("MyAirbnb.Models.Imovel", "Imovel")
                        .WithMany("Classificacao")
                        .HasForeignKey("ImovelId");

                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorId");
                });

            modelBuilder.Entity("MyAirbnb.Models.DoneChecklist", b =>
                {
                    b.HasOne("MyAirbnb.Models.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId");

                    b.HasOne("MyAirbnb.Models.Reserva", "Reserva")
                        .WithMany("DoneChecklist")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Responsavel")
                        .WithMany()
                        .HasForeignKey("ResponsavelId");
                });

            modelBuilder.Entity("MyAirbnb.Models.Empresa", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Dono")
                        .WithOne("Empresa")
                        .HasForeignKey("MyAirbnb.Models.Empresa", "DonoId");
                });

            modelBuilder.Entity("MyAirbnb.Models.Imagens", b =>
                {
                    b.HasOne("MyAirbnb.Models.DoneChecklist", "DoneChecklist")
                        .WithMany("Imagens")
                        .HasForeignKey("DoneChecklistId");

                    b.HasOne("MyAirbnb.Models.Imovel", "Imovel")
                        .WithMany("Imagens")
                        .HasForeignKey("ImovelId");
                });

            modelBuilder.Entity("MyAirbnb.Models.Imovel", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Dono")
                        .WithMany()
                        .HasForeignKey("DonoId");

                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Responsavel")
                        .WithMany()
                        .HasForeignKey("ResponsavelId");

                    b.HasOne("MyAirbnb.Models.Categoria", "TipoImovel")
                        .WithMany()
                        .HasForeignKey("TipoImovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyAirbnb.Models.Reserva", b =>
                {
                    b.HasOne("MyAirbnb.Models.ApplicationUser", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("MyAirbnb.Models.Imovel", "Imovel")
                        .WithMany("Reserva")
                        .HasForeignKey("ImovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
