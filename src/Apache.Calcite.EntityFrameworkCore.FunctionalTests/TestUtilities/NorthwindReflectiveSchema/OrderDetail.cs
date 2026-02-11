using java.math;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    public partial class NorthwindReflectiveTarget
    {
        public class OrderDetail
        {
            public int OrderID;
            public int ProductID;
            public BigDecimal UnitPrice;
            public short Quantity;
            public float Discount;
        }

    }

}
