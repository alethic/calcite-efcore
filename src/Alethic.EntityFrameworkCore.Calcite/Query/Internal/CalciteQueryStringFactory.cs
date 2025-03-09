using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Alethic.EntityFrameworkCore.Calcite.Query.Internal
{

    public class CalciteQueryStringFactory : RelationalQueryStringFactory
    {

        readonly IRelationalTypeMappingSource _typeMapper;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="typeMapper"></param>
        public CalciteQueryStringFactory(IRelationalTypeMappingSource typeMapper)
        {
            _typeMapper = typeMapper;
        }

    }

}
