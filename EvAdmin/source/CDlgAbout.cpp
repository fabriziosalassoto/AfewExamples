//	==============================================================================
//	CDlgAboutEvAdmin - About dialog
//	------------------------------------------------------------------------------
//	Copyright ©2001-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

//	==============================================================================
//	Class Definition
//	==============================================================================

class CDlgAboutEvAdmin : public IMObject
{
public:
	CDlgAboutEvAdmin(void);
	~CDlgAboutEvAdmin(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnDlgInit(MSGP);
	void* OnDlgEnd(MSGP);
public:
	IDlgCtl*	hDlgCtl;
	ILabelCtl*	hVerLbl;
	IButtonCtl*	hPictBtn;
};

//	==============================================================================
//	Dialog Class factory
//	==============================================================================

static IMObject* hAboutDlg = NULL;

IMObject* MakeDlgAboutEvAdmin(void)
{
	//	only create if the dialog is not open
	if(!hAboutDlg)
	{
		hAboutDlg = new CDlgAboutEvAdmin();
		MakeDlgCtl(hAboutDlg,RscText(RSC_DlgAboutEvAdmin),NULL);
	}
	return hAboutDlg; 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CDlgAboutEvAdmin::CDlgAboutEvAdmin(void)
{
}

//	==============================================================================
//	Destructor
//	==============================================================================

CDlgAboutEvAdmin::~CDlgAboutEvAdmin(void)
{
	hAboutDlg = NULL;
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CDlgAboutEvAdmin::Msg(MSGDISPATCH)
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

void* CDlgAboutEvAdmin::OnDlgInit(MSGP)
{
	MPARMPTRV(IDlgCtl*,hDlgCtl);
	WCHAR szText[MAX_PATH];

	hPictBtn = (IButtonCtl*)hDlgCtl->DbFindChildId(500);
	hVerLbl = (ILabelCtl*)hDlgCtl->DbFindChildId(512);

	hPictBtn->BtnSetImage(RscImage(RSCIMG_CompanyLogo));

	hSvc->SvcCopyW(szText,RscText(RSC_EvAdminVersion));
	hVerLbl->Msg(MSG_EdSetText,szText);
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	OnDlgEnd - notification: dialog is ending
//	==============================================================================

void* CDlgAboutEvAdmin::OnDlgEnd(MSGP)
{
	MPARMINT(int,endid);

	delete this;
	return IM_RTN_NOTHING;
}

//	==============================================================================
