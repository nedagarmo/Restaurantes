using System;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Restaurantes.Droid.Config))]

namespace Restaurantes.Droid
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
					directory_db = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
					platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
				}

				return platform;
			}
		}
	}
}

