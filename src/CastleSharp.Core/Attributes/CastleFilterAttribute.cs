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
        /// <summary>
        /// An optional list of allowed chat IDs.
        /// If provided, only updates from these chats will be accepted.
        /// </summary>
        public long[]? Chats { get; set; }

        /// <summary>
        /// The name of a method (in the same class) that will be called to evaluate custom logic.
        /// The method should accept a single <see cref="Update"/> parameter and return a <see cref="bool"/>.
        /// </summary>
        public string? ConditionName { get; set; }
    }
}
