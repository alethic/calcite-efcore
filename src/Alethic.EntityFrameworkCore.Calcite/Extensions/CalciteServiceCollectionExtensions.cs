using System;

using Alethic.EntityFrameworkCore.Calcite.Diagnostics.Internal;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure;
using Alethic.EntityFrameworkCore.Calcite.Infrastructure.Internal;
using Alethic.EntityFrameworkCore.Calcite.Metadata.Internal;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;

namespace Alethic.EntityFrameworkCore.Calcite.Extensions
{

    public static class CalciteServiceCollectionExtensions
    {

        /// <summary>
        ///     <para>
        ///         Adds the services required by the Calcite provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />.
        ///     </para>
        ///     <para>
        ///         Calling this method is no longer necessary when building most applications, including those that
        ///         use dependency injection in ASP.NET or elsewhere.
        ///         It is only needed when building the internal service provider for use with
        ///         the <see cref="DbContextOptionsBuilder.UseInternalServiceProvider" /> method.
        ///         This is not recommend other than for some advanced scenarios.
        ///     </para>
        /// </summary>
        /// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkCalcite(this IServiceCollection serviceCollection)
        {
            if (serviceCollection is null)
                throw new ArgumentNullException(nameof(serviceCollection));

            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<LoggingDefinitions, CalciteLoggingDefinitions>()
                .TryAdd<IDatabaseProvider, DatabaseProvider<CalciteOptionsExtension>>()
                .TryAdd<IValueGeneratorCache>(p => p.GetRequiredService<ICalciteValueGeneratorCache>())
                .TryAdd<IRelationalTypeMappingSource, CalciteTypeMappingSource>()
                .TryAdd<ISqlGenerationHelper, CalciteSqlGenerationHelper>()
                .TryAdd<IRelationalAnnotationProvider, CalciteAnnotationProvider>()
                .TryAdd<IModelValidator, CalciteModelValidator>()
                .TryAdd<IProviderConventionSetBuilder, CalciteConventionSetBuilder>()
                .TryAdd<IUpdateSqlGenerator, CalciteUpdateSqlGenerator>()
                .TryAdd<IModificationCommandBatchFactory, CalciteModificationCommandBatchFactory>()
                .TryAdd<IValueGeneratorSelector, CalciteValueGeneratorSelector>()
                .TryAdd<IRelationalConnection>(p => p.GetRequiredService<ICalciteRelationalConnection>())
                .TryAdd<IMigrationsSqlGenerator, CalciteMigrationsSqlGenerator>()
                .TryAdd<IRelationalDatabaseCreator, CalciteDatabaseCreator>()
                .TryAdd<IHistoryRepository, CalciteHistoryRepository>()
                .TryAdd<ICompiledQueryCacheKeyGenerator, CalciteCompiledQueryCacheKeyGenerator>()
                .TryAdd<IExecutionStrategyFactory, CalciteExecutionStrategyFactory>()
                .TryAdd<IMethodCallTranslatorProvider, CalciteMethodCallTranslatorProvider>()
                .TryAdd<IAggregateMethodCallTranslatorProvider, CalciteAggregateMethodCallTranslatorProvider>()
                .TryAdd<IMemberTranslatorProvider, CalciteMemberTranslatorProvider>()
                .TryAdd<IEvaluatableExpressionFilter, CalciteEvaluatableExpressionFilter>()
                .TryAdd<IQuerySqlGeneratorFactory, CalciteQuerySqlGeneratorFactory>()
                .TryAdd<IRelationalSqlTranslatingExpressionVisitorFactory, CalciteSqlTranslatingExpressionVisitorFactory>()
                .TryAdd<IQueryTranslationPostprocessorFactory, CalciteQueryTranslationPostprocessorFactory>()
                .TryAdd<IRelationalParameterBasedSqlProcessorFactory, CalciteParameterBasedSqlProcessorFactory>()
                .TryAdd<ISqlExpressionFactory, CalciteSqlExpressionFactory>()
                .TryAdd<ISingletonOptions, ICalciteSingletonOptions>(p => p.GetRequiredService<ICalciteSingletonOptions>())
                .TryAdd<IValueConverterSelector, CalciteValueConverterSelector>()
                .TryAdd<IQueryCompilationContextFactory, CalciteQueryCompilationContextFactory>()
                .TryAddProviderSpecificServices(b => b
                    .TryAddSingleton<ICalciteSingletonOptions, CalciteSingletonOptions>())
                .TryAddCoreServices();

            return serviceCollection;
        }

    }

}
