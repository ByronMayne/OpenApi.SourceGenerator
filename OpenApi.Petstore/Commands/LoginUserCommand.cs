using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class LoginUserCommand : ILoginUserCommand
    {
        public partial Task<IActionResult> ExecuteAsync(string username, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}