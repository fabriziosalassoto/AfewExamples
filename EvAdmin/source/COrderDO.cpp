//	==============================================================================
//	COrderDO - Order Information Data Object
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

class COrderDO : public IDBRow
{
public:
	COrderDO(void);
	~COrderDO(void);
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
	IStr*	ordid;
	IStr*	custid;
	IStr*	date;
	IStr*	tax;
	IStr*	totalprice;
	IStr*	status;
	IStr*	notes;
	IStr*	invoice;
	IStr*	flags;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeOrderDO(void)
{
	return (IDBRow*) new COrderDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

COrderDO::COrderDO()
{
	ordid = MakeStr();
	custid = MakeStr();
	date = MakeStr();
	tax = MakeStr();
	totalprice = MakeStr();
	status = MakeStr();
	notes = MakeStr();
	invoice = MakeStr();
	flags = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

COrderDO::~COrderDO(void)
{
	ordid->Release();
	custid->Release();
	date->Release();
	tax->Release();
	totalprice->Release();
	status->Release();
	notes->Release();
	invoice->Release();
	flags->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* COrderDO::Msg(int nMsg,...)
{
	MSGPTR

	switch(nMsg)
	{
	case MSG_DBOTableName: return L"Orders";
	case MSG_DBOMake: return new COrderDO();
	case MSG_DBOClear: return OnClear();
	case MSG_DBOPrimaryKeyIdx: return (void*)(int)1;
	case MSG_DBONrCols: return (void*)(int)9;
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

void* COrderDO::OnColHint(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return (void*)(int)DBOH_AUTOINCREMENT;
	case 2: return (void*)(int)DBOH_FOREIGNKEY;
	case 3: return (void*)(int)(DBOH_AUTOTIME|DBOH_DATEONLY);
	case 4: return (void*)(int)DBOH_NORMAL;
	case 5: return (void*)(int)DBOH_NORMAL;
	case 6: return (void*)(int)DBOH_NORMAL;
	case 7: return (void*)(int)DBOH_NORMAL;
	case 8: return (void*)(int)DBOH_NORMAL;
	case 9: return (void*)(int)DBOH_NORMAL;
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"order_id\0"
	L"cust_id\0"
	L"date\0"
	L"tax\0"
	L"total_price\0"
	L"status\0"
	L"notes\0"
	L"invoice\0"
	L"flags\0"
;

void* COrderDO::OnColFind(MSGP)
{
	MSGPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* COrderDO::OnColName(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"order_id";
	case 2: return L"cust_id";
	case 3: return L"date";
	case 4: return L"tax";
	case 5: return L"total_price";
	case 6: return L"status";
	case 7: return L"notes";
	case 8: return L"invoice";
	case 9: return L"flags";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* COrderDO::OnColDef(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"int NOT NULL AUTO_INCREMENT";
	case 2: return L"int NOT NULL";
	case 3: return L"TIMESTAMP";
	case 4: return L"decimal(8,2) NOT NULL";
	case 5: return L"decimal(8,2) NOT NULL";
	case 6: return L"char(12) NOT NULL";
	case 7: return L"text NULL";
	case 8: return L"char(20) NULL";
	case 9: return L"char(10) NULL";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* COrderDO::OnColLabel(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"Order Nr";
	case 2: return L"Cust Id";
	case 3: return L"Date";
	case 4: return L"Tax";
	case 5: return L"Total";
	case 6: return L"Status";
	case 7: return L"Notes";
	case 8: return L"Invoice";
	case 9: return L"Flags";
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* COrderDO::OnClear(void)
{
	ordid->SEmpty();
	custid->SEmpty();
	date->SEmpty();
	tax->SEmpty();
	totalprice->SEmpty();
	status->SEmpty();
	notes->SEmpty();
	invoice->SEmpty();
	flags->SEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->SLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* COrderDO::OnValidate(MSGP)
{
	MSGPARMPTR(UINT*,pVErrTbl);

	UINT ignore[100];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(ordid,1);
	VALIDATEFIELD(custid,2);
	VALIDATEFIELDOK(date,3);
	VALIDATEFIELD(tax,4);
	VALIDATEFIELD(totalprice,5);
	VALIDATEFIELD(status,6);
	VALIDATEFIELDOK(notes,7);
	VALIDATEFIELDOK(invoice,8);
	VALIDATEFIELDOK(flags,9);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* COrderDO::OnStr(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return ordid;
	case 2: return custid;
	case 3: return date;
	case 4: return tax;
	case 5: return totalprice;
	case 6: return status;
	case 7: return notes;
	case 8: return invoice;
	case 9: return flags;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* COrderDO::OnGetText(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return ordid->STextPtr();
	case 2: return custid->STextPtr();
	case 3: return date->STextPtr();
	case 4: return tax->STextPtr();
	case 5: return totalprice->STextPtr();
	case 6: return status->STextPtr();
	case 7: return notes->STextPtr();
	case 8: return invoice->STextPtr();
	case 9: return flags->STextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* COrderDO::OnSetText(MSGP)
{
	MSGPARM(int,ix);
	MSGPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: ordid->SSetTextW(pText); break;
	case 2: custid->SSetTextW(pText); break;
	case 3: date->SSetTextW(pText); break;
	case 4: tax->SSetTextW(pText); break;
	case 5: totalprice->SSetTextW(pText); break;
	case 6: status->SSetTextW(pText); break;
	case 7: notes->SSetTextW(pText); break;
	case 8: invoice->SSetTextW(pText); break;
	case 9: flags->SSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

