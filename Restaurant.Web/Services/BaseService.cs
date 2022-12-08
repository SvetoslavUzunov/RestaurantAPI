using System.Text;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Services;

public class BaseService : IBaseService
{
	public ResponseDto responseModel { get; set; }

	public IHttpClientFactory httpClient { get; set; }

	public BaseService(IHttpClientFactory httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<T> SendAsync<T>(ApiRequest apiRequest)
	{
		try
		{
			var client = httpClient.CreateClient("RestaurantAPI");

			HttpRequestMessage message = new HttpRequestMessage();
			message.Headers.Add("Accept", "application/json");
			message.RequestUri = new Uri(apiRequest.Url);
			client.DefaultRequestHeaders.Clear();

			if (apiRequest.Data is not null)
			{
				message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
			}

			HttpResponseMessage apiResponse = null;
			message.Method = apiRequest.ApiType switch
			{
				StaticDetails.ApiType.POST => HttpMethod.Post,
				StaticDetails.ApiType.PUT => HttpMethod.Put,
				StaticDetails.ApiType.DELETE => HttpMethod.Delete,
				_ => HttpMethod.Get,
			};

			apiResponse = await client.SendAsync(message);
			var apiContent = await apiResponse.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(apiContent);
		}
		catch (Exception ex)
		{
			var dto = new ResponseDto
			{
				DisplayMessage = "Error",
				ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
				IsSuccess = false,
			};

			var result = JsonConvert.SerializeObject(dto);

			return JsonConvert.DeserializeObject<T>(result);
		}
	}

	public void Dispose()
	{
		GC.SuppressFinalize(true);
	}
}
