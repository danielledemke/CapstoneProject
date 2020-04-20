using Capstone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Contracts
{
    public interface ILocationService
    {
        Task<LocationService> GetLocation();
    }
}
