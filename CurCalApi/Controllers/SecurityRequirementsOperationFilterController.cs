using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CurCalApi.Controllers
{
	public class SecurityRequirementsOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Security == null)
				operation.Security = new List<OpenApiSecurityRequirement>();


			var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
			operation.Security.Add(new OpenApiSecurityRequirement
			{
				[scheme] = new List<string>()
			});
		}
	}
}