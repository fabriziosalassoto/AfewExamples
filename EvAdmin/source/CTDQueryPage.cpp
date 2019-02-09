//	==============================================================================
//	CTDQueryPage.cpp - standard table-driven query page object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"
#include "CTDQueryPage.h"

IDBRow* MakeProductDO(void);
IDBRow* MakeContactDO(void);
IDBRow* MakeAuthKeyDO(void);
IDBRow* MakeOrderReqDO(void);
IDBRow* MakeDiscountDO(void);
IDBRow* MakeControlDO(void);

#define IDClearBtn			611
#define IDFindBtn			612
#define IDAddBtn			613
#define IDStatusLbl			614
#define IDUpdateBtn			615
#define IDDeleteBtn			616
#define IDShowOpenBtn		618
#define IDShowCompleteBtn	619

#define IDMarkPaidBtn		680
#define IDEMailKeysBtn		681
#define IDMarkShippedBtn	682
#define IDMarkInvoicedBtn	683

#define IDEMailKeysBtn2		699

IMObject* MakeSendEMail(IMObject* hNotify,WCHAR* hTo,WCHAR* hSubject,IStr* hBody);

//	==============================================================================
//	Class Definition
//	==============================================================================

#define MAXDGRIDS	11
#define MAXQDO		6

class CTDQueryPage : public IControl
{
public:
	CTDQueryPage(IWindow* hWindow,IDBConnection* hDBConnection,
		QLINETABLE** ppPageDefTable,QLINK** ppQLinkTable,
		UINT nRscPageCfg,UINT ixSelectorDO,int ixColPrimary,int nGroupField);
	~CTDQueryPage(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnCtlShow(MSGP);
	void* OnDGItemChanged(MSGP);
	void* OnBtnChange(MSGP);
	void* OnSelectorNotify(MSGP);
	void* OnFindRecord(MSGP);
	void* OnEMailSendNotify(MSGP);
public:
	void DisplayPage(void);
	void VerifyDesired(UINT nAction);
	void ProcessFlags(UINT flags,IDataGridCtl* hDGCtl,UINT ixRow,UINT ixCol);
	void UpdateQLinks(void);
	void DisplayOrderStatus(void);
	void UpdateOrderStatus(void);
	void MarkPaid(void);
	void MarkInvoiced(void);
	void SendKeysEMail(void);
	void SendKeysEMail2(void);
	void MarkShipped(void);
	void SwitchToOrderReq(void);
	void ProcessCompletedOrder(void);
public:
	IWindow*		hWindow;
	IControl*		hPgCtl;
	ILabelCtl*		hErrStgCtl;

	IDBConnection*	hDBConnection;
	QLINETABLE**	ppPageDefTable;
	QLINK**			ppQLinkTable;

	IStr*			hSKU;

	bool			fFindActive;		// true if finding, false = show all records

	IDBRow*			hQueryDO[MAXQDO];	//one per type for query DGrid use
	UINT			ixSelectorDO;		// the data object used fill row selector data grid
	int				nGroupField;		// selector field to group with thick underlines
	int				ixColPrimary;		// selector DGrid column containing primary key

	UINT			nDGrids;			// number of specified DGrid controls
	IDataGridCtl*	hDGrid[MAXDGRIDS];	// 0 = row selector, 1..9 are query dgrids
	UINT			ixSelectorDG;		// the last DGrid is a multi-row selector
	UINT			idSelectorDG;		// control id of row selector control

	bool			fNeedPayment;
	bool			fNeedEMailKeys;
	bool			fNeedShipping;
	bool			fNeedInvoice;
	bool			fIsTestIPN;

	bool			fOpenOnly;
};

//	==============================================================================
//	Page Class factory
//	==============================================================================

IControl* MakeTDQueryPage(IWindow* hWindow,IDBConnection* hDBConnection,
						  QLINETABLE** ppPageDefTable,QLINK** ppQLinkTable,
						  UINT nRscPageCfg,UINT ixSelectorDO,
						  int ixColPrimary,int nGroupField)
{
	IControl* hPage = (IControl*)new CTDQueryPage(hWindow,hDBConnection,
		ppPageDefTable,ppQLinkTable,nRscPageCfg,ixSelectorDO,ixColPrimary,nGroupField);
	return hPage; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CTDQueryPage::CTDQueryPage(IWindow* hWindowP,IDBConnection* hDBConnectionP,
						   QLINETABLE** ppPageDefTableP,QLINK** ppQLinkTableP,
						   UINT nRscPageCfg,UINT ixSelectorDOP,
						   int ixColPrimaryP,int nGroupFieldP)
{
	ppPageDefTable = ppPageDefTableP;
	ppQLinkTable = ppQLinkTableP;
	ixSelectorDO = ixSelectorDOP;
	ixColPrimary = ixColPrimaryP;
	hWindow = hWindowP;
	hDBConnection = hDBConnectionP;
	nDGrids = 0;
	nGroupField = nGroupFieldP;
	idSelectorDG = 600;
	fFindActive = false;
	fOpenOnly = true;
	hSKU = MakeStr();

	hQueryDO[DO_CONTACT] = MakeContactDO();
	hQueryDO[DO_AKEY] = MakeAuthKeyDO();
	hQueryDO[DO_PRODUCT] = MakeProductDO();
	hQueryDO[DO_ORDREQ] = MakeOrderReqDO();
	hQueryDO[DO_DISCOUNT] = MakeDiscountDO();
	hQueryDO[DO_CTRL] = MakeControlDO();

	for(UINT ix=0;ix<MAXDGRIDS;ix++)
		hDGrid[ix] = NULL;

	hErrStgCtl = NULL;
	hPgCtl = MakeContainer(hWindow,6000);
	hPgCtl->CtlShow(false);
	hMakeMgr->CtrlBuild(hPgCtl,RscText(nRscPageCfg));
}

//	==============================================================================
//	Destructor
//	==============================================================================

CTDQueryPage::~CTDQueryPage(void)
{
	hQueryDO[DO_CONTACT]->Release();
	hQueryDO[DO_AKEY]->Release();
	hQueryDO[DO_PRODUCT]->Release();
	hQueryDO[DO_ORDREQ]->Release();
	hQueryDO[DO_DISCOUNT]->Release();
	hQueryDO[DO_CTRL]->Release();
	hSKU->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CTDQueryPage::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_CtlShow: return OnCtlShow(MPPTR);
	case MSG_DGItemChanged: return OnDGItemChanged(MPPTR);
	case MSG_BtnSelectChange: return OnBtnChange(MPPTR);
	case MSG_CtlSelectorNotify: return OnSelectorNotify(MPPTR);
	case MSG_EvaFindRecord: return OnFindRecord(MPPTR);
	case MSG_EvaEMailSendNotify: return OnEMailSendNotify(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	show or hide page
//	==============================================================================

void* CTDQueryPage::OnCtlShow(MSGP)
{
	MPARMBOOL(bool,fShow);

	//	show/hide page container control
	hPgCtl->CtlShow(fShow);

	if(fShow)
	{
		DisplayPage();
	}
	else
	{
		//	recover memory used for datagrid cells
		for(UINT ix=0;ix<MAXDGRIDS;ix++)
		{
			if(hDGrid[ix])
				hDGrid[ix]->DGEmpty();
		}
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	display page
//	==============================================================================

void CTDQueryPage::ProcessFlags(UINT flags,IDataGridCtl* hDGCtl,UINT ixRow,UINT ixCol)
{
	//	process column flags
	if(flags & DGCF_BOLD)
		hDGCtl->DGSetItemBold(ixRow,ixCol,true);
	if(flags & DGCF_ARIGHT)
		hDGCtl->DGSetItemRightAlign(ixRow,ixCol,true);
	if(flags & DGCF_RO)
	{
		hDGCtl->DGSetItemReadOnly(ixRow,ixCol,true);
		hDGCtl->DGSetItemGreyBkgnd(ixRow,ixCol,true);
	}
}

void CTDQueryPage::UpdateQLinks(void)
{
	QLINK** ppQLScan = ppQLinkTable;
	QLINK* pQLink;

	//	follow each linked id field to fill other data objects
	while(pQLink = *ppQLScan++)
	{
		// get the key to find the related data object
		WCHAR* pText = hQueryDO[pQLink->ixDODef]->DBaseOText(pQLink->ixDOField);
		hQueryDO[pQLink->ixDOFind]->DBaseOClear();
		UINT nPrimaryField = hQueryDO[pQLink->ixDOFind]->DBaseOPrimaryKeyIdx();
		hQueryDO[pQLink->ixDOFind]->DBaseOSetText(nPrimaryField,pText);
		hDBConnection->DBaseOFind(hQueryDO[pQLink->ixDOFind]);
	}
}

void CTDQueryPage::DisplayOrderStatus(void)
{
	IButtonCtl* hBtn;

	fNeedPayment = false;
	fNeedEMailKeys = false;
	fNeedShipping = false;
	fNeedInvoice = false;
	fIsTestIPN = false;

	WCHAR* pFlags = hQueryDO[DO_ORDREQ]->DBaseOText(23);
	WCHAR c;
	while(c = *pFlags++)
	{
		switch(c)
		{
		case 'S': fNeedShipping = true; break;
		case 'P': fNeedPayment = true; break;
		case 'E': fNeedEMailKeys = true; break;
		case 'Q': fNeedInvoice = true; break;
		case 'T': fIsTestIPN = true; break;
		};
	}

	//	display payment needed?
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDMarkPaidBtn);
	if(hBtn)
		hBtn->CtlSetNotify(this);
	if(fNeedPayment)
	{
		hBtn->CtlSetBkColor(0xff1ed0eb); 
		hBtn->CtlShow(true);
	}
	else
		hBtn->CtlShow(false);

	//	display email needed?
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDEMailKeysBtn);
	if(hBtn)
		hBtn->CtlSetNotify(this);
	if(fNeedEMailKeys)
	{
		hBtn->CtlSetBkColor(0xff1ed0eb); 
		hBtn->CtlShow(true);
	}
	else
	{
		hBtn->CtlSetBkColor(0xffe0e0e0); 
		hBtn->CtlShow(true);
	}

	//	display shipping needed?
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDMarkShippedBtn);
	if(hBtn)
		hBtn->CtlSetNotify(this);
	if(fNeedShipping)
	{
		hBtn->CtlSetBkColor(0xff1ed0eb); 
		hBtn->CtlShow(true);
	}
	else
		hBtn->CtlShow(false);

	//	display invoice needed?
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDMarkInvoicedBtn);
	if(hBtn)
		hBtn->CtlSetNotify(this);
	if(fNeedInvoice)
	{
		hBtn->CtlSetBkColor(0xff1ed0eb); 
		hBtn->CtlShow(true);
		hBtn->CtlSetNotify(this);
	}
	else
		hBtn->CtlShow(false);

	//	once paid, or keys sent, order cannot be changed or deleted
//	bool fShow = (fNeedPayment && fNeedEMailKeys);
	bool fShow = true;

	//	only show if there is a selected order request displayed
	WCHAR* pReqId = hQueryDO[DO_ORDREQ]->DBaseOText(1);
	if(!pReqId)
		fShow = false;

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDDeleteBtn);
	hBtn->CtlShow(fShow);
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDUpdateBtn);
	hBtn->CtlShow(fShow);
}

