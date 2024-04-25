﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WonderFood.Infra.Sql.Context;

#nullable disable

namespace WonderFood.Infra.Sql.Migrations
{
    [DbContext(typeof(WonderFoodContext))]
    [Migration("20240218002902_MigrationInicialMySql")]
    partial class MigrationInicialMySql
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WonderFood.Domain.Entities.Cliente", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.Pedido", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ClienteId")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime");

                    b.Property<int>("NumeroPedido")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(200)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("NumeroPedido")
                        .IsUnique();

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.Produto", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(36)");

                    b.Property<short>("Categoria")
                        .HasColumnType("smallint");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.ProdutosPedido", b =>
                {
                    b.Property<string>("PedidoId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProdutoId")
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("PedidoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutosPedido", (string)null);
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("WonderFood.Domain.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.ProdutosPedido", b =>
                {
                    b.HasOne("WonderFood.Domain.Entities.Pedido", "Pedido")
                        .WithMany("Produtos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WonderFood.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("WonderFood.Domain.Entities.Pedido", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
