using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class DeleteUserCommand : IDeleteUserCommand
    {
        public partial Task<IActionResult> ExecuteAsync(string username, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}