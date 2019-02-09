using System;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using DFDataFlowControllers;

namespace DFWebHandlers
{
    public class DFWH_BookRead : IHttpHandler
    {
        private DFCls_BookForm oDFBook;

        public void ProcessRequest(HttpContext context)
        {
            //Debugger.Launch();            
            context.Response.ContentType = "text/plain";
            String strBookSent = "";
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                var stream = context.Request.InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                String mStrID = Encoding.UTF8.GetString(buffer);
                oDFBook = new DFCls_BookForm(mStrID);
                strBookSent = oDFBook.GetBook();
            }                        
            context.Response.Write(strBookSent);
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