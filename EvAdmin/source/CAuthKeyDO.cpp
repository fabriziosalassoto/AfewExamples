//	==============================================================================
//	CAuthKeyDO - AuthKey Information Data Object
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

class CAuthKeyDO : public IDBRow
{
public:
	CAuthKeyDO(void);
	~CAuthKeyDO(void);
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
	IStr*	keyid;
	IStr*	ordid;
	IStr*	date;
	IStr*	prodsku;
	IStr*	name;
	IStr*	company;
	IStr*	serno;
	IStr*	akey;
	IStr*	flags;
	IStr*	custid;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeAuthKeyDO(void)
{
	return (IDBRow*) new CAuthKeyDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CAuthKeyDO::CAuthKeyDO()
{
	keyid = MakeStr();
	ordid = MakeStr();
	date = MakeStr();
	prodsku = MakeStr();
	name = MakeStr();
	company = MakeStr();
	serno = MakeStr();
	akey = MakeStr();
	flags = MakeStr();
	custid = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CAuthKeyDO::~CAuthKeyDO(void)
{
	keyid->Release();
	ordid->Release();
	date->Release();
	prodsku->Release();
	name->Release();
	company->Release();
	serno->Release();
	akey->Release();
	custid->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CAuthKeyDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("AuthKeys");
	case MSG_DBaseOMake: return new CAuthKeyDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(10);
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

void* CAuthKeyDO::OnColHint(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return MRTNVAL(DBOH_AUTOINCREMENT);
	case 2: return MRTNVAL(DBOH_FOREIGNKEY);
	case 3: return MRTNVAL((DBOH_AUTOTIME|DBOH_DATEONLY));
	case 4: return MRTNVAL(DBOH_NORMAL);
	case 5: return MRTNVAL(DBOH_NORMAL);
	case 6: return MRTNVAL(DBOH_NORMAL);
	case 7: return MRTNVAL(DBOH_NORMAL);
	case 8: return MRTNVAL(DBOH_NORMAL);
	case 9: return MRTNVAL(DBOH_NORMAL);
	case 10: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"key_id\0"
	L"order_id\0"
	L"date\0"
	L"prod_sku\0"
	L"name\0"
	L"company\0"
	L"serno\0"
	L"akey\0"
	L"flags\0"
	L"cust_id\0"
;

void* CAuthKeyDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CAuthKeyDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("key_id");
	case 2: return TEXT("ord_id");
	case 3: return TEXT("date");
	case 4: return TEXT("prod_sku");
	case 5: return TEXT("name");
	case 6: return TEXT("company");
	case 7: return TEXT("serno");
	case 8: return TEXT("akey");
	case 9: return TEXT("flags");
	case 10: return TEXT("cust_id");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CAuthKeyDO::OnColDef(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("int NOT NULL AUTO_INCREMENT");
	case 2: return TEXT("int NULL");
	case 3: return TEXT("TIMESTAMP");
	case 4: return TEXT("char(20) NOT NULL");
	case 5: return TEXT("char(80) NOT NULL");
	case 6: return TEXT("char(60) NOT NULL");
	case 7: return TEXT("char(26) NULL");
	case 8: return TEXT("char(30) NULL");
	case 9: return TEXT("char(10) NULL");
	case 10: return TEXT("char(10) NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CAuthKeyDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("Key Id");
	case 2: return TEXT("Order Nr");
	case 3: return TEXT("Date");
	case 4: return TEXT("SKU");
	case 5: return TEXT("Name");
	case 6: return TEXT("Company");
	case 7: return TEXT("Serial Nr");
	case 8: return TEXT("Auth Key");
	case 9: return TEXT("Flags");
	case 10: return TEXT("Customer Id");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CAuthKeyDO::OnClear(void)
{
	keyid->StrEmpty();
	ordid->StrEmpty();
	date->StrEmpty();
	prodsku->StrEmpty();
	name->StrEmpty();
	company->StrEmpty();
	serno->StrEmpty();
	akey->StrEmpty();
	flags->StrEmpty();
	custid->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CAuthKeyDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[10];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(keyid,1);
	VALIDATEFIELD(ordid,2);
	VALIDATEFIELDOK(date,3);
	VALIDATEFIELD(prodsku,4);
	VALIDATEFIELD(name,5);
	VALIDATEFIELD(company,6);
	VALIDATEFIELD(serno,7);
	VALIDATEFIELD(akey,8);
	VALIDATEFIELDOK(flags,9);
	VALIDATEFIELDOK(custid,10);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CAuthKeyDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return keyid;
	case 2: return ordid;
	case 3: return date;
	case 4: return prodsku;
	case 5: return name;
	case 6: return company;
	case 7: return serno;
	case 8: return akey;
	case 9: return flags;
	case 10: return custid;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CAuthKeyDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return keyid->StrTextPtr();
	case 2: return ordid->StrTextPtr();
	case 3: return date->StrTextPtr();
	case 4: return prodsku->StrTextPtr();
	case 5: return name->StrTextPtr();
	case 6: return company->StrTextPtr();
	case 7: return serno->StrTextPtr();
	case 8: return akey->StrTextPtr();
	case 9: return flags->StrTextPtr();
	case 10: return custid->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CAuthKeyDO::OnSetText(MSGP)
{
	MPARMINT(int,ix);
	MPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: keyid->StrSetTextW(pText); break;
	case 2: ordid->StrSetTextW(pText); break;
	case 3: date->StrSetTextW(pText); break;
	case 4: prodsku->StrSetTextW(pText); break;
	case 5: name->StrSetTextW(pText); break;
	case 6: company->StrSetTextW(pText); break;
	case 7: serno->StrSetTextW(pText); break;
	case 8: akey->StrSetTextW(pText); break;
	case 9: flags->StrSetTextW(pText); break;
	case 10: custid->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

