#region Uses
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Diagnostics;
using System.Text.RegularExpressions;
#endregion

namespace RDPServer {
    class RDPUpdaterListener : BaseListener {
        #region Fields
        private delegate void SetTextCallback(string text);
        private Control myControlFound;
        #endregion

        #region Constructor
        public RDPUpdaterListener(Form pForm, string pIP, int pStartingPort, int pEndingPort)
            : base(pForm, pIP, pStartingPort, pEndingPort) {
        }
        #endregion

        #region Listening-Methods
        /*
         * This method receives the value corresponding to the previously selected control, updated on the browser client in order to override the local value
         */
        public override void startListening() {
            #region local-variables
            string StringReceived = "";
            string[] ControlReceived;
            int i;
            byte[] bytes;
            int milliseconds = 0;
            #endregion
            while(!Stop) {
                try {
                    InitiateConnection(this.StartingPort+2);
                    bytes = new byte[256];
                    myControlFound = null;                    
                    while(true) {
                        milliseconds = 50;
                        Thread.Sleep(milliseconds);
                        i = s.Read(bytes, 0, bytes.Length);
                        StringReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, i);                        
                        ControlReceived = DecodeControl(StringReceived);
                        myControlFound = FindControlByName(ControlReceived[0]);
                        SetText(ControlReceived[1]);                                                    
                    }                                    
                } catch(IOException Exception) {                   
                   if(mainSocket.IsBound)
                        mainSocket.Close();
                    if(listener != null)
                        listener.Stop();                    
                }
            }
        }
        #endregion

        #region CallBackMethods
        private void SetText(string NewValue) {
            SetTextCallback d = null;
            try {
                if(myControlFound.InvokeRequired) {
                    d = new SetTextCallback(SetText);
                    myControlFound.Invoke(d, new object[] { NewValue });
                } else
                    myControlFound.Text = NewValue;                
            }
            catch(Exception) { }
        }
        #endregion

        #region Decoding
        /*
         * This method decodes the string being sent from the browser containing the control name and value.
         */
        private string[] DecodeControl(string pControlReceived) {
            string[] myControl = new String[2];
            string[] control = Regex.Split(pControlReceived, ";");
            string[] name = Regex.Split(control[0], "=");
            string[] value = Regex.Split(control[1], "=");
            myControl[0] = name[1];
            myControl[1] = value[1];
            return myControl;
        }
        #endregion

        #region Finding-Control-on-Form
        /*
         * This method looks for the control on the form with the corresponding name received
         */
        private Control FindControlByName(string pControlName) {
            Control FoundControl = null;            
            foreach(Control CurrentControl in this.InvokerForm.Controls) {
                if(CurrentControl.Name == pControlName)
                    FoundControl = CurrentControl;                                    
            }         
            return FoundControl;
        }
        #endregion
    }
}