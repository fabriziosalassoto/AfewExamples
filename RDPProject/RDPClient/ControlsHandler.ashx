<%@ WebHandler Language="C#" Class="ControlsHandler" %>

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
using System.Web.Script.Serialization;

public class ControlsHandler : IHttpHandler {

    private HttpContext CurrentContext;
    private TcpClient connector;
    private string method = "";
    string RemoteIP = ""; 
    int RemotePort = 0;       
    
    public void ProcessRequest (HttpContext ctx) {
        ctx.Response.ContentType = "text/plain";
        CurrentContext = ctx;
        RemoteIP = (string)CurrentContext.Request.Form["ip"]; 
        RemotePort = int.Parse(CurrentContext.Request.Form["port"]);
        RemotePort++;
        method = (string)CurrentContext.Request.Form["method"];
        switch(method) {
            case "GetControl": GetControl();
                break;
            case "ControlValueUpdate": RemotePort++;
                ControlValueUpdate();
                break;
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private string ControlValueUpdate() {
        string ControlName = "";
        string ControlValue = "";        
        string result = "";
                
        try {
            this.connector = new TcpClient(RemoteIP, RemotePort);
            if(this.connector != null) {
                Stream stream;
                stream = this.connector.GetStream();
                MemoryStream MS = new MemoryStream();
                byte[] Sent = MS.ToArray();
                ControlName = (string)CurrentContext.Request.Form["control"];
                ControlValue = (string)CurrentContext.Request.Form["Value"];

                String messageSent = "control=" + ControlName + ";value=" + ControlValue;
                Sent = System.Text.Encoding.ASCII.GetBytes(messageSent);
                stream.Write(Sent, 0, Sent.Length);
                
                this.CurrentContext.Response.ContentType = "application/json";
                this.CurrentContext.Response.Write(result);                    
            }
        } catch(Exception problemConnecting) { }                            
        return result;
    }

    private string GetControl() {
        int posX = 0;
        int posY = 0;
        string ControlNameRead = "";
        string result = "";

        try {
            if(int.TryParse((string)CurrentContext.Request.Form["x"], out posX) && int.TryParse((string)CurrentContext.Request.Form["y"], out posY)) {
                try {                    
                    this.connector = new TcpClient(RemoteIP, RemotePort);
                    if(this.connector != null) {
                        ControlNameRead = (string)CurrentContext.Request.Form["control"];
                        Stream stream;
                        stream = this.connector.GetStream();
                        MemoryStream MS = new MemoryStream();
                        byte[] Sent = MS.ToArray();
                        String messageSent = "X=" + posX.ToString() + ";Y=" + posY.ToString() + ";OBJECT=" + ControlNameRead;
                        Sent = System.Text.Encoding.ASCII.GetBytes(messageSent);
                        stream.Write(Sent, 0, Sent.Length);

                        String messageReceived = ""; ;
                        byte[] Received = new byte[256];
                        int i = stream.Read(Received, 0, Received.Length);
                        messageReceived = System.Text.Encoding.ASCII.GetString(Received, 0, i);


                        string[] Control = Regex.Split(messageReceived, ";");
                        string[] ControlName = Regex.Split(Control[0], "=");
                        string[] ControlValue = Regex.Split(Control[1], "=");

                        var serializer = new JavaScriptSerializer();
                        result = serializer.Serialize(new {
                            controls = new[]{
                                new { name = ControlName[1], value = ControlValue[1] },                                
                            }
                        });
                        this.CurrentContext.Response.ContentType = "application/json";
                        this.CurrentContext.Response.Write(result);

                    }

                }
                catch(Exception problemConnecting) { }
            }
        }
        catch(Exception problem) {
        }
        return result;
    }  
}