﻿using System;
using Foundation;
using UIKit;
using SWTableViewCells;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace HappyCoupleMobile.iOS.Controls
{
    public partial class ExtendedSwipeAbleCell : SWTableViewCell
    {
        public static readonly NSString Key = new NSString("ExtendedSwipeAbleCell");
        public static readonly UINib Nib;

        static ExtendedSwipeAbleCell()
        {
            Nib = UINib.FromName("ExtendedSwipeAbleCell", NSBundle.MainBundle);
        }

        protected ExtendedSwipeAbleCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(string name, string comment, int quantity)
        {
            Name.Text = name;
            Comment.Text = comment;
            Quantity.Text = quantity.ToString();
        }

        public void SetVisualProperties()
        {
            BackgroundColor = Color.FromHex("#424242").ToUIColor();
            SelectedBackgroundView = new UIView { BackgroundColor = Color.FromHex("FEE94E").ToUIColor() };

            Name.TextColor = Comment.TextColor = Quantity.TextColor = UIColor.White;

            Name.Font = UIFont.FromName("Quicksand-Medium", 20);
            Comment.Font = UIFont.FromName("Quicksand-Light", 15f);
            Quantity.Font = UIFont.FromName("Quicksand-Medium", 20f);

            HideChecbox();
        }

        public void HideChecbox()
        {
            //CheckboxView.Hidden = true;
            //LeadingNameContraint.Constant = LeadingCommentContraint.Constant = 10;
        }

        public void HideQuantityLabel()
        {
            Quantity.Hidden = true;
        }
    }
}