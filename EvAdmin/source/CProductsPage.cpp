//	==============================================================================
//	CProductsPage - products page singleton object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

IDBRow* MakeProductDO2(void);

//	==============================================================================
//	Class Definition
//	==============================================================================

class CProductsPage : public IMObject
{
public:
	CProductsPage(IMObject* hWindow,IDBConnection* hDBConnection);
	~CProductsPage(void);
	virtual void* Msg(int nMsg,...);
	void* OnCtlShow(MSGP);
	void* OnDG2ItemChanged(MSGP);
	void* OnBtnChange(MSGP);
	void* OnSelectorNotify(MSGP);
public:
	void DisplayPage(void);
	void QueryDataChange(UINT nSelRow,UINT nSelCol);
	void ProductDataChange(UINT nSelRow,UINT nSelCol);
	void VerifyDeleteDesired(void);
public:
	IMObject*		hWindow;
	IDBConnection*	hDBConnection;
	IMObject*		hProductsPgCtl;
	IMObject*		hProductsDGrid;
	IMObject*		hQueryDGrid;
	IDBRow*			hQueryDO;
	IArray*			hProducts;
	IMObject*		hErrStgCtl;
};

//	==============================================================================
//	Page Class factory
//	==============================================================================

IMObject* MakeProductsPage(IMObject* hWindow,IDBConnection* hDBConnection)
{
	IMObject* hPage = new CProductsPage(hWindow,hDBConnection);
	return hPage; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CProductsPage::CProductsPage(IMObject* hWindowP,IDBConnection* hDBConnectionP)
{
	hWindow = hWindowP;
	hDBConnection = hDBConnectionP;
	hProducts = NULL;
	hQueryDO = MakeProductDO2();
	hProductsDGrid = NULL;
	hQueryDGrid = NULL;
	hErrStgCtl = NULL;
	hProductsPgCtl = MAKE(CTRL_Container,NULL,hWindow,5000);
	hProductsPgCtl->Msg(MSG_CtlShow,false);
	MAKE(CTRL_Build,hProductsPgCtl,RscText(RSC_ProductsPageCfg));
}

//	==============================================================================
//	Destructor
//	==============================================================================

CProductsPage::~CProductsPage(void)
{
	hQueryDO->Release();
	if(hProducts)
		hProducts->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CProductsPage::Msg(int nMsg,...)
{
	MSGPTR

	switch(nMsg)
	{
	case MSG_CtlShow: return OnCtlShow(msgptr);
	case MSG_DG2ItemChanged: return OnDG2ItemChanged(msgptr);
	case MSG_BtnSelectChange: return OnBtnChange(msgptr);
	case MSG_SelectorNotify: return OnSelectorNotify(msgptr);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	show or hide page
//	==============================================================================

void* CProductsPage::OnCtlShow(MSGP)
{
	MSGPARM(bool,fShow);

	//	show/hide page container control
	hProductsPgCtl->Msg(MSG_CtlShow,fShow);

	if(fShow)
	{
		// get all products data from database
		hProducts = hDBConnection->DBOSelectAll(hQueryDO);

		DisplayPage();
	}
	else
	{
		if(hProductsDGrid)
			hProductsDGrid->Msg(MSG_DG2Empty);

		if(hProducts)
			hProducts->Release();

		hProducts = NULL;
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Data Grid Item Changed
//	==============================================================================

void* CProductsPage::OnDG2ItemChanged(MSGP)
{
	MSGPARMPTR(IMObject*,hDGrid2);
	MSGPARM(UINT,nSelRow);
	MSGPARM(UINT,nSelCol);

	UINT oid = (int)hDGrid2->Msg(MSG_DbId);

	switch(oid)
	{
	case 610:	// query data grid control
		QueryDataChange(nSelRow,nSelCol);
		break;

	case 620:	// products data grid control
		ProductDataChange(nSelRow,nSelCol);
		break;
	}

	//	redisplay the container to show changes
	hWindow->Msg(MSG_WinCtrlsChanged);
	return 0;
}

//	==============================================================================
//	display page
//	==============================================================================

static UINT nColWidths[] = {110,110,80,320,80,80};

void CProductsPage::DisplayPage(void)
{
	UINT nrCols = hQueryDO->DBONrCols();

	hErrStgCtl = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,614);

	IMObject* hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,611);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,612);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,613);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn->Msg(MSG_CtlSetBkColor,0xff0000ff);
	hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,619);
	hBtn->Msg(MSG_CtlSetNotify,this);

	if(hQueryDGrid)
		hQueryDGrid->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,610);
		UINT nBkColor = (UINT)hProductsPgCtl->Msg(MSG_CtlBkgndColor);
		hQueryDGrid->Msg(MSG_CtlSetBkColor,nBkColor);
		hQueryDGrid->Msg(MSG_CtlSetNotify,this);
	}

	// define columns
	hQueryDGrid->Msg(MSG_DG2AddRows,0,2);
	hQueryDGrid->Msg(MSG_DG2AddColumns,0,nrCols);
	for(UINT ixCol=0;ixCol<nrCols;ixCol++)
	{
		hQueryDGrid->Msg(MSG_DG2SetColumnWidth,ixCol,nColWidths[ixCol]);
	}

	//	define column headings & link text cells
	for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
	{
		hQueryDGrid->Msg(MSG_DG2SetItemText,0,ixcol-1,hQueryDO->DBOColLabel(ixcol));
		hQueryDGrid->Msg(MSG_DG2SetItemReadOnly,0,ixcol-1,true);
		hQueryDGrid->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol-1,true);
		hQueryDGrid->Msg(MSG_DG2SetItemText,1,ixcol-1,hQueryDO->DBOText(ixcol));
	}

	//	label divider
	hQueryDGrid->Msg(MSG_DG2SetItemDivider,0,0,false,true);

