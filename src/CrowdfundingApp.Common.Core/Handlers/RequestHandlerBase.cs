using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Messages;

namespace CrowdfundingApp.Common.Core.Handlers
{
    public abstract class RequestHandlerBase<TRequest, TReply, TOperationContext>
        where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
        where TOperationContext : new()
    {
        protected virtual ClaimsPrincipal User { get; set; }

        protected virtual Task<ReplyMessageBase> PreExecuteAsync(TRequest request)
        {
            return Task.FromResult(ReplyMessageBase.Empty);
        }

        protected virtual Task<(ReplyMessageBase, TOperationContext)> ValidateRequestMessageAsync(TRequest requestMessage)
        {
            return Task.FromResult((ReplyMessageBase.Empty, new TOperationContext()));
        }

        protected abstract Task<TReply> ExecuteAsync(TRequest request, TOperationContext ctx);

        public virtual async Task<TReply> HandleAsync(TRequest requestMessage, ClaimsPrincipal user)
        {
            User = user;

            if(requestMessage == null)
            {
                throw new ArgumentNullException(nameof(requestMessage));
            }

            var preExecuteResult = await PreExecuteAsync(requestMessage);
            if(!preExecuteResult.Success)
            {
                var reply = new TReply();
                reply.Merge(preExecuteResult);
                return reply;
            }

            var (validationResult, ctx) = await ValidateRequestMessageAsync(requestMessage);
            if(!validationResult.Success)
            {
                var reply = new TReply();
                reply.Merge(validationResult);
                return reply;
            }

            return await ExecuteAsync(requestMessage, ctx);
        }
    }
}
