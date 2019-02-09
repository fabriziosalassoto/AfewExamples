using System;
using System.Collections.Generic;
using DBControllers;
using BRLibrary;

namespace BRBusinessObjects
{
    #region enumerations
    public enum BookStatus
    {
        New = 0,
        Modified = 1,
        NotModified = 2,
        MarkedForDeletion = 3
    }
    #endregion

    public class BRCls_Book
    {
        #region PrivateProperties

        private String BookID;
        private String BookName;
        private String BookAuthor;
        private String BookISBN;
        private String BookDescription;
        private DBCls_Books oDBBookController;
        private int ObjectStatus;

        #endregion

        #region PublicProperties
        public String GetBookID
        {
            get { return BookID; }
        }

        public String GetBookName
        {
            get { return BookName; }
        }

        public String SetBookName
        {
            set
            {
                ObjectStatus = (int)BookStatus.Modified;
                BookName = value;
            }
        }

        public String GetBookAuthor
        {
            get { return BookAuthor; }
        }

        public String SetBookAuthor
        {
            set
            {
                ObjectStatus = (int)BookStatus.Modified;
                BookAuthor = value;
            }
        }

        public String GetBookISBN
        {
            get { return BookISBN; }
        }

        public String SetBookISBN
        {
            set
            {
                ObjectStatus = (int)BookStatus.Modified;
                BookISBN = value;
            }
        }

        public String GetBookDescription
        {
            get { return BookDescription; }
        }

        public String SetBookDescription
        {
            set
            {
                ObjectStatus = (int)BookStatus.Modified;
                BookDescription = value;
            }
        }              

        public int GetStatus
        {
            get { return ObjectStatus; }
        }
        #endregion

        #region Constructors
        public BRCls_Book()
        {
            oDBBookController = new DBCls_Books();
            BookID = "";

            ObjectStatus = (int)BookStatus.New;
        }

        public BRCls_Book(String pBookID)
        {
            oDBBookController = new DBCls_Books();
            GetBookByID(pBookID);
            ObjectStatus = (int)BookStatus.NotModified;
        }

        public BRCls_Book(String[] ArrBook)
        {
            oDBBookController = new DBCls_Books();
            FillInBook(ArrBook);
            ValidateStatus();
        }
        #endregion

        #region SearchMethods
        public bool GetBookByID(String pBookID)
        {
            try
            {
                oDBBookController = new DBCls_Books();
                String[,] ArrBook = oDBBookController.SearchBook(pBookID);
                if (ArrBook != null)
                {
                    BookID = ArrBook[0, (int)BookCriteria.cBOOKID];
                    BookName = ArrBook[0, (int)BookCriteria.cBOOKNAME];
                    BookAuthor = ArrBook[0, (int)BookCriteria.cBOOKAUTHOR];
                    BookISBN = ArrBook[0, (int)BookCriteria.cBOOKISBN];
                    BookDescription = ArrBook[0, (int)BookCriteria.cBOOKDESCRIPTION];
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region BookManipulation
        public bool LoadBookFromXML(String pXML)
        {
            try
            {
                oDBBookController = new DBCls_Books();
                FillBookWithXML(pXML);
                ValidateStatus();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool SaveBook()
        {
            try
            {
                String[] ArrBook = FillInArrayWithProperties();
                if (BookID.Trim() == "")
                {
                    ObjectStatus = (int)BookStatus.New;
                }

                switch (ObjectStatus)
                {
                    case (int)BookStatus.New: 
                        BookID = Convert.ToString(oDBBookController.InsertBook(ArrBook));
                        if (BookID == "-1"){
                            return false;
                        }
                        break;
                    case (int)BookStatus.Modified: oDBBookController.UpdateBook(ArrBook);
                        break;
                    case (int)BookStatus.MarkedForDeletion: oDBBookController.EliminateBook(BookCriteria.cBOOKID, BookID);
                        break;
                }
                ObjectStatus = (int)BookStatus.NotModified;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ValidateStatus(){
            switch(this.BookID){
                case "": this.ObjectStatus = (int)BookStatus.New;
                    break;
                case "-1": this.ObjectStatus = (int)BookStatus.NotModified;
                    break;
                default:
                    this.ObjectStatus = (int)BookStatus.Modified;
                    break;
            }
        }

        public bool DeleteBook()
        {
            try
            {
                ObjectStatus = (int)BookStatus.MarkedForDeletion;
                SaveBook();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public String XMLfy()
        {
            String mStrXML = "<book>";
            mStrXML+="<bookid>"+BookID+"</bookid>";
            mStrXML += "<bookname>" + BookName + "</bookname>";
            mStrXML+="<bookauthor>"+BookAuthor + "</bookauthor>";
            mStrXML += "<bookisbn>" + BookISBN + "</bookisbn>";
            mStrXML += "<bookdescription>" + BookDescription + "</bookdescription>";
            mStrXML += "</book>";
            return mStrXML;
        }
        #endregion

        #region Fill_IN_OUT_PROPERTIES
        private String[] FillInArrayWithProperties()
        {
            String[] mArrProperties = new String[(int)TotalBookCriteria.cTotal];
            mArrProperties[(int)BookCriteria.cBOOKID] = BookID;
            mArrProperties[(int)BookCriteria.cBOOKNAME] = BookName;
            mArrProperties[(int)BookCriteria.cBOOKAUTHOR] = BookAuthor;
            mArrProperties[(int)BookCriteria.cBOOKISBN] = BookISBN;
            mArrProperties[(int)BookCriteria.cBOOKDESCRIPTION] = BookDescription;
            return mArrProperties;
        }

        private void FillBookWithXML(String pXML)
        {
            BRCls_XMLReader oXMLReader = new BRCls_XMLReader(pXML);
            oXMLReader.LookForParents("book");
            this.BookID = oDBBookController.EscapeValue(oXMLReader.LookForChildren("bookid"));
            this.BookName = oDBBookController.EscapeValue(oXMLReader.LookForChildren("bookname"));
            this.BookAuthor = oDBBookController.EscapeValue(oXMLReader.LookForChildren("bookauthor"));
            this.BookISBN = oDBBookController.EscapeValue(oXMLReader.LookForChildren("bookisbn"));
            this.BookDescription = oDBBookController.EscapeValue(oXMLReader.LookForChildren("bookdescription"));            
        }

        private void FillInBook(String[] pBook)
        {
            BookID = pBook[(int)BookCriteria.cBOOKID];
            BookName = pBook[(int)BookCriteria.cBOOKNAME];
            BookAuthor = pBook[(int)BookCriteria.cBOOKAUTHOR];
            BookISBN = pBook[(int)BookCriteria.cBOOKISBN];
            BookDescription = pBook[(int)BookCriteria.cBOOKDESCRIPTION];
        }
        #endregion
    }
}