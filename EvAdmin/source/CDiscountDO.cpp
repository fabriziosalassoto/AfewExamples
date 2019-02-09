//	==============================================================================
//	CDiscountDO - Discount Information Data Object
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

class CDiscountDO : public IDBRow
{
public:
	CDiscountDO(void);
	~CDiscountDO(void);
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
	IStr*	discid;
	IStr*	dgroup;
	IStr*	dclass;
	IStr*	discount;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeDiscountDO(void)
{
	return (IDBRow*) new CDiscountDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CDiscountDO::CDiscountDO()
{
	discid = MakeStr();
	dgroup = MakeStr();
	dclass = MakeStr();
	discount = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CDiscountDO::~CDiscountDO(void)
{
	discid->Release();
	dgroup->Release();
	dclass->Release();
	discount->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CDiscountDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("Discounts");
	case MSG_DBaseOMake: return new CDiscountDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(4);
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

void* CDiscountDO::OnColHint(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return MRTNVAL(DBOH_AUTOINCREMENT);
	case 2: return MRTNVAL(DBOH_NORMAL);
	case 3: return MRTNVAL(DBOH_NORMAL);
	case 4: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"discount_id\0"
	L"discount_group\0"
	L"discount_class\0"
	L"discount\0"
;

void* CDiscountDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CDiscountDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("discount_id");
	case 2: return TEXT("discount_group");
	case 3: return TEXT("discount_class");
	case 4: return TEXT("discount");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CDiscountDO::OnColDef(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("int NOT NULL AUTO_INCREMENT");
	case 2: return TEXT("char(12) NOT NULL");
	case 3: return TEXT("char(12) NOT NULL");
	case 4: return TEXT("decimal(8,2) NOT NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CDiscountDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("Discount Id");
	case 2: return TEXT("Discount Group");
	case 3: return TEXT("Discount Class");
	case 4: return TEXT("Discount");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CDiscountDO::OnClear(void)
{
	discid->StrEmpty();
	dgroup->StrEmpty();
	dclass->StrEmpty();
	discount->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CDiscountDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[4];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(discid,1);
	VALIDATEFIELD(dgroup,2);
	VALIDATEFIELD(dclass,3);
	VALIDATEFIELD(discount,4);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CDiscountDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return discid;
	case 2: return dgroup;
	case 3: return dclass;
	case 4: return discount;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CDiscountDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return discid->StrTextPtr();
	case 2: return dgroup->StrTextPtr();
	case 3: return dclass->StrTextPtr();
	case 4: return discount->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* CDiscountDO::OnSetText(MSGP)
{
	MPARMINT(int,ix);
	MPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: discid->StrSetTextW(pText); break;
	case 2: dgroup->StrSetTextW(pText); break;
	case 3: dclass->StrSetTextW(pText); break;
	case 4: discount->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

