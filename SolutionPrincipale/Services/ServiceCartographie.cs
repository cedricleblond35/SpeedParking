using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace SolutionPrincipale.Service
{
    public static class ServiceCartographie
    {

        private static string statut = null;
        private static JObject jsonObject = null;
        private static string jsonReception;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        internal static string selectDistanceParking(string origin, string destination, string mode)
        {
            string adresseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origin + "&destinations=" + destination + "&mode=" + mode + "&language=fr-FR&key=AIzaSyBLMmc9Fx12W79c3eJ0t7WV8e8cZgJ2irs";
            var client = new WebClient();
            jsonReception = client.DownloadString(adresseUrl);
            jsonObject = JObject.Parse(jsonReception);
            return (string)jsonObject["rows"][0]["elements"][0]["distance"]["value"];
        }

        /// <summary>
        /// Retourne l'objet avec la latitude et la longitude selon l'adresse se trouvant dans l'objet
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void geocoder<T>(T obj) where T : IAdresse
        {
            string adresse = obj.Adresse.Trim() + "," + obj.CodePostal.Trim() + "," + obj.Ville.Trim();
            string adresseEncodeUTF8 = HttpUtility.UrlEncode(adresse);

            string geocoder = "http://maps.googleapis.com/maps/api/geocode/json?address=" + adresseEncodeUTF8 + "&sensor=false";
            // Create a request using a URL that can receive a post. 
            var client = new WebClient();

            //Tant qu'on a pas le json, intérroger maps
            do
            {
                jsonReception = client.DownloadString(geocoder);
                jsonObject = JObject.Parse(jsonReception);
                statut = (string)jsonObject["status"];
            } while (statut != "OK");

            obj.Latitude = (double)jsonObject["results"][0]["geometry"]["location"]["lat"];
            obj.Longitude = (double)jsonObject["results"][0]["geometry"]["location"]["lng"];
        }
    }
}