using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RecipeBox.Models
{
  public class Giphy
  {
    public string Embed_Url { get; set; }
    public string Title { get; set; }
    public string Search { get; set; }

    public static Giphy GetGiphy(string apiKey, string search)
    {
      Task<string> apiCallTask = ApiHelper.ApiCall(apiKey, search);
      string result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      List<Giphy> giphyList = JsonConvert.DeserializeObject<List<Giphy>>(jsonResponse["data"].ToString());
      Giphy giphyObj = giphyList[0];
      return giphyObj;
    }
  }

}