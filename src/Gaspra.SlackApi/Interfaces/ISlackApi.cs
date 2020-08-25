using Gaspra.SlackApi.Models.Responses;
using Refit;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    public interface ISlackApi
    {
        [Get("/api/conversations.list")]
        Task<SlackChannelsResponse> GetChannels(
            [AliasAs("token")]string token,
            [AliasAs("types")]string types,
            [AliasAs("limit")]int limit,
            [AliasAs("cursor")]string cursor
            );

        [Get("/api/users.list")]
        Task<SlackUsersResponse> GetUsers(
            [AliasAs("token")]string token,
            [AliasAs("limit")]int limit,
            [AliasAs("cursor")]string cursor
            );

        [Get("/api/conversations.history")]
        Task<SlackConversationHistoryResponse> GetConversationHistory(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channel
            );

        [Post("/api/chat.postMessage")]
        Task<SlackPostMessageResponse> PostMessage(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channel,
            [AliasAs("text")]string message
            );

        [Post("/api/chat.postMessage")]
        Task<SlackPostMessageResponse> PostMessageWithBlocks(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channel,
            [AliasAs("text")]string backupMessage,
            [AliasAs("blocks")]string blocks
            );

        [Post("/api/chat.postMessage")]
        Task PostInThread(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channel,
            [AliasAs("thread_ts")]string threadId,
            [AliasAs("text")]string message
            );
    }
}
