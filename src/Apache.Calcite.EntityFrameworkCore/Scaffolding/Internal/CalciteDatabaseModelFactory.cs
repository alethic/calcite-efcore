using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using IKVM.Jdbc.Data;

using java.sql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

using org.apache.calcite.jdbc;

namespace Apache.Calcite.EntityFrameworkCore.Scaffolding.Internal
{

    public class CalciteDatabaseModelFactory : DatabaseModelFactory
    {

        readonly IDiagnosticsLogger<DbLoggerCategory.Scaffolding> _logger;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="typeMappingSource"></param>
        public CalciteDatabaseModelFactory(IDiagnosticsLogger<DbLoggerCategory.Scaffolding> logger, IRelationalTypeMappingSource typeMappingSource)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override DatabaseModel Create(string connectionString, DatabaseModelFactoryOptions options)
        {
            using var connection = new JdbcConnection(connectionString);
            return Create(connection, options);
        }

        /// <inheritdoc/>
        public override DatabaseModel Create(DbConnection connection, DatabaseModelFactoryOptions options)
        {
            if (connection is not JdbcConnection jdbcConnection)
                throw new InvalidOperationException("Database connection must be a JdbcConnection.");

            var connectionStartedOpen = connection.State == ConnectionState.Open;

            try
            {
                if (connectionStartedOpen == false)
                    connection.Open();

                var calcite = (CalciteConnection?)jdbcConnection.JdbcConnection.unwrap(typeof(CalciteConnection));
                if (calcite is null)
                    throw new InvalidOperationException("Could not unwrap CalciteConnection.");

                return GetDatabase(jdbcConnection, calcite);
            }
            finally
            {
                if (connectionStartedOpen == false)
                    connection.Close();
            }
        }

        /// <summary>
        /// Gets the database model.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="calcite"></param>
        /// <returns></returns>
        DatabaseModel GetDatabase(JdbcConnection connection, CalciteConnection calcite)
        {
            var model = new DatabaseModel();

            model.DatabaseName = connection.JdbcConnection.getCatalog();
            model.DefaultSchema = connection.JdbcConnection.getSchema();

            foreach (var table in GetTables(model, connection, calcite))
                model.Tables.Add(table);

            return model;
        }

        /// <summary>
        /// Gets the tables within the database.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="connection"></param>
        /// <param name="calcite"></param>
        /// <returns></returns>
        IEnumerable<DatabaseTable> GetTables(DatabaseModel database, JdbcConnection connection, CalciteConnection calcite)
        {
            using var resultSet = calcite.getMetaData().getTables(null, null, null, ["TABLE"]);
            while (resultSet.next())
                yield return GetTable(database, connection, calcite, resultSet);
        }

        /// <summary>
        /// Gets a table from the current row of the result set.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="connection"></param>
        /// <param name="calcite"></param>
        /// <param name="resultSet"></param>
        /// <returns></returns>
        DatabaseTable GetTable(DatabaseModel model, JdbcConnection connection, CalciteConnection calcite, ResultSet resultSet)
        {
            var table = new DatabaseTable();
            table.Database = model;
            table.Schema = resultSet.getString("TABLE_SCHEM");
            table.Name = resultSet.getString("TABLE_NAME");

            foreach (var column in GetColumns(table, connection, calcite))
                table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Gets the tables within the database.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="connection"></param>
        /// <param name="calcite"></param>
        /// <returns></returns>
        IEnumerable<DatabaseColumn> GetColumns(DatabaseTable table, JdbcConnection connection, CalciteConnection calcite)
        {
            using var resultSet = calcite.getMetaData().getColumns(null, table.Schema, table.Name, null);
            while (resultSet.next())
                yield return GetColumn(table, connection, calcite, resultSet);
        }

        /// <summary>
        /// Gets a column from the current row of the result set.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="connection"></param>
        /// <param name="calcite"></param>
        /// <param name="resultSet"></param>
        /// <returns></returns>
        DatabaseColumn GetColumn(DatabaseTable table, JdbcConnection connection, CalciteConnection calcite, ResultSet resultSet)
        {
            var databaseColumn = new DatabaseColumn();
            databaseColumn.Table = table;
            databaseColumn.Name = resultSet.getString("COLUMN_NAME");
            databaseColumn.IsNullable = resultSet.getInt("NULLABLE") == 1;
            databaseColumn.StoreType = resultSet.getString("TYPE_NAME");
            databaseColumn.IsStored = resultSet.getString("IS_GENERATEDCOLUMN") == "YES";
            return databaseColumn;
        }

    }

}
