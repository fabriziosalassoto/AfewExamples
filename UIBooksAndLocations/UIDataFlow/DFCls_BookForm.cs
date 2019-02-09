using System;
using System.Text;
using System.Collections.Generic;
using BRBusinessObjects;
using BRLibrary;
using System.Web.UI;

namespace DFDataFlowControllers
{
    public class DFCls_BookForm
    {
        #region Members
        BRCls_Book oBook;        
        BRCls_XMLReader oXMLReader;
        #endregion

        [PersistenceMode(PersistenceMode.InnerProperty)]
        #region Constructors
        public DFCls_BookForm()
        {
            oXMLReader = new BRCls_XMLReader();
            oBook = new BRCls_Book();                        
        }

        public DFCls_BookForm(String pStrID)
        {
            oXMLReader = new BRCls_XMLReader();
            oBook = new BRCls_Book(pStrID);
        }

        public DFCls_BookForm(byte[] pXML)
        {            
            String mXML = Encoding.UTF8.GetString(pXML);
            oXMLReader = new BRCls_XMLReader();
            oBook = new BRCls_Book();
            oBook.LoadBookFromXML(mXML);
        }
        #endregion

        #region SearchMethods
        public String GetBook(){
            String mStrXML = oXMLReader.GetXMLHeader();
            mStrXML += oBook.XMLfy();            
            return mStrXML;
        }
        #endregion

        #region ManipulationMethods
        public bool SaveBook(){
            return oBook.SaveBook();
        }

        public bool DeleteBook(){
            return oBook.DeleteBook();
        }
        #endregion
    }
}