using Discord_Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Figgle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_Bot.Models;
using Discord_Bot.Context;

namespace Discord_Bot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextConfiguration Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
            Client.GuildMemberAdded += OnMemberAdded;
            Client.Heartbeated += CheckBanDatabaseOnHeartbeat;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            };

            var Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<banCommands>();
            Commands.RegisterCommands<dbCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnMemberAdded(GuildMemberAddEventArgs e)
        {
            var guild = e.Guild;
            ulong tylerServer = 380871056757358592;
            ulong gabeTeamServer = 736434039866785900;
            ulong testServer = 714663062254256218;

            if (guild.Id == tylerServer)
            {
                var roles = Client.Guilds.Values.FirstOrDefault(x => x.Name == "Tyler's Streaming Discord").Roles.Values;
                var memberRole = roles.First(x => x.Name == "Viewers");
                await e.Member.GrantRoleAsync(memberRole);
            }

            else if (guild.Id == gabeTeamServer)
            {
                var roles = Client.Guilds.Values.FirstOrDefault(x => x.Name == "Destiny Gods").Roles.Values;
                var memberRole = roles.First(x => x.Name == "shittas");
                await e.Member.GrantRoleAsync(memberRole);
            }

            else if (guild.Id == testServer)
            {
                var roles = Client.Guilds.Values.FirstOrDefault(x => x.Name == "Bot Testing").Roles.Values;
                var memberRole = roles.First(x => x.Name == "Member");
                await e.Member.GrantRoleAsync(memberRole);
            }

        }

        private async Task CheckBanDatabaseOnHeartbeat(HeartbeatEventArgs e)
        {
            if (Client.Guilds.Count == 0)
            {
                return;
            }

            using (SqliteContext lite = new SqliteContext())
            {
                var bans = lite.Bans;
                DateTime currentTime = DateTime.Now;
                var guild = Client.Guilds.Values.FirstOrDefault(x => x.Name == "Bot Testing");
                var members = guild.Members.Values;
                var roles = guild.Roles.Values;
                var straightJacket = roles.First(x => x.Name == "Straight Jacket");
                List<ulong> userIDs = new List<ulong>();

                foreach (var user in bans)
                {
                    if (user.unbanTime <= currentTime)
                    {
                        userIDs.Add(user.userID);
                        lite.Remove(user);
                        await lite.SaveChangesAsync();
                    }
                }

                foreach (var member in members)
                {
                    foreach (var id in userIDs)
                    {
                        if (member.Id == id)
                        {
                            await member.RevokeRoleAsync(straightJacket);
                        }
                    }
                }
            }
        }
    }
}
