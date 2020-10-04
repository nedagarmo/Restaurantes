using System;
using System.Collections.Generic;

namespace Restaurantes
{
	public class Orden
	{
		public Guid id { get; set; }
		public Guid modalidad { get; set; }
		public string mesa { get; set; }
		public string celda_mesa { get { return "Mesa " + mesa; } }
		public DateTime fecha { get; set; }
		public bool estado { get; set; }
		public bool aprobada { get; set; }
		public List<Plato> plato_nav { get; set; }
		public string platos { get { return "Platos: " + plato_nav.Count; } }
		public Guid empresa { get; set; }
		public Guid mesero { get; set; }

		public Orden()
		{
			this.plato_nav = new List<Plato>();
		}

		public override string ToString()
		{
			return string.Format("[Orden: mesa={0}, fecha={1}, cantidad platos={2}]", mesa, fecha, plato_nav.Count);
		}
	}
}

