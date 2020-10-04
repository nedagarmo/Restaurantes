using System;
namespace Restaurantes
{
	public class OrdenPlatoComponente
	{
		public Guid id { get; set; }
		public Guid plato { get; set; }
		public Guid componente { get; set; }
		public Guid empresa { get; set; }

		public Componente componente_nav { get; set; }
	}
}

