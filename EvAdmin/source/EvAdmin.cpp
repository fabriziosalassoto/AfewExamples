//	==============================================================================
//	EvAdmin.cpp - EVA database Administration utility program
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "winmain.h"
#include <stdio.h>

#include "EvAdmin.h"
#include "CTDQueryPage.h"

#ifdef _USINGFWKSTATICLIB
extern DLLEXPORT void InitNetSvc(void);
extern DLLEXPORT void InitDBaseSvc(void);
#endif

UINT nXPSlot = 0;
UINT RscBase = 0;

class CApplication : public IApplication
{
public:
	CApplication(void);
	~CApplication(void);
	virtual void* Msg(MSGDISPATCH);

	void* OnInitComplete(void);
	void* OnDBConnectErr(MSGP);
	void* OnDBConnectOk(MSGP);
	void* OnDBErr(MSGP);
	void* OnSwitchPage(MSGP);
	void* OnCommand(MSGP);
	void* OnDropData(MSGP);
	void* OnSwitchAndFind(MSGP);
public:
	void SwitchPage(IControl* hNewPage,IArray** phData);
public:
	IWindow*		hWindow;
	IControl*		hMainPg;
	IControl*		hMenuBar;

	IControl*		hProductsPage;
	IControl*		hOrdersPage;
	IControl*		hContactsPage;
	IControl*		hAuthKeysPage;

	IControl*		hUtilitiesPage;
	IControl*		hControlsPage;
	IControl*		hDiscountsPage;
	IControl*		hTestPage;
	IControl*		hNowPage;

	IMObject*		hDBase;
	IDBConnection*	hDBConnection;
	bool			first;
};

IControl* MakeProductsPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeOrdersPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeContactsPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeDiscountsPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeControlsPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeAuthKeysPage(IWindow* hWindow,IDBConnection* hDBConnection);
IControl* MakeUtilitiesPage(IWindow* hWindow,IDBConnection* hDBConnection);

//	==============================================================================
//	Application Object Class Definition
//	==============================================================================

IApplication* MakeApplication(void)
{
	return new CApplication();
}

//	==============================================================================
//	Application Object Constructor
//	==============================================================================

CApplication::CApplication(void)
{
	pApplication = this;
	hDBase = NULL;
	hNowPage = NULL;
	hProductsPage = NULL;
	hOrdersPage = NULL;
	hControlsPage = NULL;
	hContactsPage = NULL;
	hDiscountsPage = NULL;
	hAuthKeysPage = NULL;
	hTestPage = NULL;
	first = true;

	//	need this for proper location of resources
	nXPSlot = (UINT)hXPMgr->Msg(MSG_GetSlotNr,VOIDP EVADMIN_XPID);
	RscBase = nXPSlot<<16;	// base for resource ids

	LoadDefaultResources();

	//	process the .INI file
	WCHAR szIniName[MAX_PATH];
	hSvc->SvcPgmNameNewType(szIniName,TEXT(".ini"));
	ProcessIniFile(szIniName);

	//	square light controls
	hUIStyleMgr->UISelect((WCHAR*)TEXT("Light Blue"));

	//	make our main window
	hWindow = MakeWindow(WINTYPE_DOCWIN,NULL,40,40,1270,742,true,false);
	hWindow->WinQuitOnClose();	// single window app - quit when it closes
	hWindow->WinDropTarget(); // we accept drop data

	//	load our standard menu
	hMenuBar = MakeMenuBar(hWindow,1,EVADMIN_MENU);

	//	define our main page (before connection to database)
	hMainPg = MakeContainer(hWindow,4000);

#ifdef _USINGFWKSTATICLIB
	InitNetSvc();// wqe want to use socket services
	InitDBaseSvc();	// MySQL database services
#else
	hXPMgr->XPLoad(L"|NetSvc.xp");
	hXPMgr->XPLoad(L"|dbasesvc.xp");
#endif

	//	initially display main page
	Msg(MSG_EvaSwitchPage,VOIDP DO_MAIN);
}

//	==============================================================================
//	Application Object Destructor
//	==============================================================================

CApplication::~CApplication(void)
{
	if(hProductsPage)
		hProductsPage->Release();

	if(hDBase)
		hDBase->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CApplication::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
		case MSG_SysInitComplete: return OnInitComplete();
		case MSG_ScriptLine: return OnCommand(MPPTR);
		case MSG_WinDropData: return OnDropData(MPPTR);
		case MSG_EvaDBConnectErr: return OnDBConnectErr(MPPTR);
		case MSG_EvaDBErr: return OnDBErr(MPPTR);
		case MSG_EvaDBConnectOk: return OnDBConnectOk(MPPTR);
		case MSG_EvaSwitchPage: return OnSwitchPage(MPPTR);
		case MSG_EvaSwitchAndFind: return OnSwitchAndFind(MPPTR);
	}
	return IM_RTN_IGNORED;
}

//	==============================================================================
//	Startup initialization complete
//	==============================================================================

