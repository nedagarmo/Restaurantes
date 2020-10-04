using System;
using SQLite.Net.Interop;

namespace Restaurantes
{
	public interface IConfig
	{
		string DirectoryDB { get; }
		ISQLitePlatform Platform { get; }
	}
}

