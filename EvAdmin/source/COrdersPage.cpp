//	==============================================================================
//	COrdersPage - orders page singleton object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

IDBRow* MakeOrderDO2(void);
IDBRow* MakeProductDO2(void);
IDBRow* MakeContactDO2(void);
IDBRow* MakePaymentDO2(void);
IDBRow* MakeAuthKeyDO2(void);

//	==============================================================================
//	Class Definition
//	==============================================================================

class COrdersPage : public IMObject
{
public:
	COrdersPage(IMObject* hWindow,IDBConnection* hDBConnection);
	~COrdersPage(void);
	virtual void* Msg(int nMsg,...);
	void* OnCtlShow(MSGP);
	void* OnDG2ItemChanged(MSGP);
	void* OnBtnChange(MSGP);
	void* OnSelectorNotify(MSGP);
public:
	void DisplayPage(void);
	void QueryDataChange(UINT nSelRow,UINT nSelCol);
	void OrderDataChange(UINT nSelRow,UINT nSelCol);
	void VerifyDeleteDesired(void);
public:
	IMObject*		hWindow;
	IDBConnection*	hDBConnection;
	IMObject*		hOrdersPgCtl;
	IMObject*		hOrdersDGrid;
	IMObject*		hQueryDGrid;
	IMObject*		hQueryDGrid2;
	IMObject*		hQueryDGrid3;
	IMObject*		hQueryDGrid4;
	IDBRow*			hQueryDO[5];
	IArray*			hOrders;
	IMObject*		hErrStgCtl;
};

#define DO_ORDER		0
#define DO_PRODUCT		1
#define DO_CONTACT		2
#define DO_PAYMENT		3
#define DO_AKEY			4

//	==============================================================================
//	Page Class factory
//	==============================================================================

