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
        public string CallBackQueryText { get; set; }
        public string ConditionName { get; set; }
    }
}
