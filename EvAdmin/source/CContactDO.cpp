//	==============================================================================
//	CContactDO - Contact Information Data Object
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

class CContactDO : public IDBRow
{
public:
	CContactDO(void);
	~CContactDO(void);
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
	IStr*	custid;
	IStr*	date;
	IStr*	name;
	IStr*	company;
	IStr*	caddress;
	IStr*	caddress2;
	IStr*	city;
	IStr*	state;
	IStr*	zip;
	IStr*	country;
	IStr*	email;
	IStr*	phone;
	IStr*	type;
	IStr*	disc_group;
	IStr*	fwdid;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeContactDO(void)
{
	return (IDBRow*) new CContactDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

CContactDO::CContactDO()
{
	custid = MakeStr();
	date = MakeStr();
	name = MakeStr();
	company = MakeStr();
	caddress = MakeStr();
	caddress2 = MakeStr();
	city = MakeStr();
	state = MakeStr();
	zip = MakeStr();
	country = MakeStr();
	email = MakeStr();
	phone = MakeStr();
	type = MakeStr();
	disc_group = MakeStr();
	fwdid = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

CContactDO::~CContactDO(void)
{
	custid->Release();
	date->Release();
	name->Release();
	company->Release();
	caddress->Release();
	caddress2->Release();
	city->Release();
	state->Release();
	zip->Release();
	country->Release();
	email->Release();
	phone->Release();
	type->Release();
	disc_group->Release();
	fwdid->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* CContactDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("Contacts");
	case MSG_DBaseOMake: return new CContactDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(15);
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

void* CContactDO::OnColHint(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return MRTNVAL(DBOH_AUTOINCREMENT);
	case 2: return MRTNVAL((DBOH_AUTOTIME|DBOH_DATEONLY));
	case 3: return MRTNVAL(DBOH_NORMAL);
	case 4: return MRTNVAL(DBOH_NORMAL);
	case 5: return MRTNVAL(DBOH_NORMAL);
	case 6: return MRTNVAL(DBOH_NORMAL);
	case 7: return MRTNVAL(DBOH_NORMAL);
	case 8: return MRTNVAL(DBOH_NORMAL);
	case 9: return MRTNVAL(DBOH_NORMAL);
	case 10: return MRTNVAL(DBOH_NORMAL);
	case 11: return MRTNVAL(DBOH_NORMAL);
	case 12: return MRTNVAL(DBOH_NORMAL);
	case 13: return MRTNVAL(DBOH_NORMAL);
	case 14: return MRTNVAL(DBOH_NORMAL);
	case 15: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"cust_id\0"
	L"date\0"
	L"name\0"
	L"company\0"
	L"address\0"
	L"address2\0"
	L"city\0"
	L"state\0"
	L"zip\0"
	L"country\0"
	L"email\0"
	L"phone\0"
	L"type\0"
	L"disc_group\0"
	L"fwd_id\0"
;

void* CContactDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* CContactDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("cust_id");
	case 2: return TEXT("date");
	case 3: return TEXT("name");
	case 4: return TEXT("company");
	case 5: return TEXT("address");
	case 6: return TEXT("address2");
	case 7: return TEXT("city");
	case 8: return TEXT("state");
	case 9: return TEXT("zip");
	case 10: return TEXT("country");
	case 11: return TEXT("email");
	case 12: return TEXT("phone");
	case 13: return TEXT("type");
	case 14: return TEXT("disc_group");
	case 15: return TEXT("fwd_id");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* CContactDO::OnColDef(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("int NOT NULL AUTO_INCREMENT");
	case 2: return TEXT("TIMESTAMP");
	case 3: return TEXT("char(80) NOT NULL");
	case 4: return TEXT("char(60) NULL");
	case 5: return TEXT("char(60) NOT NULL");
	case 6: return TEXT("char(60) NULL");
	case 7: return TEXT("char(60) NOT NULL");
	case 8: return TEXT("char(10) NULL");
	case 9: return TEXT("char(12) NULL");
	case 10: return TEXT("char(60) NOT NULL");
	case 11: return TEXT("char(255) NOT NULL");
	case 12: return TEXT("char(24) NOT NULL");
	case 13: return TEXT("char(12) NULL");
	case 14: return TEXT("char(12) NULL");
	case 15: return TEXT("int NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* CContactDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("Customer Id");
	case 2: return TEXT("Date");
	case 3: return TEXT("Name");
	case 4: return TEXT("Company");
	case 5: return TEXT("Address");
	case 6: return TEXT("Address2");
	case 7: return TEXT("City");
	case 8: return TEXT("State");
	case 9: return TEXT("Zip");
	case 10: return TEXT("Country");
	case 11: return TEXT("EMail");
	case 12: return TEXT("Phone");
	case 13: return TEXT("Type");
	case 14: return TEXT("Discount Group");
	case 15: return TEXT("Fwd Id");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* CContactDO::OnClear(void)
{
	custid->StrEmpty();
	date->StrEmpty();
	name->StrEmpty();
	company->StrEmpty();
	caddress->StrEmpty();
	caddress2->StrEmpty();
	city->StrEmpty();
	state->StrEmpty();
	zip->StrEmpty();
	country->StrEmpty();
	email->StrEmpty();
	phone->StrEmpty();
	type->StrEmpty();
	disc_group->StrEmpty();
	fwdid->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* CContactDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[15];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(custid,1);
	VALIDATEFIELDOK(date,2);
	VALIDATEFIELD(name,3);
	VALIDATEFIELD(company,4);
	VALIDATEFIELD(caddress,5);
	VALIDATEFIELDOK(caddress2,6);
	VALIDATEFIELD(city,7);
	VALIDATEFIELD(state,8);
	VALIDATEFIELD(zip,9);
	VALIDATEFIELD(country,10);
	VALIDATEFIELD(email,11);
	VALIDATEFIELD(phone,12);
	VALIDATEFIELDOK(type,13);
	VALIDATEFIELDOK(disc_group,14);
	VALIDATEFIELDOK(fwdid,15);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* CContactDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return custid;
	case 2: return date;
	case 3: return name;
	case 4: return company;
	case 5: return caddress;
	case 6: return caddress2;
	case 7: return city;
	case 8: return state;
	case 9: return zip;
	case 10: return country;
	case 11: return email;
	case 12: return phone;
	case 13: return type;
	case 14: return disc_group;
	case 15: return fwdid;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* CContactDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return custid->StrTextPtr();
	case 2: return date->StrTextPtr();
	case 3: return name->StrTextPtr();
	case 4: return company->StrTextPtr();
	case 5: return caddress->StrTextPtr();
	case 6: return caddress2->StrTextPtr();
	case 7: return city->StrTextPtr();
	case 8: return state->StrTextPtr();
	case 9: return zip->StrTextPtr();
	case 10: return country->StrTextPtr();
	case 11: return email->StrTextPtr();
	case 12: return phone->StrTextPtr();
	case 13: return type->StrTextPtr();
	case 14: return disc_group->StrTextPtr();
	case 15: return fwdid->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* CContactDO::OnSetText(MSGP)
{
	MPARMINT(int,ix);
	MPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: custid->StrSetTextW(pText); break;
	case 2: date->StrSetTextW(pText); break;
	case 3: name->StrSetTextW(pText); break;
	case 4: company->StrSetTextW(pText); break;
	case 5: caddress->StrSetTextW(pText); break;
	case 6: caddress2->StrSetTextW(pText); break;
	case 7: city->StrSetTextW(pText); break;
	case 8: state->StrSetTextW(pText); break;
	case 9: zip->StrSetTextW(pText); break;
	case 10: country->StrSetTextW(pText); break;
	case 11: email->StrSetTextW(pText); break;
	case 12: phone->StrSetTextW(pText); break;
	case 13: type->StrSetTextW(pText); break;
	case 14: disc_group->StrSetTextW(pText); break;
	case 15: fwdid->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

