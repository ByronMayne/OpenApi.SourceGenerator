using Microsoft.AspNetCore.Mvc;

namespace OpenApi.Petstore.Commands
{
    public partial class DeleteOrderCommand : IDeleteOrderCommand
    {
        public partial Task<IActionResult> ExecuteAsync(long orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}