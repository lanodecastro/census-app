using CensusApp.Api.Core.Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CensusApp.Api.Extensions
{
    public static class IComandResponseExtensions
    {
        public static IActionResult CreateHttpResponse(this ICommandResponse commandResponse)
        {
            if (commandResponse is ErrorResponse)
                return new BadRequestObjectResult(commandResponse);

            return new OkObjectResult(commandResponse);
        }
    }
}
