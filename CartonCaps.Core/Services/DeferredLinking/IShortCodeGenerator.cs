namespace CartonCaps.Core.Services.DeferredLinking
{
    /// <summary>
    /// A utility that generates a random, base62 series of characters
    /// </summary>
    public interface IShortCodeGenerator
    {
        /// <summary>
        /// Returns a string of random base62 characters
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GenerateShortCode(int length);
    }
}