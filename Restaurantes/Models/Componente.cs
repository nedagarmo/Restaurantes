using System;
namespace Restaurantes
{
	public class Componente
	{
		public Guid id { get; set; }
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public Guid menu { get; set; }
		public bool estado { get; set; }
		public Guid tipo { get; set; }
		public Guid empresa { get; set; }
	}
}

