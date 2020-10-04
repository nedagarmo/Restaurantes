using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class OrderPage : ContentPage
	{
		public OrderPage()
		{
			this.Icon = Device.OnPlatform("orders.png", null, null);
			InitializeComponent();
			btnCrear.Clicked += btnCrear_Clicked;
			lvOrdenes.ItemTapped += lvOrdenes_ItemTapped;
			lvOrdenes.ItemTemplate = new DataTemplate(typeof(OrderCell));
			LoadList();
		}

		public async void LoadList()
		{
			try
			{
				aiProcesandoOrdenes.IsRunning = true;
				btnCrear.IsEnabled = false;
				var comunication = new Comunication();
				string url = string.Format("/api/OrdersApi/{0}/{1}", Security.session.empresa, Security.session.id);
				var result = await comunication.TalkGet(url);

				if (string.IsNullOrEmpty(result) || result == "null")
				{
				 	return;
				}

				lvOrdenes.ItemsSource = null;
				lvOrdenes.ItemsSource = JsonConvert.DeserializeObject<List<Orden>>(result);

				aiProcesandoOrdenes.IsRunning = false;
				btnCrear.IsEnabled = true;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnCrear.IsEnabled = true;
				aiProcesandoOrdenes.IsRunning = false;
			}
		}

		void btnCrear_Clicked(object sender, EventArgs e)
		{
			Enviroment.Order = null;
			Navigation.PushAsync(new NewOrderPage());
		}

		void lvOrdenes_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Enviroment.Order = (Orden)e.Item;
			Navigation.PushAsync(new NewOrderPage());
		}
	}
}

