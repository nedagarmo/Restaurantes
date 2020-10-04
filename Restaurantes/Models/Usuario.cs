using System;
namespace Restaurantes
{
	public class Usuario
	{
		public Perfil perfil_nav { get; set; }
		public Restaurante restaurante_nav { get; set; }
		public object orden_nav { get; set; }
		public Guid id { get; set; }
		public string nombre { get; set; }
		public string usuario1 { get; set; }
		public string clave { get; set; }
		public string correo { get; set; }
		public string direccion { get; set; }
		public string telefono { get; set; }
		public bool estado { get; set; }
		public Guid perfil { get; set; }
		public Guid restaurante { get; set; }
		public Guid empresa { get; set; }
	}
}

