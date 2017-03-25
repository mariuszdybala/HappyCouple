﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class ProductListView : StackLayout
    {
        public static BindableProperty ProductsProperty = BindableProperty
        .Create(nameof(Products), typeof(ObservableCollection<Product>), typeof(ProductListView));

        public static BindableProperty ShowCheckboxesProperty = BindableProperty
        .Create(nameof(ShowCheckboxes), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty IsCheckedProperty = BindableProperty
        .Create(nameof(IsChecked), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty ShowControlPanelProperty = BindableProperty
         .Create(nameof(ShowControlPanel), typeof(bool), typeof(ProductListView), false);

        public static BindableProperty ProductSelectedCommandProperty = BindableProperty.Create
        (nameof(ProductSelectedCommand), typeof(ICommand), typeof(ProductListView));

        public static BindableProperty ProductCheckedCommandProperty = BindableProperty.Create
        (nameof(ProductCheckedCommand), typeof(ICommand), typeof(ProductListView));

        public bool ShowControlPanel
        {
            get { return (bool)GetValue(ShowControlPanelProperty); }
            set { SetValue(ShowControlPanelProperty, value); }
        }

        public bool ShowCheckboxes
        {
            get { return (bool)GetValue(ShowCheckboxesProperty); }
            set { SetValue(ShowCheckboxesProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public ICommand ProductSelectedCommand
        {
            get { return (ICommand)GetValue(ProductSelectedCommandProperty); }
            set { SetValue(ProductSelectedCommandProperty, value); }
        }

        public ICommand ProductCheckedCommand
        {
            get { return (ICommand)GetValue(ProductCheckedCommandProperty); }
            set { SetValue(ProductCheckedCommandProperty, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public Command<object> ItemTappedCommand { get; set; }

        public ProductListView()
        {
            InitializeComponent();
            ProductsList.RowHeight = -1;

            ItemTappedCommand = new Command<object>(OnItemTapped);
        }

        private void OnItemTapped(object panelVisibillity)
        {
            var viewCell = (ViewCell)panelVisibillity;
            var statckLayout = (StackLayout)viewCell.View;

            var grid = statckLayout.Children.OfType<StackLayout>().Last();

            grid.IsVisible = !grid.IsVisible;

            viewCell.ForceUpdateSize();
        }


        private void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //ControlPanel.IsVisible = !ControlPanel.IsVisible;
        }
    }
}
