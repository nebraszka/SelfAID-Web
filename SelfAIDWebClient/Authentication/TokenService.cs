namespace SelfAID.WebClient.Authorization
{
    public class TokenService
    {
        public string Token { get; private set; }

        public void SetToken(string token)
        {
            Token = token;
        }

        public void RemoveToken()
        {
            Token = null;
        }

        public string GetToken()
        {
            return Token;
        }
    }
}