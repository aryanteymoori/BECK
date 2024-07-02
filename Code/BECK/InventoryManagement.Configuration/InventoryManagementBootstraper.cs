using _0_Framework.Infrastructure;
using _01_BECKQuery.Contract.Inventory;
using _01_BECKQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration
{
	public class InventoryManagementBootstraper
	{
		public static void Configure(IServiceCollection services ,string connectionString)
		{
			services.AddTransient<IInventoryApplication, InventoryApplication>();
			services.AddTransient<IInventoryRepository, InventoryRepository>();

			services.AddTransient<IInventoryQuery, InventoryQuery>();

			services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

			services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
		}
	}
}
