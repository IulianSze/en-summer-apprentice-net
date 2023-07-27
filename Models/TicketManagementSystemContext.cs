using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Practica_.net.Models;

public partial class TicketManagementSystemContext : DbContext
{
    public TicketManagementSystemContext()
    {
    }

    public TicketManagementSystemContext(DbContextOptions<TicketManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<HibernateSequence> HibernateSequences { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TotalNumberOfTicketsPerCategory> TotalNumberOfTicketsPerCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-O5QV2UPV;Initial Catalog=ticket_management_system;Integrated Security=True;TrustServerCertificate=True;encrypt=false;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B880B4E69C");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8707F68775E");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EndDate).HasPrecision(6);
            entity.Property(e => e.EventDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.StartDate).HasPrecision(6);
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_eventType");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_venue");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__A9216B1F4FA6C223");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HibernateSequence>(entity =>
        {
            entity.HasKey(e => e.SequenceName).HasName("PK__hibernat__666199D528979573");

            entity.ToTable("hibernate_sequences");

            entity.Property(e => e.SequenceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sequence_name");
            entity.Property(e => e.NextVal).HasColumnName("next_val");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFEB37B3B1");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderedAt).HasPrecision(6);
            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_customer");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__TicketCa__C84589C6F26B3A6E");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventId).HasColumnName("EventID");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Ticket_category_event");
        });

        modelBuilder.Entity<TotalNumberOfTicketsPerCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("total_number_of_tickets_per_category");

            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5D24FACD3EB");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId).HasColumnName("VenueID");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
        modelBuilder.HasSequence("Event_SEQ").IncrementsBy(50);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
