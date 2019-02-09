//	==============================================================================
//	CPaymentDO - Payment Information Data Object
//	------------------------------------------------------------------------------
//	Copyright ©2009 Evolution Computing, Inc.
//	All rights reserved
//	------------------------------------------------------------------------------
//	Because thousands of these objects may be returned from a query,
//	it has been designed to have the minimal instance data, and return
//	most information from literals in the class implementation.
//
//	To make it as easy as possible to write this object, most functions are
//	implemented by passing an instance of this object to an IDBConnection object
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

//	==============================================================================
//	Class definition
//	==============================================================================

class CPaymentDO : public IDBRow
{
public:
	CPaymentDO(void);
	~CPaymentDO(void);
	virtual void* Msg(int nMsg,...);
	void* OnColHint(MSGP);
	void* OnColFind(MSGP);
	void* OnColName(MSGP);
	void* OnColDef(MSGP);
	void* OnColLabel(MSGP);
	void* OnClear(void);
	void* OnStr(MSGP);
	void* OnGetText(MSGP);
	void* OnSetText(MSGP);
	void* OnValidate(MSGP);
public:
	IStr*	pmtid;
	IStr*	pmt_type;
	IStr*	status;
	IStr*	ref_id;
	IStr*	ccname;
	IStr*	cctype;
	IStr*	ccnum;
	IStr*	ccmonth;
	IStr*	ccyear;
	IStr*	ccsec;
	IStr*	amount;
	IStr*	ordid;
	IStr*	ccfee;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakePaymentDO(void)
{
	return (IDBRow*) new CPaymentDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CPaymentDO::CPaymentDO()
{
	pmtid = MakeStr();
	pmt_type = MakeStr();
	status = MakeStr();
	ref_id = MakeStr();
	ccname = MakeStr();
	cctype = MakeStr();
	ccnum = MakeStr();
	ccmonth = MakeStr();
	ccyear = MakeStr();
	ccsec = MakeStr();
	amount = MakeStr();
	ordid = MakeStr();
	ccfee = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CPaymentDO::~CPaymentDO(void)
{
	pmtid->Release();
	pmt_type->Release();
	status->Release();
	ref_id->Release();
	ccname->Release();
	cctype->Release();
	ccnum->Release();
	ccmonth->Release();
	ccyear->Release();
	ccsec->Release();
	amount->Release();
	ordid->Release();
	ccfee->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CPaymentDO::Msg(int nMsg,...)
{
	MSGPTR

	switch(nMsg)
	{
	case MSG_DBOTableName: return L"Payments";
	case MSG_DBOMake: return new CPaymentDO();
	case MSG_DBOClear: return OnClear();
	case MSG_DBOPrimaryKeyIdx: return (void*)(int)1;
	case MSG_DBONrCols: return (void*)(int)13;
	case MSG_DBOColHint: return OnColHint(msgptr);
	case MSG_DBOColFind: return OnColFind(msgptr);
	case MSG_DBOColName: return OnColName(msgptr);
	case MSG_DBOColDef: return OnColDef(msgptr);
	case MSG_DBOColLabel: return OnColLabel(msgptr);
	case MSG_DBOStr: return OnStr(msgptr);
	case MSG_DBOText: return OnGetText(msgptr);
	case MSG_DBOSetText: return OnSetText(msgptr);
	case MSG_DBOValidate: return OnValidate(msgptr);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	Get Col Hint (assist in building creation, update SQL)
//	==============================================================================

void* CPaymentDO::OnColHint(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return (void*)(int)DBOH_AUTOINCREMENT;
	case 2: return (void*)(int)DBOH_NORMAL;
	case 3: return (void*)(int)DBOH_NORMAL;
	case 4: return (void*)(int)DBOH_NORMAL;
	case 5: return (void*)(int)DBOH_NORMAL;
	case 6: return (void*)(int)DBOH_NORMAL;
	case 7: return (void*)(int)DBOH_NORMAL;
	case 8: return (void*)(int)DBOH_NORMAL;
	case 9: return (void*)(int)DBOH_NORMAL;
	case 10: return (void*)(int)DBOH_NORMAL;
	case 11: return (void*)(int)DBOH_NORMAL;
	case 12: return (void*)(int)(DBOH_NORMAL|DBOH_FOREIGNKEY);
	case 13: return (void*)(int)DBOH_NORMAL;
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"pmt_id\0"
	L"pmt_type\0"
	L"status\0"
	L"ref_id\0"
	L"ccname\0"
	L"cctype\0"
	L"ccnum\0"
	L"ccmonth\0"
	L"ccyear\0"
	L"ccsec\0"
	L"amount\0"
	L"order_id\0"
	L"cc_fee\0"
;

void* CPaymentDO::OnColFind(MSGP)
{
	MSGPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CPaymentDO::OnColName(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"pmt_id";
	case 2: return L"pmt_type";
	case 3: return L"status";
	case 4: return L"ref_id";
	case 5: return L"ccname";
	case 6: return L"cctype";
	case 7: return L"ccnum";
	case 8: return L"ccmonth";
	case 9: return L"ccyear";
	case 10: return L"ccsec";
	case 11: return L"amount";
	case 12: return L"order_id";
	case 13: return L"cc_fee";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CPaymentDO::OnColDef(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"int NOT NULL AUTO_INCREMENT";
	case 2: return L"char(12) NOT NULL";
	case 3: return L"char(12) NOT NULL";
	case 4: return L"char(20) NOT NULL";
	case 5: return L"char(80) NOT NULL";
	case 6: return L"char(20) NULL";
	case 7: return L"char(20) NOT NULL";
	case 8: return L"char(2) NULL";
	case 9: return L"char(4) NOT NULL";
	case 10: return L"char(3) NULL";
	case 11: return L"decimal(8,2) NOT NULL";
	case 12: return L"int NOT NULL";
	case 13: return L"decimal(8,2) NULL";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CPaymentDO::OnColLabel(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"Pmt Id";
	case 2: return L"Type";
	case 3: return L"Status";
	case 4: return L"Ref Id";
	case 5: return L"CC Name";
	case 6: return L"CC Type";
	case 7: return L"CC Number";
	case 8: return L"Exp Month";
	case 9: return L"Exp Year";
	case 10: return L"SCode";
	case 11: return L"Amount";
	case 12: return L"Order Nr";
	case 13: return L"CC Fee";
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CPaymentDO::OnClear(void)
{
	pmtid->SEmpty();
	pmt_type->SEmpty();
	status->SEmpty();
	ref_id->SEmpty();
	ccname->SEmpty();
	cctype->SEmpty();
	ccnum->SEmpty();
	ccmonth->SEmpty();
	ccyear->SEmpty();
	ccsec->SEmpty();
	amount->SEmpty();
	ordid->SEmpty();
	ccfee->SEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->SLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CPaymentDO::OnValidate(MSGP)
{
	MSGPARMPTR(UINT*,pVErrTbl);

	UINT ignore[12];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(pmtid,1);
	VALIDATEFIELD(pmt_type,2);
	VALIDATEFIELD(status,3);
	VALIDATEFIELDOK(ref_id,4);
	VALIDATEFIELD(ccname,5);
	VALIDATEFIELD(cctype,6);
	VALIDATEFIELD(ccnum,7);
	VALIDATEFIELD(ccmonth,8);
	VALIDATEFIELD(ccyear,9);
	VALIDATEFIELDOK(ccsec,10);
	VALIDATEFIELD(amount,11);
	VALIDATEFIELD(ordid,12);
	VALIDATEFIELDOK(ccfee,13);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CPaymentDO::OnStr(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return pmtid;
	case 2: return pmt_type;
	case 3: return status;
	case 4: return ref_id;
	case 5: return ccname;
	case 6: return cctype;
	case 7: return ccnum;
	case 8: return ccmonth;
	case 9: return ccyear;
	case 10: return ccsec;
	case 11: return amount;
	case 12: return ordid;
	case 13: return ccfee;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CPaymentDO::OnGetText(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return pmtid->STextPtr();
	case 2: return pmt_type->STextPtr();
	case 3: return status->STextPtr();
	case 4: return ref_id->STextPtr();
	case 5: return ccname->STextPtr();
	case 6: return cctype->STextPtr();
	case 7: return ccnum->STextPtr();
	case 8: return ccmonth->STextPtr();
	case 9: return ccyear->STextPtr();
	case 10: return ccsec->STextPtr();
	case 11: return amount->STextPtr();
	case 12: return ordid->STextPtr();
	case 13: return ccfee->STextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CPaymentDO::OnSetText(MSGP)
{
	MSGPARM(int,ix);
	MSGPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: pmtid->SSetTextW(pText); break;
	case 2: pmt_type->SSetTextW(pText); break;
	case 3: status->SSetTextW(pText); break;
	case 4: ref_id->SSetTextW(pText); break;
	case 5: ccname->SSetTextW(pText); break;
	case 6: cctype->SSetTextW(pText); break;
	case 7: ccnum->SSetTextW(pText); break;
	case 8: ccmonth->SSetTextW(pText); break;
	case 9: ccyear->SSetTextW(pText); break;
	case 10: ccsec->SSetTextW(pText); break;
	case 11: amount->SSetTextW(pText); break;
	case 12: ordid->SSetTextW(pText); break;
	case 13: ccfee->SSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

