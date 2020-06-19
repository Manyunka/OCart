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
		public DbSet<Post> Posts { get; set; }
		public DbSet<Commission> Commissions { get; set; }
		public DbSet<Auction> Auctions { get; set; }

		public DbSet<Dialog> Dialogs { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<MessageFile> MessageFiles { get; set; }

		public DbSet<ArtistComment> ArtistComments { get; set; }
		public DbSet<PostComment> PostComments { get; set; }
		public DbSet<AuctionComment> AuctionComments { get; set; }
		public DbSet<CommissionComment> CommissionComments { get; set; }

		public DbSet<PostPicture> PostPictures { get; set; }
		public DbSet<AuctionPicture> AuctionPictures { get; set; }
		public DbSet<CommissionPicture> CommissionPictures { get; set; }

		public DbSet<AuctionOrder> AuctionOrders { get; set; }
		public DbSet<CommissionOrder> CommissionOrders { get; set; }

		public DbSet<AuctionOrderMessage> AuctionOrdersMessages { get; set; }
		public DbSet<CommissionOrderMessage> CommissionOrdersMessages { get; set; }

		public DbSet<AuctionOrderFile> AuctionOrderFiles { get; set; }
		public DbSet<CommissionOrderFile> CommissionOrderFiles { get; set; }

		public DbSet<Bet> Bets { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Post>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Commission>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Auction>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Dialog>().HasOne(x => x.Interlocutor).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Message>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ArtistComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<PostComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<AuctionComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrder>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<AuctionOrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionOrderMessage>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Bet>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Dialog>().HasOne(x => x.User)
				.WithMany(x => x.Dialogs)
				.HasForeignKey(x => x.UserId)
				.IsRequired();

			builder.Entity<ArtistComment>().HasOne(x => x.Artist)
				.WithMany(x => x.ArtistComments)
				.HasForeignKey(x => x.ArtistId)
				.IsRequired();

			builder.Entity<Auction>().HasOne(x => x.WinBet)
				.WithOne(x => x.Auction)
				.HasForeignKey<Auction>(x => x.WinBetId);

			builder.Entity<Commission>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,4)");
			builder.Entity<Auction>()
				.Property(p => p.InitialCostBet)
				.HasColumnType("decimal(18,4)");
			builder.Entity<Bet>()
				.Property(p => p.Cost)
				.HasColumnType("decimal(18,4)");


			builder.Entity<AuctionOrder>().HasOne(x => x.Auction).WithMany().OnDelete(DeleteBehavior.SetNull);
			builder.Entity<Commission>()
				.HasMany(x => x.CommissionOrders)
				.WithOne(x => x.Commission)
				.OnDelete(DeleteBehavior.SetNull);

		}
	}
}
