using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Restaurantes
{
	public class Comunication
	{
		public async Task<string> TalkGet(string url)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = await cliente.GetAsync(url);
			return response.Content.ReadAsStringAsync().Result;
		}

		public string TalkSyncGet(string url)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = cliente.GetAsync(url).Result;
			return response.Content.ReadAsStringAsync().Result;
		}

		public async void TalkPost(string url, StringContent http)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = await cliente.PostAsync(url, http);
			var returned = response.Content.ReadAsStringAsync().Result;
		}

		public void TalkSyncPost(string url, StringContent http)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = cliente.PostAsync(url, http).Result;
			var returned = response.Content.ReadAsStringAsync().Result;
		}

		public async void TalkPut(string url, StringContent http)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = await cliente.PutAsync(url, http);
			var returned = response.Content.ReadAsStringAsync().Result;
		}

		public void TalkSyncPut(string url, StringContent http)
		{
			HttpClient cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic", "aGlzdHJpb246SElAU2VjdXJlfDkwMDAwODY3OC8qLS1ERU5EVC0tKi8="
			);
			cliente.BaseAddress = new Uri("http://restaurantes.histrion.co");
			var response = cliente.PutAsync(url, http).Result;
			var returned = response.Content.ReadAsStringAsync().Result;
		}
	}
}

