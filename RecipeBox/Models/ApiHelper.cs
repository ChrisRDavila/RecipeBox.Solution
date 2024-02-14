using System.Threading.Tasks;
using RestSharp;

namespace RecipeBox.Models
{
  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey, string search)
    {
      RestClient client = new RestClient("https://api.giphy.com/v1/gifs");
      RestRequest request = new RestRequest($"search?api_key={apiKey}&q={search}&limit=1&offset=0&rating=g&lang=en", Method.Get);
      RestResponse response = await client.ExecuteAsync(request);
      return response.Content;
    }
  }
}