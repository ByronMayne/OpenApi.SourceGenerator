using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class FindPetsByTagsCommand : IFindPetsByTagsCommand
    {
        public partial Task<IActionResult> ExecuteAsync(IList<string> tags, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}