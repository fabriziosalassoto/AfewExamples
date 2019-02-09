using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;

namespace COBusinessObjects {
    public static class CLSCOBO_FunctionsRepository {
        private const string cs_DEMO_DEFAULT_FILE="demogenerated.txt";
        public const string cs_CURRENT_COUNTRY="United States";        

        public static object getElement(string ps_ControlName, Page po_WebPage) {
            object vo_Element=null;
            int i=1;
            while(i<po_WebPage.Controls.Count&&vo_Element==null) {
                vo_Element=po_WebPage.Controls[i].FindControl(ps_ControlName);
                i++;
            }
            return vo_Element;
        }

        public static double getDistance(double pd_LongOrigin, double pd_LatOrigin, double pd_LongDestiny, double pd_LatDestiny, String pc_unit) {
            int vi_EarthRadius=6371;  // Kilometers
            double vd_KilometersToMilesAdjuster=0.621371192237334;  // Miles in a Kilometer
            double vd_Distance=0;
            double vd_LatitudeDelta=ConvertDegreesToRadians(pd_LatDestiny-pd_LatOrigin);
            double vd_LongitudeDelta=ConvertDegreesToRadians(pd_LongDestiny-pd_LongOrigin);
            double vd_lat1=ConvertDegreesToRadians(pd_LatOrigin);
            double vd_lat2=ConvertDegreesToRadians(pd_LatDestiny);

            double a=Math.Sin(vd_LatitudeDelta/2)*Math.Sin(vd_LatitudeDelta/2)+
                       Math.Sin(vd_LongitudeDelta/2)*Math.Sin(vd_LongitudeDelta/2)*Math.Cos(vd_lat1)*Math.Cos(vd_lat2);
            double c=2*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            vd_Distance=vi_EarthRadius*c;
            if(pc_unit.ToUpper()=="M")
                vd_Distance=vd_Distance*vd_KilometersToMilesAdjuster;
            return (vd_Distance);
        }        

        public static string CreateTag(CLSCOBO_BasePoint po_BasePoint) {
            string vs_Tag="";
            vs_Tag="Zip Code: "+po_BasePoint.ZipCode+Environment.NewLine+
                   "Latitude: "+po_BasePoint.Latitude.ToString()+Environment.NewLine+
                   "Longitude: "+po_BasePoint.Longitude.ToString();
            return vs_Tag;
        }

        ////Represents X-Axis
        //public static int ConvertLongitudeToPixelX(double pd_Longitude, double pd_LongitudeMaxCountry, double pd_LongitudeMinCountry, int pi_ScreenWidth, int pi_AdjustmentCoeficient){            
        //    int vi_PixelX = 0;
        //    vi_PixelX = (int)Convert.ToInt32((pi_ScreenWidth * ((pd_LongitudeMaxCountry - pd_Longitude) / (pd_LongitudeMaxCountry - pd_LongitudeMinCountry))));            
        //    vi_PixelX=(vi_PixelX < 0) ? vi_PixelX * -1 : vi_PixelX;
        //    vi_PixelX += pi_AdjustmentCoeficient;
        //    return(vi_PixelX);
        //}


        ////Represents Y-Axis
        //public static int ConvertLatitudeToPixelY(double pd_Latitude, double pd_LatitudeMaxCountry, double pd_LatitudeMinCountry, int pi_ScreenHeight, int pi_AdjustmentCoeficient)
        //{
        //    int vi_PixelY = 0;
        //    vi_PixelY = (int)Convert.ToInt32((pi_ScreenHeight * ((pd_LatitudeMaxCountry - pd_Latitude) / (pd_LatitudeMaxCountry - pd_LatitudeMinCountry))));
        //    vi_PixelY = (vi_PixelY < 0) ? vi_PixelY * -1 : vi_PixelY;
        //    vi_PixelY += pi_AdjustmentCoeficient;
        //    return (vi_PixelY);
        //}            

        private static double ConvertDegreesToRadians(double pd_Degrees) {
            return (pd_Degrees*Math.PI/180.0);
        }

        private static double ConvertRadiansToDegrees(double pd_Radians) {
            return (pd_Radians/Math.PI*180.0);
        }

