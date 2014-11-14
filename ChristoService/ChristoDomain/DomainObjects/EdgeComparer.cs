using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristoDomain.DomainObjects
{
    public class EdgeComparer : IComparer<Edge<Coordinate>>
    {
        public int Compare(Edge<Coordinate> e1, Edge<Coordinate> e2)
        {
            return e1.getWeight() - e2.getWeight();
        }
    }
}
