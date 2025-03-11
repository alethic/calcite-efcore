using System;

using Microsoft.EntityFrameworkCore.Query;

namespace Apache.Calcite.EntityFrameworkCore.Query.Expressions.Internal
{

    /// <summary>
    /// The default query SQL generator for Calcite.
    /// </summary>
    public class CalciteQuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {

        readonly QuerySqlGeneratorDependencies dependencies;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public CalciteQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies)
        {
            this.dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
        }

        /// <summary>
        /// Creates the query SQL generator.
        /// </summary>
        /// <returns></returns>
        public QuerySqlGenerator Create() => new CalciteQuerySqlGenerator(dependencies);

    }

}
