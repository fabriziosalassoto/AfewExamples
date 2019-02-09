using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace DBBroker
{

    public class DBCls_DBConnection
    {
        private const int cParameterKey = 0;
        private const int cParameterValue = 1;
        private String strConnectionString = "";
        private SqlConnection oConnection;
        private SqlCommand oCommand;
        private SqlDataReader oReader;
        private DataTable oTable;

        public DBCls_DBConnection()
        {
            DBCls_DBInformation oDBInformation = new DBCls_DBInformation();
            strConnectionString = oDBInformation.getConnectionString();
        }

        public bool OpenConnection()
        {
            oConnection = null;
            oCommand = null;
            oReader = null;
            oTable = null;
            try
            {
                oConnection = new SqlConnection(strConnectionString);
                oConnection.Open();
            }
            catch (Exception ex)
            {
                oConnection = null;
                return false;
            }
            return true;
        }

        public String[,] QuerySQLResultFlatArray(String pSQL)
        {
            String[,] ArrResults = null;
            int x = 0, y = 0;
            try
            {
                oCommand = new SqlCommand(pSQL, oConnection);
                oReader = oCommand.ExecuteReader();
                oTable = new DataTable();
                oTable.Load(oReader);

                ArrResults = new String[oTable.Rows.Count, oTable.Columns.Count];
                foreach (DataRow oRow in oTable.Rows)
                {
                    y = 0;
                    foreach (DataColumn oColumn in oTable.Columns)
                    {
                        ArrResults[x, y] = Convert.ToString(oRow[oColumn.ColumnName]);
                        y++;
                    }
                    x++;
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                oCommand = null;
                oReader = null;
                oTable = null;
                ArrResults = new String[0, 0];
            }
            return ArrResults;
        }

        public Object[,] QuerySQLResultList(String pSQL)
        {
            Object[,] ListResults = null;
            int x = 0;
            try
            {
                oCommand = new SqlCommand(pSQL, oConnection);
                oReader = oCommand.ExecuteReader();
                oTable.Load(oReader);

                ListResults = new Object[oTable.Rows.Count, oTable.Columns.Count];
                foreach (DataRow oRow in oTable.Rows)
                {
                    ListResults[x, 0] = Convert.ToString(oRow[0]);
                    ListResults[x, 1] = Convert.ToString(oRow[1]);
                    x++;
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                oCommand = null;
                oReader = null;
                oTable = null;
                ListResults = new Object[0, 0];
            }
            return ListResults;
        }

        public bool UpdateSQL(String pSQL, List<Object> pParams)
        {
            try
            {
                oCommand = new SqlCommand(pSQL, oConnection);
                if (pParams != null)
                {
                    oCommand.Parameters.Add(Convert.ToString(pParams[cParameterKey]), SqlDbType.Image).Value = pParams[cParameterValue];
                }
                oCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                oCommand = null;
                return false;
            }
        }

        public String GetLastIDInserted(String pStrDBTable)
        {

            String mStrSQL = "SELECT IDENT_CURRENT('" + pStrDBTable + "')";
            String mStrLastIDInserted = "";

            try
            {
                oCommand = new SqlCommand(mStrSQL, oConnection);
                oReader = oCommand.ExecuteReader();
                oTable = new DataTable();
                oTable.Load(oReader);

                foreach (DataRow oDataRow in oTable.Rows)
                {
                    foreach (DataColumn oDataColumn in oTable.Columns)
                    {
                        mStrLastIDInserted = oDataRow[oDataColumn.ColumnName].ToString();
                    }
                }
            }
            catch (DataException Ex)
            {
                mStrLastIDInserted = "";
            }
            oCommand = null;
            oReader = null;
            oTable = null;
            return mStrLastIDInserted;
        }

        public String EscapeValue(String pValue)
        {
            if (pValue == null)
                pValue = "";
            else
                pValue = pValue.Replace("'", "''");
            return pValue;
        }

        public void CloseConnection()
        {
            if (oConnection != null)
            {
                oConnection.Close();
                oConnection = null;
            }
        }
    }
}