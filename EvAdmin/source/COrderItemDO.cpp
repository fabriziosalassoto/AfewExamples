//	==============================================================================
//	COrderItemDO - Order Item Information Data Object
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

class COrderItemDO : public IDBRow
{
public:
	COrderItemDO(void);
	~COrderItemDO(void);
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
	IStr*	itemid;
	IStr*	ordid;
	IStr*	prod_sku;
	IStr*	quantity;
	IStr*	itemprice;
	IStr*	discount;
	IStr*	extprice;
	IStr*	oldsn;
	IStr*	flags;
};

//	==============================================================================
//	Class factory
//	==============================================================================

IDBRow* MakeOrderItemDO(void)
{
	return (IDBRow*) new COrderItemDO(); 
}

//	==============================================================================
//	Constructor
//	==============================================================================

COrderItemDO::COrderItemDO()
{
	itemid = MakeStr();
	ordid = MakeStr();
	prod_sku = MakeStr();
	quantity = MakeStr();
	itemprice = MakeStr();
	discount = MakeStr();
	extprice = MakeStr();
	oldsn = MakeStr();
	flags = MakeStr();
}

//	==============================================================================
//	Destructor
//	==============================================================================

COrderItemDO::~COrderItemDO(void)
{
	itemid->Release();
	ordid->Release();
	prod_sku->Release();
	quantity->Release();
	itemprice->Release();
	discount->Release();
	extprice->Release();
	oldsn->Release();
	flags->Release();
}

//	==============================================================================
//	Variable length message receiver/dispatcher
//	==============================================================================

void* COrderItemDO::Msg(int nMsg,...)
{
	MSGPTR

	switch(nMsg)
	{
	case MSG_DBOTableName: return L"OrderItems";
	case MSG_DBOMake: return new COrderItemDO();
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

void* COrderItemDO::OnColHint(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return (void*)(int)DBOH_AUTOINCREMENT;
	case 2: return (void*)(int)DBOH_FOREIGNKEY;
	case 3: return (void*)(int)DBOH_NORMAL;
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
	L"item_id\0"
	L"order_id\0"
	L"prod_sku\0"
	L"quantity\0"
	L"item_price\0"
	L"discount\0"
	L"ext_price\0"
	L"oldsn\0"
	L"flags\0"
;

void* COrderItemDO::OnColFind(MSGP)
{
	MSGPARMPTR(WCHAR*,pFieldName);

	return (void*)hSvc->SvcListFindStrNr(szFieldNames,pFieldName);
}

//	==============================================================================
//	Get Col Name
//	==============================================================================

void* COrderItemDO::OnColName(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"item_id";
	case 2: return L"order_id";
	case 3: return L"prod_sku";
	case 4: return L"quantity";
	case 5: return L"item_price";
	case 6: return L"discount";
	case 7: return L"ext_price";
	case 8: return L"oldsn";
	case 9: return L"flags";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Def
//	==============================================================================

void* COrderItemDO::OnColDef(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"int NOT NULL AUTO_INCREMENT";
	case 2: return L"int NOT NULL";
	case 3: return L"char(12) NOT NULL";
	case 4: return L"int NOT NULL";
	case 5: return L"decimal(8,2) NOT NULL";
	case 6: return L"decimal(8,2) NOT NULL";
	case 7: return L"decimal(8,2) NOT NULL";
	case 8: return L"char(20) NULL";
	case 9: return L"char(10) NULL";
	};

	return NULL;
}

//	==============================================================================
//	Get Col Label
//	==============================================================================

void* COrderItemDO::OnColLabel(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return L"Item Id";
	case 2: return L"Order Nr";
	case 3: return L"SKU";
	case 4: return L"Quantity";
	case 5: return L"Item Price";
	case 6: return L"Discount";
	case 7: return L"Ext Price";
	case 8: return L"Old S/N";
	case 9: return L"Flags";
	};

	return NULL;
}

//	==============================================================================
//	Clear all fields
//	==============================================================================

void* COrderItemDO::OnClear(void)
{
	itemid->SEmpty();
	ordid->SEmpty();
	prod_sku->SEmpty();
	quantity->SEmpty();
	itemprice->SEmpty();
	discount->SEmpty();
	extprice->SEmpty();
	oldsn->SEmpty();
	flags->SEmpty();

	return IM_RTN_NOTHING;
}

//	==============================================================================
//	Validate all fields
//	==============================================================================

#define VALIDATEFIELD(v,ix) if(!v->SLength()){ pVErrTbl[ix-1]=1; valid = false; }
#define VALIDATEFIELDOK(v,ix) pVErrTbl[ix-1]=0;

void* COrderItemDO::OnValidate(MSGP)
{
	MSGPARMPTR(UINT*,pVErrTbl);

	UINT ignore[9];
	if(!pVErrTbl)
		pVErrTbl = (UINT*)&ignore;
	bool valid = true;

	//	valid if field is non-blank

	VALIDATEFIELDOK(itemid,1);
	VALIDATEFIELD(ordid,2);
	VALIDATEFIELD(prod_sku,3);
	VALIDATEFIELD(quantity,4);
	VALIDATEFIELD(itemprice,5);
	VALIDATEFIELD(discount,6);
	VALIDATEFIELD(extprice,7);
	VALIDATEFIELDOK(oldsn,8);
	VALIDATEFIELDOK(flags,9);

	return (void*)valid;
}
//	==============================================================================
//	Get Col String
//	==============================================================================

void* COrderItemDO::OnStr(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return itemid;
	case 2: return ordid;
	case 3: return prod_sku;
	case 4: return quantity;
	case 5: return itemprice;
	case 6: return discount;
	case 7: return extprice;
	case 8: return oldsn;
	case 9: return flags;
	};

	return NULL;
}

//	==============================================================================
//	Get Col Text
//	==============================================================================

void* COrderItemDO::OnGetText(MSGP)
{
	MSGPARM(int,ix);

	switch(ix)
	{
	case 1: return itemid->STextPtr();
	case 2: return ordid->STextPtr();
	case 3: return prod_sku->STextPtr();
	case 4: return quantity->STextPtr();
	case 5: return itemprice->STextPtr();
	case 6: return discount->STextPtr();
	case 7: return extprice->STextPtr();
	case 8: return oldsn->STextPtr();
	case 9: return flags->STextPtr();
	};

	return NULL;
}

//	==============================================================================
//	Set Col Text
//	==============================================================================

void* COrderItemDO::OnSetText(MSGP)
{
	MSGPARM(int,ix);
	MSGPARMPTR(WCHAR*,pText);

	switch(ix)
	{
	case 1: itemid->SSetTextW(pText); break;
	case 2: ordid->SSetTextW(pText); break;
	case 3: prod_sku->SSetTextW(pText); break;
	case 4: quantity->SSetTextW(pText); break;
	case 5: itemprice->SSetTextW(pText); break;
	case 6: discount->SSetTextW(pText); break;
	case 7: extprice->SSetTextW(pText); break;
	case 8: oldsn->SSetTextW(pText); break;
	case 9: flags->SSetTextW(pText); break;
	default:
		return IM_RTN_FALSE;
	};

	return IM_RTN_TRUE;
}

//	==============================================================================

