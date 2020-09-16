using Gaspra.SlackApi.Models.MessageBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaspra.SlackApi.Extensions.MessageBlocks
{
    public static class SlackMessageBlockElementExtensions
    {
        public static IReadOnlyCollection<SlackMessageBlockElement> CreateButtons(
            this IReadOnlyCollection<string> buttonList)
        {
            return buttonList.Select(x => new SlackMessageBlockElement()
            {
                Text = new SlackMessageBlockTextContent()
                {
                    Text = x,
                    Emoji = true
                },
                Value = $"as_{x}"
            }).ToList();
            
        }
    }
}
