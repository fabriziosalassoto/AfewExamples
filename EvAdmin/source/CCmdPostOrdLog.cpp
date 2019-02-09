//	==============================================================================
//	CCmdPostOrdLog.cpp - Client Records Management: Post orders.log to database
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"

#include "EvAdmin.h"

IDBRow* MakeAuthKeyDO(void);
IDBRow* MakeContactDO(void);

//	==============================================================================
//	Class definition
//	==============================================================================

class CCmdPostOrderLog : public IMObject
{
public:
	CCmdPostOrderLog(IDBConnection* hDBConnection);
	~CCmdPostOrderLog(void);
	virtual void* Msg(MSGDISPATCH);
	void* OnDlgFileCancel(MSGP);
	void* OnDlgFileName(MSGP);
public:
	IDBConnection*	hDBConnection;
	IStr*			hText;
};

//	==============================================================================
//	Class factory
//	==============================================================================

void* StartCmdPostOrdLog(IDBConnection* hDBConnection)
{
	return new CCmdPostOrderLog(hDBConnection);
}


//	==============================================================================
//	Constructor
//	==============================================================================

static WCHAR szFilters[] = L"-0ProcOrders Log files\0*.log\0\0";
static int nFilter = 0;
static WCHAR szLogFileName[MAX_PATH] = {0};

CCmdPostOrderLog::CCmdPostOrderLog(IDBConnection* hDBConnectionP)
{
	hDBConnection = hDBConnectionP;
	hText = NULL;

	if(szLogFileName[0] == 0)
	{
		hSvc->SvcFullFileName(szLogFileName,TEXT("#Orders0.log"));
	}

	MakeDlgFileName(this,szFilters,szLogFileName,szLogFileName,
		FDLG_OPEN,&nFilter,NULL);
}

//	==============================================================================
//	Destructor
//	==============================================================================

