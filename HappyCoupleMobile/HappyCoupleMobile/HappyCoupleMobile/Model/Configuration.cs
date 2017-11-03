using HappyCoupleMobile.Model.Interfaces;
using SQLite.Net.Attributes;

namespace HappyCoupleMobile.Model
{
	[Table("Configuration")]
	public class Configuration : IModel
	{
		[PrimaryKey, AutoIncrement, Column("id")]
		public int Id { get; set; }

		[Column("key")]
		public string Key { get; set; }

		[Column("value")]
		public string Value { get; set; }
	}
}
