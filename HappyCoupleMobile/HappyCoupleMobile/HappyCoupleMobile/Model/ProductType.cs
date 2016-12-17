using HappyCoupleMobile.Model.Interfaces;
using SQLite.Net.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("ProductType")]
    public class ProductType : IModel
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

        public ProductType CopyWithNewName(string name, bool isFavourite = false)
        {
           return new ProductType
            {
                Name = name,
                IconName = this.IconName,
                IsPrimary = false,
                IsFavourite = isFavourite,
                UserId = this.UserId
            };
        }
    }
}