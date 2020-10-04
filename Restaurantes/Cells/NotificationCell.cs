using System;
using Xamarin.Forms;

namespace Restaurantes
{
	public class NotificationCell: ViewCell
	{
		public NotificationCell()
		{
			var mensaje = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				FontAttributes = FontAttributes.Bold
			};
			mensaje.SetBinding(Label.TextProperty, new Binding("mensaje"));

			View = new StackLayout
			{
				Children = { mensaje },
				Orientation = StackOrientation.Horizontal
			};
		}
	}
}

