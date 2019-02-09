#region Uses
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Diagnostics;
#endregion

namespace RDPServer {
    class RDPImageListener : BaseListener {
        #region Constructor
        public RDPImageListener(Form pForm, string pIP, int pStartingPort, int pEndingPort)
            : base(pForm, pIP, pStartingPort, pEndingPort) {
        }
        #endregion

        #region Listening-Methods
        /*
         * This method does the screenshot returned to the remote client through the web browser
         */
        public override void startListening() {
            #region local-variables
            Bitmap screeny = null;
            Graphics theShot = null;
            BinaryFormatter bFormat = null;
            #endregion

            while(!Stop) {
                try {                    
                    InitiateConnection(StartingPort);
                    while(true) {
                        screeny = new Bitmap(InvokerForm.Width, InvokerForm.Height, PixelFormat.Format32bppArgb);
                        theShot = Graphics.FromImage(screeny);
                        theShot.ScaleTransform(.25F, .25F);
                        theShot.CopyFromScreen(InvokerForm.Left, InvokerForm.Top, 0, 0, InvokerForm.Size, CopyPixelOperation.SourceCopy);
                        bFormat = new BinaryFormatter();
                        bFormat.Serialize(s, screeny);                        
                        Thread.Sleep(imageDelay);
                        theShot.Dispose();
                        screeny.Dispose();
                    }
                } catch(Exception) {
                    if(mainSocket.IsBound)
                        mainSocket.Close();
                    if(listener != null)
                        listener.Stop();
                }
            }
        }
        #endregion
    }
}