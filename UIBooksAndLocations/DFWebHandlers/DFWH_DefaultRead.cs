using System;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using DFDataFlowControllers;

namespace DFWebHandlers
{
    public class DFWH_DefaultRead : IHttpHandler
    {
        private DFCls_DefaultForm oDFDefault;

        public void ProcessRequest(HttpContext context)
        {
            Debugger.Launch();            
            context.Response.ContentType = "text/plain";
            String strBooksAndLocationsSent = "";            
            oDFDefault = new DFCls_DefaultForm();
            strBooksAndLocationsSent = oDFDefault.GetBooksAndLocations();
            context.Response.Write(strBooksAndLocationsSent);
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