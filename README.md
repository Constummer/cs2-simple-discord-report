Simple Discord Report Plugin

Simply it will send message to discord web hook whatever the player types

Example Usage
```!report something happened, send admin please```

Example Discord Output
``` Server1 | Player1 | 7777777777(steamid) =  something happened, send admin please```


Add as many as commands as you like into ```Commands``` section of the json as given example. 



```json
{
  "Prefix": "Prefix", //prefix to player response
  "PlayerResponseNotEnoughInput": "Daha fazla bilgi vermelisiniz", //when player input is not enough like when player only types !report
  "Commands": { //add as many you like like this
    "report": "https://discord.com/api/webhooks/****************/*************************",
    "report2": "https://discord.com/api/webhooks/****************/*************************",
    "reports": "https://discord.com/api/webhooks/****************/*************************"
  },
  "PlayerResponseSuccessfull": "Report başarıyla iletildi", //This will be shown to player
  "ServerName": "Server1" //This will be shown in discord, if you dont want to see just remove it like "ServerName": "" or completly remove the property
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
