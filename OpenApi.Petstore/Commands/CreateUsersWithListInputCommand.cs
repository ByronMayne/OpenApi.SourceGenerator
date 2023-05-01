using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class CreateUsersWithListInputCommand : ICreateUsersWithListInputCommand
    {
        public partial Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}