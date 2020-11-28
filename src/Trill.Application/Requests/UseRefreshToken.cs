namespace Trill.Application.Requests
{
    public class UseRefreshToken
    {
        public string RefreshToken { get; }

        public UseRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}