﻿using System;
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

		public DbSet<ArtistСomment> ArtistСomments { get; set; }
		public DbSet<PostСomment> PostСomments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Post>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Commission>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Auction>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<Dialog>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<ArtistСomment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);
			builder.Entity<PostСomment>().HasOne(x => x.Creator).WithMany().OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Dialog>().HasOne(x => x.User)
				.WithMany()
				.HasForeignKey(x => x.UserId)
				.IsRequired();

			builder.Entity<ArtistСomment>().HasOne(x => x.Artist)
				.WithMany()
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
