using System;
namespace Restaurantes
{
	public class Restaurante
	{
		public Guid id { get; set; }
		public string nombre { get; set; }
		public Guid empresa { get; set; }
		public Empresa empresa_nav { get; set; }
	}
}

