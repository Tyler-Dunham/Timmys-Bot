﻿using Discord_Bot.Context;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class dbCommands : BaseCommandModule
    {
        [Command("migrate")]
        [Description("Finish migrations.")]
        public async Task MigrateLite(CommandContext ctx)
        {
            try
            {

                Console.WriteLine("***migrating...***");

                await using SqliteContext lite = new SqliteContext();

                if (lite.Database.GetPendingMigrationsAsync().Result.Any())
                {
                    await lite.Database.MigrateAsync();
                }

                await ctx.Channel.SendMessageAsync($"Migration complete.");
            }

            catch (Exception e)
            {
                await ctx.Channel.SendMessageAsync($"Error: {e}");
            }
        }
    }
}
