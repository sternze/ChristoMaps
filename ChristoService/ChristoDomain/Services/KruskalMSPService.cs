using ChristoDomain.DomainObjects;
using ChristoDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristoDomain.Services
{
    public class KruskalMSPService : IMinimalSpanningTree
    {
        private Dictionary<Coordinate, HashSet<Coordinate>> nodeSubsets = new Dictionary<Coordinate, HashSet<Coordinate>>();
        private HashSet<Edge<Coordinate>> mstEdges = new HashSet<Edge<Coordinate>>();
        private Edge<Coordinate>[] edges;

        public HashSet<DomainObjects.Edge<Coordinate>> FindMinSpanTree()
        {
            int size = edges.Length;
            for (int i = 0; i < size; i++)
            {
                var currEdge = edges[i];
                var from = currEdge.getNode1();
                var to = currEdge.getNode2();
                HashSet<Coordinate> fromSet;

                if (!nodeSubsets.TryGetValue(from, out fromSet))
                {
                    //TODO: Throw Exception
                    return null;
                }
                HashSet<Coordinate> toSet;

                if (!nodeSubsets.TryGetValue(to, out toSet))
                {
                    //TODO: Throw Exception
                    return null;
                }
                // Nachsehen ob die beiden Knoten bereits im selben Subset sind. Wenn ja, weitermachen, sonst überspringen.
                if (!fromSet.Equals(toSet))
                {
                    if (fromSet.Count > toSet.Count)
                    {
                        MoveValuesFromSet1IntoSet2(toSet, fromSet);
                    }
                    else
                    {
                        MoveValuesFromSet1IntoSet2(fromSet, toSet);
                    }

                    mstEdges.Add(currEdge);

                    //				System.out.println("Adding " + currEdge);
                    
                }
            }


            return mstEdges;
        }


        private HashSet<Coordinate> MoveValuesFromSet1IntoSet2(HashSet<Coordinate> source, HashSet<Coordinate> dest) {
		    Object[] srcArray = source.ToArray();
		    source = null;
		    int transferSize = srcArray.Length;
		
		    for (int j=0; j<transferSize; j++) {
			    nodeSubsets[(Coordinate) srcArray[j]] = dest;
			    dest.Add((Coordinate) srcArray[j]);
		    }
		    return dest;
	    }

        public void SetNodes(HashSet<DomainObjects.Coordinate> nodes)
        {
            foreach(var node in nodes) {
                nodeSubsets.Add(node, new HashSet<Coordinate>(new[] { node }));
		    }
        }

        public void SetEdges(HashSet<DomainObjects.Edge<Coordinate>> edges, IComparer<DomainObjects.Edge<Coordinate>> comp)
        {
            this.edges = edges.ToArray();

            Array.Sort(this.edges, comp);
            this.mstEdges = new HashSet<Edge<Coordinate>>();
        }
    }
}
