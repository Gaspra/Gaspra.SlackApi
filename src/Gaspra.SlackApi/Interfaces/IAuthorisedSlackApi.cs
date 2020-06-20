using Gaspra.SlackApi.Models.Responses;
using Refit;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    [Headers("Authorization: Bearer")]
    public interface IAuthorisedSlackApi
    {
        [Get("/api/conversations.list")]
        Task<ChannelsResponse> GetChannels();

        [Get("/api/conversations.history")]
        Task<ConversationHistoryResponse> GetConversationHistory(
            [AliasAs("channel")]string channelId
            );

        [Post("/api/chat.postMessage")]
        Task PostMessage(
            [AliasAs("channel")]string channelId,
            [AliasAs("text")]string message
            );

        [Post("/api/chat.postMessage")]
        Task PostInThread(
            [AliasAs("channel")]string channelId,
            [AliasAs("thread_ts")]string threadId,
            [AliasAs("text")]string message
            );
    }
}
