// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading;
using System.Threading.Tasks;

using Apache.Calcite.Data;
using Apache.Calcite.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Apache.Calcite.EntityFrameworkCore.FunctionalTests.TestUtilities
{

    /// <summary>
    /// <see cref="RelationalTestStore"/> implementation backed by an Apache Calcite ADO.NET connection.
    /// </summary>
    public class CalciteTestStore : RelationalTestStore
    {

        public const int CommandTimeout = 30;

        /// <summary>
        /// Creates the specified store.
        /// </summary>
        public static CalciteTestStore Create(string name)
            => new(name, shared: false);

        /// <summary>
        /// Gets or creates the specified store.
        /// </summary>
        public static CalciteTestStore GetOrCreate(string name)
            => new(name, shared: true);

        static string BuildConnectionString(string name)
            => $"schema={name};conformance=LENIENT;parserFactory=org.apache.calcite.server.ServerDdlExecutor#PARSER_FACTORY";

        readonly string? _initScript;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected CalciteTestStore(string name, bool shared, string? initScript = null)
            : base(name, shared, new CalciteConnection(BuildConnectionString(name)))
        {
            _initScript = initScript;
        }

        /// <inheritdoc/>
        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
        {
            if (UseConnectionString)
            {
                return builder.UseCalcite(ConnectionString, b =>
                {
                    b.CommandTimeout(CommandTimeout);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            }

            if (Connection is not CalciteConnection connection)
                throw new InvalidOperationException("Calcite Provider must be provided a CalciteConnection.");

            return builder.UseCalcite(connection, b =>
            {
                b.CommandTimeout(CommandTimeout);
                b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        }

        /// <inheritdoc/>
        protected override async Task InitializeAsync(Func<DbContext> createContext, Func<DbContext, Task>? seed, Func<DbContext, Task>? clean)
        {
            using var context = createContext();

            if (clean != null)
                await clean(context);

            await CleanAsync(context);

            if (seed != null)
                await seed(context);
        }

        /// <inheritdoc/>
        public override async Task CleanAsync(DbContext context)
        {
            context.Database.EnsureClean();

            if (_initScript is not null)
                await context.Database.ExecuteScriptAsync(_initScript, CancellationToken.None);
        }

    }

}
