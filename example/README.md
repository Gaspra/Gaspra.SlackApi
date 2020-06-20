# Gaspra.SlackApi.Example

### Setup
1) Run the `Setup.ps1` from the root of the example folder. 

The setup powershell will build `Gaspra.SlackApi` and `Gaspra.SlackApi.Example`. The example service requires the `.dll` from the `Gaspra.SlackApi` debug configuration (this means any changes you make to the `Gaspra.SlackApi` will be live in the example).

2) Replace the `token` and `channelName` variables in the `SlackApiService.cs`

![image](https://user-images.githubusercontent.com/35634732/85205270-42387180-b312-11ea-9813-e78265d7f3dc.png)

The `token` can be found on your Slack App's OAuth & Permissions page:

![image](https://user-images.githubusercontent.com/35634732/85205330-afe49d80-b312-11ea-8cee-b828690baca2.png)

The `channelName` is the channel you have added the app to and wish it to post messages to. 

3) Run the example project and check the slack channel:

![image](https://user-images.githubusercontent.com/35634732/85205390-110c7100-b313-11ea-990c-0ea783728850.png)
