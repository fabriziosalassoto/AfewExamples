using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace COBusinessObjects
{
    public class CLSCOBO_BasePoint
    {
        private string ai_ZipCode;
        private double ai_Latitude;
        private double ai_Longitude;
        private List<CLSCOBO_BasePoint> ao_BasePoints;
        
        public CLSCOBO_BasePoint(){
            ai_ZipCode = string.Empty;
            ai_Latitude = 0;
            ai_Longitude = 0;            
        }

        public CLSCOBO_BasePoint(string ps_ZipCode)
        {
            ai_ZipCode = string.Empty;
            ai_Latitude = 0;
            ai_Longitude = 0;

            string vs_Address = ps_ZipCode+", "+CLSCOBO_FunctionsRepository.cs_CURRENT_COUNTRY;
            vs_Address.Replace(" ","+");
            ao_BasePoints=CLSCOBO_GoogleGEOMap.GeoPoints(vs_Address, "");
            if(ao_BasePoints.Count>0){
                foreach (CLSCOBO_BasePoint vo_BasePoint in ao_BasePoints)
                {
                    this.ZipCode = vo_BasePoint.ZipCode;
                    this.Longitude = vo_BasePoint.Longitude;
                    this.Latitude = vo_BasePoint.Latitude;
                    break; // for now.
                }
            }
        }

        public string ZipCode
        {
            set { ai_ZipCode = value; }
            get { return ai_ZipCode; }
        }

        public double Longitude
        {
            set { ai_Longitude = value; }
            get { return ai_Longitude; }
        }

        public double Latitude
        {
            set { ai_Latitude = value; }
            get { return ai_Latitude; }
        }

        public void fetchCoordinates(){}
    }
}