void CTDQueryPage::DisplayPage(void)
{
	//	locate and initialize standard buttons and labels
	hErrStgCtl = (ILabelCtl*)hPgCtl->DbFindChildId(IDStatusLbl);
//	hErrStgCtl->CtlSetBkColor(0xfff0e8e0);
	hErrStgCtl->CtlSetTxColor(0xff0000ff);

	IButtonCtl* hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDClearBtn);
	hBtn->CtlSetNotify(this);

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDFindBtn);
	hBtn->CtlSetNotify(this);

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDAddBtn);
	if(hBtn)
	{
		hBtn->CtlSetNotify(this);
		hBtn->CtlSetBkColor(0xff1ed0eb);   
	}

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDUpdateBtn);
	if(hBtn)
	{
		hBtn->CtlSetNotify(this);
		hBtn->CtlSetBkColor(0xff2c7afb);
	}

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDDeleteBtn);
	if(hBtn)
	{
		hBtn->CtlSetNotify(this);
		hBtn->CtlSetBkColor(0xff0000ff);
	}

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDShowOpenBtn);
	hBtn->CtlSetNotify(this);

	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDShowCompleteBtn);
	hBtn->CtlSetNotify(this);

	//	resend email (authkeys page) button
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDEMailKeysBtn2);
	if(hBtn)
	{
		hBtn->CtlSetNotify(this);
		hBtn->CtlSetBkColor(0xff1ed0eb); 
	}

	//	create and count or reset all datagrids
	nDGrids = 0;
	IDataGridCtl* hDGCtl = NULL;

	UINT nDGCols;
	UINT* pColWids;
	UINT* pFieldIdx;
	UINT* pDOIdx;
	UINT* pFlags;

	QLINETABLE*	pPDTable;
	QLINETABLE** pPDScan = ppPageDefTable;
	while(pPDTable = *pPDScan++)
	{
		UINT nBkColor = (UINT)hPgCtl->CtlBkgndColor();
		hDGCtl = hDGrid[nDGrids];

		if(hDGCtl)
		{
			//	reset the data grid control to empty
			hDGCtl->DGEmpty();
		}
		else
		{
			//	create the data grid control
			idSelectorDG += 10;
			hDGCtl = hDGrid[nDGrids] = (IDataGridCtl*)hPgCtl->DbFindChildId(idSelectorDG);
			hDGCtl->CtlSetBkColor(nBkColor);
			hDGCtl->CtlSetNotify(this);
			hDGCtl->DGTabHz(true);
		}

		//	define the columns of the data grid control
		nDGCols = pPDTable->nrCols;
		pColWids = pPDTable->pColWid;
		pFieldIdx = pPDTable->pFieldNr;
		pDOIdx = pPDTable->pDOIdx;
		pFlags = pPDTable->pColFlags;

		hDGCtl->DGAddRows(0,2);
		hDGCtl->DGAddColumns(0,nDGCols);

		for(UINT ixCol=0;ixCol<nDGCols;ixCol++)
		{
			//	set the column width
			hDGCtl->DGSetColumnWidth(ixCol,pColWids[ixCol]);

			//	define the column headings
			hDGCtl->DGSetItemText(0,ixCol,hQueryDO[pDOIdx[ixCol]]->DBaseOColLabel(pFieldIdx[ixCol]));
			hDGCtl->DGSetItemReadOnly(0,ixCol,true);
			hDGCtl->DGSetItemGreyBkgnd(0,ixCol,true);

			//	specify the displayed text item from the data object
			hDGCtl->DGSetItemText(1,ixCol,hQueryDO[pDOIdx[ixCol]]->DBaseOText(pFieldIdx[ixCol]));

			//	process column flags
			UINT flags = pFlags[ixCol];
			ProcessFlags(flags,hDGCtl,1,ixCol);

			//	thick divider at bottom of top row
			hDGCtl->DGSetItemDivider(0,0,false,true);
		}
		//	count the data grid control
		nDGrids++;
	}

	//	set up the last hDGrid control as the row selector data grid
	ixSelectorDG = nDGrids-1;
	IStr* hPrior = MakeStr();

	//	add the selector datagrid data rows

	bool status;
	IDBRow* hQDO = hQueryDO[ixSelectorDO];

	if(fFindActive)
		status = (hDBConnection->DBaseOSelect(hQDO,false)!=0);
	else
		status = (hDBConnection->DBaseOSelectAll(hQDO,false)!=0);

	if(status)
	{
		//	the code above already added the first data row
		hDGrid[ixSelectorDG]->DGRmvRows(1,1);

		IDBRow* hDO = hQDO->DBaseOMake();
		UINT ixRow = 0;

		while(hDBConnection->DBaseGetNextRow())
		{
			hDBConnection->Msg(MSG_DBaseOGetRowData,hDO);

			if(ixSelectorDO == DO_ORDREQ)
			{
				WCHAR* pStatus = hDO->DBaseOText(10);
				bool fComplete = hSvc->SvcCmpUnicode(pStatus,L"COMPLETE")==0;
				if(fComplete && fOpenOnly)
					continue;
				if(!fComplete && !fOpenOnly)
					continue;
			}

			ixRow++;
			hDGrid[ixSelectorDG]->DGAddRows(ixRow,1);

			for(UINT ixCol=0;ixCol<nDGCols;ixCol++)
			{
				hDGrid[ixSelectorDG]->DGSetItemText(ixRow,ixCol,
					hDO->DBaseOText(pFieldIdx[ixCol]));
				hDGrid[ixSelectorDG]->DGSetItemNotifyOnly(ixRow,ixCol,true);
				hDGrid[ixSelectorDG]->DGSetItemDelOnlyBkgnd(ixRow,ixCol,true);

				//	process column flags
				UINT flags = pFlags[ixCol];
				ProcessFlags(flags,hDGCtl,ixRow,ixCol);
			}

			//	visually divide groups based on different group field values
			if(nGroupField >= 0)
			{
				WCHAR* pTest = hDO->DBaseOText(nGroupField);
				bool fChange = hSvc->SvcCmpUnicode(hPrior->StrTextPtr(),pTest)!=0;
				hPrior->StrSetTextW(pTest);
				hDGrid[ixSelectorDG]->DGSetItemDivider(ixRow,0,fChange,false);
			}
		}
		hDO->Release();
	}
	hPrior->Release();

	if(ixSelectorDO == DO_ORDREQ)
		DisplayOrderStatus();
}

