using Gaspra.SlackApi.Models.MessageBlocks;
using Gaspra.SlackApi.Models.MessageBlocks.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaspra.SlackApi.Extensions.MessageBlocks
{
    public static class SlackMessageBlockElementExtensions
    {
        public static IReadOnlyCollection<SlackMessageBlockElement> CreateButtons(
            this IReadOnlyCollection<SlackButton> buttonList)
        {
            return buttonList.Select(x => new SlackMessageBlockElement()
            {
                Text = new SlackMessageBlockTextContent()
                {
                    Text = x.Text,
                    Emoji = true
                },
                Value = x.Value
            }).ToList();
            
        }
    }
}
