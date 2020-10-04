using System;
using System.Collections.Generic;

namespace Restaurantes
{
	public class ComponenteTipo
	{
		public Guid id { get; set; }
		public string nombre { get; set; }
		public List<Componente> componente_nav { get; set; }
	}
}

