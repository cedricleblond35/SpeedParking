using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SolutionPrincipale.Service
{
    public static class ServiceCartographie
    {
        private static string geocoderJson = null;
        private static string statut = null;
        private static JObject jsonGeo = null;

        public static void geocoder<T>(T obj) where T : IAdresse
        {
            string adresse = obj.Adresse + ", " + obj.CodePostal + ", " + obj.Ville;
            string adresseEncodeUTF8 = HttpUtility.UrlEncode(adresse);

            string geocoder = "http://maps.googleapis.com/maps/api/geocode/json?address=" + adresseEncodeUTF8 + "&sensor=false";
            // Create a request using a URL that can receive a post. 
            var client = new WebClient();

            //Tant qu'on a pas le json, intérroger maps
            do
            {
                geocoderJson = client.DownloadString(geocoder);
                jsonGeo = JObject.Parse(geocoderJson);
                statut = (string)jsonGeo["status"];
            } while (statut != "OK");

            obj.Latitude = (string)jsonGeo["results"]["geometry"]["location"]["lat"];
            obj.Longitude = (string)jsonGeo["results"]["geometry"]["location"]["lng"];
        }
    }
}