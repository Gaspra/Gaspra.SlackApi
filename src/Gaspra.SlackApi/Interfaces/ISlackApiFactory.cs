namespace Gaspra.SlackApi.Interfaces
{
    public interface ISlackApiFactory
    {
        IAuthorisedSlackApi CreateAuthorisedSlackApi(string authorisationToken);
        ISlackApi CreateSlackApi();
    }
}
