using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CastleSharp.ConsoleTest
{
    public class ConditionTest
    {
        public static bool PingFilterd(Update update) => update.Message.Chat.Username.ToLower() == "sir_miti";
    }
}
