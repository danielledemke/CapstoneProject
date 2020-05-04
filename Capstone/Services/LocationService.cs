using Capstone.Contracts;
using Capstone.JSON;
using Capstone.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class LocationService : ILocationService
    {
        public LocationService()
        {

        }

        public async Task<LocationService> GetLocation()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/js?key={ApiKey.GoogleKey}&callback=initMap");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<LocationService>(json);
                return location;
            }
            return null;

        }
        public async Task<CoordsJson> GetUserCoords(Consumer consumer)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={consumer.ZipCode},US&sensor=false&key={ApiKey.GoogleKey}");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CoordsJson>(json);
            }
            return null;
        }
    }
}
