using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Restaurantes
{
	public class Plato
	{
		public Guid id { get; set; }
		public int numero { get; set; }
		public Guid orden { get; set; }
		public Guid empresa { get; set; }
		public Guid menu { get; set; }
		public Menu menu_nav { get; set; }
		public List<OrdenPlatoComponente> orden_plato_componente_nav { get; set; }

		public string descripcion { get { return this.ToString(this.id, false); } }
		public string descripcion_completa { get { return this.ToString(this.id, true); } }

		public string ToString(Guid ide, bool completa)
		{
			try
			{
				string cadena = "";

				if (orden_plato_componente_nav != null)
				{
					if (orden_plato_componente_nav.Count > 0)
					{
						if (orden_plato_componente_nav[0].componente_nav != null)
						{
							foreach (var componente in orden_plato_componente_nav)
							{
								if (componente != null)
								{
									cadena += componente.componente_nav.nombre + ", ";
								}
							}

							cadena = cadena.Substring(0, cadena.Length - 2);
						}
					}
				}

				if(string.IsNullOrEmpty(cadena))
				{
					var comunication = new Comunication();
					string url = string.Format("/api/ComponentsApi/{0}/{1}", Security.session.empresa, ide);
					cadena = comunication.TalkSyncGet(url);

					if (string.IsNullOrEmpty(cadena) || cadena == "null")
					{
						return "";
					}

					cadena = cadena.Substring(1, cadena.Length - 2);
				}

				if (!completa)
				{
					if (cadena.Length > Device.OnPlatform(65, 85, 85)) { cadena = cadena.Substring(0, Device.OnPlatform(65, 85, 85)) + "..."; }
				}

				return cadena;
			}
			catch (Exception)
			{
				return "";
			}
		}
	}
}

