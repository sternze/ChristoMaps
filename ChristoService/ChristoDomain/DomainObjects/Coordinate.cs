using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChristoDomain.DomainObjects
{
    [DataContract]
    public class Coordinate
    {
        public Coordinate(double lat, double lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }

        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}
