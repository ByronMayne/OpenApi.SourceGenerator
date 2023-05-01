using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class GetPetByIdCommand : IGetPetByIdCommand
    {
        public partial Task<IActionResult> ExecuteAsync(long petId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}