void* CApplication::OnInitComplete(void)
{
	//	we get called more than once - every control, then hApplication
	if(first)
		hDBase = MakeDbAccess(this);
	first = false;
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Error connecting to database
//	==============================================================================

void* CApplication::OnDBConnectErr(MSGP)
{
	MPARMPTR(WCHAR*,pErrStg);

	MakeDlgAlert(RscText(RSC_DBConnectionErr),pErrStg,RscImage(RSCIMG_CompanyLogo));

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Error in database operation
//	==============================================================================

void* CApplication::OnDBErr(MSGP)
{
	MPARMPTR(MSGS_EvaDBErr*,pmsg);

	MakeDlgAlert(pmsg->pErrTitle,pmsg->pErrStg,RscImage(RSCIMG_CompanyLogo));

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	connect to database	successful
//	==============================================================================

void* CApplication::OnDBConnectOk(MSGP)
{
	MPARMPTR(IDBConnection*,hDBConnectionP);

	hDBConnection = hDBConnectionP;

	//	define the products page
	hProductsPage = MakeProductsPage(hWindow,hDBConnection);

	//	define the orders page
	hOrdersPage = MakeOrdersPage(hWindow,hDBConnection);

	//	define the contacts page
	hContactsPage = MakeContactsPage(hWindow,hDBConnection);

	//	define the authorization keys page
	hAuthKeysPage = MakeAuthKeysPage(hWindow,hDBConnection);

	//	define the controls page
	hControlsPage = MakeControlsPage(hWindow,hDBConnection);

	//	define the discounts page
	hDiscountsPage = MakeDiscountsPage(hWindow,hDBConnection);

	//	define the utilities page
	hUtilitiesPage = MakeUtilitiesPage(hWindow,hDBConnection);

	Msg(MSG_EvaSwitchPage,VOIDP DO_ORDREQ);
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Switch displayed page
//	==============================================================================

void* CApplication::OnSwitchAndFind(MSGP)
{
	MPARMINT(UINT,kDO);
	MPARMPTR(WCHAR*,pText);

	IStr* hTemp = MakeStr(pText);
	IControl* hPage = ((IEvAdmin*)hApplication)->EvaSwitchPage(kDO);
	if(pText)
		((IEvAdmin*)hPage)->EvaFindRecord(kDO,hTemp->StrTextPtr());
	hTemp->Release();

	return IM_RTN_NOTHING;
}

void*  CApplication::OnSwitchPage(MSGP)
{
	MPARMINT(UINT,kDO);

	IControl* hNewPage = NULL;

	switch(kDO)
	{
	case DO_ORDREQ: hNewPage = hOrdersPage; break;
	case DO_CONTACT: hNewPage = hContactsPage; break;
	case DO_PRODUCT: hNewPage = hProductsPage; break;
	case DO_AKEY: hNewPage = hAuthKeysPage; break;
	case DO_CTRL: hNewPage = hControlsPage; break;
	case DO_DISCOUNT: hNewPage = hDiscountsPage; break;
	case DO_UTILITIES: hNewPage = hUtilitiesPage; break;
	case DO_TEST: hNewPage = hTestPage; break;
	};

	if(hNowPage != NULL)
		hNowPage->CtlShow(false);
	else
		hMainPg->CtlShow(false);

	if(hNewPage != NULL)
		hNewPage->CtlShow(true);
	else
		hMainPg->CtlShow(true);

	hNowPage = hNewPage;
	hWindow->WinCtrlsChanged();

	return hNowPage;
}

//	==============================================================================
//	Process command string (from the Menu - we disabled hCmdRcvr)
//	==============================================================================

IMObject* MakeDlgAboutEvAdmin(void);

static WCHAR szCmdList[] =
	L"ABOUT\0HELP\0EXIT\0PRODUCTS\0ORDERS\0CONTACTS\0\UTILITIES\0"
	L"AUTHKEYS\0CONTROLS\0DISCOUNTS\0TEST\0"
	L"\0";

void* CApplication::OnCommand(MSGP)
{
	MPARMPTR(WCHAR*,pCmd);

	UINT nCmd = (UINT)hSvc->SvcListFindStrNr(szCmdList,pCmd);
	switch(nCmd)
	{
	case 1:		// ABOUT
		MakeDlgAboutEvAdmin();
		break;

	case 2:		// HELP
		MakeDlgAboutEvAdmin();
		break;

	case 3:		// EXIT
		hSystem->SysEndApp();
		break;

	case 4:		// PRODUCTS
		if(hProductsPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_PRODUCT);
		break;

	case 5:		// ORDERS
		if(hOrdersPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_ORDREQ);
		break;

	case 6:		// CONTACTS
		if(hContactsPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_CONTACT);
		break;

	case 7:		// UTILITIES
		if(hUtilitiesPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_UTILITIES);
		break;

	case 8:		// AUTHKEYS
		if(hAuthKeysPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_AKEY);
		break;

	case 9:	// CONTROLS
		if(hControlsPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_CTRL);
		break;

	case 10:	// DISCOUNTS
		if(hDiscountsPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_DISCOUNT);
		break;

	case 11:	// TEST
		if(hTestPage)
			((IEvAdmin*)this)->EvaSwitchPage(DO_TEST);
		break;

	default:
		//	invalid command
		break;
	}

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Receive Drop Data
//	==============================================================================

void* CApplication::OnDropData(MSGP)
{
	MPARMPTR(MSGS_WinDropData*,pmsg);
	int x = pmsg->x;
	int y = pmsg->y;
	void* pDataObject = pmsg->pData;

	IStr* hDroppedText = hWinMgr->WMgrGetDropStr(pDataObject,SDF_TextW);

	if(!hDroppedText)
	{
		hDroppedText = hWinMgr->WMgrGetDropStr(pDataObject,SDF_FNameW);
		if(hDroppedText)
		{
			WCHAR* pType = hSvc->SvcFileType(hDroppedText->StrTextPtr());
			if(hSvc->SvcCmpUnicode(pType,TEXT(".txt"))!=0)
			{
				hDroppedText->Release();
				return IM_RTN_FALSE;
			}
			IStr* hText = hSvc->SvcLoadTextFile(hDroppedText->StrTextPtr());
			hDroppedText->Release();
			hDroppedText = hText;
		}
		else
			return IM_RTN_FALSE;
	}

	if(hDroppedText)
	{
		hDroppedText->Release();
	}
	return IM_RTN_TRUE;
}

//	==============================================================================
