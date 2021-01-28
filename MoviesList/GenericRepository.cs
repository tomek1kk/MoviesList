using MoviesList.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesList
{
    // Since SWapiCSharp's GetById() method doesn't work anymore, I copied some code from their GitHub repo, adjusted to my needs and fixed bug.
    // source: https://github.com/M-Yankov/SWapi-CSharp/tree/master/SWapi-CSharp
    // still using their model classes
    public class GenericRepository<T> : IGenericRepository<T>
    {
        const string apiEndpoint = "https://swapi.dev/api/";

        public T GetByUrl(string url)
        {
            string jsonResponse = GetDataResult(url);
            if (jsonResponse == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public T GetById(int id, string type)
        {
            return GetByUrl($"{apiEndpoint}{type}{'/'}{id}{'/'}");
        }

        public ICollection<T> GetAll(string type)
        {
            string jsonResponse = GetDataResult($"{apiEndpoint}{type}{'/'}");
            if (jsonResponse == null)
                return null;

            var helper = JsonConvert.DeserializeObject<Helper>(jsonResponse);
            if (helper == null)
                return null;
            return helper.Results.ToList();
        }


        public string GetDataResult(string url)
        {
            if (url == null)
                return null;
            WebRequest request = WebRequest.Create(url);
            var response = request.GetResponse();
            string json;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                json = reader.ReadToEnd();
            return json;
        }

        internal class Helper
        {
            [JsonProperty]
            public ICollection<T> Results { get; set; }
        }
    }

}
