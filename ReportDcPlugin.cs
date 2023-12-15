using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ReportDcPlugin;

public class ReportDcPlugin : BasePlugin
{
    public override string ModuleName => "ReportDcPlugin";

    public override string ModuleVersion => "0.0.1";
    public override string ModuleAuthor => "Constummer";
    public override string ModuleDescription => "ReportDcPlugin";

    private static readonly HttpClient _httpClient;

    static ReportDcPlugin()
    {
        _httpClient = new HttpClient();
    }

    public class Config
    {
        public string Prefix { get; set; }
        public string PlayerResponseNotEnoughInput { get; set; }
        public Dictionary<string, string> Commands { get; set; }
        public string PlayerResponseSuccessfull { get; set; }
        public string ServerName { get; internal set; }
    }

    private static Config? _config;

    public override void Unload(bool hotReload)
    {
        base.Unload(hotReload);
    }

    public override void Load(bool hotReload)
    {
        var configPath = Path.Join(ModuleDirectory, "Config.json");
        if (!File.Exists(configPath))
        {
            var data = new Config()
            {
                Prefix = "Prefix",
                PlayerResponseNotEnoughInput = "Daha fazla bilgi vermelisiniz",
                PlayerResponseSuccessfull = "Report başarıyla iletildi",
                Commands = new Dictionary<string, string>()
                {
                    {"report","https://discord.com/api/webhooks/****************/*************************" },
                    {"report2","https://discord.com/api/webhooks/****************/*************************" },
                    {"reports","https://discord.com/api/webhooks/****************/*************************" }
                },
                ServerName = "Server1"
            };
            File.WriteAllText(configPath, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
            _config = data;
        }
        else
        {
            var text = File.ReadAllText(configPath);

            _config = JsonSerializer.Deserialize<Config>(text);
        };

        if (_config?.Commands != null)
        {
            foreach (var command in _config.Commands)
            {
                AddCommand(command.Key, command.Key, (player, info) =>
                {
                    if (ValidateCallerPlayer(player) == false)
                    {
                        return;
                    }

                    if (info.ArgCount <= 1)
                    {
                        player!.PrintToChat(AddPrefixToTheMessage(_config.PlayerResponseNotEnoughInput, _config.Prefix));
                    };

                    var msg = $"{player.PlayerName} | {player.SteamID} = {info.ArgString}";

                    if (string.IsNullOrWhiteSpace(_config.ServerName) == false)
                    {
                        msg = $"{_config.ServerName} | {msg}";
                    }

                    Server.NextFrame(async () =>
                    {
                        await PostAsync(command.Value, msg);
                    });
                    player.PrintToChat(AddPrefixToTheMessage(_config.PlayerResponseSuccessfull, _config.Prefix));
                });
            }
        }
        base.Load(hotReload);
    }

    private static string AddPrefixToTheMessage(string message, string prefix)
    {
        if (string.IsNullOrWhiteSpace(prefix))
            return message;
        return $"[{prefix}]{message}";
    }

    private static bool ValidateCallerPlayer(CCSPlayerController? player)
    {
        if (player == null) return false;
        if (player.IsBot) return false;
        if (player == null
            || !player.IsValid
            || player.PlayerPawn == null
            || !player.PlayerPawn.IsValid
            || player.PlayerPawn.Value == null
            || !player.PlayerPawn.Value.IsValid
            ) return false;
        return true;
    }

    private async Task PostAsync(string uri, string message)
    {
        try
        {
            var body = JsonSerializer.Serialize(new { content = message });
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = (await _httpClient.PostAsync($"{uri}", content)).EnsureSuccessStatusCode();
        }
        catch
        {
        }
    }
}