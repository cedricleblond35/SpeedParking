using BO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System;

namespace SolutionPrincipale.Service
{
    public class ServiceParking
    {
        private static List<Parking> parkingsJson;
        private static double longEven;
        private static double latitudeEven;

        internal static List<Parking> GetListEventCarpark(Evenement evenement)
        {
            latitudeEven = evenement.Latitude;
            longEven = evenement.Longitude;
            parkingsJson = GetListeParking();


            throw new NotImplementedException();
        }


        private static string EnvoiRequete(string info)
        {
            var client = new WebClient();
            var parkingsJson = "";
            switch (info)
            {
                case "parks":
                    parkingsJson = client.DownloadString(@"http://data.citedia.com/r1/parks?crs=EPSG:4326");
                    break;
                case "tarifs":
                    parkingsJson = client.DownloadString(@"http://data.citedia.com/r1/parks/timetable-and-prices");
                    break;

            }

            return parkingsJson;
        }
        public static List<Parking> GetListeParking()
        {
            //JSON
            var result = JsonConvert.DeserializeObject<RootObject>(EnvoiRequete("parks"));
            var parkings = result.parks;
            var features = result.features;
            List<Parking> listeParkings = new List<Parking>();
            foreach(var i in parkings)
            {
                listeParkings.Add(new Parking(i.id, i.parkInformation.name, i.parkInformation.status, i.parkInformation.max, i.parkInformation.free));
            }
            foreach (var n in features.features)
            {
                foreach (var j in listeParkings)
                {
                    if(j.Id == n.id)
                    {
                        j.GeometrieType = n.geometry.type;
                        j.Longitude = n.geometry.coordinates[0];
                        j.Latitude = n.geometry.coordinates[1];

                        j.CrsType = n.crs.type;
                        j.CrsNom = n.crs.properties.name;
                    }
                }
            }
            //CSV
            /*using (TextFieldParser parser = new TextFieldParser(@"C:\Users\clebellec2016\Downloads\timetable-and-prices"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        foreach (var j in listeParkings)
                        {
                            if (field == "Parking" && j.Id == field)
                            {
                                j.GeometrieType = n.geometry.type;
                                j.Coordonnees = n.geometry.coordinates;
                                j.CrsType = n.crs.type;
                                j.CrsNom = n.crs.properties.name;
                            }
                        }
                    }
                }
            }*/
            return listeParkings;
        }

        
    }

    public class ParkInformation
    {
        public string name { get; set; }
        public string status { get; set; }
        public int max { get; set; }
        public int free { get; set; }
    }

    public class Park
    {
        public ParkInformation parkInformation { get; set; }
        public string id { get; set; }
    }

    public class Properties
    {
        public string name { get; set; }
    }

    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Crs crs { get; set; }
        public Geometry geometry { get; set; }
        public string id { get; set; }
    }

    public class Features
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }

    public class RootObject
    {
        public List<Park> parks { get; set; }
        public Features features { get; set; }
    }
}
