using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class ViewOrderPage : ContentPage
	{
		public ViewOrderPage(bool operacion)
		{
			InitializeComponent();
			txtMesa.Text = Enviroment.Order.mesa;
			LoadList();
			btnLista.Clicked += btnLista_Clicked;
			btnLista.IsVisible = operacion;
		}

		async void btnLista_Clicked(object sender, EventArgs e)
		{
			Notificacion notificacion = new Notificacion()
			{
				id = Guid.NewGuid(),
				orden = Enviroment.Order.id,
				mensaje = string.Format("La orden de la mesa {0} ya está lista!", Enviroment.Order.mesa),
				fecha = DateTime.Now,
				empresa = Security.session.empresa
			};

			var comunication = new Comunication();
			string url = "/api/NotificationsApi";
			var request = JsonConvert.SerializeObject(notificacion);
			var content = new StringContent(request, Encoding.UTF8, "text/json");
			comunication.TalkSyncPost(url, content);

			await Navigation.PushAsync(new KitchenPage());
			var main = (KitchenPage)Application.Current.MainPage.Navigation.NavigationStack[0];
			Navigation.RemovePage(main);
			Navigation.RemovePage(this);
		}

		async void LoadList()
		{
			try
			{
				btnLista.IsEnabled = false;
				var comunication = new Comunication();

				foreach (var plato in Enviroment.Order.plato_nav)
				{
					string url = string.Format("/api/MenusApi/{0}", plato.menu);
					string result = await comunication.TalkGet(url);
					if (string.IsNullOrEmpty(result) || result == "null")
					{
						return;
					}

					Menu menu = JsonConvert.DeserializeObject<Menu>(result);
					StackLayout stack = new StackLayout();
					stack.Children.Add(new Label { Text = "----------", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center });
					stack.Children.Add(new Label { Text = "Menú: " + menu.nombre, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });
					stack.Children.Add(new Label { Text = "Plato No. " + plato.numero.ToString(), HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center });

					stack.Children.Add(new Label { Text = "Componentes: " + plato.descripcion_completa, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center });
					slPlatos.Children.Add(stack);
				}

				btnLista.IsEnabled = true;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnLista.IsEnabled = false;
			}
		}
	}
}

