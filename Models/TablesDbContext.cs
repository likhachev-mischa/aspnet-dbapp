using Microsoft.EntityFrameworkCore;

namespace dbapp.Models
{
	public class TablesContext : DbContext
	{
		public TablesContext()
		{
		//	Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<Table> Tables { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Privilege> Privileges { get; set; }
		public DbSet<RoleToPrivilege> RoleToPrivileges { get; set; }
		public DbSet<UserToPrivilege> UserToPrivileges { get; set; }
		public DbSet<UserToRole> UserToRoles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TablesDB;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RoleToPrivilege>().HasOne(rtp => rtp.Role).WithMany().HasForeignKey(rtp => rtp.RoleId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<RoleToPrivilege>().HasOne(rtp => rtp.Privilege).WithMany()
				.HasForeignKey(rtp => rtp.PrivilegeId).OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UserToRole>().HasOne(rtp => rtp.User).WithMany().HasForeignKey(rtp => rtp.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UserToRole>().HasOne(rtp => rtp.Role).WithMany().HasForeignKey(rtp => rtp.RoleId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UserToPrivilege>().HasOne(rtp => rtp.User).WithMany().HasForeignKey(rtp => rtp.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UserToPrivilege>().HasOne(rtp => rtp.Privilege).WithMany()
				.HasForeignKey(rtp => rtp.PrivilegeId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}