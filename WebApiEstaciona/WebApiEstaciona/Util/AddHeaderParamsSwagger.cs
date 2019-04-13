using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEstaciona.Util
{
    public class AddHeaderParamsSwagger : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<Swashbuckle.AspNetCore.Swagger.IParameter>();

            operation.Parameters.Add(new HeaderParameter()
            {
                Name = "Token",
                In = "header",
                Type = "string",
                Required = false
            });
        }

        class HeaderParameter : NonBodyParameter
        {
        }
    }
}
