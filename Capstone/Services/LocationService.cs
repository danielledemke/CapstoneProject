using Capstone.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Capstone.Services
{
    public class LocationService : ILocationService
    {

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

        public async Task<LocationService> DisplayLocation()
        {
            var location = GetLocation();

        }
    }
}
