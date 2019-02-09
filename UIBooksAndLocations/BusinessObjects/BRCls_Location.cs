using System;
using System.Collections.Generic;
using DBControllers;
using BRLibrary;

namespace BRBusinessObjects
{
    #region enumerations
    public enum LocationStatus
    {
        New = 0,
        Modified = 1,
        NotModified = 2,
        MarkedForDeletion = 3
    }
    #endregion

    public class BRCls_Location
    {
        #region PrivateProperties

        private String LocationID;
        private String LocationName;
        private String LocationDescription;
        private String LocationAddress;
        private String LocationLatitude;
        private String LocationLongitude;
        private DBCls_Locations oDBLocationController;
        private int ObjectStatus;

        #endregion

        #region PublicProperties
        public String GetID
        {
            get { return LocationID; }
        }

        public String GetLocationName
        {
            get { return LocationName; }
        }

        public String SetLocationName
        {
            set
            {
                ObjectStatus = (int)LocationStatus.Modified;
                LocationName = value;
            }
        }

        public String GetLocationDescription
        {
            get { return LocationDescription; }
        }

        public String SetLocationDescription
        {
            set
            {
                ObjectStatus = (int)LocationStatus.Modified;
                LocationDescription = value;
            }
        }

        public String GetLocationAddress
        {
            get { return LocationAddress; }
        }

        public String SetLocationAddress
        {
            set
            {
                ObjectStatus = (int)LocationStatus.Modified;
                LocationAddress = value;
            }
        }

        public String GetLocationLatitude
        {
            get { return LocationLatitude; }
        }

        public String SetLocationLatitude
        {
            set
            {
                ObjectStatus = (int)LocationStatus.Modified;
                LocationLatitude = value;
            }
        }

        public String GetLocationLongitude
        {
            get { return LocationLongitude; }
        }

        public String SetLocationLongitude
        {
            set
            {
                ObjectStatus = (int)LocationStatus.Modified;
                LocationLongitude = value;
            }
        }

        public int GetStatus
        {
            get { return ObjectStatus; }
        }
        #endregion

        #region Constructors
        public BRCls_Location()
        {
            oDBLocationController = new DBCls_Locations();
            LocationID = "";

            ObjectStatus = (int)LocationStatus.New;
        }

        public BRCls_Location(String pLocationID)
        {
            oDBLocationController = new DBCls_Locations();
            GetLocationByID(pLocationID);
            ObjectStatus = (int)LocationStatus.NotModified;
        }

        public BRCls_Location(String[] ArrLocation)
        {
            oDBLocationController = new DBCls_Locations();
            FillInLocation(ArrLocation);
            ObjectStatus = (int)LocationStatus.NotModified;
        }
        #endregion

