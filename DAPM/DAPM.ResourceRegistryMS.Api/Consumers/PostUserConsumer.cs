using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    // Author: Maxime Rochat - s241741
    public class PostUserConsumer : IQueueConsumer<PostUserMessage>
    {
        private IUserService _userService;

        public PostUserConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ConsumeAsync(PostUserMessage message)
        {
            await _userService.PostUser(message.user);
            return;
        }
    }
}
