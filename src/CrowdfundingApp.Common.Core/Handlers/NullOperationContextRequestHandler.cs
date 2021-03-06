﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Messages;

namespace CrowdfundingApp.Common.Core.Handlers
{
    public abstract class NullOperationContextRequestHandler<TRequest, TReply> : RequestHandlerBase<TRequest, TReply, NullOperationContext>
         where TRequest : MessageBase, new()
        where TReply : ReplyMessageBase, new()
    {
        protected virtual Task<ReplyMessageBase> PreExecuteAsync(TRequest request)
        {
            return Task.FromResult(ReplyMessageBase.Empty);
        }

        protected virtual async Task<ReplyMessageBase> ValidateRequestMessageAsync(TRequest requestMessage)
        {
            var (reply, ctx) = await base.ValidateRequestMessageAsync(requestMessage);
            return reply;
        }

        protected override sealed Task<TReply> ExecuteAsync(TRequest request, NullOperationContext ctx)
        {
            return ExecuteAsync(request);
        }

        protected abstract Task<TReply> ExecuteAsync(TRequest request);

        public override async Task<TReply> HandleAsync(TRequest requestMessage, ClaimsPrincipal user)
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

            var validationResult = await ValidateRequestMessageAsync(requestMessage);
            if(!validationResult.Success)
            {
                var reply = new TReply();
                reply.Merge(validationResult);
                return reply;
            }

            return await ExecuteAsync(requestMessage);
        }
    }
}
