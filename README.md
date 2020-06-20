# Gaspra.SlackApi

[![nuget package](https://github.com/Gaspra/Gaspra.SlackApi/workflows/publish%20to%20nuget/badge.svg?branch=master)](https://www.nuget.org/packages/Gaspra.SlackApi/)

### About

Simple .NET Core 3.1 class library for making Slack API calls.

### Usage

Unauthorised Slack API (token must be passed with each method call)

```
public class SlackApp
{
    private readonly ISlackApi slackApi;
    
    public SlackApp (ISlackApiFactory slackApiFactory)
    {
        this.slackApi = slackApiFactory.CreateSlackApi();
    }
    
    public async Task SendMessage()
    {
        await slackApi.PostMessage("token", "channelId", "message");
    }
}
```

Authorised Slack API

```
public class SlackApp
{
    private readonly IAuthorisedSlackApi authorisedSlackApi;
    
    public SlackApp (ISlackApiFactory slackApiFactory)
    {
        this.authorisedSlackApi = slackApiFactory.CreateAuthorisedSlackApi("token");
    }
    
    public async Task SendMessage()
    {
        await authorisedSlackApi.PostMessage("channelId", "message");
    }
}
```