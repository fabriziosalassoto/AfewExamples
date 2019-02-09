//	==============================================================================
//	COrderReqDO - Order Item Information Data Object
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

class COrderReqDO : public IDBRow
{
public:
	COrderReqDO(void);
	~COrderReqDO(void);
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
	IStr*	reqid;

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

	IStr*	prodsku;
	IStr*	itemprice;
	IStr*	quantity;
	IStr*	oldsn;

	IStr*	cdsku;
	IStr*	cdprice;
	IStr*	cdquantity;
	IStr*	shipping;

	IStr*	tax;

	IStr*	notes;
	IStr*	flags;
	IStr*	status;

	IStr*	proddesc;
	IStr*	cddesc;
	IStr*	total;
	IStr*	pmtfee;
	IStr*	txnid;

	IStr*	ordtype;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeOrderReqDO(void)
{
	return (IDBRow*) new COrderReqDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

COrderReqDO::COrderReqDO()
{
	reqid = MakeStr();
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
	prodsku = MakeStr();
	itemprice = MakeStr();
	quantity = MakeStr();
	oldsn = MakeStr();
	cdsku = MakeStr();
	cdprice = MakeStr();
	cdquantity = MakeStr();
	shipping = MakeStr();
	tax = MakeStr();
	notes = MakeStr();
	flags = MakeStr();
	status = MakeStr();
	proddesc = MakeStr();
	cddesc = MakeStr();
	total = MakeStr();
	pmtfee = MakeStr();
	txnid = MakeStr();
	ordtype = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

COrderReqDO::~COrderReqDO(void)
{
	reqid->Release();
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
	prodsku->Release();
	itemprice->Release();
	quantity->Release();
	oldsn->Release();
	cdsku->Release();
	cdprice->Release();
	cdquantity->Release();
	shipping->Release();
	tax->Release();
	notes->Release();
	flags->Release();
	status->Release();
	proddesc->Release();
	cddesc->Release();
	total->Release();
	pmtfee->Release();
	txnid->Release();
	ordtype->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* COrderReqDO::Msg(MSGDISPATCH)
{
	switch(MSGID)
	{
	case MSG_DBaseOTableName: return TEXT("OrderReq");
	case MSG_DBaseOMake: return new COrderReqDO();
	case MSG_DBaseOClear: return OnClear();
	case MSG_DBaseOPrimaryKeyIdx: return MRTNVAL(1);
	case MSG_DBaseONrCols: return MRTNVAL(30);
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

void* COrderReqDO::OnColHint(MSGP)
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
	case 16: return MRTNVAL(DBOH_NORMAL);
	case 17: return MRTNVAL(DBOH_NORMAL);
	case 18: return MRTNVAL(DBOH_NORMAL);
	case 19: return MRTNVAL(DBOH_NORMAL);
	case 20: return MRTNVAL(DBOH_NORMAL);
	case 21: return MRTNVAL(DBOH_NORMAL);
	case 22: return MRTNVAL(DBOH_NORMAL);
	case 23: return MRTNVAL(DBOH_NORMAL);
	case 24: return MRTNVAL(DBOH_NORMAL);
	case 25: return MRTNVAL(DBOH_NORMAL);
	case 26: return MRTNVAL(DBOH_NORMAL);
	case 27: return MRTNVAL(DBOH_NORMAL);
	case 28: return MRTNVAL(DBOH_NORMAL);
	case 29: return MRTNVAL(DBOH_NORMAL);
	case 30: return MRTNVAL(DBOH_NORMAL);
	};

	return NULL;
}

//	==============================================================================
//	Find Col Name to index 1..nr fields, 0 = not found
//	==============================================================================

static WCHAR szFieldNames[] =
	L"req_id\0"
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
	L"prodsku\0"
	L"price\0"
	L"quantity\0"
	L"oldsn\0"
	L"cdsku\0"
	L"cdprice\0"
	L"cdquantity\0"
	L"shipping\0"
	L"tax\0"
	L"notes\0"
	L"flags\0"
	L"status\0"
	L"proddesc\0"
	L"cddesc\0"
	L"total\0"
	L"pmtfee\0"
	L"txnid\0"
	L"ordtype\0"
;

void* COrderReqDO::OnColFind(MSGP)
{
	MPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* COrderReqDO::OnColName(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("req_id");
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

	case 13: return TEXT("prodsku");
	case 14: return TEXT("price");
	case 15: return TEXT("quantity");
	case 16: return TEXT("oldsn");

	case 17: return TEXT("cdsku");
	case 18: return TEXT("cdquantity");
	case 19: return TEXT("cdprice");

	case 20: return TEXT("shipping");
	case 21: return TEXT("tax");

	case 22: return TEXT("notes");
	case 23: return TEXT("flags");
	case 24: return TEXT("status");
	case 25: return TEXT("proddesc");
	case 26: return TEXT("cddesc");
	case 27: return TEXT("total");
	case 28: return TEXT("pmtfee");
	case 29: return TEXT("txnid");
	case 30: return TEXT("ordtype");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* COrderReqDO::OnColDef(MSGP)
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
	case 13: return TEXT("char(64) NOT NULL");
	case 14: return TEXT("decimal(8,2) NOT NULL");
	case 15: return TEXT("int NOT NULL");
	case 16: return TEXT("char(24) NULL");
	case 17: return TEXT("char(12) NOT NULL");
	case 18: return TEXT("int NULL");
	case 19: return TEXT("decimal(8,2) NOT NULL");
	case 20: return TEXT("decimal(8,2) NOT NULL");
	case 21: return TEXT("decimal(8,2) NOT NULL");
	case 22: return TEXT("text NULL");
   	case 23: return TEXT("char(10) NULL");
	case 24: return TEXT("char(12) NOT NULL");
	case 25: return TEXT("char(64) NOT NULL");
	case 26: return TEXT("char(64) NOT NULL");
	case 27: return TEXT("decimal(8,2) NOT NULL");
	case 28: return TEXT("decimal(8,2) NOT NULL");
	case 29: return TEXT("char(24) NOT NULL");
	case 30: return TEXT("char(12) NOT NULL");
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* COrderReqDO::OnColLabel(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return TEXT("req_id");
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

	case 13: return TEXT("prod_sku");
	case 14: return TEXT("price");
	case 15: return TEXT("quantity");
	case 16: return TEXT("old_sn");

	case 17: return TEXT("cd_sku");
	case 18: return TEXT("cd_quantity");
	case 19: return TEXT("cd_price");

	case 20: return TEXT("shipping");
	case 21: return TEXT("tax");

	case 22: return TEXT("notes");
	case 23: return TEXT("flags");
	case 24: return TEXT("status");

	case 25: return TEXT("product");
	case 26: return TEXT("cd");
	case 27: return TEXT("total");
	case 28: return TEXT("pmt_fee");
	case 29: return TEXT("txn_id");
	case 30: return TEXT("ord_type");
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* COrderReqDO::OnClear(void)
{
	reqid->StrEmpty();
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
	prodsku->StrEmpty();
	itemprice->StrEmpty();
	quantity->StrEmpty();
	oldsn->StrEmpty();
	cdsku->StrEmpty();
	cdprice->StrEmpty();
	cdquantity->StrEmpty();
	shipping->StrEmpty();
	tax->StrEmpty();
	notes->StrEmpty();
	flags->StrEmpty();
	status->StrEmpty();
	proddesc->StrEmpty();
	cddesc->StrEmpty();
	total->StrEmpty();
	pmtfee->StrEmpty();
	txnid->StrEmpty();
	ordtype->StrEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->StrLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* COrderReqDO::OnValidate(MSGP)
{
	MPARMPTR(UINT*,pVErrTbl);

	UINT ignore[11];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(reqid,1);
	VALIDATEFIELD(date,2);
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
	VALIDATEFIELD(prodsku,13);
	VALIDATEFIELD(itemprice,14);
	VALIDATEFIELD(quantity,15);
	VALIDATEFIELDOK(oldsn,16);
	VALIDATEFIELDOK(cdsku,17);
	VALIDATEFIELDOK(cdprice,18);
	VALIDATEFIELDOK(cdquantity,19);
	VALIDATEFIELDOK(shipping,20);
	VALIDATEFIELD(tax,21);
	VALIDATEFIELDOK(notes,22);
	VALIDATEFIELDOK(flags,23);
	VALIDATEFIELDOK(status,24);
	VALIDATEFIELDOK(proddesc,25);
	VALIDATEFIELDOK(cddesc,26);
	VALIDATEFIELDOK(total,27);
	VALIDATEFIELDOK(pmtfee,28);
	VALIDATEFIELDOK(txnid,29);
	VALIDATEFIELDOK(ordtype,30);

	return MRTNVAL(valid);
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* COrderReqDO::OnStr(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return reqid;
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
	case 13: return prodsku;
	case 14: return itemprice;
	case 15: return quantity;
	case 16: return oldsn;
	case 17: return cdsku;
	case 18: return cdprice;
	case 19: return cdquantity;
	case 20: return shipping;
	case 21: return tax;
	case 22: return notes;
	case 23: return flags;
	case 24: return status;
	case 25: return proddesc;
	case 26: return cddesc;
	case 27: return total;
	case 28: return pmtfee;
	case 29: return txnid;
	case 30: return ordtype;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* COrderReqDO::OnGetText(MSGP)
{
	MPARMINT(int,ix);

	switch(ix)
	{
	case 1: return reqid->StrTextPtr();
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
	case 13: return prodsku->StrTextPtr();
	case 14: return itemprice->StrTextPtr();
	case 15: return quantity->StrTextPtr();
	case 16: return oldsn->StrTextPtr();
	case 17: return cdsku->StrTextPtr();
	case 18: return cdprice->StrTextPtr();
	case 19: return cdquantity->StrTextPtr();
	case 20: return shipping->StrTextPtr();
	case 21: return tax->StrTextPtr();
	case 22: return notes->StrTextPtr();
	case 23: return flags->StrTextPtr();
	case 24: return status->StrTextPtr();
	case 25: return proddesc->StrTextPtr();
	case 26: return cddesc->StrTextPtr();
	case 27: return total->StrTextPtr();
	case 28: return pmtfee->StrTextPtr();
	case 29: return txnid->StrTextPtr();
	case 30: return ordtype->StrTextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* COrderReqDO::OnSetText(MSGP)
{
	MPARMPTR(MSGS_DBaseOSetText*,pmsg);
	int ix = pmsg->nCol;
	WCHAR* pText = pmsg->pText;

	switch(ix)
	{
	case 1: reqid->StrSetTextW(pText); break;
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
	case 13: prodsku->StrSetTextW(pText); break;
	case 14: itemprice->StrSetTextW(pText); break;
	case 15: quantity->StrSetTextW(pText); break;
	case 16: oldsn->StrSetTextW(pText); break;
	case 17: cdsku->StrSetTextW(pText); break;
	case 18: cdprice->StrSetTextW(pText); break;
	case 19: cdquantity->StrSetTextW(pText); break;
	case 20: shipping->StrSetTextW(pText); break;
	case 21: tax->StrSetTextW(pText); break;
	case 22: notes->StrSetTextW(pText); break;
	case 23: flags->StrSetTextW(pText); break;
	case 24: status->StrSetTextW(pText); break;
	case 25: proddesc->StrSetTextW(pText); break;
	case 26: cddesc->StrSetTextW(pText); break;
	case 27: total->StrSetTextW(pText); break;
	case 28: pmtfee->StrSetTextW(pText); break;
	case 29: txnid->StrSetTextW(pText); break;
	case 30: ordtype->StrSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

