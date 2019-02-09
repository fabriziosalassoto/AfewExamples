using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COBusinessObjects;
// New references for encapsulation
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using System.Web.SessionState;
using System.ComponentModel;
using Artem.Web.UI.Controls;
using System.Threading;

namespace COWebDataFlow
{
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class CLSCODF_Consolidation{        
        
        private const double c_MAXIMUM_LONGITUDE = -124.716797;        
        private const double c_MINIMUM_LONGITUDE = -66.621094;        
        private const double c_MAXIMUM_LATITUDE = 49.05227;        
        private const double c_MINIMUM_LATITUDE = 24.367114;
   
        private static Page ao_WebPage;
        private GoogleMap ao_GoogleMap;
        private static CLSCOBO_ConsolidationProblem ao_ConsolidationProblem;  

        public CLSCODF_Consolidation(Page po_WebPage){
            ao_WebPage = po_WebPage;            
        }        

        private void LoadFileZips(string ps_FileName){
            TextBox vo_TextBox=(TextBox)CLSCOBO_FunctionsRepository.getElement("TxtFileName", ao_WebPage);            
            if(ao_GoogleMap==null)
                ao_GoogleMap=(GoogleMap)CLSCOBO_FunctionsRepository.getElement("GoogleMap1", ao_WebPage);
            ao_GoogleMap.Markers.Clear();
            ao_GoogleMap.Polylines.Clear();
            ao_ConsolidationProblem=null;            
            ao_ConsolidationProblem=CLSCOBO_FunctionsRepository.ReadFile(ref ps_FileName);
            vo_TextBox.Text=ps_FileName;
            FillInGraphicData();            
        }

        public void FillInDemoData(){            
            LoadFileZips("");            
        }        

        public void SendFile(){
            FileUpload vo_FileUpload = (FileUpload)CLSCOBO_FunctionsRepository.getElement("FileUpload2", ao_WebPage);
            TextBox vo_TextBox = (TextBox)CLSCOBO_FunctionsRepository.getElement("TxtFileName", ao_WebPage);

            if (CLSCOBO_FunctionsRepository.UploadFile(vo_FileUpload.FileBytes,vo_FileUpload.FileName)){
                LoadFileZips(vo_FileUpload.FileName);
                vo_TextBox.Text = "File Uploaded: " + vo_FileUpload.FileName;
            }else
                vo_TextBox.Text = "No File Uploaded.";            
        }        

        private void FillInGraphicData(){
            List<CLSCOBO_BIND_DestinationZipCode> vl_ListDestinationZipCodes = new List<CLSCOBO_BIND_DestinationZipCode>();
            CLSCOBO_BIND_DestinationZipCode vo_BIND_DestinationZipCode;
            ListView vo_DestinationZipCodes = (ListView)CLSCOBO_FunctionsRepository.getElement("LstViewDestinations",ao_WebPage);
            TextBox vo_OriginZipCode = (TextBox)CLSCOBO_FunctionsRepository.getElement("TxtOriginPoint",ao_WebPage);

            if (ao_ConsolidationProblem != null){
                vo_OriginZipCode.Text = ao_ConsolidationProblem.OriginPoint.ZipCode;
                foreach (CLSCOBO_DeliveryPoint vo_DeliveryPoint in ao_ConsolidationProblem.Deliveries){
                    vo_BIND_DestinationZipCode = new CLSCOBO_BIND_DestinationZipCode();
                    vo_BIND_DestinationZipCode.ZipCode = vo_DeliveryPoint.ZipCode;
                    vo_BIND_DestinationZipCode.Weight = vo_DeliveryPoint.Weight.ToString();
                    vl_ListDestinationZipCodes.Add(vo_BIND_DestinationZipCode);
                }
                vo_DestinationZipCodes.DataSource = vl_ListDestinationZipCodes;
                vo_DestinationZipCodes.DataBind();
                DrawPoints();
            }else
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());            
        }     

