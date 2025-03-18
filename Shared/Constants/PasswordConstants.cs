using System.Security.Cryptography;

namespace Shared.Constants
{
	internal static class PasswordConstants
	{
		public static readonly int SALT_SIZE = 16;
		public static readonly int HASH_SIZE = 32;
		public static readonly int ITERATIONS = 100000;
		public static readonly HashAlgorithmName ALGORITHM = HashAlgorithmName.SHA256;
	}
}
