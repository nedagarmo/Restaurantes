using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Restaurantes
{
	public partial class AccountPage : ContentPage
	{
		public AccountPage()
		{
			this.Icon = Device.OnPlatform("historical.png", null, null);
			InitializeComponent();

			aiProcesandoCuenta.IsRunning = true;

			lbUsuario.Text = Security.session.nombre;
			lbRestaurante.Text = Security.session.restaurante_nav.nombre;
			lbEmpresa.Text = Security.session.restaurante_nav.empresa_nav.nombre;
			lbPerfil.Text = Security.session.perfil_nav.nombre;
			btnSalir.Clicked += btnSalir_Clicked;

			aiProcesandoCuenta.IsRunning = false;
		}

		async void btnSalir_Clicked(object sender, EventArgs e)
		{
			Enviroment.NotificationsListener = false;
			Application.Current.MainPage = new NavigationPage(new LoginPage());
			await Application.Current.MainPage.Navigation.PopAsync();
		}
	}
}

