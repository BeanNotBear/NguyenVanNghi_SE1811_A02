using Shared.Constants;
using System.Security.Cryptography;

namespace Shared.PasswordHasher
{
	public sealed class PasswordHasher
	{
		private static PasswordHasher? _instance;
		private static readonly object padlock = new object();

		private PasswordHasher()
		{
		}

		public static PasswordHasher Instance
		{
			get
			{
				lock (padlock)
				{
					if (_instance == null)
					{
						_instance = new PasswordHasher();
					}
					return _instance;
				}
			}
		}

		public string Hash(string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(PasswordConstants.SALT_SIZE);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, PasswordConstants.ITERATIONS, PasswordConstants.ALGORITHM, PasswordConstants.HASH_SIZE);
			return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
		}

		public bool Verify(string password, string passwordHash)
		{
			string[] parts = passwordHash.Split('-');
			byte[] hash = Convert.FromHexString(parts[0]);
			byte[] salt = Convert.FromHexString(parts[1]);

			byte[] inputPasswordHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, PasswordConstants.ITERATIONS, PasswordConstants.ALGORITHM, PasswordConstants.HASH_SIZE);
			return CryptographicOperations.FixedTimeEquals(hash, inputPasswordHash);
		}
	}
}