IMObject* MakeOrdersPage(IMObject* hWindow,IDBConnection* hDBConnection)
{
	IMObject* hPage = new COrdersPage(hWindow,hDBConnection);
	return hPage; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

COrdersPage::COrdersPage(IMObject* hWindowP,IDBConnection* hDBConnectionP)
{
	hWindow = hWindowP;
	hDBConnection = hDBConnectionP;
	hOrders = NULL;

	hQueryDO[DO_ORDER] = MakeOrderDO2();
	hQueryDO[DO_CONTACT] = MakeContactDO2();
	hQueryDO[DO_PAYMENT] = MakePaymentDO2();
	hQueryDO[DO_AKEY] = MakeAuthKeyDO2();
	hQueryDO[DO_PRODUCT] = MakeProductDO2();

	hOrdersDGrid = NULL;
	hQueryDGrid = NULL;
	hQueryDGrid2 = NULL;
	hQueryDGrid3 = NULL;
	hQueryDGrid4 = NULL;
	hErrStgCtl = NULL;
	hOrdersPgCtl = MAKE(CTRL_Container,NULL,hWindow,6000);
	hOrdersPgCtl->Msg(MSG_CtlShow,false);
	MAKE(CTRL_Build,hOrdersPgCtl,RscText(RSC_OrdersPageCfg));
}

//	==============================================================================
//	Destructor
//	==============================================================================

COrdersPage::~COrdersPage(void)
{
	hQueryDO[DO_ORDER]->Release();
	hQueryDO[DO_CONTACT]->Release();
	hQueryDO[DO_PAYMENT]->Release();
	hQueryDO[DO_AKEY]->Release();
	hQueryDO[DO_PRODUCT]->Release();

	if(hOrders)
		hOrders->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* COrdersPage::Msg(int nMsg,...)
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

void* COrdersPage::OnCtlShow(MSGP)
{
	MSGPARM(bool,fShow);

	//	show/hide page container control
	hOrdersPgCtl->Msg(MSG_CtlShow,fShow);

	if(fShow)
	{
		// get all Orders data from database
		hOrders = hDBConnection->DBOSelectAll(hQueryDO[DO_ORDER]);

		DisplayPage();
	}
	else
	{
		if(hOrdersDGrid)
			hOrdersDGrid->Msg(MSG_DG2Empty);

		if(hOrders)
			hOrders->Release();

		hOrders = NULL;
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Data Grid Item Changed
//	==============================================================================

void* COrdersPage::OnDG2ItemChanged(MSGP)
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

	case 620:	// Orders data grid control
		OrderDataChange(nSelRow,nSelCol);
		break;
	}

	//	redisplay the container to show changes
	hWindow->Msg(MSG_WinCtrlsChanged);
	return 0;
}

//	==============================================================================
//	display page
//	==============================================================================

static UINT DG1NrCols = 11;
static UINT DG1Obj[] = {DO_ORDER,DO_ORDER,DO_ORDER,DO_ORDER,DO_ORDER,DO_ORDER,
						DO_ORDER,DO_ORDER,DO_ORDER,DO_ORDER,DO_ORDER};
static UINT DG1FieldNr[] = {1,4,5,6,7, 8,9,10,11,14, 15};
static UINT DG1Wid[] = {60,80,110,110,80, 80,80,80,100,120, 80};

static UINT DG2NrCols = 8;
static UINT DG2Obj[] = {DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,
						DO_CONTACT,DO_CONTACT};
static UINT DG2FieldNr[] = {2,3,4,5,6,7,12,11};
static UINT DG2Wid[] = {200,100,160,70,70,50,180,160};

static UINT DG3NrCols = 8;
static UINT DG3Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT,DO_CONTACT};
static UINT DG3FieldNr[] = {3,4,5,6,7, 8,9,10};
static UINT DG3Wid[] = {200,200,200,200,100, 60,60,100};

static UINT DG4NrCols = 5;
static UINT DG4Obj[] = {DO_PRODUCT,DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY};
static UINT DG4FieldNr[] = {4,4,5,6,7};
static UINT DG4Wid[] = {240,200,200,200,200};

void COrdersPage::DisplayPage(void)
{
	hErrStgCtl = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,614);

	IMObject* hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,611);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,612);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,613);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn->Msg(MSG_CtlSetBkColor,0xff0000ff);
	hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,619);
	hBtn->Msg(MSG_CtlSetNotify,this);

	if(hQueryDGrid)
		hQueryDGrid->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,610);
		UINT nBkColor = (UINT)hOrdersPgCtl->Msg(MSG_CtlBkgndColor);
		hQueryDGrid->Msg(MSG_CtlSetBkColor,nBkColor);
		hQueryDGrid->Msg(MSG_CtlSetNotify,this);
	}

	if(hQueryDGrid2)
		hQueryDGrid2->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid2 = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,630);
		UINT nBkColor = (UINT)hOrdersPgCtl->Msg(MSG_CtlBkgndColor);
		hQueryDGrid2->Msg(MSG_CtlSetBkColor,nBkColor);
		hQueryDGrid2->Msg(MSG_CtlSetNotify,this);
	}

	if(hQueryDGrid3)
		hQueryDGrid3->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid3 = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,640);
		UINT nBkColor = (UINT)hOrdersPgCtl->Msg(MSG_CtlBkgndColor);
		hQueryDGrid3->Msg(MSG_CtlSetBkColor,nBkColor);
		hQueryDGrid3->Msg(MSG_CtlSetNotify,this);
	}

	if(hQueryDGrid4)
		hQueryDGrid4->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid4 = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,650);
		UINT nBkColor = (UINT)hOrdersPgCtl->Msg(MSG_CtlBkgndColor);
		hQueryDGrid4->Msg(MSG_CtlSetBkColor,nBkColor);
		hQueryDGrid4->Msg(MSG_CtlSetNotify,this);
	}

	//	------------------------------------------------------------------------------
	//	Query DGrid
	//	------------------------------------------------------------------------------

	// define columns
	hQueryDGrid->Msg(MSG_DG2AddRows,0,2);
	hQueryDGrid->Msg(MSG_DG2AddColumns,0,DG1NrCols);
	for(UINT ixCol=0;ixCol<DG1NrCols;ixCol++)
	{
		hQueryDGrid->Msg(MSG_DG2SetColumnWidth,ixCol,DG1Wid[ixCol]);
	}

	//	define column headings & link text cells
	for(UINT ixcol=0;ixcol<DG1NrCols;ixcol++)
	{
		hQueryDGrid->Msg(MSG_DG2SetItemText,0,ixcol,hQueryDO[DG1Obj[ixcol]]->DBOColLabel(DG1FieldNr[ixcol]));
		hQueryDGrid->Msg(MSG_DG2SetItemReadOnly,0,ixcol,true);
		hQueryDGrid->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol,true);
		hQueryDGrid->Msg(MSG_DG2SetItemText,1,ixcol,hQueryDO[DG1Obj[ixcol]]->DBOText(DG1FieldNr[ixcol]));
	}

	//	label divider
	hQueryDGrid->Msg(MSG_DG2SetItemDivider,0,0,false,true);

	//	------------------------------------------------------------------------------
	//	Query DGrid 2
	//	------------------------------------------------------------------------------

	// define columns
	hQueryDGrid2->Msg(MSG_DG2AddRows,0,2);
	hQueryDGrid2->Msg(MSG_DG2AddColumns,0,DG2NrCols);
	for(UINT ixCol=0;ixCol<DG2NrCols;ixCol++)
	{
		hQueryDGrid2->Msg(MSG_DG2SetColumnWidth,ixCol,DG2Wid[ixCol]);
	}

	//	define column headings & link text cells
	for(UINT ixcol=0;ixcol<DG2NrCols;ixcol++)
	{
		hQueryDGrid2->Msg(MSG_DG2SetItemText,0,ixcol,hQueryDO[DG2Obj[ixcol]]->DBOColLabel(DG2FieldNr[ixcol]));
		hQueryDGrid2->Msg(MSG_DG2SetItemReadOnly,0,ixcol,true);
		hQueryDGrid2->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol,true);
		hQueryDGrid2->Msg(MSG_DG2SetItemText,1,ixcol,hQueryDO[DG2Obj[ixcol]]->DBOText(DG2FieldNr[ixcol]));
	}

	//	label divider
	hQueryDGrid2->Msg(MSG_DG2SetItemDivider,0,0,false,true);

	//	------------------------------------------------------------------------------
	//	Query DGrid 3
	//	------------------------------------------------------------------------------

	// define columns
	hQueryDGrid3->Msg(MSG_DG2AddRows,0,2);
	hQueryDGrid3->Msg(MSG_DG2AddColumns,0,DG3NrCols);
	for(UINT ixCol=0;ixCol<DG3NrCols;ixCol++)
	{
		hQueryDGrid3->Msg(MSG_DG2SetColumnWidth,ixCol,DG3Wid[ixCol]);
	}

	//	define column headings & link text cells
	for(UINT ixcol=0;ixcol<DG3NrCols;ixcol++)
	{
		hQueryDGrid3->Msg(MSG_DG2SetItemText,0,ixcol,hQueryDO[DG3Obj[ixcol]]->DBOColLabel(DG3FieldNr[ixcol]));
		hQueryDGrid3->Msg(MSG_DG2SetItemReadOnly,0,ixcol,true);
		hQueryDGrid3->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol,true);
		hQueryDGrid3->Msg(MSG_DG2SetItemText,1,ixcol,hQueryDO[DG3Obj[ixcol]]->DBOText(DG3FieldNr[ixcol]));
	}

	//	label divider
	hQueryDGrid3->Msg(MSG_DG2SetItemDivider,0,0,false,true);

	//	------------------------------------------------------------------------------
	//	Query DGrid 4
	//	------------------------------------------------------------------------------

	// define columns
	hQueryDGrid4->Msg(MSG_DG2AddRows,0,2);
	hQueryDGrid4->Msg(MSG_DG2AddColumns,0,DG4NrCols);
	for(UINT ixCol=0;ixCol<DG4NrCols;ixCol++)
	{
		hQueryDGrid4->Msg(MSG_DG2SetColumnWidth,ixCol,DG4Wid[ixCol]);
	}

	//	define column headings & link text cells
	for(UINT ixcol=0;ixcol<DG4NrCols;ixcol++)
	{
		hQueryDGrid4->Msg(MSG_DG2SetItemText,0,ixcol,hQueryDO[DG4Obj[ixcol]]->DBOColLabel(DG4FieldNr[ixcol]));
		hQueryDGrid4->Msg(MSG_DG2SetItemReadOnly,0,ixcol,true);
		hQueryDGrid4->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol,true);
		hQueryDGrid4->Msg(MSG_DG2SetItemText,1,ixcol,hQueryDO[DG4Obj[ixcol]]->DBOText(DG4FieldNr[ixcol]));
	}

	//	label divider
	hQueryDGrid4->Msg(MSG_DG2SetItemDivider,0,0,false,true);

	//	------------------------------------------------------------------------------
	//	Orders DGrid
	//	------------------------------------------------------------------------------

	if(hOrdersDGrid)
		hOrdersDGrid->Msg(MSG_DG2Empty);
	else
	{
		hOrdersDGrid = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,620);
		UINT nBkColor = (UINT)hOrdersPgCtl->Msg(MSG_CtlBkgndColor);
		hOrdersDGrid->Msg(MSG_CtlSetBkColor,nBkColor);
		hOrdersDGrid->Msg(MSG_CtlSetNotify,this);
	}

	// define columns
	hOrdersDGrid->Msg(MSG_DG2AddRows,0,1);
	hOrdersDGrid->Msg(MSG_DG2AddColumns,0,DG1NrCols);
	for(UINT ixCol=0;ixCol<DG1NrCols;ixCol++)
	{
		hOrdersDGrid->Msg(MSG_DG2SetColumnWidth,ixCol,DG1Wid[ixCol]);
	}

	//	define column headings
	for(UINT ixcol=0;ixcol<DG1NrCols;ixcol++)
	{
		hOrdersDGrid->Msg(MSG_DG2SetItemText,0,ixcol,hQueryDO[DG1Obj[ixcol]]->DBOColLabel(DG1FieldNr[ixcol]));
		hOrdersDGrid->Msg(MSG_DG2SetItemReadOnly,0,ixcol,true);
		hOrdersDGrid->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol,true);
	}

	//	display the data returned from the Orders database

	if(hOrders)
	{
		UINT nOrders = hOrders->AryCount();

		for(UINT ix=1;ix<=nOrders;ix++)
		{
			IDBRow* hDO;
			hOrders->AryGetObj(ix-1,(IMObject**)&hDO);

			hOrdersDGrid->Msg(MSG_DG2AddRows,ix,1);

			for(UINT ixcol=0;ixcol<DG1NrCols;ixcol++)
			{
				hOrdersDGrid->Msg(MSG_DG2SetItemText,ix,ixcol,hDO->DBOText(DG1FieldNr[ixcol]));
				hOrdersDGrid->Msg(MSG_DG2SetItemNotifyOnly,ix,ixcol,true);
				hOrdersDGrid->Msg(MSG_DG2SetItemDelOnlyBkgnd,ix,ixcol,true);
			}
		}
	}
}

