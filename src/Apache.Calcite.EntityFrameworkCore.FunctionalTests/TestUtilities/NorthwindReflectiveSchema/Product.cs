using java.math;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities.NorthwindReflectiveSchema
{

    public class Product
    {
        public int ProductID;
        public string ProductName;
        public java.lang.Integer? SupplierID;
        public java.lang.Integer? CategoryID;
        public string QuantityPerUnit;
        public BigDecimal? UnitPrice;
        public short UnitsInStock;
        public java.lang.Short? UnitsOnOrder;
        public java.lang.Short? ReorderLevel;
        public bool Discontinued;
    }

}
