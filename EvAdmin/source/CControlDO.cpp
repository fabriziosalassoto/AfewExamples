//	==============================================================================
//	CControlDO - Control Information Data Object
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
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

class CControlDO : public IDBRow
{
public:
	CControlDO(void);
	~CControlDO(void);
	virtual void* Msg(MSGDISPATCH);
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
	IStr*	ctlid;
	IStr*	aztax;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeControlDO(void)
{
	return (IDBRow*) new CControlDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CControlDO::CControlDO()
{
	ctlid = MakeStr();
	aztax = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CControlDO::~CControlDO(void)
{
	ctlid->Release();
	aztax->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CControlDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("Controls");
	case MSG_DBaseOMake: return new CControlDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(2);
	case MSG_DBaseOColHint: return OnColHint(MPPTR);
	case MSG_DBaseOColFind: return OnColFind(MPPTR);
	case MSG_DBaseOColName: return OnColName(MPPTR);
	case MSG_DBaseOColDef: return OnColDef(MPPTR);
	case MSG_DBaseOColLabel: return OnColLabel(MPPTR);
	case MSG_DBaseOStr: return OnStr(MPPTR);
	case MSG_DBaseOText: return OnGetText(MPPTR);
	case MSG_DBaseOSetText: return OnSetText(MPPTR);
	case MSG_DBaseOValidate: return OnValidate(MPPTR);
	};

	return IM_RTN_IGNORED;
}

//	==============================================================================
//	Get Col Hint (assist in building creation, update SQL)
//	==============================================================================

void* CControlDO::OnColHint(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return MRTNVAL(DBOH_AUTOINCREMENT);
	case 2: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"ctl_id\0"
	L"aztax\0"
;

void* CControlDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CControlDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("ctl_id");
	case 2: return TEXT("aztax");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CControlDO::OnColDef(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("int NOT NULL AUTO_INCREMENT");
	case 2: return TEXT("char(12) NOT NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CControlDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("Control Id");
	case 2: return TEXT("AZ Sales Tax");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CControlDO::OnClear(void)
{
	ctlid->StrEmpty();
	aztax->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CControlDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[2];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(ctlid,1);
	VALIDATEFIELD(aztax,2);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CControlDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return ctlid;
	case 2: return aztax;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CControlDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return ctlid->StrTextPtr();
	case 2: return aztax->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* CControlDO::OnSetText(MSGP)
{
	MPARMINT(int,ix);
	MPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: ctlid->StrSetTextW(pText); break;
	case 2: aztax->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

