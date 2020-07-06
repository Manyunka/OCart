using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCart.Models;

namespace OCart.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Activity> Activities { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Picture> Pictures { get; set; }

		public DbSet<AuctionOrder> AuctionOrders { get; set; }
		public DbSet<CommissionOrder> CommissionOrders { get; set; }

		public DbSet<AuctionOrderMessage> AuctionOrdersMessages { get; set; }
		public DbSet<CommissionOrderMessage> CommissionOrdersMessages { get; set; }

		public DbSet<AuctionOrderFile> AuctionOrderFiles { get; set; }
		public DbSet<CommissionOrderFile> CommissionOrderFiles { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Activity>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Comment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Commission>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,4)");
			builder.Entity<Auction>()
				.Property(p => p.InitialBet)
				.HasColumnType("decimal(18,4)");


			builder.Entity<AuctionOrder>().HasOne(x => x.Auction).WithMany().OnDelete(DeleteBehavior.SetNull);
			builder.Entity<Commission>()
				.HasMany(x => x.CommissionOrders)
				.WithOne(x => x.Commission)
				.OnDelete(DeleteBehavior.SetNull);

		}
	}
}
