#region uses
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
    abstract class BaseListener {
        #region Constants
        protected internal const int lostInTop = 30;
        protected internal const int lostinLeft = 10;
        #endregion

        #region Fields
        protected internal Form InvokerForm = null;
        protected internal IPAddress GivenIPAddress = null;
        protected internal int StartingPort = 0;
        protected internal int EndingPort = 0;
        protected internal volatile bool Stop = false;
        protected internal TcpListener listener;
        protected internal Socket mainSocket;
        protected internal Stream s;
        protected internal int imageDelay;
        #endregion

        #region Constructor
        public BaseListener(Form pForm, string pIP, int pStartingPort, int pEndingPort) {
            IPAddress.TryParse(pIP,out GivenIPAddress);
            InvokerForm = pForm;            
            StartingPort = pStartingPort;
            EndingPort = pEndingPort;
        }
        #endregion

        #region Listening-Overridable-Methods
        public void InitiateConnection(int port) {
            imageDelay = 1000;
            listener = new TcpListener(this.GivenIPAddress, port);
            listener.Start();
            mainSocket = listener.AcceptSocket();
            s = new NetworkStream(mainSocket); 
        }

        public abstract void startListening();
        #endregion

        #region Close-Connections
        public void KillMe() {
            mainSocket.Close();
            listener.Stop();            
        }
        #endregion
    }
}