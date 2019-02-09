//	==============================================================================
//	CUtilitiesPage - utilities page singleton object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

void* CCmdCreateDB(IDBConnection* hDBConnection);
void* CCmdRestoreDB(IDBConnection* hDBConnection);
void* CCmdBackupDB(IDBConnection* hDBConnection);
void* StartCmdPostOrdLog(IDBConnection* hDBConnection);

//	==============================================================================
//	Class Definition
//	==============================================================================

#define DANGERDISABLED	0x00000119
#define DANGERDISABLED2	0xff00ff00
#define DANGERENABLED	0xff0000ff

class CUtilitiesPage : public IMObject
{
public:
	CUtilitiesPage(IWindow* hWindow,IDBConnection* hDBConnection);
	~CUtilitiesPage(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnCtlShow(MSGP);
	void* OnBtnChange(MSGP);
public:
	void DisplayPage(void);
	void UpdDangerous(void);
public:
	IWindow*		hWindow;
	IDBConnection*	hDBConnection;
	IControl*		hUtilitiesPgCtl;
	bool			fDangerousOk;

	IButtonCtl*		hDangerBtn;
	IButtonCtl*		hDBtn1;
	IButtonCtl*		hDBtn2;
	IButtonCtl*		hDBtn3;
	IButtonCtl*		hBtn;
};

//	==============================================================================
//	Page Class factory
//	==============================================================================

IControl* MakeUtilitiesPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = (IControl*)new CUtilitiesPage(hWindow,hDBConnection);
	return hPage; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CUtilitiesPage::CUtilitiesPage(IWindow* hWindowP,IDBConnection* hDBConnectionP)
{
	hWindow = hWindowP;
	hDBConnection = hDBConnectionP;
	hUtilitiesPgCtl = MakeContainer(hWindow,7000);
	hUtilitiesPgCtl->CtlShow(false);
	hMakeMgr->CtrlBuild(hUtilitiesPgCtl,RscText(RSC_UtilitiesPageCfg));

	fDangerousOk = false;
	hDangerBtn = (IButtonCtl*)hUtilitiesPgCtl->DbFindChildId(610);
	hDangerBtn->CtlSetBkColor(DANGERDISABLED2);
	hDangerBtn->BtnSetState(false);
	hDangerBtn->CtlSetNotify(this);

	hDBtn1 = (IButtonCtl*)hUtilitiesPgCtl->DbFindChildId(620);
	hDBtn1->CtlSetBkColor(DANGERDISABLED);
	hDBtn1->CtlSetNotify(this);

	hDBtn3 = (IButtonCtl*)hUtilitiesPgCtl->DbFindChildId(630);
	hDBtn3->CtlSetBkColor(DANGERDISABLED);
	hDBtn3->CtlSetNotify(this);

	hBtn = (IButtonCtl*)hUtilitiesPgCtl->DbFindChildId(720);
	hBtn->CtlSetNotify(this);

	hDBtn2 = (IButtonCtl*)hUtilitiesPgCtl->DbFindChildId(730);
	hDBtn2->CtlSetBkColor(DANGERDISABLED);
	hDBtn2->CtlSetNotify(this);
}

//	==============================================================================
//	Destructor
//	==============================================================================

CUtilitiesPage::~CUtilitiesPage(void)
{
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CUtilitiesPage::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_CtlShow: return OnCtlShow(MPPTR);
	case MSG_BtnSelectChange: return OnBtnChange(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	show or hide page
//	==============================================================================

void* CUtilitiesPage::OnCtlShow(MSGP)
{
	MPARMBOOL(bool,fShow);

	//	show/hide page container control
	hUtilitiesPgCtl->CtlShow(fShow);

	if(fShow)
	{
		DisplayPage();
	}
	else
	{
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	display page
//	==============================================================================

void CUtilitiesPage::DisplayPage(void)
{
}

//	==============================================================================
//	Show dangerous state
//	==============================================================================

void CUtilitiesPage::UpdDangerous(void)
{
	if(fDangerousOk)
	{
		hDangerBtn->CtlSetBkColor(DANGERENABLED);
		hDangerBtn->BtnSetState(true);
		hDBtn1->CtlSetBkColor(DANGERENABLED);
		hDBtn2->CtlSetBkColor(DANGERENABLED);
		hDBtn3->CtlSetBkColor(DANGERENABLED);
	}
	else
	{
		hDangerBtn->CtlSetBkColor(DANGERDISABLED2);
		hDangerBtn->BtnSetState(false);
		hDBtn1->CtlSetBkColor(DANGERDISABLED);
		hDBtn2->CtlSetBkColor(DANGERDISABLED);
		hDBtn3->CtlSetBkColor(DANGERDISABLED);
	}

	hDangerBtn->CtlDraw();
	hDBtn1->CtlDraw();
	hDBtn2->CtlDraw();
	hDBtn3->CtlDraw();
}

//	==============================================================================
//	process button change
//	==============================================================================

void* CUtilitiesPage::OnBtnChange(MSGP)
{
	MPARMPTR(MSGS_BtnSelectChange*,pmsg);
	int btnid = pmsg->oid;
	IControl* hCtrl = pmsg->hCtrl;
	bool fPressed = pmsg->fPressed;

	switch(btnid)
	{
	case 610:	// enable dangerous operations
		fDangerousOk = !fDangerousOk;
		UpdDangerous();
		break;

	case 620:	// create database
		if(fPressed && fDangerousOk) // only on button up event
		{
			CCmdCreateDB(hDBConnection);
			//	clear enable dangerous flag
			fDangerousOk = false;
			UpdDangerous();
		}
		break;

	case 630:	// add orders.log data to database
		if(fPressed && fDangerousOk) // only on button up event
		{
			StartCmdPostOrdLog(hDBConnection);
			//	clear enable dangerous flag
			fDangerousOk = false;
			UpdDangerous();
		}
		break;

	case 720:	// backup database
		if(fPressed) // only on button up event
		{
			CCmdBackupDB(hDBConnection);
		}
		break;

	case 730:	// restore database
		if(fPressed && fDangerousOk) // only on button up event
		{
			CCmdCreateDB(hDBConnection);
			CCmdRestoreDB(hDBConnection);
			//	clear enable dangerous flag
			fDangerousOk = false;
			UpdDangerous();
		}
		break;
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
