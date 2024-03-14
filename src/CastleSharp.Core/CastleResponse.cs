namespace CastleSharp.Core
{
    public class CastleResponse
    {
        public bool IsSuccess { get; set; }


        public static CastleResponse Success()
        {
            return new CastleResponse
            {
                IsSuccess = true,
            };
        }

        public static CastleResponse Failed()
        {
            return new CastleResponse
            {
                IsSuccess = false,
            };
        }

    }
}
