using Newtonsoft.Json.Serialization;
using SpaceX.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceX.Infra.Data.SerializerContractResolvers
{
    public class LaunchSerializerResolver : DefaultContractResolver
    {
        private readonly IDictionary<string, string> _propertyMapping;

        internal LaunchSerializerResolver()
        {
            _propertyMapping = new Dictionary<string, string>
            {
                 { nameof(Launch.FlightNumber), "flight_number" }
                ,{ nameof(Launch.MissionName),  "mission_name"  }
                ,{ nameof(Launch.LaunchDate),  "launch_date_local"  }
                ,{ nameof(Launch.LaunchYear),  "launch_year"  }
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            _propertyMapping.TryGetValue(propertyName, out var resolvedName);
            return resolvedName;
        }

    }
}