//	==============================================================================
//	query data item changed
//	==============================================================================

void COrdersPage::QueryDataChange(UINT nSelRow,UINT nSelCol)
{
	//	update edited text
	WCHAR* pText = (WCHAR*)hQueryDGrid->Msg(MSG_DG2GetItemText,nSelRow,nSelCol);

	IDBRow* hQDO = hQueryDO[DO_ORDER];
	hQDO->DBOSetText(DG1FieldNr[nSelCol],pText);
}

//	==============================================================================
//	Order data item changed
//	==============================================================================

void COrdersPage::OrderDataChange(UINT nSelRow,UINT nSelCol)
{
	if(hOrders)
	{
		WCHAR* pText;

		// copy selected order to QUERY data objects
		IDBRow* hDO;
		hOrders->AryGetObj(nSelRow-1,(IMObject**)&hDO);
		UINT nrCols = hDO->DBONrCols();

		//	copy row to DO_ORDER
		for(UINT ixcol=0;ixcol<nrCols;ixcol++)
		{
			WCHAR* pText = hDO->DBOText(ixcol);
			hQueryDO[DO_ORDER]->DBOSetText(ixcol,pText);
		}

		//	copy contact data
		pText = hQueryDO[DO_ORDER]->DBOText(2);	// customer id
		hQueryDO[DO_CONTACT]->DBOClear();
		hQueryDO[DO_CONTACT]->DBOSetText(1,pText);
		hDBConnection->DBOFind(hQueryDO[DO_CONTACT]);

		//	copy payment data
		pText = hQueryDO[DO_ORDER]->DBOText(3);	// payment id
		hQueryDO[DO_PAYMENT]->DBOClear();
		hQueryDO[DO_PAYMENT]->DBOSetText(1,pText);
		hDBConnection->DBOFind(hQueryDO[DO_PAYMENT]);

		//	copy authorization data
		pText = hQueryDO[DO_ORDER]->DBOText(1);	// order number
		hQueryDO[DO_AKEY]->DBOClear();
		hQueryDO[DO_AKEY]->DBOSetText(1,pText);
		hDBConnection->DBOFind(hQueryDO[DO_AKEY]);

		//	copy product data
		pText = hQueryDO[DO_ORDER]->DBOText(5);	// product SKU
		hQueryDO[DO_PRODUCT]->DBOClear();
		hQueryDO[DO_PRODUCT]->DBOSetText(1,pText);
		hDBConnection->DBOFind(hQueryDO[DO_PRODUCT]);

		//	display changes
		DisplayPage();
		hWindow->Msg(MSG_WinCtrlsChanged);
	}
}

