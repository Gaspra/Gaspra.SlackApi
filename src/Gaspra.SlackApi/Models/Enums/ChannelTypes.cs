using System.ComponentModel;

namespace Gaspra.SlackApi.Models.Enums
{
    public enum ChannelTypes
    {
        [Description("public_channel")]
        PublicChannel,
        [Description("private_channel")]
        PrivateChannel,
        [Description("mpim")]
        GroupMessage,
        [Description("im")]
        DirectMessage
    }
}
