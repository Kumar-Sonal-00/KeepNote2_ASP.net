using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Keepnote_Step1_boilerplate;
using Keepnote_Step1_boilerplate.Controllers;
using Keepnote_Step1_boilerplate.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Test
{
    public class NoteControllerTest1:IClassFixture<WebApplicationFactory<Startup>>
    {
        //private readonly NoteController noteController;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        public NoteControllerTest1(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TestGetAllNotes()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",httpResponse.Content.Headers.ContentType.ToString());

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Contains("<tr>", stringResponse);
            Assert.Contains("NoteId", stringResponse);
            Assert.Contains("NoteTitle", stringResponse);
        }

        [Fact]
        public async Task TestAddNote()
        {
            Note note = new Note { NoteId = 1, NoteTitle = "Technology", NoteContent = "ASP.NET Core", NoteStatus = "Completed" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new XmlMediaTypeFormatter();
            // The endpoint or route of the controller action.
            var content = JsonConvert.SerializeObject(note);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/note/create",note, formatter);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", httpResponse.Content.Headers.ContentType.ToString());

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Contains("<tr>", stringResponse);
            Assert.Contains("NoteId", stringResponse);
            Assert.Contains("NoteTitle", stringResponse);
        }
    }
}