//	==============================================================================
//	Selector notification
//	==============================================================================

static WCHAR szDelOk[4096];

void COrdersPage::VerifyDeleteDesired(void)
{
	IDBRow* hQDO = hQueryDO[DO_ORDER];

	if(hQDO->DBOStr(1)->SLength())
	{
		//	verify ok to delete
		szDelOk[0] = szDelOk[1] = 0;
		IStr* hTemp = MakeStr();
		hTemp->Msg(MSG_SFormat,L"ss",L"Ok to delete record %1 = %2 shown above",
			hQDO->DBOColName(1),hQDO->DBOText(1));
		hSvc->SvcListAppendStr(szDelOk,hTemp->STextPtr());
		hSvc->SvcListAppendStr(szDelOk,L"Do NOT Delete Anything!");

		MOUSEPKT* ppkt = (MOUSEPKT*)hWinMgr->Msg(MSG_GetMousePkt);
		IMObject* hSelector = (IMObject*)MakePopupSelR(999,
			this,hOrdersDGrid,ppkt->x,ppkt->y,
			szDelOk,NULL,NULL);
		hSelector->Msg(MSG_CtlSetSelColors,COLOR_WHITE,COLOR_RED);
		hSelector->Msg(MSG_SelMarkItem,hSvc->SvcListGetStr(szDelOk,1));
	}
}

