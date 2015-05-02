using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristoDomain.DomainObjects
{
    public class Edge<T>
    {
        private T node1;
        private T node2;
        private int weight;

        public T getNode1()
        {
            return node1;
        }

        public void setNode1(T node1)
        {
            this.node1 = node1;
        }

        public T getNode2()
        {
            return node2;
        }

        public void setNode2(T node2)
        {
            this.node2 = node2;
        }

        public int getWeight()
        {
            return weight;
        }

        public void setWeight(int weight)
        {
            this.weight = weight;
        }

        public Edge(T node1, T node2, int weight)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.weight = weight;
        }

        public Edge()
        {

        }

        public bool equals(Object obj) {
		    if(!(obj is Edge<T>))
			    return false;
		    var e = (Edge<T>)obj;
		    if(e.node1.Equals(node1) && e.node2.Equals(node2) || e.node1.Equals(node2) && e.node2.Equals(node1))
			    return true;
		    return false;
	    }

        public String toString()
        {
            return node1 + " -(" + weight + ")- " + node2;
        }
    }
}
