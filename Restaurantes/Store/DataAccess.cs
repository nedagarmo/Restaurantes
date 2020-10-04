using System;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace Restaurantes
{
	public class DataAccess: IDisposable
	{
		private SQLiteConnection connection;

		public DataAccess()
		{
			var config = DependencyService.Get<IConfig>();
			connection = new SQLiteConnection(config.Platform, System.IO.Path.Combine(config.DirectoryDB, "NPosRestaurantes.db3"));
			connection.CreateTable<Login>();
		}

		public void InsertLogin(Login login)
		{
			connection.Insert(login);
		}

		public void UpdateLogin(Login login)
		{
			connection.Update(login);
		}

		public void DeleteLogin(Login login)
		{
			connection.Delete(login);
		}

		public Login GetLogin(int login_id)
		{
			return connection.Table<Login>().FirstOrDefault(l => l.id == login_id);
		}

		public void Dispose()
		{
			connection.Dispose();
		}
	}
}

