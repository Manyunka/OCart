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

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Post>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Commission>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Auction>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Dialog>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<ArtistComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<PostComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<AuctionComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<CommissionComment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Dialog>().HasOne(x => x.User)
				.WithMany(x => x.Dialogs)
				.HasForeignKey(x => x.UserId)
				.IsRequired();

			builder.Entity<ArtistComment>().HasOne(x => x.Artist)
				.WithMany(x => x.ArtistComments)
				.HasForeignKey(x => x.ArtistId)
				.IsRequired();

			builder.Entity<Commission>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,4)");
			builder.Entity<Auction>()
				.Property(p => p.InitialBet)
				.HasColumnType("decimal(18,4)");
			builder.Entity<Auction>()
				.Property(p => p.FinishedBet)
				.HasColumnType("decimal(18,4)");
		}
	}
}
