using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
			Children.Add(new OrderPage());
			Children.Add(new NotificationsPage());
			Children.Add(new AccountPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.FadeTo(1, 750, Easing.Linear);

			Enviroment.StartNotificationsListener();
		}

		public async void SearchNewsNotifications()
		{
			var comunication = new Comunication();
			string url = string.Format("/api/NotificationsApi/{0}/{1}", Security.session.empresa, Security.session.id);
			var result = await comunication.TalkGet(url);

			if (string.IsNullOrEmpty(result) || result == "null")
			{
				return;
			}

			List<Notificacion> lnotificaciones = JsonConvert.DeserializeObject<List<Notificacion>>(result);
			if (Enviroment.Notifications.Count < lnotificaciones.Count)
			{
				foreach (var page in Application.Current.MainPage.Navigation.NavigationStack)
				{
					if (page.GetType().Equals(typeof(MainPage)))
					{
						((NotificationsPage)((MainPage)page).Children[1]).LoadList();
						((OrderPage)((MainPage)page).Children[0]).LoadList();
						await DisplayAlert("NPOS Mensaje", "Hay ordenes listas para servir!!, revisa la pestaña de notificación.", "Aceptar");
					}
				}
			}
		}
	}
}

