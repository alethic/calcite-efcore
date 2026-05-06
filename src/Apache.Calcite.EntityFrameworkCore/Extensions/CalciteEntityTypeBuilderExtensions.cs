using System;
using System.Linq.Expressions;

using Apache.Calcite.EntityFrameworkCore.Metadata.Builders;
using Apache.Calcite.EntityFrameworkCore.Utilities;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apache.Calcite.EntityFrameworkCore.Extensions
{

    public static class CalciteEntityTypeBuilderExtensions
    {

        /// <summary>
        /// Configures a sequence available for the given property. This entity is not effected by the sequence, but the sequence can be used as a value generator for other entities.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="entity"></param>
        /// <param name="name"></param>
        /// <param name="valuePropertyExpression"></param>
        /// <returns></returns>
        public static CalciteEntitySequenceBuilder<TEntity, TValue> HasEntitySequence<TEntity, TValue>(this EntityTypeBuilder<TEntity> entity, string name, Expression<Func<TEntity, TValue>> valuePropertyExpression)
            where TEntity : class
        {
            Check.NotEmpty(name);
            Check.NotNull(entity);
            Check.NotNull(valuePropertyExpression);

            var property = entity.Metadata.FindProperty(valuePropertyExpression.GetPropertyAccess());
            if (property == null)
                throw new InvalidOperationException("The property specified in the expression is not part of the entity.");

            return new CalciteEntitySequenceBuilder<TEntity, TValue>(entity.Metadata.Model.EntitySequence(name, entity.Metadata, property, ConfigurationSource.Explicit));
        }

    }

}