//	------------------------------------------------------------------------------

	if(hProductsDGrid)
		hProductsDGrid->Msg(MSG_DG2Empty);
	else
	{
		hProductsDGrid = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,620);
		UINT nBkColor = (UINT)hProductsPgCtl->Msg(MSG_CtlBkgndColor);
		hProductsDGrid->Msg(MSG_CtlSetBkColor,nBkColor);
		hProductsDGrid->Msg(MSG_CtlSetNotify,this);
	}

	// define columns
	hProductsDGrid->Msg(MSG_DG2AddRows,0,1);
	hProductsDGrid->Msg(MSG_DG2AddColumns,0,nrCols);
	for(UINT ixCol=0;ixCol<nrCols;ixCol++)
	{
		hProductsDGrid->Msg(MSG_DG2SetColumnWidth,ixCol,nColWidths[ixCol]);
	}

	//	define column headings
	for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
	{
		hProductsDGrid->Msg(MSG_DG2SetItemText,0,ixcol-1,hQueryDO->DBOColLabel(ixcol));
		hProductsDGrid->Msg(MSG_DG2SetItemReadOnly,0,ixcol-1,true);
		hProductsDGrid->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol-1,true);
	}

	//	display the data returned from the products database
	IStr* hPriorProduct = MakeStr();

	if(hProducts)
	{
		UINT nProducts = hProducts->AryCount();

		for(UINT ix=1;ix<=nProducts;ix++)
		{
			IDBRow* hDO;
			hProducts->AryGetObj(ix-1,(IMObject**)&hDO);

			hProductsDGrid->Msg(MSG_DG2AddRows,ix,1);

			for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
			{
				hProductsDGrid->Msg(MSG_DG2SetItemText,ix,ixcol-1,hDO->DBOText(ixcol));
				if(ixcol==1)
				{
					hProductsDGrid->Msg(MSG_DG2SetItemNotifyOnly,ix,0,true);
					hProductsDGrid->Msg(MSG_DG2SetItemRdOnlyBkgnd,ix,0,true);
				}
			}

			//	visually divide groups based on different 1st 4 chars of SKU
			bool fChange = hSvc->Msg(MSG_SvcCmpUnicode,hPriorProduct->STextPtr(),hDO->DBOText(2))!=0;
			hPriorProduct->SSetTextW(hDO->DBOText(2));
			hProductsDGrid->Msg(MSG_DG2SetItemDivider,ix,0,fChange,false);
		}
	}
}

//	==============================================================================
//	query data item changed
//	==============================================================================

void CProductsPage::QueryDataChange(UINT nSelRow,UINT nSelCol)
{
	//	update edited text
	WCHAR* pText = (WCHAR*)hQueryDGrid->Msg(MSG_DG2GetItemText,nSelRow,nSelCol);

	hQueryDO->DBOSetText(nSelCol+1,pText);
}

//	==============================================================================
//	product data item changed
//	==============================================================================

