using System;
using System.Collections.Generic;

namespace HappyCoupleMobile.Providers.Interfaces
{
	public interface IAssemblyInfoProvider
	{
		IEnumerable<Type> GetDefinedMigrationTypes();
	}
}
