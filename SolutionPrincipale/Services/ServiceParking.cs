using BO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SolutionPrincipale.Service
{
    public class ServiceParking
    {
        private static List<Parking> ListParkings;
        private static double longEven;
        private static double latitudeEven;

        /// <summary>
        /// Sélection les 3 parkings les plus proches ayant au moins 10 places de libres
        /// Indiqué un parcour uniquement à pied : transit_routing_preference=less_walking
        /// </summary>
        /// <param name="evenement">Objet événement</param>
        /// <param name="parkings">Liste des Parking</param>
        /// <param name="free">Nombre de places libre minimum</param>
        /// <returns></returns>
        internal static List<Parking> SelectNearestCarPark(Evenement evenement, List<Parking> parkings, int free)
        {
            var OrigineLatitude = evenement.Latitude.ToString().Replace(',', '.');
            var OrigineLongitude = evenement.Longitude.ToString().Replace(',', '.');

            foreach (Parking parking in parkings.Where(p => p.NbPlacesLibres > free - 1))
            {
                var DestinLatitude = parking.Latitude.ToString().Replace(',', '.');
                var DestinationLongitude = parking.Longitude.ToString().Replace(',', '.');
                var adressURL = "https://maps.googleapis.com/maps/api/directions/json?origin=" +
                    OrigineLatitude + "," + OrigineLongitude +
                    "&destination=" + DestinLatitude + "," + DestinationLongitude +
                    "&key=AIzaSyBLMmc9Fx12W79c3eJ0t7WV8e8cZgJ2irs&transit_routing_preference=less_walking";

                var client = new WebClient();
                var jsonObject = JObject.Parse(client.DownloadString(@adressURL));
                var distance = (int)jsonObject["routes"][0]["legs"][0]["distance"]["value"];

                parking.Distance = distance;
            }
            return parkings.OrderByDescending(p => p.Distance).Take(3).ToList();
        }

        /// <summary>
        /// Retourne l'oject avec les coordonnées grâce à sa latitude et longitude
        /// 
        /// </summary>
        /// <param name="parkings"></param>
        /// <returns></returns>
        internal static List<Parking> FindAdress(List<Parking> parkings)
        {
            foreach (Parking parking in parkings)
            {
                var adressURL = "https://maps.googleapis.com/maps/api/geocode/json?"
                    + "latlng=" + parking.Latitude.ToString().Replace(',', '.') + "," + parking.Longitude.ToString().Replace(',', '.')
                    + "&key=AIzaSyBLMmc9Fx12W79c3eJ0t7WV8e8cZgJ2irs";

                var client = new WebClient();
                var jsonObject = JObject.Parse(client.DownloadString(@adressURL));
                var street_number = jsonObject["results"][0]["address_components"][0]["long_name"].ToString();
                var route = jsonObject["results"][0]["address_components"][1]["long_name"].ToString();
                parking.Adresse = street_number + route;
                parking.Ville = jsonObject["results"][0]["address_components"][3]["long_name"].ToString();
                parking.CodePostal = jsonObject["results"][0]["address_components"][6]["long_name"].ToString();

            }
            return parkings;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="evenement"></param>
        /// <returns></returns>
        internal static List<Parking> GetListEventCarpark(Evenement evenement)
        {

            var ListParkings = GetListeParking();
            return ListParkings;
        }

        /// <summary>
        /// Retourne les coordonnées GPS (latitude, longitude) d'un parking
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static string EnvoiRequete(string info)
        {
            var client = new WebClient();
            return client.DownloadString(@"http://data.citedia.com/r1/parks?crs=EPSG:4326");
            /*
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
        */


        }

        /// <summary>
        /// Retourne une liste de tous les parkings
        /// </summary>
        /// <returns></returns>
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
