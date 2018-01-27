using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Html_Url { get; set; }
        public string Created_At { get; set; }

        public List<Project> GetTopThreeProjects()
        {

            var client = new RestClient("https://api.github.com/users/");

            var request = new RestRequest(string.Format("{0}/starred", "hansenja24"), Method.GET);
            request.AddHeader("User-Agent", "hansenja24");
            request.AddParameter("per_page", "3");
            request.AddParameter("direction", "desc");

            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonResponse.ToString());
            return projectList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
