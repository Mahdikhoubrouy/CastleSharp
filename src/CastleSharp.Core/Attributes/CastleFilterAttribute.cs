using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CastleSharp.Core.Attributes
{
    /// <summary>
    /// Specifies that a method or class should be included in custom filtering logic.
    /// Can be applied to controllers or handler methods to enable conditional execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class CastleFilterAttribute : Attribute
    {
        public long[]? Chats { get; set; }
        public string? ConditionName { get; set; }
    }
}
