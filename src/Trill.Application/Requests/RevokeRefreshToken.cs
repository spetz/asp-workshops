namespace Trill.Application.Requests
{
    public class RevokeRefreshToken
    {
        public string RefreshToken { get; }

        public RevokeRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}