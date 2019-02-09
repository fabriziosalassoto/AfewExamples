using System;
using System.Collections.Generic;
using DBControllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace BRBusinessObjects
{
    public class BRCls_Books : List<BRCls_Book>
    {
        #region PrivateProperties
        private DBCls_Books oDBBooksController;
        #endregion

        #region Constructors
        public BRCls_Books()
        {
            oDBBooksController = new DBCls_Books();
        }

        public BRCls_Books(String[,] pArrBooks)
        {
            oDBBooksController = new DBCls_Books();
            FillList(pArrBooks);

        }
        #endregion

        #region SearchMethods
        public bool GetBooks()
        {
            String[,] mArrBooks = oDBBooksController.ListBooks();
            FillList(mArrBooks);
            return true;
        }        

        public bool GetStoriesByCriteria(BookCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[,] mArrBooks = oDBBooksController.ListBooksByCriteria(pCriteriaKey, pCriteriaValue);
            FillList(mArrBooks);
            return true;
        }
        #endregion

        #region ManipulationMethods
        public bool SaveAllBooks()
        {
            foreach (BRCls_Book oBook in this)
            {
                oBook.SaveBook();
            }
            return true;
        }

        public String JSONfy()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion

        #region PrivateMethods
        private void FillList(String[,] pArrBooks)
        {
            String[] mArrBook = null;
            for (int x = 0; x < pArrBooks.GetLength(0); x++)
            {
                mArrBook = new String[(int)TotalBookCriteria.cTotal];
                for (int y = 0; y < pArrBooks.GetLength(1); y++)
                {
                    mArrBook[y] = pArrBooks[x, y];
                }
                BRCls_Book oBook = new BRCls_Book(mArrBook);
                this.Add(oBook);
            }
        }
        #endregion
    }
}