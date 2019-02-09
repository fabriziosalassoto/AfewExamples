<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

public class ImageHandler : IHttpHandler {
    
    private TcpClient connector;    
    /*private StreamWriter eventSender;
    private Thread theThread;*/
    private TcpClient client;
    private int resolutionX;
    private int resolutionY;    
    public bool sendKeysAndMouse = false;
    private HttpContext CurrentContext;
    
    public void ProcessRequest (HttpContext ctx) {
        ctx.Response.ContentType = "text/plain";
        CurrentContext = ctx;                
        var wrapper = StartRead();
        ctx.Response.Write(wrapper);        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }    
    
    private string StartRead(){
        string returnedImage = "";
        try {
            this.connector = new TcpClient((string)CurrentContext.Request.Form["ip"], int.Parse(CurrentContext.Request.Form["port"]));                
        } catch(Exception problem) {}

        try {
            if(this.connector != null) {
                Stream stream;
                stream = this.connector.GetStream();
                MemoryStream MS = new MemoryStream();
                BinaryFormatter bFormat = new BinaryFormatter();
                Bitmap inImage = bFormat.Deserialize(stream) as Bitmap;
                inImage.Save(MS, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = MS.ToArray();
                string imageBase64 = Convert.ToBase64String(bytes);
                string imageSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
                this.connector.Close();
                returnedImage = imageSrc;                
            }
        }
        catch(Exception) { }         
        return returnedImage;        
    }

}