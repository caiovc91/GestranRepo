using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestran.Backend.Domain.Entities;
using Gestran.Backend.Domain.Enums;

namespace Gestran.Backend.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<CheckListCollection> CheckListCollections => Set<CheckListCollection>();
        public DbSet<CheckList> CheckLists => Set<CheckList>();
        public DbSet<CheckListItem> CheckListItems => Set<CheckListItem>();
        public DbSet<CheckListItemType> CheckListItemTypes => Set<CheckListItemType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.AccessHashCode).IsRequired();
                entity.Property(e => e.IsAccessActive).IsRequired();
                entity.Property<UserRole>(e => e.Role)
                      .HasConversion<string>()
                      .IsRequired();

                entity.HasMany(e => e.OwnedCollections)
                      .WithOne(c => c.Owner)
                      .HasForeignKey(c => c.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.ExecutedCheckLists)
                      .WithOne(c => c.ExecutedBy)
                      .HasForeignKey(c => c.ExecutedById)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CheckListCollection>( entity =>
            {
                entity.ToTable("CheckListCollections");
                entity.HasKey(e => e.CollectionId);

                entity.HasMany(e => e.CheckLists)
                      .WithOne(cl => cl.Collection)
                      .HasForeignKey(cl => cl.CollectionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CheckList>(entity =>
            {
                entity.ToTable("CheckLists");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CheckListName).IsRequired();
                entity.Property(e => e.CreationDate).IsRequired();
                entity.Property(e => e.LastUpdateDate).IsRequired(false);
                entity.Property(e => e.ApprovalDate).IsRequired(false);
                entity.Property<CheckListStatus>(e => e.CurrentStatus)
                      .HasConversion<string>()
                      .IsRequired();
                entity.Property(e => e.GeneralComments).IsRequired(false);

                entity.HasMany(e => e.CheckListItems)
                      .WithOne(i => i.CheckList)
                      .HasForeignKey(i => i.CheckListId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CheckListItem>(entity =>
            {
                entity.ToTable("CheckListItems");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ItemTypeId).IsRequired();

                entity.Property(e => e.Comments).IsRequired(false);
                entity.Property<bool?>(e => e.IsChecked).IsRequired(false);

                entity.HasOne(e => e.ItemType)
                      .WithMany(t => t.CheckListItems)
                      .HasForeignKey(e => e.ItemTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CheckListItemType>(entity =>
            {
                entity.ToTable("CheckListItemTypes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TypeName).IsRequired();
            });
        }
    }
}
