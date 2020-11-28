namespace Trill.Core.Services
{
    public interface IRng
    {
        string Generate(int length = 30);
    }
}