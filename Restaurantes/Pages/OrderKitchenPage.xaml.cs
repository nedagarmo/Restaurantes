using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class OrderKitchenPage : ContentPage
	{
		public OrderKitchenPage()
		{
			this.Icon = Device.OnPlatform("orders.png", null, null);
			InitializeComponent();
			btnRecargar.Clicked += btnRecargar_Clicked;
			lvOrdenesCocina.ItemTapped += lvOrdenesCocina_ItemTapped;
			lvOrdenesCocina.ItemTemplate = new DataTemplate(typeof(OrderCell));
			LoadList();
		}

		void btnRecargar_Clicked(object sender, EventArgs e)
		{
			LoadList();
		}

		void lvOrdenesCocina_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Enviroment.Order = (Orden)e.Item;
			Navigation.PushAsync(new ViewOrderPage(true));
		}

		async void LoadList()
		{
			try
			{
				btnRecargar.IsEnabled = false;
				lvOrdenesCocina.ItemsSource = null;
				var comunication = new Comunication();
				string url = string.Format("/api/OrdersApi/{0}/{1}", Security.session.restaurante, Guid.Empty);
				var result = await comunication.TalkGet(url);

				btnRecargar.IsEnabled = true;
				if (string.IsNullOrEmpty(result) || result == "null")
				{
					return;
				}

				lvOrdenesCocina.ItemsSource = JsonConvert.DeserializeObject<List<Orden>>(result);
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnRecargar.IsEnabled = true;
			}
		}
	}
}

