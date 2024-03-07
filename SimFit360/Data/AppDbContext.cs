using Microsoft.EntityFrameworkCore;
using SimFit360.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimFit360.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Maschine> Maschines { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<Badge> Badges { get; set; }
		public DbSet<UserBadges> UserBadges { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(
				"server=localhost;" +
				"database=simfit360;" +
				"user=root;" +
				"password=;",
				ServerVersion.Parse("8.0.30")
			);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>().HasData(
				new User 
				{ 
					Id = 1, 
					Name = "Jeroen", 
					Email = "jeroen@gmail.com", 
					Pincode = SecureHasher.Hash("0000"),
					Barcode = 11111111
				},
				new User
				{
					Id = 2, 
					Name = "Boyan", 
					Email = "boyan@gmail.com",
					Pincode = SecureHasher.Hash("1111"),
					Barcode = 22222222
				},
				new User
				{
					Id = 3,
					Name = "Caltan",
					Email = "caltan@gmail.com",
					Pincode = SecureHasher.Hash("2222"),
					Barcode = 33333333
				},
				new User
				{
					Id = 4,
					Name = "Junbo",
					Email = "junbo@gmail.com",
					Pincode = SecureHasher.Hash("3333"),
					Barcode = 44444444
				}
			);

			modelBuilder.Entity<Maschine>().HasData(
				new Maschine 
				{ 
					Id = 1, 
					Name = "Treadmill" 
				},
				new Maschine 
				{ 
					Id = 2, 
					Name = "Bike" 
				},
				new Maschine 
				{ 
					Id = 3, 
					Name = "Rowing machine" 
				},
				new Maschine 
				{ 
					Id = 4, 
					Name = "Cross trainer" 
				}
			);

			modelBuilder.Entity<Activity>().HasData(
				new Activity
				{
					Id = 1, 
					Time = new TimeSpan(0, 35, 0), 
					Date = new DateTime(2024, 2, 22), 
					Kcal = 200, 
					UserId = 1, 
					MaschineId = 1 
				},
				new Activity
				{
					Id = 2, 
					Time = new TimeSpan(0, 40, 0),
					Date = new DateTime(2024, 2, 22),
					Kcal = 300, 
					UserId = 2, 
					MaschineId = 2 
				},
				new Activity
				{
					Id = 3, 
					Time = new TimeSpan(0, 50, 0),
					Date = new DateTime(2024, 2, 22),
					Kcal = 400, 
					UserId = 3, 
					MaschineId = 3 
				},
				new Activity
				{
					Id = 4, 
					Time = new TimeSpan(1, 05, 0),
					Date = new DateTime(2024, 2, 22),
					Kcal = 500, 
					UserId = 4, 
					MaschineId = 4 
				}
			);

			modelBuilder.Entity<Badge>().HasData(
				new Badge
				{
					Id = 1, 
					Name = "Beginner" 
				},
				new Badge
				{
					Id = 2, 
					Name = "Intermediate" 
				},
				new Badge
				{
					Id = 3, 
					Name = "Advanced" 
				},
				new Badge
				{
					Id = 4, 
					Name = "Expert" 
				}
			);

			modelBuilder.Entity<UserBadges>().HasData(
				new UserBadges
				{
					Id = 1, 
					UserId = 1, 
					BadgeId = 1 
				},
				new UserBadges
				{
					Id = 2, 
					UserId = 2, 
					BadgeId = 2 
				},
				new UserBadges
				{
					Id = 3, 
					UserId = 3, 
					BadgeId = 3 
				},
				new UserBadges
				{
					Id = 4, 
					UserId = 4, 
					BadgeId = 4 
				}
			);
		}
	}
}
