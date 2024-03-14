using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CallBackQueryAttribute : Attribute
    {
        /// <summary>
        /// call back query section of data check || for example : callbackQuery.Data == CallBackQueryText
        /// </summary>
        public string CallBackQueryText { get; set; }

        /// <summary>
        /// The name of your custom condition 
        /// </summary>
        public string ConditionName { get; set; }
    }
}
