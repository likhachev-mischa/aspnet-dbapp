using Microsoft.EntityFrameworkCore;

namespace dbapp.Models
{
	public class TablesDbStorage
	{
		private readonly TablesContext m_context;

		public TablesDbStorage(TablesContext context)
		{
			m_context = context;
		}

		public async Task<List<User>?> GetUsersAsync()
		{
			var res = await m_context.Users.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<User?> GetUserAsync(int id)
		{
			var res = await m_context.Users.FirstOrDefaultAsync(x => x.UserId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddUserAsync(User user)
		{
			await m_context.Users.AddAsync(user);
			await m_context.SaveChangesAsync();
		}

		public async Task DeleteUserAsync(int id)
		{ 
			User? user = await GetUserAsync(id);
			if (user != null)
			{
				m_context.Users.Remove(user);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdateUserAsync(User user)
		{
			m_context.Users.Update(user);
			await m_context.SaveChangesAsync();
		}

		public async Task<List<Role>?> GetRolesAsync()
		{
			var res = await m_context.Roles.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<Role?> GetRoleAsync(int id)
		{
			var res = await m_context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddRoleAsync(Role role)
		{
			await m_context.Roles.AddAsync(role);
			await m_context.SaveChangesAsync();
		}

		public async Task DeleteRoleAsync(int id)
		{
			Role? role = await GetRoleAsync(id);
			if (role != null)
			{
				m_context.Roles.Remove(role);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdateRoleAsync(Role role)
		{
			m_context.Roles.Update(role);
			await m_context.SaveChangesAsync();
		}

		public async Task<List<UserToRole>?> GetUserToRolesAsync()
		{
			var res = await m_context.UserToRoles.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<UserToRole?> GetUserToRoleAsync(int id)
		{
			var res = await m_context.UserToRoles.FirstOrDefaultAsync(x => x.UserToRoleId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddUserToRoleAsync(UserToRole userToRole)
		{
			await m_context.UserToRoles.AddAsync(userToRole);
			await m_context.SaveChangesAsync();
		}

		public async Task DeleteUserToRoleAsync(int id)
		{
			UserToRole? userToRole = await GetUserToRoleAsync(id);
			if (userToRole != null)
			{
				m_context.UserToRoles.Remove(userToRole);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdateUserToRoleAsync(UserToRole userToRole)
		{
			m_context.UserToRoles.Update(userToRole);
			await m_context.SaveChangesAsync();
		}

		public async Task<List<Privilege>?> GetPrivilegesAsync()
		{
			var res = await m_context.Privileges.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<Privilege?> GetPrivilegeAsync(int id)
		{
			var res = await m_context.Privileges.FirstOrDefaultAsync(x => x.PrivilegeId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddPrivilegeAsync(Privilege privilege)
		{
			await m_context.Privileges.AddAsync(privilege);
			await m_context.SaveChangesAsync();
		}

		public async Task DeletePrivilegeAsync(int id)
		{
			Privilege? privilege = await GetPrivilegeAsync(id);
			if (privilege != null)
			{
				m_context.Privileges.Remove(privilege);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdatePrivilegeAsync(Privilege privilege)
		{
			m_context.Privileges.Update(privilege);
			await m_context.SaveChangesAsync();
		}

		public async Task<List<RoleToPrivilege>?> GetRoleToPrivilegesAsync()
		{
			var res = await m_context.RoleToPrivileges.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<RoleToPrivilege?> GetRoleToPrivilegeAsync(int id)
		{
			var res = await m_context.RoleToPrivileges.FirstOrDefaultAsync(x => x.RoleToPrivilegeId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddRoleToPrivilegeAsync(RoleToPrivilege roleToPrivilege)
		{
			await m_context.RoleToPrivileges.AddAsync(roleToPrivilege);
			await m_context.SaveChangesAsync();
		}

		public async Task DeleteRoleToPrivilegeAsync(int id)
		{
			RoleToPrivilege? roleToPrivilege = await GetRoleToPrivilegeAsync(id);
			if (roleToPrivilege != null)
			{
				m_context.RoleToPrivileges.Remove(roleToPrivilege);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdateRoleToPrivilegeAsync(RoleToPrivilege roleToPrivilege)
		{
			m_context.RoleToPrivileges.Update(roleToPrivilege);
			await m_context.SaveChangesAsync();
		}

		public async Task<List<UserToPrivilege>?> GetUserToPrivilegesAsync()
		{
			var res = await m_context.UserToPrivileges.ToListAsync();
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task<UserToPrivilege?> GetUserToPrivilegeAsync(int id)
		{
			var res = await m_context.UserToPrivileges.FirstOrDefaultAsync(x => x.UserToPrivilegeId == id);
			await m_context.SaveChangesAsync();
			return res;
		}

		public async Task AddUserToPrivilegeAsync(UserToPrivilege userToPrivilege)
		{
			await m_context.UserToPrivileges.AddAsync(userToPrivilege);
			await m_context.SaveChangesAsync();
		}

		public async Task DeleteUserToPrivilegeAsync(int id)
		{
			UserToPrivilege? userToPrivilege = await GetUserToPrivilegeAsync(id);
			if (userToPrivilege != null)
			{
				m_context.UserToPrivileges.Remove(userToPrivilege);
			}
			await m_context.SaveChangesAsync();
		}

		public async Task UpdateUserToPrivilegeAsync(UserToPrivilege userToPrivilege)
		{
			m_context.UserToPrivileges.Update(userToPrivilege);
			await m_context.SaveChangesAsync();
		}


	}
}
