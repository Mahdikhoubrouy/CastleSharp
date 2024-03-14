namespace CastleWindsor.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string commandText)
        {
            CommandText = commandText;
        }

        public string CommandText { get; set; }
        public string ConditionName { get; set; }
    }
}
