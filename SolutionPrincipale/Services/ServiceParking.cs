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
using System.IO;

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

                var nbrData = (int)jsonObject["results"][0]["address_components"].Count();

                var route = "";
                var street_number = "";
                for (int i = 0; i < nbrData; i++)
                {
                    switch (jsonObject["results"][0]["address_components"][i]["types"][0].ToString())
                    {
                        
                        case "route":
                            route = (string)jsonObject["results"][0]["address_components"][i]["long_name"].ToString();
                            break;
                        case "street_number":
                            street_number = (string)jsonObject["results"][0]["address_components"][i]["long_name"].ToString();
                            break;
                        case "locality":
                            parking.Ville = (string)jsonObject["results"][0]["address_components"][i]["long_name"].ToString();
                            break;
                        case "postal_code":
                            parking.CodePostal = (string)jsonObject["results"][0]["address_components"][i]["long_name"].ToString();
                            break;
                    }
                }
                parking.Adresse = street_number + route;

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
            using (var stream = new MemoryStream())
            {
                var client = new WebClient();
                var test = client.DownloadString(@"http://data.citedia.com/r1/parks/timetable-and-prices");
                var bytes = System.Text.Encoding.Default.GetBytes(test);
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (TextFieldParser parser = new TextFieldParser(stream))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    List<Parking> liste = new List<Parking>();
                    int compteur = 0;
                    while (!parser.EndOfData)
                    {
                        //fields[0] = id, fields[1] = horaires, fields[2] = tarifs, fields[3] = adresse, fields[4] = capacite, fields[5] = seuil-complet,
                        string[] fields = parser.ReadFields();
                        if (compteur > 0)
                        {
                            Parking parking = listeParkings.FirstOrDefault(p => p.Nom == fields[0]);
                            parking.Horaires = fields[1];
                            parking.Tarifs = fields[2];
                        }
                        compteur++;
                    }
                }
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