        private void DrawPoints(){
            if(ao_GoogleMap==null)
                ao_GoogleMap=(GoogleMap)CLSCOBO_FunctionsRepository.getElement("GoogleMap1", ao_WebPage);
            GoogleMarker vo_GoogleMarker;

            vo_GoogleMarker=new GoogleMarker(ao_ConsolidationProblem.OriginPoint.Latitude, ao_ConsolidationProblem.OriginPoint.Longitude);
            vo_GoogleMarker.Text=CLSCOBO_FunctionsRepository.CreateTag(ao_ConsolidationProblem.OriginPoint);            
            vo_GoogleMarker.Title=ao_ConsolidationProblem.OriginPoint.ZipCode;
            ao_GoogleMap.Markers.Add(vo_GoogleMarker);
            
            foreach (CLSCOBO_DeliveryPoint po_DeliveryPoint in ao_ConsolidationProblem.Deliveries){                           
                vo_GoogleMarker = new GoogleMarker(po_DeliveryPoint.Latitude,po_DeliveryPoint.Longitude);
                vo_GoogleMarker.Text=CLSCOBO_FunctionsRepository.CreateTag(po_DeliveryPoint);
                vo_GoogleMarker.Title=po_DeliveryPoint.ZipCode;
                ao_GoogleMap.Markers.Add(vo_GoogleMarker);                
            }                                   
        }

        public void CalculateConsolidationSolution(){ 
            CLSCOBO_ConsolidationEngine ao_ConsolidationEngine;
            CLSCOBO_OriginPoint vo_OriginPoint=null;
            GooglePolyline vo_PolyLine=null;                  
            GoogleLocation vo_GoogleLocation;
            Color vo_Color=Color.Black;
            int pi_Quadrant=0;            
            
            int x = 0;
            if(ao_GoogleMap==null)
                ao_GoogleMap=(GoogleMap)CLSCOBO_FunctionsRepository.getElement("GoogleMap1", ao_WebPage);

            if (ao_ConsolidationProblem != null){
                ao_ConsolidationEngine = new CLSCOBO_ConsolidationEngine(ao_ConsolidationProblem);               
                foreach(CLSCOBO_ConsolidationSolution vo_ConsolidationSolutionPointer in ao_ConsolidationEngine.ConsolidationSolutions) {                    
                //CLSCOBO_ConsolidationSolution vo_ConsolidationSolutionPointer=ao_ConsolidationEngine.ConsolidationSolutions[ao_ConsolidationEngine.ConsolidationSolutions.Count-1];
                    ao_GoogleMap.Polylines.Clear();
                    pi_Quadrant=0;                    
                    foreach(CLSCOBO_TruckItinerary vo_TruckItineraryPointer in vo_ConsolidationSolutionPointer.TruckItineraries) {
                        vo_PolyLine=new GooglePolyline();
                        vo_PolyLine.Color=(pi_Quadrant==1)?Color.Blue:(pi_Quadrant==2)?Color.Red:(pi_Quadrant==3)?Color.Green:Color.Gold;
                       
                        vo_OriginPoint=ao_ConsolidationEngine.ConsolidationSolutions[x].TruckItineraries[0].OriginPoint;
                        vo_GoogleLocation=new GoogleLocation(vo_OriginPoint.Latitude, vo_OriginPoint.Longitude);                        
                        vo_PolyLine.Points.Add(vo_GoogleLocation);
                        foreach(CLSCOBO_DeliveryPoint vo_DeliveryPointPointer in vo_TruckItineraryPointer.Deliveries) {                            
                            vo_GoogleLocation=new GoogleLocation(vo_DeliveryPointPointer.Latitude, vo_DeliveryPointPointer.Longitude);
                            if((vo_GoogleLocation.Latitude!=0) && (vo_GoogleLocation.Longitude!=0)) {
                                vo_PolyLine.Points.Add(vo_GoogleLocation);
                            }                            
                        }
                        pi_Quadrant++;
                        ao_GoogleMap.Polylines.Add(vo_PolyLine);
                        System.Threading.Thread.Sleep(50);                     
                    }                    
                                       
                }                
            }
        }        
    }    

    public class CLSCOBO_BIND_DestinationZipCode{
        private string as_ZipCode;
        private string as_Weight;

        public CLSCOBO_BIND_DestinationZipCode(){
            as_ZipCode = "";
            as_Weight = "";
        }

        public string ZipCode{
            set { as_ZipCode = value; }
            get { return as_ZipCode; }
        }

        public string Weight{
            set { as_Weight = value; }
            get { return as_Weight; }
        }
    }    
}