CCmdPostOrderLog::~CCmdPostOrderLog(void)
{
	if(hText)
		hText->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CCmdPostOrderLog::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DlgFileName: return OnDlgFileName(MPPTR);
	case MSG_DlgFileCancel: return OnDlgFileCancel(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	file name dialog canceled
//	==============================================================================

void* CCmdPostOrderLog::OnDlgFileCancel(MSGP)
{
	MPARMINT(FDLGTYPE,nDlgType);

	//	end this command object
	Release();
	return IM_RTN_NOTHING;
}

//	==============================================================================
//	receive file dialog name
//	==============================================================================

#define IFGET(pFor,hStg)if(hText->StrSkipUntil(pFor))\
	{hText->StrSkipWhiteSpace(); hText->StrScanUntil(L"\n",&hStg);}
#define IFGETML(pFor,hStg)if(hText->StrSkipUntil(pFor))\
	{hText->StrSkipWhiteSpace(); hText->StrScanUntil(L"~",&hStg);}

static WCHAR szMonthName[] = {L"January\0February\0March\0April\0May\0June\0"
		L"July\0August\0September\0October\0November\0December\0\0"};

void* CCmdPostOrderLog::OnDlgFileName(MSGP)
{
	MPARMPTR(MSGS_DlgFileName*,pmsg);
	FDLGTYPE nDlgType = pmsg->nDlgType;
	WCHAR* pFileName = pmsg->pFileName;
	IDocFile* hDocFile = pmsg->hDocFile;

	hText = hSvc->SvcLoadTextFile(pFileName);
	if(!hText)
	{
		//	end this command object
		Release();
		return IM_RTN_NOTHING;
	}

	IDBRow* hAKDO = MakeAuthKeyDO();
	IDBRow* hCDO = MakeContactDO();

	hText->StrScanStart();

	IStr* hDate = MakeStr();
	IStr* hCompany = MakeStr();
	IStr* hName = MakeStr();
	IStr* hAdr1 = MakeStr();
	IStr* hAdr2 = MakeStr();
	IStr* hCity = MakeStr();
	IStr* hState = MakeStr();
	IStr* hZip = MakeStr();
	IStr* hCountry = MakeStr();
	IStr* hEMail = MakeStr();
	IStr* hPhone = MakeStr();
	IStr* hFax = MakeStr();
	IStr* hOrdered = MakeStr();
	IStr* hBackupOrdered = MakeStr();
	IStr* hProduct = MakeStr();
	IStr* hQuantity = MakeStr();
	IStr* hSerialNr = MakeStr();
	IStr* hAuthKey = MakeStr();
	IStr* hOurNotes = MakeStr();
	IStr* hUserNotes = MakeStr();
	IStr* hSKU = MakeStr();
	IStr* hMonth = MakeStr();
	IStr* hCustId = MakeStr();
	IStr* hFormat = MakeStr();

	while(true)
	{
		IFGET(L"#Fmt:",hFormat)
		else break;
		IFGET(L"Date:",hDate)
		else break;
		IFGET(L"Name:",hName);
		IFGET(L"Comp:",hCompany);
		IFGET(L"Adr1:",hAdr1);
		IFGET(L"Adr2:",hAdr2);
		IFGET(L"City:",hCity);
		IFGET(L"State:",hState);
		IFGET(L"Zip:",hZip);
		IFGET(L"Ctry:",hCountry);
		IFGET(L"EMail:",hEMail);
		IFGET(L"Phone:",hPhone);
		IFGET(L"Fax:",hFax);
		IFGET(L"Ord:",hOrdered);
		IFGET(L"Bkup:",hBackupOrdered);
		IFGET(L"Prod:",hProduct);
		IFGET(L"Qty:",hQuantity);
		IFGET(L"SN:",hSerialNr);
		IFGET(L"AK:",hAuthKey);
		IFGETML(L"UsrNotes:",hOurNotes);
		IFGETML(L"OurNotes:",hUserNotes);

		//	determine product SKU from hOrdered text
		hSKU->StrEmpty();
		if(hSvc->SvcContainsW(TEXT("FastCAD7"),hProduct->StrTextPtr(),0)!=0)
			hSKU->StrSetTextW(TEXT("FC75NEW"));
		else if(hSvc->SvcContainsW(TEXT("EasyCAD7"),hProduct->StrTextPtr(),0)!=0)
			hSKU->StrSetTextW(TEXT("EC75NEW"));
		else if(hSvc->SvcContainsW(TEXT("EasyCAD6"),hProduct->StrTextPtr(),0)!=0)
			hSKU->StrSetTextW(TEXT("EC65NEW"));
		else if(hSvc->SvcContainsW(TEXT("FastCAD6"),hProduct->StrTextPtr(),0)!=0)
			hSKU->StrSetTextW(TEXT("FC65NEW"));

		//	convert date to timestamp
		UINT nDay,nMonth,nYear;
		hDate->StrScanStart();
		hDate->StrSkipUntil(L", ");
		hDate->StrScanUntil(L" ",&hMonth);
		nMonth = hSvc->SvcListFindStrNr(szMonthName,hMonth->StrTextPtr());
		hDate->StrScanUInt(&nDay);
		hDate->StrScanMatchChr(L',');
		hDate->StrScanUInt(&nYear);

		hDate->StrEmpty();
		hDate->StrFmtInt(nYear);
		hDate->StrAppendC(L'-');
		if(nMonth < 10)
			hDate->StrAppendC(L'0');
		hDate->StrFmtInt(nMonth);
		hDate->StrAppendC(L'-');
		if(nDay < 10)
			hDate->StrAppendC(L'0');
		hDate->StrFmtInt(nDay);

		//	write to contacts table
		if(hName->StrLength() && hEMail->StrLength())
		{
			hCDO->DBaseOSetText(2,hDate->StrTextPtr());
			hCDO->DBaseOSetText(3,hName->StrTextPtr());
			hCDO->DBaseOSetText(4,hCompany->StrTextPtr());
			hCDO->DBaseOSetText(5,hAdr1->StrTextPtr());
			hCDO->DBaseOSetText(6,hAdr2->StrTextPtr());
			hCDO->DBaseOSetText(7,hCity->StrTextPtr());
			hCDO->DBaseOSetText(8,hState->StrTextPtr());
			hCDO->DBaseOSetText(9,hZip->StrTextPtr());
			hCDO->DBaseOSetText(10,hCountry->StrTextPtr());
			hCDO->DBaseOSetText(11,hEMail->StrTextPtr());
			hCDO->DBaseOSetText(12,hPhone->StrTextPtr());
			hCDO->DBaseOSetText(13,TEXT("Old PO"));

			//	add to database
			hDBConnection->DBaseOAdd(hCDO);
			UINT custid = hDBConnection->DBaseGetInsertId();
			hCustId->StrEmpty();
			hCustId->StrFmtInt(custid);
		}

		//	if valid key, write to akey table
		if(hName->StrLength() && hSerialNr->StrLength() && hAuthKey->StrLength()
			&& hSKU->StrLength())
		{
			hAKDO->DBaseOSetText(2,TEXT("0"));
			hAKDO->DBaseOSetText(3,hDate->StrTextPtr());
			hAKDO->DBaseOSetText(4,hSKU->StrTextPtr());
			hAKDO->DBaseOSetText(5,hName->StrTextPtr());
			hAKDO->DBaseOSetText(6,hCompany->StrTextPtr());
			hAKDO->DBaseOSetText(7,hSerialNr->StrTextPtr());
			hAKDO->DBaseOSetText(8,hAuthKey->StrTextPtr());
			hAKDO->DBaseOSetText(9,TEXT("P"));
			hAKDO->DBaseOSetText(10,hCustId->StrTextPtr());

			//	add to database
			hDBConnection->DBaseOAdd(hAKDO);
		}
	}

	hDate->Release();
	hCompany->Release();
	hName->Release();
	hAdr1->Release();
	hAdr2->Release();
	hCity->Release();
	hState->Release();
	hZip->Release();
	hCountry->Release();
	hEMail->Release();
	hPhone->Release();
	hFax->Release();
	hOrdered->Release();
	hBackupOrdered->Release();
	hProduct->Release();
	hQuantity->Release();
	hSerialNr->Release();
	hAuthKey->Release();
	hOurNotes->Release();
	hUserNotes->Release();
	hSKU->Release();
	hMonth->Release();
	hCustId->Release();
	hFormat->Release();

	hAKDO->Release();
	hCDO->Release();

	//	end this command object
	Release();
	return IM_RTN_NOTHING;
}

//	==============================================================================
