CREATE TABLE "Employees" (
	"EmployeeID" INTEGER NOT NULL,
	"LastName" VARCHAR(20) NOT NULL,
	"FirstName" VARCHAR(10) NOT NULL,
	"Title" VARCHAR(30) NULL,
	"TitleOfCourtesy" VARCHAR(25) NULL,
	"BirthDate" TIMESTAMP NULL,
	"HireDate" TIMESTAMP NULL,
	"Address" VARCHAR(60) NULL,
	"City" VARCHAR(15) NULL,
	"Region" VARCHAR(15) NULL,
	"PostalCode" VARCHAR(10) NULL,
	"Country" VARCHAR(15) NULL,
	"HomePhone" VARCHAR(24) NULL,
	"Extension" VARCHAR(4) NULL,
	"Photo" VARBINARY NULL,
	"Notes" VARCHAR NULL,
	"ReportsTo" INTEGER NULL,
	"PhotoPath" VARCHAR(255) NULL
)
GO

CREATE TABLE "Categories" (
	"CategoryID" INTEGER NOT NULL,
	"CategoryName" VARCHAR(15) NOT NULL,
	"Description" VARCHAR NULL,
	"Picture" VARBINARY NULL
)
GO

CREATE TABLE "Customers" (
	"CustomerID" VARCHAR(5) NOT NULL,
	"CompanyName" VARCHAR(40) NOT NULL,
	"ContactName" VARCHAR(30) NULL, 
	"ContactTitle" VARCHAR(30) NULL,
	"Address" VARCHAR(60) NULL,
	"City" VARCHAR(15) NULL,
	"Region" VARCHAR(15) NULL,
	"PostalCode" VARCHAR(10) NULL,
	"Country" VARCHAR(15) NULL,
	"Phone" VARCHAR(24) NULL,
	"Fax" VARCHAR(24) NULL
)
GO

CREATE TABLE "Shippers" (
	"ShipperID" INTEGER NOT NULL,
	"CompanyName" VARCHAR(40) NOT NULL,
	"Phone" VARCHAR(24) NULL
)
GO

CREATE TABLE "Suppliers" (
	"SupplierID" INTEGER NOT NULL,
	"CompanyName" VARCHAR(40) NOT NULL,
	"ContactName" VARCHAR(30) NULL,
	"ContactTitle" VARCHAR(30) NULL,
	"Address" VARCHAR(60) NULL,
	"City" VARCHAR(15) NULL,
	"Region" VARCHAR(15) NULL,
	"PostalCode" VARCHAR(10) NULL,
	"Country" VARCHAR(15) NULL,
	"Phone" VARCHAR(24) NULL,
	"Fax" VARCHAR(24) NULL,
	"HomePage" VARCHAR NULL
)
GO

CREATE TABLE "Orders" (
	"OrderID" INTEGER NOT NULL,
	"CustomerID" CHAR(5) NULL,
	"EmployeeID" INTEGER NULL,
	"OrderDate" TIMESTAMP NULL,
	"RequiredDate" TIMESTAMP NULL,
	"ShippedDate" TIMESTAMP NULL,
	"ShipVia" INTEGER NULL,
	"Freight" DECIMAL(19, 4) NULL DEFAULT 0,
	"ShipName" VARCHAR(40) NULL,
	"ShipAddress" VARCHAR(60) NULL,
	"ShipCity" VARCHAR(15) NULL,
	"ShipRegion" VARCHAR(15) NULL,
	"ShipPostalCode" VARCHAR(10) NULL,
	"ShipCountry" VARCHAR(15) NULL
)
GO

CREATE TABLE "Products" (
	"ProductID" INTEGER NOT NULL,
	"ProductName" VARCHAR(40) NOT NULL,
	"SupplierID" INTEGER NULL,
	"CategoryID" INTEGER NULL,
	"QuantityPerUnit" VARCHAR (20) NULL,
	"UnitPrice" DECIMAL(19, 4) NULL DEFAULT (0),
	"UnitsInStock" SMALLINT NULL DEFAULT (0),
	"UnitsOnOrder" SMALLINT NULL DEFAULT (0),
	"ReorderLevel" SMALLINT NULL DEFAULT (0),
	"Discontinued" BOOLEAN NOT NULL DEFAULT (FALSE)
)
GO

CREATE TABLE "Order Details" (
	"OrderID" INTEGER NOT NULL,
	"ProductID" INTEGER NOT NULL,
	"UnitPrice" DECIMAL(19, 4) NOT NULL DEFAULT (0),
	"Quantity" SMALLINT NOT NULL DEFAULT (1),
	"Discount" REAL NOT NULL DEFAULT (0)
)
GO
