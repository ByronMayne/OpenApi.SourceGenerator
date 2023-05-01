using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class UploadFileCommand : IUploadFileCommand
    {
        public partial Task<IActionResult> ExecuteAsync(long petId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}