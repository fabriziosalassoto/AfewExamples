using System;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using DFDataFlowControllers;

namespace DFWebHandlers
{
    public class DFWH_LocationRead : IHttpHandler
    {
        private DFCls_LocationForm oDFLocation;

        public void ProcessRequest(HttpContext context)
        {
            //Debugger.Launch();
            context.Response.ContentType = "text/plain";
            String strLocationSent = "";
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var stream = context.Request.InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                String mStrID = Encoding.UTF8.GetString(buffer);
                oDFLocation = new DFCls_LocationForm(mStrID);
                strLocationSent = oDFLocation.GetLocation();
            }
            context.Response.Write(strLocationSent);
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