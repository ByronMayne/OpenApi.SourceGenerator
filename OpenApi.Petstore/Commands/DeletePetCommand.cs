using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class DeletePetCommand : IDeletePetCommand
    {
        public partial Task<IActionResult> ExecuteAsync(string api_key, long petId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}