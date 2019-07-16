using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceX.Business.Entities
{
    public class Launch
    {
        public int FlightNumber { get; set; }
        public string MissionName { get; set; }
        public DateTime LaunchDate { get; set; }
        public int LaunchYear { get; set; }
    }
}