void* COrdersPage::OnSelectorNotify(MSGP)
{
	MSGPARM(UINT,nItem);
	MSGPARMPTR(void*,parm1);
	MSGPARMPTR(void*,parm2);
	MSGPARMPTR(UINT,SelectorOId);

	if(nItem==1)	// 0=selector cancelled, 1=delete, 2=cancel
	{
		IDBRow* hQDO = hQueryDO[DO_ORDER];
		if(hDBConnection->DBODelete(hQDO))
		{
			// record was deleted:
			hQDO->DBOClear();
			//	update list of all Orders
			if(hOrders)
				hOrders->Release();
			hOrders = hDBConnection->DBOSelectAll(hQDO);
			DisplayPage();
		}
	}

	//	clear the delete button	from button down state
	IMObject* hBtn = (IMObject*)hOrdersPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_BtnSetState,false);

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	process button change
//	==============================================================================

void* COrdersPage::OnBtnChange(MSGP)
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
		hQueryDO[DO_ORDER]->Msg(MSG_DBOClear);
		DisplayPage();
		break;

	case 612:	// find records (select)
		{
			hNewResults = hDBConnection->DBOSelect(hQueryDO[DO_ORDER]);
			if(hNewResults)
			{
				if(hOrders)
					hOrders->Release();
				hOrders = hNewResults;
				DisplayPage();
			}
		}
		break;

	case 613:	// add query DO to Orders table
		if(hQueryDO[DO_ORDER]->DBOValidate(NULL))
		{
			if(hDBConnection->DBOAdd(hQueryDO[DO_ORDER]))
			{
				//	update list of all Orders
				if(hOrders)
					hOrders->Release();
				hOrders = hDBConnection->DBOSelectAll(hQueryDO[DO_ORDER]);
			}
			DisplayPage();
		}
		break;

	case 619:	// show everything
		if(hOrders)
			hOrders->Release();
		hOrders = hDBConnection->DBOSelectAll(hQueryDO[DO_ORDER]);
		DisplayPage();
		break;
	}

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
