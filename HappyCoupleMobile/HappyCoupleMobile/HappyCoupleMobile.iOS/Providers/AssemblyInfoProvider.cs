using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HappyCoupleMobile.Migrations;
using HappyCoupleMobile.Providers.Interfaces;

namespace HappyCoupleMobile.iOS.Providers
{
	public class AssemblyInfoProvider : IAssemblyInfoProvider
	{
		public IEnumerable<Type> GetDefinedMigrationTypes()
		{
			Assembly executingAssembly = typeof(Migrator).GetTypeInfo().Assembly;
			return executingAssembly.GetTypes()
				.Where(type => type.Name.Contains("Migration") && type.GetInterface(nameof(IMigration)) != null);
		}
	}
}
