using QuickLearn.Samples.NdefApiApp;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using TRex.Metadata;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace QuickLearn.Samples.NdefApiApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v1", "NdefParser");
                        c.ReleaseTheTRex();

                        c.OperationFilter<IncludeParameterNamesInOperationIdFilter>();

                    })
                        .EnableSwaggerUi(c =>
                            {

                            });
        }
    }

    internal class IncludeParameterNamesInOperationIdFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                // Select the capitalized parameter names
                var parameters = operation.parameters.Select(
                    p => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(p.name));

                // Set the operation id to match the format "OperationByParam1AndParam2"
                operation.operationId = string.Format(
                    "{0}By{1}",
                    operation.operationId,
                    string.Join("And", parameters));
            }
        }
    }
}