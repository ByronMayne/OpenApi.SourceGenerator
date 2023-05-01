using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class UpdatePetCommand : IUpdatePetCommand
    {
        public partial Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}