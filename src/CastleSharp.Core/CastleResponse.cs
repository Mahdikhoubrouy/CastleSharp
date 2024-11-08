namespace CastleSharp.Core
{
    /// <summary>
    /// Response Model of CastelSharp
    /// </summary>
    public class CastleResponse
    {
        /// <summary>
        /// Find And did answer for command
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// make success
        /// </summary>
        /// <returns></returns>
        public static CastleResponse Success()
        {
            return new CastleResponse
            {
                IsSuccess = true,
            };
        }

        /// <summary>
        /// make fail
        /// </summary>
        /// <returns></returns>
        public static CastleResponse Failed()
        {
            return new CastleResponse
            {
                IsSuccess = false,
            };
        }

    }
}
