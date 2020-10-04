using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class LoginPage : ContentPage
	{
		private DataAccess db;

		public LoginPage()
		{
			InitializeComponent();
			btnEntrar.Clicked += btnEntrar_Clicked;
			using (db = new DataAccess())
			{
				var login = db.GetLogin(1);

				if (login != null)
				{
					txtUsuario.Text = login.user;
					txtClave.Text = login.password;
					txtCodigo.Text = login.enterprise;
				}
			}
		}

		private async void btnEntrar_Clicked(Object sender, EventArgs e)
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(txtUsuario.Text))
				{
					await DisplayAlert("Error", "Debe ingresar un usuario válido", "Aceptar");
					txtUsuario.Focus();
					return;
				}

				if (string.IsNullOrEmpty(txtClave.Text))
				{
					await DisplayAlert("Error", "Debe ingresar una contraseña", "Aceptar");
					txtClave.Focus();
					return;
				}

				if (string.IsNullOrEmpty(txtCodigo.Text))
				{
					await DisplayAlert("Error", "Debe ingresar un código de verificación", "Aceptar");
					txtCodigo.Focus();
					return;
				}

				aiProcesando.IsRunning = true;
				btnEntrar.IsEnabled = false;
				var comunication = new Comunication();
				string url = string.Format("/api/UsersApi/{0}/{1}/{2}", txtUsuario.Text, txtClave.Text, txtCodigo.Text);
				result = await comunication.TalkGet(url);

				if (string.IsNullOrEmpty(result) || result == "null")
				{
					await DisplayAlert("Error", "Credenciales incorrectas.  Por favor, revise los datos proporcionados.", "Aceptar");
					txtClave.Text = string.Empty;
					txtClave.Focus();
					return;
				}

				var usuario = JsonConvert.DeserializeObject<Usuario>(result);

				using (db = new DataAccess())
				{
					var login = db.GetLogin(1);
					if (login == null)
					{
						db.InsertLogin(new Login() { user = txtUsuario.Text, password = txtClave.Text, enterprise = txtCodigo.Text });
					}
					else
					{
						db.UpdateLogin(new Login() { id = 1, user = txtUsuario.Text, password = txtClave.Text, enterprise = txtCodigo.Text });
					}
				}

				btnEntrar.IsEnabled = true;
				aiProcesando.IsRunning = false;

				Security.session = usuario;
				Security.is_loggued = true;
				switch (usuario.perfil_nav.nombre)
				{
					case "Mesero":
						await Navigation.PushAsync(new MainPage());
						break;
					case "Cocina":
						await Navigation.PushAsync(new KitchenPage());
						break;
					default:
						await Navigation.PushAsync(new MainPage());
						break;
				}

				Navigation.RemovePage(this);
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				txtClave.Text = string.Empty;
				txtClave.Focus();
			}
		}
	}
}
