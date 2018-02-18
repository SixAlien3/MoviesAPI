using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace MoviesAPI.Infrastructure
{
    public class MoviesApiExceptionLogger: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.ToString();
            //Do whatever logging you need to do here.
            Trace.TraceError(context.ExceptionContext.Exception.ToString());

        }
    }
}