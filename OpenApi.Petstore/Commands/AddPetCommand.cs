using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class AddPetCommand : IAddPetCommand
    {
        public partial Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}