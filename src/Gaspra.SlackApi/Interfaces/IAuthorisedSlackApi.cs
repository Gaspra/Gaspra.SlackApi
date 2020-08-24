using Gaspra.SlackApi.Models;
using Gaspra.SlackApi.Models.Responses;
using Refit;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    [Headers("Authorization: Bearer")]
    public interface IAuthorisedSlackApi
    {
        [Get("/api/conversations.list")]
        Task<SlackChannelsResponse> GetChannels();

        [Get("/api/conversations.history")]
        Task<SlackConversationHistoryResponse> GetConversationHistory(
            [AliasAs("channel")]string channel
            );

        [Post("/api/chat.postMessage")]
        Task PostMessage(
            [AliasAs("channel")]string channel,
            [AliasAs("text")]string message
            );

        [Post("/api/chat.postMessage")]
        Task<SlackMessage> PostMessageAndReturnDetails(
            [AliasAs("channel")]string channel,
            [AliasAs("text")]string message
            );

        [Post("/api/chat.postMessage")]
        Task PostInThread(
            [AliasAs("channel")]string channel,
            [AliasAs("thread_ts")]string threadId,
            [AliasAs("text")]string message
            );
    }
}
