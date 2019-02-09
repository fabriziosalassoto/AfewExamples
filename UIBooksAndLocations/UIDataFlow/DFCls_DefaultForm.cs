using System;
using System.Collections.Generic;
using BRBusinessObjects;
using BRLibrary;

namespace DFDataFlowControllers
{
    public class DFCls_DefaultForm
    {
        BRCls_Books oListBooks;
        BRCls_Locations oListLocations;
        BRCls_XMLReader oXMLReader;

        public DFCls_DefaultForm()
        {
            oXMLReader = new BRCls_XMLReader();
            oListBooks = new BRCls_Books();
            oListLocations = new BRCls_Locations();
            oListBooks.GetBooks();
            oListLocations.GetLocations();            
        }

        public DFCls_DefaultForm(String pXML)
        {
            oXMLReader = new BRCls_XMLReader();
            oListBooks = new BRCls_Books();
            oListLocations = new BRCls_Locations();                        
        }

        public String GetBooksAndLocations()
        {
            string mStrJSON = "";
            mStrJSON += oListBooks.JSONfy();
            mStrJSON += oListLocations.JSONfy();
            return mStrJSON;
        }

    }
}