namespace Trill.Application.Requests
{
    public class SignIn
    {
        public string Name { get; }
        public string Password { get; }

        public SignIn(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}