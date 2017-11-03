using System;

namespace HappyCoupleMobile.Custom
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MigrationAttribute : Attribute
	{
		public int MigrationId { get; set; }

		public MigrationAttribute(int migrationId)
		{
			MigrationId = migrationId;
		}
	}
}
