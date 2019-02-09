using System;
using System.Text;
using System.Collections.Generic;
using BRBusinessObjects;
using BRLibrary;
using System.Web.UI;

namespace DFDataFlowControllers
{
    public class DFCls_LocationForm
    {
        #region Members
        BRCls_Location oLocation;
        BRCls_XMLReader oXMLReader;
        #endregion

        [PersistenceMode(PersistenceMode.InnerProperty)]
        #region Constructors
        public DFCls_LocationForm()
        {
            oXMLReader = new BRCls_XMLReader();
            oLocation = new BRCls_Location();            
        }

        public DFCls_LocationForm(String pStrID)
        {
            oXMLReader = new BRCls_XMLReader();
            oLocation = new BRCls_Location(pStrID);
        }

        public DFCls_LocationForm(byte[] pXML)
        {
            String mXML = Encoding.UTF8.GetString(pXML);
            oXMLReader = new BRCls_XMLReader();
            oLocation = new BRCls_Location();            
            oLocation.LoadLocationFromXML(mXML);
        }
        #endregion

        #region SearchMethods
        public String GetLocation(){
            String mStrXML = oXMLReader.GetXMLHeader();
            mStrXML += oLocation.XMLfy();
            return mStrXML;
        }
        #endregion

        #region ManipulationMethods
        public bool SaveLocation(){
            return oLocation.SaveLocation();
        }

        public bool DeleteLocation(){
            return oLocation.DeleteLocation();
        }
        #endregion
    }
}