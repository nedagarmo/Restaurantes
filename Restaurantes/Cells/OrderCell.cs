using System;
using Xamarin.Forms;

namespace Restaurantes
{
	public class OrderCell: ViewCell
	{
		public OrderCell()
		{
			var mesa = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				FontAttributes = FontAttributes.Bold
			};
			mesa.SetBinding(Label.TextProperty, new Binding("celda_mesa"));

			var fecha = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			fecha.SetBinding(Label.TextProperty, new Binding("fecha"));

			var platos = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center
			};
			platos.SetBinding(Label.TextProperty, new Binding("platos"));

			View = new StackLayout
			{
				Children = { mesa, fecha, platos },
				Orientation = StackOrientation.Horizontal
			};
		}
	}
}

