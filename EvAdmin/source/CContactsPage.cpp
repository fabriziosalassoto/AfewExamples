//	==============================================================================
//	CContactsPage - Contacts page singleton object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

IDBRow* MakeContactDO2(void);

//	==============================================================================
//	Class Definition
//	==============================================================================

class CContactsPage : public IMObject
{
public:
	CContactsPage(IMObject* hWindow,IDBConnection* hDBConnection);
	~CContactsPage(void);
	virtual void* Msg(int nMsg,...);
	void* OnCtlShow(MSGP);
	void* OnDG2ItemChanged(MSGP);
	void* OnBtnChange(MSGP);
	void* OnSelectorNotify(MSGP);
public:
	void DisplayPage(void);
	void QueryDataChange(UINT nSelRow,UINT nSelCol);
	void ContactDataChange(UINT nSelRow,UINT nSelCol);
	void VerifyDeleteDesired(void);
public:
	IMObject*		hWindow;
	IDBConnection*	hDBConnection;
	IMObject*		hContactsPgCtl;
	IMObject*		hContactsDGrid;
	IMObject*		hQueryDGrid;
	IDBRow*			hQueryDO;
	IArray*			hContacts;
	IMObject*		hErrStgCtl;
};

//	==============================================================================
//	Page Class factory
//	==============================================================================

IMObject* MakeContactsPage(IMObject* hWindow,IDBConnection* hDBConnection)
{
	IMObject* hPage = new CContactsPage(hWindow,hDBConnection);
	return hPage; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CContactsPage::CContactsPage(IMObject* hWindowP,IDBConnection* hDBConnectionP)
{
	hWindow = hWindowP;
	hDBConnection = hDBConnectionP;
	hContacts = NULL;
	hQueryDO = MakeContactDO2();
	hContactsDGrid = NULL;
	hQueryDGrid = NULL;
	hErrStgCtl = NULL;
	hContactsPgCtl = MAKE(CTRL_Container,NULL,hWindow,7000);
	hContactsPgCtl->Msg(MSG_CtlShow,false);
	MAKE(CTRL_Build,hContactsPgCtl,RscText(RSC_ContactsPageCfg));
}

//	==============================================================================
//	Destructor
//	==============================================================================

CContactsPage::~CContactsPage(void)
{
	hQueryDO->Release();
	if(hContacts)
		hContacts->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CContactsPage::Msg(int nMsg,...)
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

void* CContactsPage::OnCtlShow(MSGP)
{
	MSGPARM(bool,fShow);

	//	show/hide page container control
	hContactsPgCtl->Msg(MSG_CtlShow,fShow);

	if(fShow)
	{
		// get all Contacts data from database
		hContacts = hDBConnection->DBOSelectAll(hQueryDO);

		DisplayPage();
	}
	else
	{
		if(hContactsDGrid)
			hContactsDGrid->Msg(MSG_DG2Empty);

		if(hContacts)
			hContacts->Release();

		hContacts = NULL;
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Data Grid Item Changed
//	==============================================================================

void* CContactsPage::OnDG2ItemChanged(MSGP)
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

	case 620:	// Contacts data grid control
		ContactDataChange(nSelRow,nSelCol);
		break;
	}

	//	redisplay the container to show changes
	hWindow->Msg(MSG_WinCtrlsChanged);
	return 0;
}

//	==============================================================================
//	display page
//	==============================================================================

static UINT nColWidths[] = {50,80,110,140,140, 100,80,50,50,80, 140,100,50,40};

void CContactsPage::DisplayPage(void)
{
	UINT nrCols = hQueryDO->DBONrCols();

	hErrStgCtl = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,614);

	IMObject* hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,611);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,612);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,613);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_CtlSetNotify,this);
	hBtn->Msg(MSG_CtlSetBkColor,0xff0000ff);
	hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,619);
	hBtn->Msg(MSG_CtlSetNotify,this);

	if(hQueryDGrid)
		hQueryDGrid->Msg(MSG_DG2Empty);
	else
	{
		hQueryDGrid = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,610);
		UINT nBkColor = (UINT)hContactsPgCtl->Msg(MSG_CtlBkgndColor);
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

	if(hContactsDGrid)
		hContactsDGrid->Msg(MSG_DG2Empty);
	else
	{
		hContactsDGrid = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,620);
		UINT nBkColor = (UINT)hContactsPgCtl->Msg(MSG_CtlBkgndColor);
		hContactsDGrid->Msg(MSG_CtlSetBkColor,nBkColor);
		hContactsDGrid->Msg(MSG_CtlSetNotify,this);
	}

	// define columns
	hContactsDGrid->Msg(MSG_DG2AddRows,0,1);
	hContactsDGrid->Msg(MSG_DG2AddColumns,0,nrCols);
	for(UINT ixCol=0;ixCol<nrCols;ixCol++)
	{
		hContactsDGrid->Msg(MSG_DG2SetColumnWidth,ixCol,nColWidths[ixCol]);
	}

	//	define column headings
	for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
	{
		hContactsDGrid->Msg(MSG_DG2SetItemText,0,ixcol-1,hQueryDO->DBOColLabel(ixcol));
		hContactsDGrid->Msg(MSG_DG2SetItemReadOnly,0,ixcol-1,true);
		hContactsDGrid->Msg(MSG_DG2SetItemGreyBkgnd,0,ixcol-1,true);
	}

	//	display the data returned from the Contacts database

	if(hContacts)
	{
		UINT nContacts = hContacts->AryCount();

		for(UINT ix=1;ix<=nContacts;ix++)
		{
			IDBRow* hDO;
			hContacts->AryGetObj(ix-1,(IMObject**)&hDO);

			hContactsDGrid->Msg(MSG_DG2AddRows,ix,1);

			for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
			{
				hContactsDGrid->Msg(MSG_DG2SetItemText,ix,ixcol-1,hDO->DBOText(ixcol));
				if(ixcol==1)
				{
					hContactsDGrid->Msg(MSG_DG2SetItemNotifyOnly,ix,0,true);
					hContactsDGrid->Msg(MSG_DG2SetItemRdOnlyBkgnd,ix,0,true);
				}
			}
		}
	}
}

