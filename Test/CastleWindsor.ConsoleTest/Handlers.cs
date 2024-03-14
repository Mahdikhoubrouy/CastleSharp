using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastleWindsor.Core.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace CastleWindsor.ConsoleTest
{
    public class Handlers
    {
        public static bool HelloCondition(Message message) => message.Text!.ToLower().StartsWith("hello");

        [Command("hello",ConditionName = "HelloCondition")]
        public static async Task Hello(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hi ✅", replyToMessageId: message.MessageId);
        }


        [Command("Ping")]
        public static async Task Ping(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Pong Baby ✅", replyToMessageId: message.MessageId);
        }
    }
}