//	==============================================================================
//	Data Grid Item Changed
//	==============================================================================

void* CTDQueryPage::OnDGItemChanged(MSGP)
{
	MPARMPTR(MSGS_DGItemChanged*,pmsg);
	IDataGridCtl* hDGrid2 = pmsg->hDGrid2;
	UINT nSelRow = pmsg->nSelCol;
	UINT nSelCol = pmsg->nSelRow;

	hDBConnection->DBaseClearError();

	UINT oid = (int)hDGrid2->DbId();
	UINT ixDGrid = (oid - 610)/10;

	QLINETABLE* pPDTable = ppPageDefTable[ixDGrid];
	UINT nDGCols = pPDTable->nrCols;
	UINT* pColWids = pPDTable->pColWid;
	UINT* pFieldIdx = pPDTable->pFieldNr;
	UINT* pDOIdx = pPDTable->pDOIdx;

	if(oid == idSelectorDG)
	{
		//	fetch query DO values for selected row
		WCHAR* pText = (WCHAR*)hDGrid[ixDGrid]->DGGetItemText(nSelRow,ixColPrimary);
		UINT ixPKey = hQueryDO[ixSelectorDO]->DBaseOPrimaryKeyIdx();
		hQueryDO[ixSelectorDO]->DBaseOSetText(ixPKey,pText);
		hDBConnection->DBaseOFind(hQueryDO[ixSelectorDO]);

		//	follow each linked id field to fill other data objects
		UpdateQLinks();

		//	regenerate the display page
		DisplayPage();
	}
	else
	{
		//	update edited item text on the query data object
		WCHAR* pText = (WCHAR*)hDGrid[ixDGrid]->DGGetItemText(nSelRow,nSelCol);
		IDBRow* hQDO = hQueryDO[pDOIdx[nSelCol]];
		hQDO->DBaseOSetText(pFieldIdx[nSelCol],pText);
	}

	//	redisplay the container to show changes
	hWindow->WinCtrlsChanged();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Selector notification
//	==============================================================================

static WCHAR szOkMsg[4096];

#define SEL_ACTION_DELETE	0
#define SEL_ACTION_UPDATE	1
#define SEL_ACTION_ADD		2

static WCHAR szVerifyMsg [] =
	L"Ok to delete record %1 = %2 shown above\0"
	L"Ok to update record %1 = %2 shown above\0"
	L"Ok to add new record %1 = %2 shown above\0"
;

void CTDQueryPage::VerifyDesired(UINT nAction)
{
	IDBRow* hQDO = hQueryDO[ixSelectorDO];

//	if(hQDO->DBaseOStr(1)->StrLength())	 // only valid for products... others have autoincrement id field
	{
		//	verify ok to delete/add/etc.
		szOkMsg[0] = szOkMsg[1] = 0;
		IStr* hTemp = MakeStr();
		WCHAR* pPattern = hSvc->SvcListGetStr(szVerifyMsg,nAction);
		StrFormat(hTemp,L"ss",pPattern,
			hQDO->DBaseOColName(1),hQDO->DBaseOText(1));
		hSvc->SvcListAppendStr(szOkMsg,hTemp->StrTextPtr());
		hSvc->SvcListAppendStr(szOkMsg,TEXT("Do NOT Change Anything!"));

		MOUSEPKT* ppkt = (MOUSEPKT*)hWinMgr->WMgrGetMousePkt();
		IControl* hSelector = MakePopupSelR(999,
			this,hDGrid[ixSelectorDG],ppkt->x,ppkt->y,
			szOkMsg,(void*)nAction,NULL);
		hSelector->CtlSetSelColors(COLOR_WHITE,COLOR_RED);
		hSelector->CtlSelMarkItem(hSvc->SvcListGetStr(szOkMsg,1));
	}
}

void* CTDQueryPage::OnSelectorNotify(MSGP)
{
	MPARMPTR(MSGS_CtlSelectorNotify*,pmsg);
	UINT nItem = pmsg->nNowItem;
	UINT nAction = (UINT)pmsg->parm1;
	void* parm2 = pmsg->parm2;
	UINT SelectorOId = pmsg->oid;

	if(nItem==1)	// 0=selector cancelled, 1=act, 2=cancel
	{
		switch(nAction)
		{
			case SEL_ACTION_DELETE:
			{
				IDBRow* hQDO = hQueryDO[ixSelectorDO];
				if(hDBConnection->DBaseODelete(hQDO))
				{
					// existing record was deleted
					// clear all query DO data
					for(UINT ix=0;ix<MAXQDO;ix++)
						hQueryDO[ix]->DBaseOClear();
					DisplayPage();
				}
				break;
			}

			case SEL_ACTION_UPDATE:
			{
				IDBRow* hQDO = hQueryDO[ixSelectorDO];
				if(hDBConnection->DBaseOUpdate(hQDO))
				{
					// existing record was updated
					UpdateQLinks();
					DisplayPage();
				}
				break;
			}

			case SEL_ACTION_ADD:
			{
				IDBRow* hQDO = hQueryDO[ixSelectorDO];
				if(hDBConnection->DBaseOAdd(hQDO))
				{
					// new record was added
					DisplayPage();
				}
				break;
			}
		}
	}

	//	clear the add button from button down state
	IButtonCtl* hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDAddBtn);
	if(hBtn)
		hBtn->Msg(MSG_BtnSetState,false);

	//	clear the update button from button down state
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDUpdateBtn);
	if(hBtn)
		hBtn->BtnSetState(false);

	//	clear the delete button from button down state
	hBtn = (IButtonCtl*)hPgCtl->DbFindChildId(IDDeleteBtn);
	if(hBtn)
		hBtn->BtnSetState(false);

	//	redisplay the container to show changes
	hErrStgCtl->LblSetText(hDBConnection->DBaseErrorStg());
	hWindow->WinCtrlsChanged();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	process button change
