using System;
using SQLite.Net.Attributes;

namespace Restaurantes
{
	public class Login
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string user { get; set; }
		public string password { get; set; }
		public string enterprise { get; set; }
	}
}

