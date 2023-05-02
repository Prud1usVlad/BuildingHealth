using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Services.Interfaces;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingHealth.Mobile.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;


        public CommentService(HttpClient httpClient, JsonSerializerOptions serializerOptions)
        {
            _httpClient = httpClient;
            _serializerOptions = serializerOptions;
        }


        public async Task<List<Comment>> GetProjectComments(string projectId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Comments/Project/" + projectId);
            response.EnsureSuccessStatusCode();

            string stringResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Comment>>(stringResult, _serializerOptions);
        }

        public async Task PostComment(Comment comment)
        {
            comment.User = null;

            HttpResponseMessage response = await _httpClient.PostAsync("Comments/",
                new StringContent(JsonSerializer.Serialize(comment),
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
