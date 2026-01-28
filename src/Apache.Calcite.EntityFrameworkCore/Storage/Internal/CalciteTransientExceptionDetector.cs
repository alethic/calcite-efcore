using System;

namespace Apache.Calcite.EntityFrameworkCore.Storage.Internal
{

    public static class CalciteTransientExceptionDetector
    {

        public static bool ShouldRetryOn(Exception ex)
        {
            return false;
        }

    }

}
