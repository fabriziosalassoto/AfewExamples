//	==============================================================================
//	CSendEMail.cpp - Client Records Management: Send Client EMail
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

//	==============================================================================
//	Class Definition
//	==============================================================================

class CSendEMail : public IMObject
{
public:
	CSendEMail(IMObject* hNotifyP,WCHAR* hToP,WCHAR* hSubjectP,IStr* hBodyP);
	~CSendEMail(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnNetSvcNotify(MSGP);
public:
	IMObject*	hNotify;

	IEMailAcct* hEMAcct;
	IEMail*		hEMail;

	IData*		hReqData;

	IStr*		hTo;
	IStr*		hSubject;
	IStr*		hBody;
};

//	==============================================================================
//	Make email send object
//	==============================================================================

IMObject* MakeSendEMail(IMObject* hNotify,WCHAR* hTo,WCHAR* hSubject,IStr* hBody)
{
	IMObject* hSendEMail = new CSendEMail(hNotify,hTo,hSubject,hBody);
	return hSendEMail;
}

//	==============================================================================
//	Constructor
//	==============================================================================

CSendEMail::CSendEMail(IMObject* hNotifyP,WCHAR* hToP,WCHAR* hSubjectP,IStr* hBodyP)
{
	hNotify = hNotifyP;
	hTo = MakeStr(hToP);
	hBody = hBodyP;
	hSubject = MakeStr(hSubjectP);

	hEMAcct = hNetSvc->NetSvcMakeEMailAccount(TEXT("#default.email.ini"));
	hReqData = MakeData();
	hEMail = hNetSvc->NetSvcMakeEMail(hSubject->StrTextPtr(),hBody->StrTextPtr());
	hNetSvc->NetSvcSendEMail(hEMAcct,hTo,NULL,NULL,
		hEMail,this,hReqData);
}

//	==============================================================================
//	Destructor
//	==============================================================================

CSendEMail::~CSendEMail(void)
{
	hEMAcct->Release();
	hEMail->Release();
	hTo->Release();
	hBody->Release();
	hSubject->Release();
	hReqData->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CSendEMail::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_NetSvcNotify: return OnNetSvcNotify(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	Request completed notification
//	==============================================================================

void* CSendEMail::OnNetSvcNotify(MSGP)
{
	MPARMPTR(IData*,hReqDataP);

	UINT err;
	if(err = hReqData->DataGetErrNr())
	{
		//	return error status message
		IStr* hErr = hNetSvc->NetSvcErrStr(err);
		hErr->StrAppendW(TEXT("\r\n\r\n"));
		hErr->StrAppend(hTo);

		if(hNotify)
			((IEvAdmin*)hNotify)->EvaEMailSendNotify(hErr->StrTextPtr());
		//	show email send error alert dialog
		MakeDlgAlert(TEXT("Error sending EMail"),hErr->StrTextPtr(),NULL);
		hErr->Release();
	}
	else
	{
		if(hNotify)
			hNotify->Msg(MSG_EvaEMailSendNotify,NULL);
		//	show email sent ok alert dialog
		MakeDlgAlert(TEXT("EMail successfully sent"),hTo->StrTextPtr(),NULL);
	}

	//	release this object
	Release();
	return IM_RTN_NOTHING;
}

//	==============================================================================
