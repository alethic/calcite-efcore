using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Xunit;
using Xunit.Abstractions;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.Query
{

    public class NorthwindCompiledQueryCalciteTest : NorthwindCompiledQueryTestBase<NorthwindQueryCalciteFixture<NoopModelCustomizer>>
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="testOutputHelper"></param>
        public NorthwindCompiledQueryCalciteTest(NorthwindQueryCalciteFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper) :
            base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [ConditionalFact]
        public virtual void Keyless_query2()
        {
            var query = EF.CompileQuery((NorthwindContext context) => context.CustomerQueries);

            using (var context = CreateContext())
            {
                Assert.Equal(91, query(context).Count());
            }

            using (var context = CreateContext())
            {
                Assert.Equal(91, query(context).ToList().Count);
            }
        }

    }

}
