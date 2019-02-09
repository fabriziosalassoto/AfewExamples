//	==============================================================================
//	CDlgLogon - Logon dialog
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

//	==============================================================================
//	Class Definition
//	==============================================================================

class CDlgLogon : public IDialog
{
public:
	CDlgLogon(IMObject* hNotify);
	~CDlgLogon(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnDlgInit(MSGP);
	void* OnDlgEnd(MSGP);
public:
	IDlgCtl*	hDlgCtl;
	IEditCtl*	hUNameCtl;
	IEditCtl*	hPasswordCtl;
	IButtonCtl*	hPictBtn;

	IMObject*	hNotify;
};

//	==============================================================================
//	Dialog Class factory
//	==============================================================================

static IMObject* hLogonDlg = NULL;

IMObject* MakeDlgLogon(IMObject* hNotify)
{
	//	only create if the dialog is not open
	if(!hLogonDlg)
	{
		hLogonDlg = (IMObject*)new CDlgLogon(hNotify);
		MakeDlgCtl(hLogonDlg,RscText(RSC_DlgLogon),NULL);
	}
	return hLogonDlg; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CDlgLogon::CDlgLogon(IMObject* hNotifyP)
{
	hNotify = hNotifyP;
}

//	==============================================================================
//	Destructor
//	==============================================================================

CDlgLogon::~CDlgLogon(void)
{
	hLogonDlg = NULL;
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CDlgLogon::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DlgInit: return OnDlgInit(MPPTR);
	case MSG_DlgEnd: return OnDlgEnd(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	OnDlgInit - initialize dialog controls now
//	==============================================================================

void* CDlgLogon::OnDlgInit(MSGP)
{
	MPARMPTRV(IDlgCtl*,hDlgCtl);

	hPictBtn = (IButtonCtl*)hDlgCtl->DbFindChildId(500);
	hUNameCtl = (IEditCtl*)hDlgCtl->DbFindChildId(501);
	hPasswordCtl = (IEditCtl*)hDlgCtl->DbFindChildId(502);

	hPictBtn->BtnSetImage(RscImage(RSCIMG_CompanyLogo));

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	OnDlgEnd - notification: dialog is ending
//	==============================================================================

void* CDlgLogon::OnDlgEnd(MSGP)
{
	MPARMINT(int,endid);

	IStr* hUName = MakeStr((WCHAR*)hUNameCtl->EdGetText());
	IStr* hPassword = MakeStr((WCHAR*)hPasswordCtl->EdGetText());

	((IEvAdmin*)hNotify)->DlgLogonNotify(hUName,hPassword);

	delete this;
	return IM_RTN_NOTHING;
}

//	==============================================================================
