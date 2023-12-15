Simple Discord Report Plugin

Simply it will send message to discord web hook whatever the player types

Example Usage
```!report something happened, send admin please```



Add as many as commands as you like into ```Commands``` section of the json as given example. 



```json
{
  "Prefix": "Prefix",
  "PlayerResponseNotEnoughInput": "Give More Specific Detail",
  "Commands": {
    "report": "https://discord.com/api/webhooks/****************/*************************",
    "report2": "https://discord.com/api/webhooks/****************/*************************",
    "reports": "https://discord.com/api/webhooks/****************/*************************"
  },
  "PlayerResponseSuccessfull": "Report Submitted Successfully"
}
```

How to get Discord Webhook

* 1 - Open the Discord channel you want to receive notifications.
* 2 - From the channel menu, select Edit channel.
* 3 - Select Integrations.
* 4 - If there are no existing webhooks, select Create Webhook. Otherwise, select View Webhooks then New Webhook.
* 5 - Enter the name of the bot to post the message.
* 6 - Optional. Edit the avatar.
* 7 - Copy the URL from the WEBHOOK URL field.
* 8 - Select Save.