        public static CLSCOBO_ConsolidationProblem ReadFile(ref String ps_FileName) {
            string vs_Line;
            string vi_OriginDestinationIndicator="";
            TextReader vs_TextReader;
            CLSCOBO_DeliveryPoint vo_DeliveryPoint;
            CLSCOBO_OriginPoint vo_OriginPoint;
            CLSCOBO_ConsolidationProblem vo_ConsolidationProblem;

            try {
                if(string.IsNullOrEmpty(ps_FileName)) {
                    vo_ConsolidationProblem=ReadDemoData(ref ps_FileName);
                } else {
                    vo_ConsolidationProblem=new CLSCOBO_ConsolidationProblem();
                    ps_FileName=System.AppDomain.CurrentDomain.BaseDirectory+(string.IsNullOrEmpty(ps_FileName)?cs_DEMO_DEFAULT_FILE:ps_FileName);
                    vs_TextReader=new StreamReader(ps_FileName);

                    while((vs_Line=vs_TextReader.ReadLine())!=null) {
                        vi_OriginDestinationIndicator=vs_Line.Substring(0, vs_Line.IndexOf(","));
                        vs_Line=vs_Line.Remove(0, vs_Line.IndexOf(",")+1);
                        if(vi_OriginDestinationIndicator.ToUpper()=="O") {
                            vo_OriginPoint=new CLSCOBO_OriginPoint(vs_Line.Substring(0, vs_Line.IndexOf(",")));
                            vo_ConsolidationProblem.OriginPoint=vo_OriginPoint;
                        } else {
                            vo_DeliveryPoint=new CLSCOBO_DeliveryPoint(vs_Line.Substring(0, vs_Line.IndexOf(",")));
                            vo_DeliveryPoint.Weight=double.Parse(vs_Line.Remove(0, vs_Line.IndexOf(",")+1));
                            vo_ConsolidationProblem.Deliveries.Add(vo_DeliveryPoint);
                        }
                    }
                    vs_TextReader.Close();
                }
            } catch {
                vo_ConsolidationProblem=null;
            }
            return vo_ConsolidationProblem;
        }

        public static CLSCOBO_ConsolidationProblem ReadDemoData(ref string ps_FileName){            
            string vs_Line;
            string vs_FileName;
            string vs_State="";
            string vs_FirstZip="";
            string vs_SecondZip="";
            string vs_SelectedZip="";
            double vd_Weight=0;
            int vi_RandomPoint=-1;
            int vi_ChoosenAsOriginPoint=-1;
            int vi_RandomChoosenPoint=-1;
            int vi_RandomZipSelected=-1;

            TextReader vs_TextReader;
            List<CLSCOBO_DeliveryPoint> vl_DeliveryPoints=null;
            CLSCOBO_DeliveryPoint vo_TemporaryPoint;

            CLSCOBO_DeliveryPoint vo_DeliveryPoint;
            CLSCOBO_OriginPoint vo_OriginPoint;
            CLSCOBO_ConsolidationProblem vo_ConsolidationProblem;

            try {                                
                vs_FileName=System.AppDomain.CurrentDomain.BaseDirectory+cs_DEMO_DEFAULT_FILE;
                ps_FileName=cs_DEMO_DEFAULT_FILE;
                vs_TextReader=new StreamReader(vs_FileName);
                vl_DeliveryPoints=new List<CLSCOBO_DeliveryPoint>();            
                while((vs_Line=vs_TextReader.ReadLine())!=null) {
                    vs_SelectedZip="";
                    vs_State=vs_Line.Substring(0, vs_Line.IndexOf(","));
                    vs_Line=vs_Line.Remove(0, vs_Line.IndexOf(",")+1);
                    vs_FirstZip=vs_Line.Substring(0, vs_Line.IndexOf(","));
                    vs_Line=vs_Line.Remove(0, vs_Line.IndexOf(",")+1);
                    vs_SecondZip=vs_Line.Substring(0, vs_Line.IndexOf(","));
                    vs_Line=vs_Line.Remove(0, vs_Line.IndexOf(",")+1);
                    vd_Weight=double.Parse(vs_Line.Remove(0, vs_Line.IndexOf(",")+1));
                    vi_RandomZipSelected=GenerateRandomNumber(1, 2);
                    vs_SelectedZip=(vi_RandomZipSelected==1)?(string.IsNullOrEmpty(vs_FirstZip)?vs_SecondZip:vs_FirstZip):(string.IsNullOrEmpty(vs_SecondZip)?vs_FirstZip:vs_SecondZip);
                    vs_SelectedZip=vs_State+" "+vs_SelectedZip;
                    vo_TemporaryPoint=new CLSCOBO_DeliveryPoint(vs_SelectedZip);
                    vo_TemporaryPoint.Weight=vd_Weight;
                    vl_DeliveryPoints.Add(vo_TemporaryPoint);
                }                
                vs_TextReader.Close();
                vi_RandomPoint=GenerateRandomNumber(2, vl_DeliveryPoints.Count-1);
                vi_ChoosenAsOriginPoint=GenerateRandomNumber(0, vi_RandomPoint);
                vo_ConsolidationProblem=new CLSCOBO_ConsolidationProblem();
                vo_OriginPoint=new CLSCOBO_OriginPoint(vl_DeliveryPoints[vi_ChoosenAsOriginPoint].ZipCode);
                vo_ConsolidationProblem.OriginPoint=vo_OriginPoint;
                vl_DeliveryPoints.RemoveAt(vi_ChoosenAsOriginPoint);
                vi_RandomPoint--;
                for(int x=1;x<=vi_RandomPoint;x++) {
                    vi_RandomChoosenPoint=GenerateRandomNumber(0, vl_DeliveryPoints.Count-1);                                        
                    vo_DeliveryPoint=vl_DeliveryPoints[vi_RandomChoosenPoint];
                    vo_ConsolidationProblem.AddDeliveryPoint(vo_DeliveryPoint);                    
                    vl_DeliveryPoints.RemoveAt(vi_RandomChoosenPoint);
                }
            } catch {
                vo_ConsolidationProblem=null;
            }
            return vo_ConsolidationProblem;
        }