//	==============================================================================
//	query data item changed
//	==============================================================================

void CContactsPage::QueryDataChange(UINT nSelRow,UINT nSelCol)
{
	//	update edited text
	WCHAR* pText = (WCHAR*)hQueryDGrid->Msg(MSG_DG2GetItemText,nSelRow,nSelCol);

	hQueryDO->DBOSetText(nSelCol+1,pText);
}

//	==============================================================================
//	Contact data item changed
//	==============================================================================

void CContactsPage::ContactDataChange(UINT nSelRow,UINT nSelCol)
{
	if(hContacts)
	{
		IDBRow* hDO;
		hContacts->AryGetObj(nSelRow-1,(IMObject**)&hDO);

		//	update edited text
		WCHAR* pText = (WCHAR*)hContactsDGrid->Msg(MSG_DG2GetItemText,nSelRow,nSelCol);

		if(nSelCol == 0) // click on SKU - copy object to QUERY data object
		{
			UINT nrCols = hQueryDO->DBONrCols();
			for(UINT ixcol=1;ixcol<=nrCols;ixcol++)
			{
				pText = (WCHAR*)hContactsDGrid->Msg(MSG_DG2GetItemText,nSelRow,ixcol-1);
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

void CContactsPage::VerifyDeleteDesired(void)
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
			this,hContactsDGrid,ppkt->x,ppkt->y,
			szDelOk,NULL,NULL);
		hSelector->Msg(MSG_CtlSetSelColors,COLOR_WHITE,COLOR_RED);
		hSelector->Msg(MSG_SelMarkItem,hSvc->SvcListGetStr(szDelOk,1));
	}
}

void* CContactsPage::OnSelectorNotify(MSGP)
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
			//	update list of all Contacts
			if(hContacts)
				hContacts->Release();
			hContacts = hDBConnection->DBOSelectAll(hQueryDO);
			DisplayPage();
		}
	}

	//	clear the delete button	from button down state
	IMObject* hBtn = (IMObject*)hContactsPgCtl->Msg(MSG_DbFindChildId,615);
	hBtn->Msg(MSG_BtnSetState,false);

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	process button change
//	==============================================================================

void* CContactsPage::OnBtnChange(MSGP)
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
				if(hContacts)
					hContacts->Release();
				hContacts = hNewResults;
				DisplayPage();
			}
		}
		break;

	case 613:	// add query DO to Contacts table
		if(hQueryDO->DBOValidate(NULL))
		{
			if(hDBConnection->DBOAdd(hQueryDO))
			{
				//	update list of all Contacts
				if(hContacts)
					hContacts->Release();
				hContacts = hDBConnection->DBOSelectAll(hQueryDO);
			}
			DisplayPage();
		}
		break;

	case 619:	// show everything
		if(hContacts)
			hContacts->Release();
		hContacts = hDBConnection->DBOSelectAll(hQueryDO);
		DisplayPage();
		break;
	}

	//	redisplay the container to show changes
	hErrStgCtl->Msg(MSG_LblSetText,hDBConnection->DBErrorStg());
	hWindow->Msg(MSG_WinCtrlsChanged);
	return IM_RTN_NOTHING;
}

//	==============================================================================
