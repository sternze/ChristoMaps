using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ChristoDomain.DomainObjects;

namespace ChristoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IChristoService
    {
        [OperationContract]
        void SolveTSP(List<Coordinate> coordinates, int[][] distanceMatrix);

        [OperationContract]
        void Test();
    }
}
