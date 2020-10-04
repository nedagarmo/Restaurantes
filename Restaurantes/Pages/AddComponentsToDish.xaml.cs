using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class AddComponentsToDish : ContentPage
	{
		List<ComponenteTipo> ltipocomponentes = new List<ComponenteTipo>();
		List<Menu> lmenus = new List<Menu>();
		Guid plato_id;
		int operacion;

		public AddComponentsToDish(Guid plato, int operacion)
		{
			InitializeComponent();

			this.plato_id = plato;
			this.operacion = operacion;
			FillComboMenu();
			btnCancelar.Clicked += btnCancelar_Clicked;
			btnGuardar.Clicked += btnGuardar_Clicked;
			cboMenu.SelectedIndexChanged += cboMenu_SelectedIndexChanged;

			if (operacion == 2) { GenerateControls(); }
		}

		async void FillComboMenu()
		{
			try
			{
				aiProcesandoComponentes.IsRunning = true;
				btnGuardar.IsEnabled = false;

				var comunication = new Comunication();
				string url = string.Format("/api/MenusApi/{0}/{1}", Security.session.empresa, Security.session.restaurante);
				var result = await comunication.TalkGet(url);

				if (string.IsNullOrEmpty(result) || result == "null")
				{
					return;
				}

				lmenus = JsonConvert.DeserializeObject<List<Menu>>(result);

				foreach (var menu in lmenus)
				{
					cboMenu.Items.Add(menu.nombre);
				}

				if (operacion == 2)
				{
					cboMenu.SelectedIndex = lmenus.IndexOf(lmenus.Find(m => m.id.Equals(Enviroment.Order.plato_nav.Find(f => f.id.Equals(plato_id)).menu)));
				}

				btnGuardar.IsEnabled = true;
				aiProcesandoComponentes.IsRunning = false;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnGuardar.IsEnabled = true;
				aiProcesandoComponentes.IsRunning = false;
			}

		}

		async void GenerateControls()
		{
			try
			{
				if (cboMenu.SelectedIndex >= 0)
				{
					aiProcesandoComponentes.IsRunning = true;
					btnGuardar.IsEnabled = false;

					var comunication = new Comunication();
					string url = string.Format("/api/ComponentTypesApi/{0}/{1}", Security.session.empresa, lmenus[cboMenu.SelectedIndex].id);
					var result = await comunication.TalkGet(url);

					if (string.IsNullOrEmpty(result) || result == "null")
					{
						return;
					}

					ltipocomponentes = JsonConvert.DeserializeObject<List<ComponenteTipo>>(result);
					foreach (var tipo in ltipocomponentes)
					{
						slControls.Children.Add(new Label() { Text = tipo.nombre, FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center });

						foreach (var componente in tipo.componente_nav)
						{
							StackLayout stack = new StackLayout();
							stack.Orientation = StackOrientation.Horizontal;
							stack.Children.Add(new Label { Text = componente.nombre, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center });
							var checker = new Switch { ClassId = componente.id.ToString(), HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center };
							if (operacion == 2) { 
								if (Enviroment.Order.plato_nav.Find(f => f.id.Equals(plato_id)).orden_plato_componente_nav.Find(f => f.componente.Equals(componente.id)) != null) 
								{ 
									checker.IsToggled = true; 
								} 
							}
							stack.Children.Add(checker);

							slControls.Children.Add(stack);
						}
					}

					btnGuardar.IsEnabled = true;
					aiProcesandoComponentes.IsRunning = false;
				}
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnGuardar.IsEnabled = true;
				aiProcesandoComponentes.IsRunning = false;
			}
		}

		void cboMenu_SelectedIndexChanged(object sender, EventArgs e)
		{
			slControls.Children.Clear();
			GenerateControls();
		}

		async void btnGuardar_Clicked(object sender, EventArgs e)
		{
			try
			{
				aiProcesandoComponentes.IsRunning = true;
				btnGuardar.IsEnabled = false;

				var comunication = new Comunication();

				switch (operacion)
				{
					// En caso de que se deba registrar un plato nuevo
					case 1:
						Plato plato = new Plato();
						plato.id = plato_id;
						plato.numero = Enviroment.Order.plato_nav.Count + 1;
						plato.orden = Enviroment.Order.id;
						plato.menu = lmenus[cboMenu.SelectedIndex].id;
						plato.empresa = Security.session.empresa;
						plato.orden_plato_componente_nav = new List<OrdenPlatoComponente>();
						Enviroment.Order.plato_nav.Add(plato);
						break;
					// En caso de que se deba actualizar un plato existente	
					case 2:
						Enviroment.Order.plato_nav.Find(f => f.id.Equals(plato_id)).orden_plato_componente_nav.Clear();
						break;
					default:
						break;
				}

				foreach (var item in slControls.Children)
				{
					if (item.GetType().Equals(new StackLayout().GetType()))
					{
						var stack = (StackLayout)item;
						foreach (var control in stack.Children)
						{
							if (control.GetType().Equals(new Switch().GetType()))
							{
								Switch s = (Switch)control;

								if (s.IsToggled)
								{
									OrdenPlatoComponente opc = new OrdenPlatoComponente();
									opc.id = Guid.NewGuid();
									opc.componente = Guid.Parse(s.ClassId);
									opc.plato = plato_id;
									opc.empresa = Security.session.empresa;

									string url = string.Format("/api/ComponentsApi/{0}", opc.componente);
									var result = await comunication.TalkGet(url);

									if (string.IsNullOrEmpty(result) || result == "null")
									{
										await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
										btnGuardar.IsEnabled = true;
										aiProcesandoComponentes.IsRunning = false;
										return;
									}

									opc.componente_nav = JsonConvert.DeserializeObject<Componente>(result);

									Enviroment.Order.plato_nav.Find(f => f.id.Equals(plato_id)).orden_plato_componente_nav.Add(opc);
								}
							}
						}
					}
				}

				btnGuardar.IsEnabled = true;
				aiProcesandoComponentes.IsRunning = false;

				var form = (NewOrderPage)Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count - 1];
				form.reloadList();
				await Navigation.PopModalAsync();
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				btnGuardar.IsEnabled = true;
				aiProcesandoComponentes.IsRunning = false;
			}
		}

		void btnCancelar_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}

