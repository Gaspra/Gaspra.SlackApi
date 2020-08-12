using Gaspra.SlackApi.Models;
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

        [Get("/api/conversations.history")]
        Task<SlackConversationHistoryResponse> GetConversationHistory(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channelId
            );

        [Post("/api/chat.postMessage")]
        Task<SlackMessage> PostMessage(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channelId,
            [AliasAs("text")]string message
            );

        [Post("/api/chat.postMessage")]
        Task PostInThread(
            [AliasAs("token")]string token,
            [AliasAs("channel")]string channelId,
            [AliasAs("thread_ts")]string threadId,
            [AliasAs("text")]string message
            );
    }
}
