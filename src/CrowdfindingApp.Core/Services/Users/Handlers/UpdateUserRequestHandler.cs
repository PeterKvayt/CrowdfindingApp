using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.Handlers;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Users;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using CrowdfindingApp.Common.Data.BusinessModels;
using System.Linq;
using CrowdfindingApp.Core.Services.Users.Validators;
using CrowdfindingApp.Common.Core.Maintainers.FileStorageProvider;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequestMessage, ReplyMessageBase, User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private ChangePasswordRequestHandler _changePasswordRequestHandler;
        private IFileStorage _fileStorage;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper, ChangePasswordRequestHandler changePasswordRequestHandler, IFileStorage fileStorage)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _changePasswordRequestHandler = changePasswordRequestHandler ?? throw new ArgumentNullException(nameof(changePasswordRequestHandler));
            _fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));
        }

        protected override async Task<(ReplyMessageBase, User)> ValidateRequestMessageAsync(UpdateUserRequestMessage requestMessage)
        {
            var (reply, user) = await base.ValidateRequestMessageAsync(requestMessage);

            var validator = new UpdateUserValidator();
            var validationResult = await validator.ValidateAsync(requestMessage);
            await reply.MergeAsync(validationResult);

            user = await _userRepository.GetByIdAsync(User.GetUserId());
            if(user == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, user);
            }

            // ToDo: validate user name

            if(!requestMessage.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                var emailIsFree = await _userRepository.GetUserByEmailAsync(requestMessage.Email) == null;
                if(!emailIsFree)
                {
                    reply.AddValidationError(UserErrorKeys.UniqueEmail);
                    return (reply, user);
                }
            }

            return (reply, user);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(UpdateUserRequestMessage request, User userForUpdate)
        {
            if(request.CurrentPassword.NonNullOrWhiteSpace())
            {
                var changePasswordReply = await _changePasswordRequestHandler.HandleAsync(
                    new ChangePasswordRequestMessage(request.CurrentPassword, request.NewPassword, request.ConfirmPassword), User);

                if(changePasswordReply.Errors?.Any() ?? false)
                {
                    return changePasswordReply;
                }
            }

            _mapper.Map(request, userForUpdate);
            userForUpdate.Image = await SaveImageAsync(userForUpdate.Id, userForUpdate.Image);

            await _userRepository.UpdateAsync(userForUpdate, _mapper, userForUpdate);
            return new ReplyMessageBase();
        }

        private async Task<string> SaveImageAsync(Guid userId, string image)
        {
            if(image.IsNullOrWhiteSpace())
            {
                return null;
            }
            image = image.Split('/').Last();
            await _fileStorage.SaveUserImageAsync(image, userId);
            return image;
        }
    }
}
