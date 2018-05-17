using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;
using System.Net;
using System.IO;
using System.Text;

namespace SolutionPrincipale.Service
{
    public static class Cartographie
    {
        public static string geocoderJson { get; private set; }

        public static void geocoder<T>(T obj) where T : IAdresse
        {
            string adresse = obj.Adresse + ", " + obj.CodePostal + ", " + obj.Ville;
            string adresseEncodeUTF8 = HttpUtility.UrlEncode(adresse);

            string geocoder = "http://maps.googleapis.com/maps/api/geocode/json?address="+ adresseEncodeUTF8 + "&sensor=false";
            // Create a request using a URL that can receive a post. 
            var client = new WebClient();
           
            //Tant qu'on a pas le json, intérroger maps
            do
            {
                geocoderJson = client.DownloadString(geocoder);
            } while (geocoderJson == null);
        }
    }
}