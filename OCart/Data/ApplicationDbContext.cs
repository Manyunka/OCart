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
		public DbSet<Post> Posts { get; set; }
		public DbSet<Commission> Commissions { get; set; }
		public DbSet<Auction> Auctions { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Picture> Pictures { get; set; }

		public DbSet<AuctionOrder> AuctionOrders { get; set; }
		public DbSet<CommissionOrder> CommissionOrders { get; set; }

		public DbSet<OrderMessage> AuctionOrdersMessages { get; set; }
		public DbSet<CommissionOrderMessage> CommissionOrdersMessages { get; set; }

		public DbSet<OrderFile> AuctionOrderFiles { get; set; }
		public DbSet<CommissionOrderFile> CommissionOrderFiles { get; set; }

		public DbSet<Bet> Bets { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Activity>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Comment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<OrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Bet>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrder>().HasOne(x => x.Auction).WithMany().OnDelete(DeleteBehavior.SetNull);
			builder.Entity<Commission>()
				.HasMany(x => x.CommissionOrders)
				.WithOne(x => x.Commission)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
