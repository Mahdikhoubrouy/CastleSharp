namespace CastleSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        public string CommandText { get; set; }
        public string ConditionName { get; set; }
    }
}
