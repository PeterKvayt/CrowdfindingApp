using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages;

namespace CrowdfindingApp.Common.Handlers
{
    public abstract class RequestHandlerBase<TRequest, TReply> : IRequestHandlerBase<TRequest, TReply>
        where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        protected virtual Task<ReplyMessageBase> PreExecuteAsync(TRequest request)
        {
            return Task.FromResult(ReplyMessageBase.Empty);
        }

        protected virtual Task<ReplyMessageBase> ValidateRequestMessageAsync(TRequest requestMessage)
        {
            return Task.FromResult(ReplyMessageBase.Empty);
        }

        protected abstract Task<TReply> ExecuteAsync(TRequest request);

        public virtual async Task<TReply> HandleAsync(TRequest requestMessage)
        {
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

            var validationResult = await ValidateRequestMessageAsync(requestMessage);
            if(!validationResult.Success)
            {
                var reply = new TReply();
                reply.Merge(validationResult);
                return reply;
            }

            return await ExecuteAsync(requestMessage);
        }

        Task<TReply> IRequestHandlerBase<TRequest, TReply>.PreExecuteAsync(TRequest request)
        {
            throw new NotImplementedException();
        }

        Task<TReply> IRequestHandlerBase<TRequest, TReply>.ValidateRequestMessage(TRequest requestMessage)
        {
            throw new NotImplementedException();
        }

        Task<TReply> IRequestHandlerBase<TRequest, TReply>.ExecuteAsync(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
