
using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages;

namespace CrowdfindingApp.Common.Handlers
{
    public interface IRequestHandlerBase<TRequest, TReply>
        where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        Task<TReply> PreExecuteAsync(TRequest request);

        Task<TReply> ValidateRequestMessage(TRequest requestMessage);

        Task<TReply> ExecuteAsync(TRequest request);

        Task<TReply> HandleAsync(TRequest requestMessage);
    }
}
