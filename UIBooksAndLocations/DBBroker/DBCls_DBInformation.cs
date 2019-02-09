using System;
using System.Configuration;

namespace DBBroker
{
    internal class DBCls_DBInformation
    {
        internal DBCls_DBInformation() { }

        internal String getConnectionString(){
            return System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
        }
    }
}
