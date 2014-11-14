using ChristoDomain.DomainObjects;
using ChristoDomain.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace ChristoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ChristoService : IChristoService
    {

        private readonly string GOOGLE_MAPS_REQUEST = @"https://maps.googleapis.com/maps/api/distancematrix/json";

        private IMinimalSpanningTree mst;
        public ChristoService(IMinimalSpanningTree mst)
        {
            if (mst == null)
            {
                throw new ArgumentNullException();
            }
            this.mst = mst;
        }

        public void SolveTSP(List<ChristoDomain.DomainObjects.Coordinate> coordinates, int[][] distanceMatrix)
        {
            var mst = GetMinimumSpanningTree(coordinates, distanceMatrix);
        }

        private HashSet<Edge<Coordinate>> GetMinimumSpanningTree(List<ChristoDomain.DomainObjects.Coordinate> coordinates, int[][] distanceMatrix)
        {
            var edges = new HashSet<Edge<Coordinate>>();

            for(var i = 0; i < distanceMatrix.Length) {
                for(var j = 0; j < distanceMatrix[i].Length; j++) {
                    edges.Add(new Edge<Coordinate>(coordinates.ElementAt(i), coordinates.ElementAt(j), distanceMatrix[i][j]));
                }
            }

            mst.SetEdges(edges, new EdgeComparer());
            mst.SetNodes(new HashSet<Coordinate>(coordinates));
            return mst.FindMinSpanTree();
        }

        private int[,] GetGoogleDistanceMatrix(List<Coordinate> coordinates)
        {
            var distanceMatrix = new int[coordinates.Count, coordinates.Count];

            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            
            string coords = "";
            foreach (var coordinate in coordinates)
            {
                coords += coordinate.Latitude.ToString(nfi) + "," + coordinate.Longitude.ToString(nfi) + "|";
            }

            var requestString = "?origins=" + coords + "&destinations=" + coords + "&mode=driving&language=en-US";

            //Pass request to google api with orgin and destination details
            //HttpWebRequest request =
            //    (HttpWebRequest)WebRequest.Create(GOOGLE_MAPS_REQUEST + requestString);
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(GOOGLE_MAPS_REQUEST);

            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("origins", coords);
            outgoingQueryString.Add("destinations", coords);
            outgoingQueryString.Add("mode", "driving");
            outgoingQueryString.Add("language", "en-US");
            string postData = outgoingQueryString.ToString();

            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] postBytes = ascii.GetBytes(postData.ToString());

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            // add post data to request
            Stream postStream = request.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Flush();
            postStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(result))
                {
                    var jo = JObject.Parse(result);
                    if (jo["status"].ToString() == "OK")
                    {
                        for (var i = 0; i < coordinates.Count; i++)
                        {
                            var results = jo["rows"][i]["elements"];
                            for (var j = 0; j < results.Count(); j++)
                            {
                                var element = results[j];
                                distanceMatrix[i,j] = int.Parse(element["distance"]["value"].ToString());
                            }
                        }
                    }
                }
            }
            return distanceMatrix;
        }


        public void Test()
        {
            var coordinates = new List<Coordinate>();

            coordinates.Add(new Coordinate(41.43206, -81.38992));
            coordinates.Add(new Coordinate(40.43206, -80.38992));

            GetGoogleDistanceMatrix(coordinates);
        }
    }
}