        #region SearchMethods
        public bool GetLocationByID(String pLocationID)
        {
            try
            {
                oDBLocationController = new DBCls_Locations();
                String[,] ArrLocation = oDBLocationController.SearchLocation(pLocationID);
                if (ArrLocation != null)
                {
                    LocationID = ArrLocation[0, (int)LocationCriteria.cLOCATIONID];
                    LocationName = ArrLocation[0, (int)LocationCriteria.cLOCATIONNAME];
                    LocationDescription = ArrLocation[0, (int)LocationCriteria.cLOCATIONDESCRIPTION];
                    LocationAddress = ArrLocation[0, (int)LocationCriteria.cLOCATIONADDRESS];
                    LocationLatitude = ArrLocation[0, (int)LocationCriteria.cLOCATIONLATITUDE];
                    LocationLongitude = ArrLocation[0, (int)LocationCriteria.cLOCATIONLONGITUDE];
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region LocationManipulation
        public bool LoadLocationFromXML(String pXML)
        {
            try
            {
                oDBLocationController = new DBCls_Locations();
                FillLocationWithXML(pXML);
                ValidateStatus();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool SaveLocation()
        {
            try
            {
                String[] ArrLocation = FillInArrayWithProperties();
                if (LocationID.Trim() == "")
                {
                    ObjectStatus = (int)LocationStatus.New;
                }

                switch (ObjectStatus)
                {
                    case (int)LocationStatus.New: 
                        LocationID = Convert.ToString(oDBLocationController.InsertLocation(ArrLocation));
                        if (LocationID == "-1"){
                            return false;
                        }
                        break;
                    case (int)LocationStatus.Modified: oDBLocationController.UpdateLocation(ArrLocation);
                        break;
                    case (int)LocationStatus.MarkedForDeletion: oDBLocationController.EliminateLocation(LocationCriteria.cLOCATIONID, LocationID);
                        break;
                }
                ObjectStatus = (int)LocationStatus.NotModified;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ValidateStatus(){
            switch (this.LocationID){
                case "": this.ObjectStatus = (int)LocationStatus.New;
                    break;
                case "-1": this.ObjectStatus = (int)LocationStatus.NotModified;
                    break;
                default:
                    this.ObjectStatus = (int)LocationStatus.Modified;
                    break;
            }
        }
        
        public bool DeleteLocation()
        {
            try
            {
                ObjectStatus = (int)LocationStatus.MarkedForDeletion;
                SaveLocation();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public String XMLfy()
        {
            String mStrXML = "<location>";
            mStrXML += "<locationid>" + LocationID + "</locationid>";
            mStrXML += "<locationname>" + LocationName + "</locationname>";
            mStrXML += "<locationdescription>" + LocationDescription + "</locationdescription>";
            mStrXML += "<locationaddress>" + LocationAddress + "</locationaddress>";
            mStrXML += "<locationlatitude>" + LocationLatitude + "</locationlatitude>";
            mStrXML += "<locationlongitude>" + LocationLongitude + "</locationlongitude>";
            mStrXML += "</location>";
            return mStrXML;
        }
        #endregion

        #region Fill_IN_OUT_PROPERTIES
        private String[] FillInArrayWithProperties()
        {
            String[] mArrProperties = new String[(int)TotalLocationCriteria.cTotal];
            mArrProperties[(int)LocationCriteria.cLOCATIONID] = LocationID;
            mArrProperties[(int)LocationCriteria.cLOCATIONNAME] = LocationName;
            mArrProperties[(int)LocationCriteria.cLOCATIONDESCRIPTION] = LocationDescription;
            mArrProperties[(int)LocationCriteria.cLOCATIONADDRESS] = LocationAddress;
            mArrProperties[(int)LocationCriteria.cLOCATIONLATITUDE] = LocationLatitude;
            mArrProperties[(int)LocationCriteria.cLOCATIONLONGITUDE] = LocationLongitude;
            return mArrProperties;
        }

        private void FillLocationWithXML(String pXML)
        {
            BRCls_XMLReader oXMLReader = new BRCls_XMLReader(pXML);
            oXMLReader.LookForParents("location");
            this.LocationID = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationid"));
            this.LocationName = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationname"));
            this.LocationDescription = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationdescription"));
            this.LocationAddress = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationaddress"));            
            this.LocationLatitude = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationlatitude"));
            this.LocationLongitude = oDBLocationController.EscapeValue(oXMLReader.LookForChildren("locationlongitude"));
        }

        private void FillInLocation(String[] pLocation)
        {
            LocationID = pLocation[(int)LocationCriteria.cLOCATIONID];
            LocationName = pLocation[(int)LocationCriteria.cLOCATIONNAME];
            LocationDescription = pLocation[(int)LocationCriteria.cLOCATIONDESCRIPTION];
            LocationAddress = pLocation[(int)LocationCriteria.cLOCATIONADDRESS];
            LocationDescription = pLocation[(int)LocationCriteria.cLOCATIONDESCRIPTION];
            LocationLatitude = pLocation[(int)LocationCriteria.cLOCATIONLATITUDE];
            LocationLongitude = pLocation[(int)LocationCriteria.cLOCATIONLONGITUDE];
        }
        #endregion
    }
}