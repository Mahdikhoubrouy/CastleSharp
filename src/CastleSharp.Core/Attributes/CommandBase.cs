using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleSharp.Core.Attributes
{
    /// <summary>
    /// Command Base
    /// </summary>
    public abstract class CommandBase : Attribute
    {
        /// <summary>
        /// The static command text
        /// </summary>
        public string? Command { get; set; }

        /// <summary>
        /// The name of custom condition 
        /// </summary>
        public string? ConditionName { get; set; }
    }
}
