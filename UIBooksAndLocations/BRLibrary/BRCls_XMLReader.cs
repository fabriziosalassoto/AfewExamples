using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace BRLibrary
{
    public class BRCls_XMLReader
    {
        #region PrivateProperties
        private XmlDocument oXMLDocument;
        private XmlNodeList oXMLTopNodes;
        #endregion

        #region Constructors
        public BRCls_XMLReader() { }

        public BRCls_XMLReader(String pXMLStream)
        {
            oXMLDocument = new XmlDocument();
            oXMLDocument.LoadXml(pXMLStream);
        }
        #endregion

        #region ManipulationMethods
        public void LookForParents(String pXMLTag)
        {
            oXMLTopNodes = oXMLDocument.GetElementsByTagName(pXMLTag);
        }

        public String LookForChildren(String pXMLTag)
        {
            String strChildrenNodes = "";
            foreach (XmlElement oXMLNode in oXMLTopNodes)
            {
                if (oXMLNode.GetElementsByTagName(pXMLTag)[0].InnerText != "" &&
                   oXMLNode.GetElementsByTagName(pXMLTag)[0].InnerText != "undefined")
                {         
                    strChildrenNodes = oXMLNode.GetElementsByTagName(pXMLTag)[0].InnerText;
                }
                else
                {
                    strChildrenNodes = null;
                }
            }
            return strChildrenNodes;
        }

        public void CloseFile()
        {
            oXMLDocument = null;
        }
        #endregion

        public String GetXMLHeader()
        {
            return "<?xml version='1.0' encoding='iso-8859-1'?>";
        }
    }    
}