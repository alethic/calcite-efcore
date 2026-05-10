using System;
using System.Linq;

using java.math;

using Microsoft.EntityFrameworkCore.TestModels.Northwind;

using org.apache.calcite.adapter.java;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities.NorthwindReflectiveSchema
{

    /// <summary>
    /// Object targeted by <see cref="ReflectiveSchema"/>.
    /// </summary>

    public partial class SchemaTarget
    {

        public Customer[] Customers = NorthwindData.Instance.Customers
            .Select(i => new Customer()
            {
                CustomerID = i.CustomerID,
                CompanyName = i.CompanyName,
                ContactName = i.ContactName,
                ContactTitle = i.ContactTitle,
                Address = i.Address,
                City = i.City,
                Region = i.Region,
                PostalCode = i.PostalCode,
                Country = i.Country,
                Phone = i.Phone,
                Fax = i.Fax,
            })
            .ToArray();

        public CustomerQuery[] CustomerQueries = NorthwindData.Instance.CustomerQueries
            .Select(i => new CustomerQuery()
            {
                CompanyName = i.CompanyName,
                ContactName = i.ContactName,
                ContactTitle = i.ContactTitle,
                Address = i.Address,
                City = i.City,
            })
            .ToArray();

        public Product[] Products = NorthwindData.Instance.Products
            .Select(i => new Product()
            {
                ProductID = i.ProductID,
                ProductName = i.ProductName,
                SupplierID = i.SupplierID != null ? new java.lang.Integer((int)i.SupplierID) : null,
                CategoryID = i.CategoryID != null ? new java.lang.Integer((int)i.CategoryID) : null,
                QuantityPerUnit = i.QuantityPerUnit,
                UnitPrice = new BigDecimal(i.UnitPrice.ToString()),
                UnitsInStock = (short)i.UnitsInStock,
                UnitsOnOrder = i.UnitsOnOrder != null ? new java.lang.Short((short)i.UnitsOnOrder) : null,
                ReorderLevel = i.ReorderLevel != null ? new java.lang.Short((short)i.ReorderLevel) : null,
                Discontinued = i.Discontinued,
            })
            .ToArray();

        public Order[] Orders = NorthwindData.Instance.Orders
            .Select(i => new Order()
            {
                OrderID = i.OrderID,
                CustomerID = i.CustomerID,
                EmployeeID = i.EmployeeID != null ? new java.lang.Integer((int)i.EmployeeID) : null,
                OrderDate = i.OrderDate is DateTime d ? new java.sql.Date(d.Year - 1900, d.Month - 1, d.Day) : null,
                RequiredDate = i.RequiredDate is DateTime d2 ? new java.sql.Date(d2.Year - 1900, d2.Month - 1, d2.Day) : null,
                ShippedDate = i.ShippedDate is DateTime d3 ? new java.sql.Date(d3.Year - 1900, d3.Month - 1, d3.Day) : null,
                ShipVia = i.ShipVia != null ? new java.lang.Integer((int)i.ShipVia) : null,
                Freight = new BigDecimal(i.Freight.ToString()),
                ShipName = i.ShipName,
                ShipAddress = i.ShipAddress,
                ShipCity = i.ShipCity,
                ShipRegion = i.ShipRegion,
                ShipPostalCode = i.ShipPostalCode,
                ShipCountry = i.ShipCountry,
            })
            .ToArray();

        public OrderDetail[] OrderDetails = NorthwindData.Instance.OrderDetails
            .Select(i => new OrderDetail()
            {
                OrderID = i.OrderID,
                ProductID = i.ProductID,
                UnitPrice = new BigDecimal(i.UnitPrice.ToString()),
                Quantity = i.Quantity,
                Discount = i.Discount,
            })
            .ToArray();

        public Employee[] Employees = NorthwindData.Instance.Employees
            .Select(i => new Employee()
            {
                EmployeeID = (int)i.EmployeeID,
                LastName = i.LastName,
                FirstName = i.FirstName,
                Title = i.Title,
                TitleOfCourtesy = i.TitleOfCourtesy,
                BirthDate = i.BirthDate is DateTime d ? new java.sql.Date(d.Year - 1900, d.Month - 1, d.Day) : null,
                HireDate = i.HireDate is DateTime d2 ? new java.sql.Date(d2.Year - 1900, d2.Month - 1, d2.Day) : null,
                Address = i.Address,
                City = i.City,
                Region = i.Region,
                PostalCode = i.PostalCode,
                Country = i.Country,
                HomePhone = i.HomePhone,
                Extension = i.Extension,
                Photo = i.Photo,
                Notes = i.Notes,
                ReportsTo = i.ReportsTo != null ? new java.lang.Integer((int)i.ReportsTo) : null,
                PhotoPath = i.PhotoPath,
            })
            .ToArray();

    }

}
