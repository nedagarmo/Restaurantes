using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Restaurantes
{
	public class Enviroment
	{
		// variable que contiene la lista de platos de una orden
		public static Orden Order { get; set; }
		public static List<Notificacion> Notifications { get; set; }
		public static bool NotificationsListener { get; set; }

		public static void StartNotificationsListener()
		{
			try
			{
				NotificationsListener = true;
				var main = (MainPage)Application.Current.MainPage.Navigation.NavigationStack[1];
				Device.StartTimer(TimeSpan.FromSeconds(20), () => { main.SearchNewsNotifications(); return NotificationsListener; });
			}
			catch (Exception) { }
		}
	}
}

