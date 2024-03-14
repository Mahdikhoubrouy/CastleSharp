namespace CastleSharp.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// message section of text check || for example : message.Text == CommandText
        /// </summary>
        /// 
        public string CommandText { get; set; }

        /// <summary>
        /// The name of your custom condition 
        /// </summary>
        public string ConditionName { get; set; }
    }
}
