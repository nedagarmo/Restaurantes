using System;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Restaurantes.iOS.Config))]

namespace Restaurantes.iOS
{
	public class Config: IConfig
	{
		private string directory_db;
		private ISQLitePlatform platform;

		public string DirectoryDB
		{
			get
			{
				if (string.IsNullOrEmpty(directory_db))
				{
					var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					directory_db = System.IO.Path.Combine(directory, "..", "Library");
				}

				return directory_db;
			}
		}

		public ISQLitePlatform Platform
		{
			get
			{
				if (platform == null)
				{
					platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
				}

				return platform;
			}
		}
	}
}

