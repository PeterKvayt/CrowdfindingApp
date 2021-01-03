
namespace CrowdfindingApp.Common.Helpers
{
    public interface IHasher
    {
        /// <summary>
        /// Hash value.
        /// </summary>
        /// <param name="value">Value for hash.</param>
        /// <returns>First: hashed value; second: salt.</returns>
        (string, string) GetHashWithSalt(string value);

        bool Equals(string hash, string valueToHash, string salt);
    }
}
