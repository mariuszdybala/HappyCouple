using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model.Interfaces;
using SQLite.Net.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("ProductType")]
    public class ProductType : IModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("type")]
        public string Type { get; set; }
	    
	    [Column("group")]
	    public ProductGroup Group { get; set; }

        [Column("icon_name")]
        public string IconName { get; set; }
    }
}