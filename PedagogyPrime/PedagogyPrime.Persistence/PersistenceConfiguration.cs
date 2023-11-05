﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PedagogyPrime.Persistence.Context;
using System.Reflection;

namespace PedagogyPrime.Persistence
{
	public static class PersistenceConfiguration
	{
		public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["ConnectionString"];
			var migrationsAssembly = typeof(PedagogyPrimeDbContext).GetTypeInfo().Assembly.GetName().Name;
			services.AddDbContext<PedagogyPrimeDbContext>(options => options.UseSqlServer(connectionString, sql =>
			{
				sql.MigrationsAssembly(migrationsAssembly);
				sql.MigrationsHistoryTable("__EFMigrationHistory");
			}));
		}
	}
}