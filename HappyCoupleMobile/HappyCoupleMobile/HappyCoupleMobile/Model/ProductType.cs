using SQLite.Net.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("Product")]
    public class ProductType
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("icon_name")]
        public string IconName { get; set; }

        [Column("is_primary")]
        public bool IsPrimary { get; set; }

        [Column("is_favourite")]
        public bool IsFavourite { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }
    }
}