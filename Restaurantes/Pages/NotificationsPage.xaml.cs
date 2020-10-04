using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class NotificationsPage : ContentPage
	{
		public NotificationsPage()
		{
			this.Icon = Device.OnPlatform("notifications.png", null, null);
			InitializeComponent();
			lvNotificaciones.ItemTemplate = new DataTemplate(typeof(NotificationCell));
			lvNotificaciones.ItemTapped += lvNotificaciones_ItemTapped;
			LoadList();
		}

		void lvNotificaciones_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Enviroment.Order = ((Notificacion) e.Item).orden_nav;
			Navigation.PushAsync(new ViewOrderPage(false));
		}

		public async void LoadList()
		{
			try
			{
				aiProcesandoNotificaciones.IsRunning = true;
				var comunication = new Comunication();
				string url = string.Format("/api/NotificationsApi/{0}/{1}", Security.session.empresa, Security.session.id);
				var result = await comunication.TalkGet(url);

				if (string.IsNullOrEmpty(result) || result == "null")
				{
					return;
				}

				Enviroment.Notifications = JsonConvert.DeserializeObject<List<Notificacion>>(result);

				lvNotificaciones.ItemsSource = null;
				lvNotificaciones.ItemsSource = Enviroment.Notifications;

				aiProcesandoNotificaciones.IsRunning = false;
			}
			catch (Exception)
			{
				await DisplayAlert("Error", "Se han detectado problemas de conexión.  Por favor, inténtelo más tarde.", "Aceptar");
				aiProcesandoNotificaciones.IsRunning = false;
			}
		}
	}
}

