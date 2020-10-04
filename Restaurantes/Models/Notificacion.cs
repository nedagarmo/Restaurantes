using System;
namespace Restaurantes
{
	public class Notificacion
	{
		public Guid id { get; set; }
		public Guid orden { get; set; }
		public string mensaje { get; set; }
		public DateTime fecha { get; set; }
		public Guid empresa { get; set; }
		public Orden orden_nav { get; set; }
	}
}