        public static bool UploadFile(byte[] po_FileContentBytes, string ps_FileName) {
            if(!string.IsNullOrEmpty(ps_FileName)) {
                try {
                    FileStream vo_FileStream=new FileStream(@System.AppDomain.CurrentDomain.BaseDirectory+ps_FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    vo_FileStream.Write(po_FileContentBytes, 0, po_FileContentBytes.Count());
                    vo_FileStream.Close();
                    vo_FileStream.Dispose();
                    return true;
                } catch {
                    return false;
                }
            } else
                return false;
        }

        public static int GenerateRandomNumber(int pi_MinimumMargin, int pi_MaximumMargin) {
            Random vr_Random=new Random();
            return vr_Random.Next(pi_MinimumMargin, pi_MaximumMargin);
        }

        public static int getPointerOfMaximumDeliveryPointsInTruckItineraries(List<CLSCOBO_TruckItinerary> pl_TruckItineraries) {
            int vi_CurrentMaximumDeliveryPointsPointer=0;
            int vi_CurrentMaximumDeliveryPoints=0;
            int vi_TemporaryMaximumDeliveryPointsPointer=0;
            int vi_TemporaryMaximumDeliveryPoints=0;

            foreach(CLSCOBO_TruckItinerary vo_PointerTruckItinerary in pl_TruckItineraries) {
                vi_TemporaryMaximumDeliveryPoints=vo_PointerTruckItinerary.TotalDestinationsCount;
                if(vi_TemporaryMaximumDeliveryPoints>vi_CurrentMaximumDeliveryPoints) {
                    vi_CurrentMaximumDeliveryPoints=vi_TemporaryMaximumDeliveryPoints;
                    vi_CurrentMaximumDeliveryPointsPointer=vi_TemporaryMaximumDeliveryPointsPointer;
                }
                vi_TemporaryMaximumDeliveryPointsPointer++;
            }
            return vi_CurrentMaximumDeliveryPointsPointer;
        }

        public static int CalculateCuadrant(CLSCOBO_BasePoint po_FromPoint, CLSCOBO_BasePoint po_ToPoint) {
            int vi_Quadrant=0;
            if(po_ToPoint.Latitude>po_FromPoint.Latitude&&
                       po_ToPoint.Longitude<po_FromPoint.Longitude) {
                vi_Quadrant=1;
            }else if(po_ToPoint.Latitude>po_FromPoint.Latitude&&
                       po_ToPoint.Longitude>po_FromPoint.Longitude) {
                vi_Quadrant=2;
            }else if(po_ToPoint.Latitude<po_FromPoint.Latitude&&
                       po_ToPoint.Longitude<po_FromPoint.Longitude) {
                vi_Quadrant=3;
            }else if(po_ToPoint.Latitude<po_FromPoint.Latitude&&
                       po_ToPoint.Longitude>po_FromPoint.Longitude) {
                vi_Quadrant=4;
            }
            return vi_Quadrant;
        }
    }
}