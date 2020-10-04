using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Restaurantes
{
	public partial class KitchenPage : TabbedPage
	{
		public KitchenPage()
		{
			InitializeComponent();
			Children.Add(new OrderKitchenPage());
			Children.Add(new AccountPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.FadeTo(1, 750, Easing.Linear);
		}
	}
}

