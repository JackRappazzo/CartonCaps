namespace CartonCaps.Core.Services.DeferredLinking
{
    public interface IShortCodeGenerator
    {
        string GenerateShortCode(int length);
    }
}