//	==============================================================================

void* CTDQueryPage::OnBtnChange(MSGP)
{
	MPARMPTR(MSGS_BtnSelectChange*,pmsg);
	int btnid = pmsg->oid;
	IControl* hCtrl = pmsg->hCtrl;
	bool fPressed = pmsg->fPressed;

	if(fPressed)
	{
		switch(btnid)
		{
		case IDEMailKeysBtn2: SendKeysEMail2(); break;

		case IDDeleteBtn: // delete button - start verify on button click
			VerifyDesired(SEL_ACTION_DELETE);
			break;

		case IDUpdateBtn: // update button - start verify on button click
			VerifyDesired(SEL_ACTION_UPDATE);
			break;

		case IDAddBtn: // add button - start verify on button click
			if(hQueryDO[ixSelectorDO]->DBaseOValidate(NULL))
			{
				VerifyDesired(SEL_ACTION_ADD);
			}
			break;

		case IDClearBtn:	
			// clear all query DO data
			for(UINT ix=0;ix<MAXQDO;ix++)
				hQueryDO[ix]->DBaseOClear();
			hDBConnection->DBaseClearError();
			break;

		case IDFindBtn:	// find records (select)
			//	selector will use the appropriate query DO to select
			fFindActive = true;
			break;

		case IDShowOpenBtn:	// show all open orders
			fOpenOnly = true;
			fFindActive = false;
//			// clear all query DO data
//			for(UINT ix=0;ix<MAXQDO;ix++)
//				hQueryDO[ix]->Msg(MSG_DBaseOClear);
			SwitchToOrderReq();
			return IM_RTN_NOTHING;
//			break;

		case IDShowCompleteBtn:	// show completed orders
			fOpenOnly = false;
			fFindActive = false;
//			// clear all query DO data
//			for(UINT ix=0;ix<MAXQDO;ix++)
//				hQueryDO[ix]->Msg(MSG_DBaseOClear);
			SwitchToOrderReq();
			return IM_RTN_NOTHING;
//			break;

		case IDMarkPaidBtn:	MarkPaid(); break;
		case IDMarkShippedBtn: MarkShipped(); break;
		case IDEMailKeysBtn: SendKeysEMail(); break;
		case IDMarkInvoicedBtn: MarkInvoiced(); break;
		}

		//	redisplay the container to show changes
		hErrStgCtl->LblSetText(hDBConnection->DBaseErrorStg());
		DisplayPage();
		hWindow->WinCtrlsChanged();
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Find Specified Record
//	==============================================================================

void* CTDQueryPage::OnFindRecord(MSGP)
{
	MPARMPTR(MSGS_EvaFindRecord*,pmsg);
	UINT ixFindDO = pmsg->ixFindDO;
	WCHAR* pPrimaryKey = pmsg->pPrimaryKey;

	//	fetch query DO values for selected item
	UINT nPrimaryField = hQueryDO[ixFindDO]->DBaseOPrimaryKeyIdx();
	hQueryDO[ixFindDO]->DBaseOSetText(nPrimaryField,pPrimaryKey);
	hDBConnection->DBaseOFind(hQueryDO[ixFindDO]);

	//	follow each linked id field to fill other data objects
	UpdateQLinks();

	//	redisplay the container to show changes
	idSelectorDG = 600;
	DisplayPage();
	hErrStgCtl->LblSetText(hDBConnection->DBaseErrorStg());
	hWindow->WinCtrlsChanged();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Update Order Status in database
//	==============================================================================

void CTDQueryPage::UpdateOrderStatus(void)
{
	//	update flags in order request
	IStr* hTemp = MakeStr();

	if(fNeedPayment)
		hTemp->StrAppendC('P');
	if(fNeedInvoice)
		hTemp->StrAppendC('Q');
	if(fNeedEMailKeys)
		hTemp->StrAppendC('E');
	if(fNeedShipping)
		hTemp->StrAppendC('S');
	if(fIsTestIPN)
		hTemp->StrAppendC('T');

	hQueryDO[DO_ORDREQ]->DBaseOSetText(23,hTemp->StrTextPtr());

	//	update status in order request
	if(!(fNeedPayment || fNeedInvoice || fNeedEMailKeys || fNeedShipping))
	{
		ProcessCompletedOrder();
		hTemp->StrSetTextW(TEXT("COMPLETE"));
	}
	else if (fNeedPayment)
		hTemp->StrSetTextW(TEXT("PENDING_PMT"));
	else
		hTemp->StrSetTextW(TEXT("PENDING"));

	hQueryDO[DO_ORDREQ]->DBaseOSetText(24,hTemp->StrTextPtr());
	hDBConnection->DBaseOUpdate(hQueryDO[DO_ORDREQ]);

	hTemp->Release();
}

void CTDQueryPage::ProcessCompletedOrder(void)
{
	WCHAR* pStatus = hQueryDO[DO_ORDREQ]->DBaseOText(24);
	if(hSvc->SvcCmpUnicode(pStatus,TEXT("COMPLETE"))!=0)
	{
		//	just now completed
		//	TODO: add to contacts if not already there
	}
}

void CTDQueryPage::MarkPaid(void)
{
	fNeedPayment = false;
	UpdateOrderStatus();
}

void CTDQueryPage::MarkInvoiced(void)
{
	fNeedInvoice = false;
	UpdateOrderStatus();
}

void CTDQueryPage::SendKeysEMail(void)
{
	WCHAR pDownload[] = L"http://fastcad.com/x-support.html";

	WCHAR* pName = hQueryDO[DO_ORDREQ]->DBaseOText(3);
	WCHAR* pCompany = hQueryDO[DO_ORDREQ]->DBaseOText(4);
	WCHAR* pTo = hQueryDO[DO_ORDREQ]->DBaseOText(11);

	IStr* hEMAdr = MakeStr();
	StrFormat(hEMAdr,TEXT("ss"),TEXT("%1<%2>"),pName,pTo);
	pTo = hEMAdr->StrTextPtr();

	bool fValid = false;
	IStr* hBody = MakeStr();
	// email prefix
	hBody->StrSetTextW(RscText(RSC_EMKeysPrefix));

	//	lookup every authorization key that matches the order id
	IDBRow* hQDO = hQueryDO[DO_AKEY];
	hQDO->DBaseOClear();
	WCHAR* pText = hQueryDO[DO_ORDREQ]->DBaseOText(1);
	hQDO->DBaseOSetText(2,pText);
	IArray* hResults = NULL;
	if(hResults = hDBConnection->DBaseOSelect(hQDO,true))
	{
		UINT nr = hResults->AryCount();
		for(UINT ix=0;ix<nr;ix++)
		{	
			IDBRow* hDO = *((IDBRow**)hResults->AryItemPtr(ix));
			hDBConnection->Msg(MSG_DBaseOGetRowData,hDO);

			WCHAR* pKSKU = hDO->DBaseOText(4);
			WCHAR* pKName = hDO->DBaseOText(5);
			WCHAR* pKCompany = hDO->DBaseOText(6);
			WCHAR* pSN = hDO->DBaseOText(7);
			WCHAR* pAKey = hDO->DBaseOText(8);

			// get product name
			UINT nPrimaryField = hQueryDO[DO_PRODUCT]->DBaseOPrimaryKeyIdx();
			hQueryDO[DO_PRODUCT]->DBaseOSetText(nPrimaryField,pKSKU);
			hDBConnection->DBaseOFind(hQueryDO[DO_PRODUCT]);
			WCHAR* pProduct = hQueryDO[DO_PRODUCT]->DBaseOText(4);

			// for each product key
			StrFormat(hBody,TEXT("sssss"),RscText(RSC_EMKeysData),pKName,pKCompany,pSN,pAKey,pProduct);
			fValid = true;
		}
		hResults->Release();
	}

	// email suffix
	StrFormat(hBody,TEXT("ss"),RscText(RSC_EMKeysSuffix),pDownload);
	if(fValid)
 		MakeSendEMail(this,pTo,TEXT("Evolution Computing Authorization Keys"),hBody);
	else
	{
		IStr* hTemp = MakeStr();
		StrFormat(hTemp,TEXT("s"),TEXT("No keys match order id: %1"),pText);
		MakeDlgAlert(TEXT("No matching keys for order"),hTemp->StrTextPtr(),NULL);
		hTemp->Release();
	}

	hEMAdr->Release();
}

void* CTDQueryPage::OnEMailSendNotify(MSGP)
{
	MPARMPTR(WCHAR*,pErrorMsg);
	if(!pErrorMsg)
	{
		fNeedEMailKeys = false;
		UpdateOrderStatus();
	}

	//	redisplay the container to show changes
	DisplayPage();
	hWindow->WinCtrlsChanged();
	return IM_RTN_NOTHING;
}

void CTDQueryPage::MarkShipped(void)
{
	fNeedShipping = false;
	UpdateOrderStatus();
}

void CTDQueryPage::SwitchToOrderReq(void)
{
	UpdateOrderStatus();
	((IEvAdmin*)hApplication)->EvaSwitchAndFind(DO_ORDREQ,NULL);
}


//	resend authorization keys

void CTDQueryPage::SendKeysEMail2(void)
{
	WCHAR pDownload[] = L"http://fastcad.com/x-support.html";

	WCHAR* pName = hQueryDO[DO_AKEY]->DBaseOText(5);
	WCHAR* pCompany = hQueryDO[DO_AKEY]->DBaseOText(6);

//	WCHAR* pCustId = hQueryDO[DO_AKEY]->DBaseOText(10);
//	hQueryDO[DO_CONTACT]->DBaseOClear();
//	UINT nPrimaryField = hQueryDO[DO_CONTACT]->DBaseOPrimaryKeyIdx();
//	hQueryDO[DO_CONTACT]->DBaseOSetText(nPrimaryField,pCustId);
//	hDBConnection->DBaseOFind(hQueryDO[DO_CONTACT]);
	WCHAR* pToEMail = hQueryDO[DO_CONTACT]->DBaseOText(11);

	IStr* hEMAdr = MakeStr();
	StrFormat(hEMAdr,TEXT("ss"),TEXT("%1<%2>"),pName,pToEMail);
	WCHAR* pTo = hEMAdr->StrTextPtr();

	IStr* hBody = MakeStr();
	// email prefix
	hBody->StrSetTextW(RscText(RSC_EMKeysPrefix));

	//	send keys from current DO_AKEY
	IDBRow* hDO = hQueryDO[DO_AKEY];

	WCHAR* pKSKU = hDO->DBaseOText(4);
	WCHAR* pKName = hDO->DBaseOText(5);
	WCHAR* pKCompany = hDO->DBaseOText(6);
	WCHAR* pSN = hDO->DBaseOText(7);
	WCHAR* pAKey = hDO->DBaseOText(8);

	// get product name
	UINT nPrimaryField = hQueryDO[DO_PRODUCT]->DBaseOPrimaryKeyIdx();
	hQueryDO[DO_PRODUCT]->DBaseOSetText(nPrimaryField,pKSKU);
	hDBConnection->DBaseOFind(hQueryDO[DO_PRODUCT]);
	WCHAR* pProduct = hQueryDO[DO_PRODUCT]->DBaseOText(4);

	// format product key
	StrFormat(hBody,TEXT("sssss"),RscText(RSC_EMKeysData),pKName,pKCompany,pSN,pAKey,pProduct);

	// email suffix
	StrFormat(hBody,TEXT("ss"),RscText(RSC_EMKeysSuffix),pDownload);
 	MakeSendEMail(this,pTo,TEXT("Evolution Computing Authorization Keys"),hBody);

	hEMAdr->Release();
}

//	==============================================================================
