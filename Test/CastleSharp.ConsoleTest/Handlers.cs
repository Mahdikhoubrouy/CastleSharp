using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastleSharp.Core.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace CastleSharp.ConsoleTest
{
    public class Handlers
    {
        public static bool HelloCondition(Message message) => message.Text!.ToLower().StartsWith("hello");

        [Command(ConditionName = "HelloCondition")]
        public static async Task Hello(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hi ✅", replyToMessageId: message.MessageId);
        }


        [Command(Command = "Ping")]
        public static async Task Ping(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Pong Baby ✅", replyToMessageId: message.MessageId);
        }
    }
}
