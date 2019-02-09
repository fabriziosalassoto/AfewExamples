//	==============================================================================
//	CDbAccess.cpp - Client Records Management: Database Object
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

//	==============================================================================
//	Class Definition
//	==============================================================================

class CDbAccess : public IMObject
{
public:
	CDbAccess(IMObject* hNotify);
	~CDbAccess(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnDlgLogonNotify(MSGP);
public:
	IMObject*		hNotify;
	IStr*			hUName;
	IStr*			hPassword;
	IDBConnection*	hDBConnection;
};

//	==============================================================================
//	Make database access object
//	==============================================================================

IMObject* MakeDbAccess(IMObject* hNotify)
{
	IMObject* hDbAccess = new CDbAccess(hNotify);
	MakeDlgLogon(hDbAccess);
	return hDbAccess;
}

//	==============================================================================
//	Constructor
//	==============================================================================

CDbAccess::CDbAccess(IMObject* hNotifyP)
{
	hNotify = hNotifyP;
	hUName = NULL;
	hPassword = NULL;
	hDBConnection = NULL;
}

//	==============================================================================
//	Destructor
//	==============================================================================

CDbAccess::~CDbAccess(void)
{
	if(hDBConnection)
		hDBConnection->DBaseClose();

	if(hUName)
		hUName->Release();
	if(hPassword)
		hPassword->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CDbAccess::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DlgLogonNotify: return OnDlgLogonNotify(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CDbAccess::OnDlgLogonNotify(MSGP)
{
	MPARMPTR(MSGS_DlgLogonNotify*,pmsg);
	IStr* hUNameP = pmsg->hUName;
	IStr* hPasswordP = pmsg->hPassword;

	if(hUName)
		hUName->Release();
	if(hPassword)
		hPassword->Release();

	hUName = hUNameP;
	hPassword = hPasswordP;

	hDBConnection = hDBSvc->DBConnect(RscText(RSC_DBHost),hUName->StrTextPtr(),
		hPassword->StrTextPtr(),RscText(RSC_DBName));

	if(!hDBConnection)
	{
		 UINT nError = (UINT)hDBSvc->DBaseErrorNr();
		 WCHAR* pErrorStg = (WCHAR*)hDBSvc->DBaseErrorStg();
		 hNotify->Msg(MSG_EvaDBConnectErr,pErrorStg);
	}
	else
		((IEvAdmin*)hNotify)->EvaDBConnectOk(hDBConnection);

	return IM_RTN_NOTHING;
}

//	==============================================================================
