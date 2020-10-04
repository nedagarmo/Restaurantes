using System;
using Xamarin.Forms;

namespace Restaurantes
{
	public class DishCell: ViewCell
	{
		public DishCell()
		{
			var numero = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				FontAttributes = FontAttributes.Bold
			};
			numero.SetBinding(Label.TextProperty, new Binding("numero"));

			var descripcion = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			descripcion.SetBinding(Label.TextProperty, new Binding("descripcion"));

			View = new StackLayout
			{
				Children = { numero, descripcion },
				Orientation = StackOrientation.Horizontal
			};
		}
	}
}

