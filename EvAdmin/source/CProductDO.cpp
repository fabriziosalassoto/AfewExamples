//	==============================================================================
//	CProductDO - Product Information Data Object
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

class CProductDO : public IDBRow
{
public:
	CProductDO(void);
	~CProductDO(void);
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
	IStr*	prodsku;
	IStr*	prodname;
	IStr*	prodclass;
	IStr*	skudesc;
	IStr*	price;
	IStr*	disc_class;
	IStr*	flags;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeProductDO(void)
{
	return (IDBRow*) new CProductDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CProductDO::CProductDO()
{
	prodsku = MakeStr();
	prodname = MakeStr();
	prodclass = MakeStr();
	skudesc = MakeStr();
	price = MakeStr();
	disc_class = MakeStr();
 	flags = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CProductDO::~CProductDO(void)
{
	prodsku->Release();
	prodname->Release();
	prodclass->Release();
	skudesc->Release();
	price->Release();
	disc_class->Release();
	flags->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CProductDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("Products");
	case MSG_DBaseOMake: return new CProductDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(7);
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

void* CProductDO::OnColHint(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return MRTNVAL(DBOH_NORMAL);
	case 2: return MRTNVAL(DBOH_NORMAL);
	case 3: return MRTNVAL(DBOH_NORMAL);
	case 4: return MRTNVAL(DBOH_NORMAL);
	case 5: return MRTNVAL(DBOH_NORMAL);
	case 6: return MRTNVAL(DBOH_NORMAL);
	case 7: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"prod_sku\0"
	L"prodname\0"
	L"prodclass\0"
	L"skudesc\0"
	L"price\0"
	L"disc_class\0"
	L"flags\0"
;

void* CProductDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CProductDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("prod_sku");
	case 2: return TEXT("prodname");
	case 3: return TEXT("prodclass");
	case 4: return TEXT("skudesc");
	case 5: return TEXT("price");
	case 6: return TEXT("disc_class");
	case 7: return TEXT("flags");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CProductDO::OnColDef(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("char(12) NOT NULL");
	case 2: return TEXT("char(64) NOT NULL");
	case 3: return TEXT("char(20) NOT NULL");
	case 4: return TEXT("char(255) NOT NULL");
	case 5: return TEXT("decimal(8,2) NOT NULL");
	case 6: return TEXT("char(12) NULL");
	case 7: return TEXT("char(12) NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CProductDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("SKU");
	case 2: return TEXT("Product");
	case 3: return TEXT("Class");
	case 4: return TEXT("Description");
	case 5: return TEXT("Price");
	case 6: return TEXT("Discount Class");
	case 7: return TEXT("Flags");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CProductDO::OnClear(void)
{
	prodsku->StrEmpty();
	prodname->StrEmpty();
	prodclass->StrEmpty();
	skudesc->StrEmpty();
	price->StrEmpty();
	disc_class->StrEmpty();
	flags->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CProductDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[7];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELD(prodsku,1);
	VALIDATEFIELD(prodname,2);
	VALIDATEFIELD(prodclass,3);
	VALIDATEFIELD(skudesc,4);
	VALIDATEFIELD(price,5);
	VALIDATEFIELD(disc_class,6);
	VALIDATEFIELDOK(flags,7);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CProductDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return prodsku;
	case 2: return prodname;
	case 3: return prodclass;
	case 4: return skudesc;
	case 5: return price;
	case 6: return disc_class;
	case 7: return flags;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CProductDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return prodsku->StrTextPtr();
	case 2: return prodname->StrTextPtr();
	case 3: return prodclass->StrTextPtr();
	case 4: return skudesc->StrTextPtr();
	case 5: return price->StrTextPtr();
	case 6: return disc_class->StrTextPtr();
	case 7: return flags->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CProductDO::OnSetText(MSGP)
{
	MPARMINT(int,ix);
	MPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: prodsku->StrSetTextW(pText); break;
	case 2: prodname->StrSetTextW(pText); break;
	case 3: prodclass->StrSetTextW(pText); break;
	case 4: skudesc->StrSetTextW(pText); break;
	case 5: price->StrSetTextW(pText); break;
	case 6: disc_class->StrSetTextW(pText); break;
	case 7: flags->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

