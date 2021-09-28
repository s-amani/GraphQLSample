using HotChocolate;
using Microsoft.Extensions.Logging;

namespace GraphQLSample.Web.Filters
{
    public class GraphErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            // log using 3rd party logging libraries, Nlong, Serilog, Elmah and etc...

            // =======================================================================
            
            return error.WithMessage(error.Exception!=null ? $"Code: {error.Code} - Error: {error.Exception.Message}" : "Unexpected Execution Error");
        }
    }
}
