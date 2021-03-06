﻿using System;
using System.Linq;
using System.Windows.Input;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
	public class AutoResizeStackLayout : StackLayout
	{
		private VisualElement _contentElement;

		private VisualElement ContentElement => _contentElement ?? GetContentElement();

		public AutoResizeStackLayout()
		{
			VerticalOptions = LayoutOptions.FillAndExpand;
			Spacing = 0;
		}

		private VisualElement GetContentElement()
		{
			if(Children.Count > 1)
			{
				throw new Exception("AutoResizeStackLayout can have only one child");
			}

			var stackChild = Children.FirstOrDefault();

			if(stackChild == null)
			{
				throw new Exception("AutoResizeStackLayout needs at least one child");
			}

			var contentElement = stackChild as VisualElement;

			if (contentElement == null)
			{
				throw new Exception("AutoResizeStackLayout child has to be VisualElement");
			}

			_contentElement = contentElement;

			return _contentElement;
		}

		protected override void OnChildAdded(Element child)
		{
			base.OnChildAdded(child);

			ContentElement.SizeChanged += ContentElementSizeChanged;
		}

		private void ContentElementSizeChanged(object sender, EventArgs e)
		{
			OnSizeChanged();
		}

		private void OnSizeChanged()
		{
			var contentElementHeight = ContentElement.Height;

			this.HeightRequest = contentElementHeight < 0 ? 0 : contentElementHeight;
		}
	}
}
