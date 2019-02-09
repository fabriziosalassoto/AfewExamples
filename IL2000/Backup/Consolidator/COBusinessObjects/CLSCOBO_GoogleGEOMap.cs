using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace COBusinessObjects
{
    public static class CLSCOBO_GoogleGEOMap
    {
        public static List<CLSCOBO_BasePoint> GeoPoints(string ps_Address, string ps_Key)
        {
            XmlDocument vo_Xml = new XmlDocument();
            vo_Xml.LoadXml(GetXml(ps_Address, ps_Key));
            XmlNodeList vo_Response = vo_Xml.GetElementsByTagName("Response");
            XmlNodeList vo_Placemarks =((XmlElement)vo_Response[0]).GetElementsByTagName("Placemark");           
            List<CLSCOBO_BasePoint> vo_BasePoints = new List<CLSCOBO_BasePoint>();
            //int i = 0;

            foreach (XmlElement vo_Node in vo_Placemarks){
                try
                {
                    XmlNodeList vo_Point = vo_Node.GetElementsByTagName("Point");
                    XmlNodeList vo_Address = vo_Node.GetElementsByTagName("address");
                    CLSCOBO_BasePoint vo_BasePoint = new CLSCOBO_BasePoint();

                    vo_BasePoint.Longitude = Convert.ToDouble(vo_Point[0].InnerText.Split(new char[] { ',' })[0]);
                    vo_BasePoint.Latitude = Convert.ToDouble(vo_Point[0].InnerText.Split(new char[] { ',' })[1]);
                    vo_BasePoint.ZipCode = vo_Address[0].InnerText;
                    vo_BasePoints.Add(vo_BasePoint);
                }
                catch { }
            }
            return vo_BasePoints;
        }
               
        private static string GetXml(string address, string key)
        {            
            string url = string.Format("http://maps.google.com/maps/geo?output=xml&q={0}&key={1}", HttpUtility.UrlEncode(address), key);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }                  
        }                    
    }      
}
