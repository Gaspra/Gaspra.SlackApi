using Gaspra.SlackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    public interface ISlackUserExtensions
    {
        Task<IList<SlackUser>> GetAllSlackUsers(
            string token,
            int searchLimit = 50);

        Task<SlackUser> GetSlackUserWithName(
            string token,
            string name,
            int searchLimit = 50);
    }
}
