using SQLite.Net.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("internal_id")]
        public int InternalId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

    }
}