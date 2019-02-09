using System;
using System.Collections.Generic;
using DBBroker;

namespace DBControllers
{
    public enum TotalBookCriteria
    {
        cTotal = 5
    }

    public enum BookCriteria
    {
        cBOOKID = 0, cBOOKNAME = 1, cBOOKAUTHOR = 2, cBOOKISBN = 3, cBOOKDESCRIPTION = 4
    }

    public class DBCls_Books
    {
        private DBCls_DBConnection oConnection;

        private const int cCRITERIAKEY = 0;
        private const int cCRITERIAVALUE = 1;

        public DBCls_Books()
        {
            oConnection = new DBCls_DBConnection();
        }

        public String[,] SearchBook(String pStrID)
        {
            String[,] ArrBooks;
            String mStrSQL = "";

            mStrSQL = "SELECT BOOKID, BOOKNAME, BOOKAUTHOR, BOOKISBN, BOOKDESCRIPTION " +
                      "FROM BOOKS WHERE BOOKID=" + pStrID;
            try
            {
                oConnection.OpenConnection();
                ArrBooks = oConnection.QuerySQLResultFlatArray(mStrSQL);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrBooks;
        }

        public int InsertBook(String[] pArrBook)
        {
            String mStrSQL = "";
            int mIntLastID = -1;

            mStrSQL = "INSERT INTO BOOKS( " +
                             "BOOKNAME, BOOKAUTHOR, BOOKISBN, BOOKDESCRIPTION) " +                             
                      "VALUES (" +
                            "'" + pArrBook[(int)BookCriteria.cBOOKNAME] + "', " +
                            "'" + pArrBook[(int)BookCriteria.cBOOKAUTHOR] + "', " +
                            "'" + pArrBook[(int)BookCriteria.cBOOKISBN] + "', " +
                            "'" + pArrBook[(int)BookCriteria.cBOOKDESCRIPTION] + "')";
            try{
                oConnection.OpenConnection();
                if (oConnection.UpdateSQL(mStrSQL, null)){
                    mIntLastID = GetLastBookIDInserted();
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

        public bool UpdateBook(String[] pArrBook)
        {
            String mStrSQL = "";
            bool mBoolSuccess = false;
            mStrSQL = "UPDATE BOOKS SET " +
                              "BOOKNAME = '" + pArrBook[(int)BookCriteria.cBOOKNAME] + "', " +
                              "BOOKAUTHOR = '" + pArrBook[(int)BookCriteria.cBOOKAUTHOR] + "', " +
                              "BOOKISBN = '" + pArrBook[(int)BookCriteria.cBOOKISBN] + "', " +
                              "BOOKDESCRIPTION = '" + pArrBook[(int)BookCriteria.cBOOKDESCRIPTION] + "' " +
                    "WHERE BOOKID=" + pArrBook[(int)BookCriteria.cBOOKID];
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

        public bool EliminateBook(BookCriteria pCriteriaKey, String pCriteriaValue)
        {
            bool mBoolSuccess = false;
            String mStrSQL = "";
            String[] ArrCriteria = GetDBFieldByCriteria(pCriteriaKey, pCriteriaValue);

            mStrSQL = "DELETE FROM BOOKS " +
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

        public String[,] ListBooks()
        {
            String[,] ArrBooks;
            String mStrSQL = "";

            mStrSQL = "SELECT BOOKID, BOOKNAME, BOOKAUTHOR, BOOKISBN, BOOKDESCRIPTION " +
                      "FROM BOOKS";
            try
            {
                oConnection.OpenConnection();
                ArrBooks = oConnection.QuerySQLResultFlatArray(mStrSQL);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrBooks;
        }

        public String[,] ListBooksByCriteria(BookCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[,] ArrBooks;
            String[] ArrCriteria = GetDBFieldByCriteria(pCriteriaKey, pCriteriaValue);
            String mStrSQL = "";

            mStrSQL = "SELECT BOOKID, BOOKNAME, BOOKAUTHOR, BOOKISBN, BOOKDESCRIPTION " +
                      "FROM BOOKS " +
                      "WHERE " + ArrCriteria[cCRITERIAKEY] + " = " +
                                 ArrCriteria[cCRITERIAVALUE];
            try
            {
                oConnection.OpenConnection();
                ArrBooks = oConnection.QuerySQLResultFlatArray(mStrSQL);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConnection.CloseConnection();
            }
            return ArrBooks;
        }

        private String[] GetDBFieldByCriteria(BookCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[] ArrCriteriaField = new String[2];
            switch (pCriteriaKey)
            {
                case BookCriteria.cBOOKID: ArrCriteriaField[cCRITERIAKEY] = "BOOKID";
                    ArrCriteriaField[cCRITERIAVALUE] = pCriteriaValue;
                    break;
                case BookCriteria.cBOOKNAME: ArrCriteriaField[cCRITERIAKEY] = "BOOKNAME";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case BookCriteria.cBOOKAUTHOR: ArrCriteriaField[cCRITERIAKEY] = "BOOKAUTHOR";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case BookCriteria.cBOOKISBN: ArrCriteriaField[cCRITERIAKEY] = "BOOKISBN";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;
                case BookCriteria.cBOOKDESCRIPTION: ArrCriteriaField[cCRITERIAKEY] = "BOOKDESCRIPTION";
                    ArrCriteriaField[cCRITERIAVALUE] = "'" + pCriteriaValue + "'";
                    break;                
            }
            return ArrCriteriaField;
        }

        public int GetLastBookIDInserted()
        {
            int intLastID = -1;
            String strLastID = "";

            try
            {
                oConnection.OpenConnection();
                strLastID = oConnection.GetLastIDInserted("BOOKS");
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