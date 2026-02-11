using java.math;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public partial class NorthwindReflectiveTarget
    {
        public class Order
        {
            public int OrderID;
            public string CustomerID;
            public java.lang.Integer? EmployeeID;
            public java.sql.Date? OrderDate;
            public java.sql.Date? RequiredDate;
            public java.sql.Date? ShippedDate;
            public java.lang.Integer? ShipVia;
            public BigDecimal? Freight;
            public string ShipName;
            public string ShipAddress;
            public string ShipCity;
            public string ShipRegion;
            public string ShipPostalCode;
            public string ShipCountry;
        }

    }

}
