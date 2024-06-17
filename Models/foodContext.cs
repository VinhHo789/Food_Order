﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Models;

public partial class foodContext : DbContext
{
    public foodContext(DbContextOptions<foodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Cthd> Cthds { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Monan> Monans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__ACCOUNT__B15BE12F1747FB80");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK__ACCOUNT__MAKH__59FA5E80");
        });

        modelBuilder.Entity<Cthd>(entity =>
        {
            entity.HasKey(e => new { e.Mahd, e.Mama }).HasName("PK__CTHD__563CD650638DE297");

            entity.ToTable("CTHD");

            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Mama).HasColumnName("MAMA");
            entity.Property(e => e.Mak)
                .HasMaxLength(255)
                .HasColumnName("MAK");
            entity.Property(e => e.SL).HasColumnName("SL");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.Mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHD__MAHD__5CD6CB2B");

            entity.HasOne(d => d.MamaNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.Mama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHD__MAMA__5DCAEF64");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahd).HasName("PK__HOADON__603F20CED85E6DCB");

            entity.ToTable("HOADON");

            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngayhd).HasColumnName("NGAYHD");
            entity.Property(e => e.Tht)
                .HasColumnType("money")
                .HasColumnName("THT");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK__HOADON__MAKH__534D60F1");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh).HasName("PK__KHACHHAN__603F592C515A9364");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Ho)
                .HasMaxLength(255)
                .HasColumnName("HO");
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("TEN");
        });

        modelBuilder.Entity<Monan>(entity =>
        {
            entity.HasKey(e => e.Mama).HasName("PK__MONAN__603F69E20AC90990");

            entity.ToTable("MONAN");

            entity.Property(e => e.Mama).HasColumnName("MAMA");
            entity.Property(e => e.Donggia)
                .HasColumnType("money")
                .HasColumnName("DONGIA");
            entity.Property(e => e.Loaima)
                .HasMaxLength(50)
                .HasColumnName("LOAIMA");
            entity.Property(e => e.Tenma)
                .HasMaxLength(255)
                .HasColumnName("TENMA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}