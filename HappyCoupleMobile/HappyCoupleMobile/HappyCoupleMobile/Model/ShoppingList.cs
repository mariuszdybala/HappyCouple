using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("ShoppingList")]
    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("added_by_id")]
        public int AddedById { get; set; }

        [Column("edited_by_id")]
        public int EditedById { get; set; }

        [Column("deleted_by_id")]
        public int DeletedById { get; set; }

        [Column("closed_by_id")]
        public int ClosedById { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("add_date")]
        public DateTime AddDate{ get; set; }

        [Column("delete_date")]
        public DateTime DeleteDate { get; set; }

        [Column("edit_date")]
        public DateTime EditDate { get; set; }

        [Column("close_date")]
        public DateTime CloseDate { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }
    }
}
