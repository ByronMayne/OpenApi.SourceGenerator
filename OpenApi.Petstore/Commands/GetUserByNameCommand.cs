using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class GetUserByNameCommand : IGetUserByNameCommand
    {
        public partial Task<IActionResult> ExecuteAsync(string username, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}