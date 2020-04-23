using Capstone.JSON;
using Capstone.Models;
using Capstone.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Capstone.Contracts
{
    public interface ILocationService
    {
        Task GetUserCoords(Consumer consumer);
    }
}
