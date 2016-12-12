using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("Product")]
   public class Product
    {
        private const int MaxWorld = 4;

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("added_by_id")]
        public int AddedById { get; set; }

        [Column("edited_by_id")]
        public int EditedById { get; set; }

        [Column("deleted_by_id")]
        public int DeletedById { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("add_date")]
        public DateTime AddDate { get; set; }

        [Column("delete_date")]
        public DateTime DeleteDate { get; set; }

        [Column("edit_date")]
        public DateTime EditDate { get; set; }

        [Column("product_type")]
        public ProductType ProductType { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("quantity")]
        public string Quantity { get; set; }

        [Column("shopping_list_fk"), ForeignKey(typeof(ShoppingList))]
        public int? ShoppingListId { get; set; }
    }
}
