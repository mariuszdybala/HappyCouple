﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace HappyCoupleMobile.Model
{
    [Table("ShoppingList")]
    public class ShoppingList : IModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("added_by_id")]
        public int AddedById { get; set; }

        [Column("edited_by_id")]
        public int? EditedById { get; set; }

        [Column("closed_by_id")]
        public int? ClosedById { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("add_date")]
        public DateTime AddDate{ get; set; }

        [Column("edit_date")]
        public DateTime? EditDate { get; set; }

        [Column("close_date")]
        public DateTime? CloseDate { get; set; }

        [Column("shopping_list_status")]
        public ShoppingListStatus Status { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Product> Products { get; set; }

        public ShoppingList()
        {
            Products = new List<Product>();
        }
    }
}
