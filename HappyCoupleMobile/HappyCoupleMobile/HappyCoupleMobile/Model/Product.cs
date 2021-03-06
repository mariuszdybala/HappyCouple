﻿using System;
using HappyCoupleMobile.Model.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace HappyCoupleMobile.Model
{
   [Table("Product")]
   public class Product : IModel
    {

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_favourite")]
        public bool IsFavourite { get; set; }

        [Column("is_bought")]
        public bool IsBought { get; set; }

        [Column("is_hidden")]
        public bool IsHidden { get; set; }

        [Column("added_by_id")]
        public int AddedById { get; set; }

        [Column("edited_by_id")]
        public int EditedById { get; set; }

        [Column("deleted_by_id")]
        public int DeletedById { get; set; }

        [Column("add_date")]
        public DateTime AddDate { get; set; }

        [Column("delete_date")]
        public DateTime DeleteDate { get; set; }

        [Column("edit_date")]
        public DateTime EditDate { get; set; }

        [Column("product_type_fp"), ForeignKey(typeof(ProductType))]
        public int ProductTypeId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public ProductType ProductType { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("shopping_list_fk"), ForeignKey(typeof(ShoppingList))]
        public int? ShoppingListId { get; set; }
	    
	    [Column("favourite_product_id")]
	    public int? FavouriteProductId { get; set; }
    }
}