void CProductsPage::ProductDataChange(UINT nSelRow,UINT nSelCol)
{
	if(hProducts)
	{
		IDBRow* hDO;
		hProducts->AryGetObj(nSelRow-1,(IMObject**)&hDO);

		//	update edited text
		WCHAR* pText = (WCHAR*)hProductsDGrid->Msg(MSG_DG2GetItemText,nSelRow,nSelCol);

		if(nSelCol == 0) // click on SKU - copy object to QUERY data object
		{
			UINT nrCols = hQueryDO->DBONrCols();
			for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
			{
				pText = (WCHAR*)hProductsDGrid->Msg(MSG_DG2GetItemText,nSelRow,ixcol-1);
				hQueryDO->DBOSetText(ixcol,pText);
				hQueryDGrid->Msg(MSG_DG2SetItemText,1,ixcol-1,hQueryDO->DBOText(ixcol));
			}
		}
		else
		{
			hDO->DBOSetText(nSelCol+1,pText);
			//	update database
			hDBConnection->DBOUpdate(hDO);
			hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
		}

		hWindow->Msg(MSG_WinCtrlsChanged);
	}
}

//	==============================================================================
//	Selector notification
//	==============================================================================

static WCHAR szDelOk[4096];

void CProductsPage::VerifyDeleteDesired(void)
{
	if(hQueryDO->DBOStr(1)->SLength())
	{
		//	verify ok to delete
		szDelOk[0] = szDelOk[1] = 0;
		IStr* hTemp = MakeStr();
		hTemp->Msg(MSG_SFormat,L"ss",L"Ok to delete record %1 = %2 shown above",
			hQueryDO->DBOColName(1),hQueryDO->DBOText(1));
		hSvc->SvcListAppendStr(szDelOk,hTemp->STextPtr());
		hSvc->SvcListAppendStr(szDelOk,L"Do NOT Delete Anything!");

		MOUSEPKT* ppkt = (MOUSEPKT*)hWinMgr->Msg(MSG_GetMousePkt);
		IMObject* hSelector = (IMObject*)MakePopupSelR(999,
			this,hProductsDGrid,ppkt->x,ppkt->y,
			szDelOk,NULL,NULL);
		hSelector->Msg(MSG_CtlSetSelColors,COLOR_WHITE,COLOR_RED);
		hSelector->Msg(MSG_SelMarkItem,hSvc->SvcListGetStr(szDelOk,1));
	}
}

void* CProductsPage::OnSelectorNotify(MSGP)
{
	MSGPARM(UINT,nItem);
	MSGPARMPTR(void*,parm1);
	MSGPARMPTR(void*,parm2);
	MSGPARMPTR(UINT,SelectorOId);

	IDBRow* hDO = (IDBRow*)parm2;

	if(nItem==1)	// 0=selector cancelled, 1=delete, 2=cancel
	{
		if(hDBConnection->DBODelete(hQueryDO))
		{
			// record was deleted:
			hQueryDO->DBOClear();
			//	update list of all products
			if(hProducts)
				hProducts->Release();
			hProducts = hDBConnection->DBOSelectAll(hQueryDO);
			DisplayPage();
		}
	}

	//	clear the delete button	from button down state
	IMObject* hBtn = (IMObject*)hProductsPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_BtnSetState,false);

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	process button change
//	==============================================================================

void* CProductsPage::OnBtnChange(MSGP)
{
	MSGPARM(int,btnid);
	MSGPARMPTR(IMObject*,hCtrl);
	MSGPARM(bool,fPressed);

	IArray* hNewResults;

	if(btnid == 615) // delete button - start verify on button click
	{
		if(fPressed)
			VerifyDeleteDesired();
	}

	if(!fPressed)
		return IM_RTN_NOTHING; // only respond to button up messages

	switch(btnid)
	{
	case 611:	// clear query DO
		hQueryDO->Msg(MSG_DBOClear);
		DisplayPage();
		break;

	case 612:	// find records (select)
		{
			hNewResults = hDBConnection->DBOSelect(hQueryDO);
			if(hNewResults)
			{
				if(hProducts)
					hProducts->Release();
				hProducts = hNewResults;
				DisplayPage();
			}
		}
		break;

	case 613:	// add query DO to products table
		if(hQueryDO->DBOValidate(NULL))
		{
			if(hDBConnection->DBOAdd(hQueryDO))
			{
				//	update list of all products
				if(hProducts)
					hProducts->Release();
				hProducts = hDBConnection->DBOSelectAll(hQueryDO);
			}
			DisplayPage();
		}
		break;

	case 619:	// show everything
		if(hProducts)
			hProducts->Release();
		hProducts = hDBConnection->DBOSelectAll(hQueryDO);
		DisplayPage();
		break;
	}

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
