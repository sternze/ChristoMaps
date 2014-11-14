using ChristoDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristoDomain.Interfaces
{
    public interface IMinimalSpanningTree
    {

        void SetNodes(HashSet<Coordinate> nodes);

        void SetEdges(HashSet<Edge<Coordinate>> edges, IComparer<DomainObjects.Edge<Coordinate>> comp);

        HashSet<Edge<Coordinate>> FindMinSpanTree();
    }
}
