using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class NewOrderPage : ContentPage
	{
		int operacion = 1;
		Orden orden = new Orden();

		public NewOrderPage()
		{
			InitializeComponent();

			btnAgregar.Clicked += btnAgregar_Clicked;
			btnGuardar.Clicked += btnGuardar_Clicked;
			lvPlatos.ItemTapped += lvPlatos_ItemTapped;
			lvPlatos.ItemTemplate = new DataTemplate(typeof(DishCell));

			if (Enviroment.Order == null)
			{
				operacion = 1;
				orden.id = Guid.NewGuid();
				orden.fecha = DateTime.Now;
				orden.empresa = Security.session.empresa;
				orden.mesero = Security.session.id;
				orden.modalidad = Guid.Parse("A97C514E-FB6B-4A07-9913-EE42825E192D");
				orden.estado = true;
				orden.aprobada = true;
				Enviroment.Order = orden;
				txtMesa.Focus();
			}
			else
			{
				operacion = 2;
				this.Title = "Editar Orden";
				txtMesa.Text = Enviroment.Order.mesa;

				Button btnCancelar = new Button() { 
					Text = "Cancelar Orden", 
					HorizontalOptions = LayoutOptions.FillAndExpand, 
					VerticalOptions = LayoutOptions.Center, 
					TextColor = Color.White, 
					BackgroundColor = Color.Red 
				};
				btnCancelar.Clicked += btnCancelar_Clicked;

				slButtons.Children.Add(btnCancelar);
				LoadList();
			}
		}

		async void btnCancelar_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("NPOS Mensaje", "Está seguro de querer cancelar la orden?", "Si", "No");
			if (response)
			{
				try
				{
					aiProcesando.IsRunning = true;
					btnGuardar.IsEnabled = false;
					btnAgregar.IsEnabled = false;

					var comunication = new Comunication();
					string url = "/api/OrdersApi";
					Enviroment.Order.estado = false;
					var request = JsonConvert.SerializeObject(Enviroment.Order);
					var content = new StringContent(request, Encoding.UTF8, "text/json");
					url = url + "/" + Enviroment.Order.id;
					comunication.TalkSyncPut(url, content);

					aiProcesando.IsRunning = false;
					btnGuardar.IsEnabled = true;
					btnAgregar.IsEnabled = true;
				}
				catch (Exception)
				{
					await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
					aiProcesando.IsRunning = false;
					btnGuardar.IsEnabled = true;
					btnAgregar.IsEnabled = true;
				}

				await Navigation.PushAsync(new MainPage());
				var main = (MainPage)Application.Current.MainPage.Navigation.NavigationStack[0];
				Navigation.RemovePage(main);
				Navigation.RemovePage(this);
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.FadeTo(1, 750, Easing.Linear);
		}

		async void LoadList()
		{
			try
			{
				aiProcesando.IsRunning = true;
				btnGuardar.IsEnabled = false;
				btnAgregar.IsEnabled = false;

				lvPlatos.ItemsSource = Enviroment.Order.plato_nav;

				aiProcesando.IsRunning = false;
				btnGuardar.IsEnabled = true;
				btnAgregar.IsEnabled = true;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				aiProcesando.IsRunning = false;
				btnGuardar.IsEnabled = true;
				btnAgregar.IsEnabled = true;
			}
		}

		void lvPlatos_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Plato plato = (Plato)e.Item;
			Navigation.PushModalAsync(new AddComponentsToDish(plato.id, 2));
		}

		void btnAgregar_Clicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new AddComponentsToDish(Guid.NewGuid(), 1));
		}

		async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			var comunication = new Comunication();
			string url = "/api/OrdersApi";

			if (!string.IsNullOrEmpty(txtMesa.Text))
			{
				Enviroment.Order.mesa = txtMesa.Text;

				var request = JsonConvert.SerializeObject(Enviroment.Order);
				var content = new StringContent(request, Encoding.UTF8, "text/json");

				switch (operacion)
				{
					case 1:
						comunication.TalkPost(url, content);
						break;
					case 2:
						url = url + "/" + Enviroment.Order.id;
						comunication.TalkPut(url, content);
						break;
					default:
						break;
				}

				await DisplayAlert("NPOS Mensaje", "Se ha guardado la orden correctamente", "Aceptar");
				await Navigation.PushAsync(new MainPage());
				var main = (MainPage) Application.Current.MainPage.Navigation.NavigationStack[0];
				Navigation.RemovePage(main);
				Navigation.RemovePage(this);
			}
			else
			{ 
				await DisplayAlert("NPOS Mensaje", "Por favor ingrese el identificador de la mesa que está atendiendo.", "Aceptar");
				txtMesa.Focus();
			}
		}

		public void reloadList()
		{
			lvPlatos.ItemsSource = null;
			LoadList();
		}
	}
}

