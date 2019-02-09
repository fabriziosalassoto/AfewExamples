using System;
using System.Collections.Generic;
using DBBroker;

namespace DBControllers
{
    public enum TotalLocationCriteria
    {
        cTotal = 6
    }

    public enum LocationCriteria
    {
        cLOCATIONID = 0, cLOCATIONNAME = 1, cLOCATIONDESCRIPTION = 2, cLOCATIONADDRESS = 3,
        cLOCATIONLATITUDE = 4, cLOCATIONLONGITUDE = 5
    }

    public class DBCls_Locations
    {
        private DBCls_DBConnection oConnection;

        private const int cCRITERIAKEY = 0;
        private const int cCRITERIAVALUE = 1;

        public DBCls_Locations()
        {
            oConnection = new DBCls_DBConnection();
        }

        public String[,] SearchLocation(String pStrID)
        {
            String[,] ArrLocations;
            String mStrSQL = "";

            mStrSQL = "SELECT LOCATIONID, LOCATIONNAME, LOCATIONDESCRIPTION, LOCATIONADDRESS, " +
                             "LOCATIONLATITUDE, LOCATIONLONGITUDE " +
                      "FROM LOCATIONS WHERE LOCATIONID=" + pStrID;
            try
            {
                oConnection.OpenConnection();
                ArrLocations = oConnection.QuerySQLResultFlatArray(mStrSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrLocations;
        }

        public int InsertLocation(String[] pArrLocation)
        {
            String mStrSQL = "";
            int mIntLastID = -1;

            mStrSQL = "INSERT INTO LOCATIONS( " +
                             "LOCATIONNAME, LOCATIONDESCRIPTION, LOCATIONADDRESS, " + 
                             "LOCATIONLATITUDE, LOCATIONLONGITUDE) " +
                      "VALUES (" +
                            "'" + pArrLocation[(int)LocationCriteria.cLOCATIONNAME] + "', " +
                            "'" + pArrLocation[(int)LocationCriteria.cLOCATIONDESCRIPTION] + "', " +
                            "'" + pArrLocation[(int)LocationCriteria.cLOCATIONADDRESS] + "', " +
                                  pArrLocation[(int)LocationCriteria.cLOCATIONLATITUDE] + ", " +
                                  pArrLocation[(int)LocationCriteria.cLOCATIONLONGITUDE] + ")";
            try{
                oConnection.OpenConnection();
                if (oConnection.UpdateSQL(mStrSQL, null)){
                    mIntLastID = GetLastLocationIDInserted();
                }
            } catch (Exception ex) {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return mIntLastID;
        }

        public bool UpdateLocation(String[] pArrLocation)
        {
            String mStrSQL = "";
            bool mBoolSuccess = false;
            mStrSQL = "UPDATE LOCATIONS SET " +
                              "LOCATIONNAME = '" + pArrLocation[(int)LocationCriteria.cLOCATIONNAME] + "', " +
                              "LOCATIONDESCRIPTION = '" + pArrLocation[(int)LocationCriteria.cLOCATIONDESCRIPTION] + "', " +
                              "LOCATIONADDRESS = '" + pArrLocation[(int)LocationCriteria.cLOCATIONADDRESS] + "', " +
                              "LOCATIONLATITUDE = " + pArrLocation[(int)LocationCriteria.cLOCATIONLATITUDE] + ", " +
                              "LOCATIONLONGITUDE = " + pArrLocation[(int)LocationCriteria.cLOCATIONLONGITUDE] + " " +                                                            
                    "WHERE LOCATIONID = " + pArrLocation[(int)LocationCriteria.cLOCATIONID];            
            try
            {
                oConnection.OpenConnection();
                oConnection.UpdateSQL(mStrSQL, null);
                mBoolSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return mBoolSuccess;
        }

        public bool EliminateLocation(LocationCriteria pCriteriaKey, String pCriteriaValue)
        {
            bool mBoolSuccess = false;
            String mStrSQL = "";
            String[] ArrCriteria = GetDBFieldByCriteria(pCriteriaKey, pCriteriaValue);

            mStrSQL = "DELETE FROM LOCATIONS " +
                      "WHERE " + ArrCriteria[cCRITERIAKEY] + " = " +
                                 ArrCriteria[cCRITERIAVALUE];
            try
            {
                oConnection.OpenConnection();
                oConnection.UpdateSQL(mStrSQL, null);
                mBoolSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return mBoolSuccess;
        }

        public String[,] ListLocations()
        {
            String[,] ArrLocations;
            String mStrSQL = "";

            mStrSQL = "SELECT LOCATIONID, LOCATIONNAME, LOCATIONDESCRIPTION, LOCATIONADDRESS, " +
                             "LOCATIONLATITUDE, LOCATIONLONGITUDE " +
                      "FROM LOCATIONS";
            try
            {
                oConnection.OpenConnection();
                ArrLocations = oConnection.QuerySQLResultFlatArray(mStrSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrLocations;
        }

        public String[,] ListLocationsByCriteria(LocationCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[,] ArrLocations;
            String[] ArrCriteria = GetDBFieldByCriteria(pCriteriaKey, pCriteriaValue);
            String mStrSQL = "";

            mStrSQL = "SELECT LOCATIONID, LOCATIONNAME, LOCATIONDESCRIPTION, LOCATIONADDRESS, " +
                             "LOCATIONLATITUDE, LOCATIONLONGITUDE " +
                      "FROM LOCATIONS " +
                      "WHERE " + ArrCriteria[cCRITERIAKEY] + " = " +
                                 ArrCriteria[cCRITERIAVALUE];
            try
            {
                oConnection.OpenConnection();
                ArrLocations = oConnection.QuerySQLResultFlatArray(mStrSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrLocations;
        }

        private String[] GetDBFieldByCriteria(LocationCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[] ArrCriteriaField = new String[2];
            switch (pCriteriaKey)
            {
                case LocationCriteria.cLOCATIONID: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONID";
                    ArrCriteriaField[cCRITERIAVALUE] = pCriteriaValue;
                    break;
                case LocationCriteria.cLOCATIONNAME: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONNAME";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case LocationCriteria.cLOCATIONDESCRIPTION: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONDESCRIPTION";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case LocationCriteria.cLOCATIONADDRESS: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONADDRESS";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case LocationCriteria.cLOCATIONLATITUDE: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONLATITUDE";
                    ArrCriteriaField[cCRITERIAVALUE] = pCriteriaValue;
                    break;
                case LocationCriteria.cLOCATIONLONGITUDE: ArrCriteriaField[cCRITERIAKEY] = "LOCATIONLONGITUDE";
                    ArrCriteriaField[cCRITERIAVALUE] = pCriteriaValue;
                    break;
            }
            return ArrCriteriaField;
        }

        public int GetLastLocationIDInserted()
        {
            int intLastID = -1;
            String strLastID = "";

            try
            {
                oConnection.OpenConnection();
                strLastID = oConnection.GetLastIDInserted("LOCATIONS");
                oConnection.CloseConnection();
                intLastID = int.Parse(strLastID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return intLastID;
        }

        public String EscapeValue(String pValue)
        {
            return oConnection.EscapeValue(pValue);
        }
    }
}