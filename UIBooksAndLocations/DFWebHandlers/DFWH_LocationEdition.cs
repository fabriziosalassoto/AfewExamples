using System;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using DFDataFlowControllers;

namespace DFWebHandlers
{
    public class DFWH_LocationEdition : IHttpHandler
    {
        private DFCls_LocationForm oDFLocation;

        public void ProcessRequest(HttpContext context)
        {
            //Debugger.Launch();
            context.Response.ContentType = "text/plain";            
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var stream = context.Request.InputStream;
                byte[] mXML = new byte[stream.Length];
                stream.Read(mXML, 0, mXML.Length);                
                oDFLocation = new DFCls_LocationForm(mXML);
                if (oDFLocation.SaveLocation()){
                    context.Response.Write(oDFLocation.GetLocation());
                } else{
                    context.Response.Write("failure");
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}