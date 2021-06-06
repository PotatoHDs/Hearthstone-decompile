using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
	public static class SHA1
	{
		public const int Length = 40;

		public const string Null = "0000000000000000000000000000000000000000";

		public static string Calc(byte[] bytes, int start, int count)
		{
			byte[] array = System.Security.Cryptography.SHA1.Create().ComputeHash(bytes, start, count);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		public static string Calc(byte[] bytes)
		{
			return Calc(bytes, 0, bytes.Length);
		}

		public static IEnumerator Calc(byte[] bytes, int inputCount, Action<string> hash)
		{
			System.Security.Cryptography.SHA1 hasher = System.Security.Cryptography.SHA1.Create();
			int offset = 0;
			while (bytes.Length - offset >= inputCount)
			{
				offset += hasher.TransformBlock(bytes, offset, inputCount, bytes, offset);
				yield return null;
			}
			hasher.TransformFinalBlock(bytes, offset, bytes.Length - offset);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] hash2 = hasher.Hash;
			foreach (byte b in hash2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			hash(stringBuilder.ToString());
		}

		public static string Calc(string message)
		{
			byte[] array = new byte[message.Length * 2];
			Buffer.BlockCopy(message.ToCharArray(), 0, array, 0, array.Length);
			return Calc(array);
		}

		public static string Calc(FileInfo path)
		{
			return Calc(File.ReadAllBytes(path.FullName));
		}
	}

	public static class Rijndael
	{
		public const int REQUIRED_SECRET_KEY_LENGTH = 32;

		public static byte[] Encrypt(byte[] whatToEncrypt, byte[] secretKey)
		{
			int num = ((secretKey != null) ? secretKey.Length : 0);
			if (num != 32)
			{
				throw new CryptographicException($"Key size ({num}) not supported by algorithm - expected {32} bytes");
			}
			return new RijndaelManaged
			{
				Key = secretKey,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateEncryptor().TransformFinalBlock(whatToEncrypt, 0, whatToEncrypt.Length);
		}

		public static byte[] Decrypt(byte[] whatToDecrypt, byte[] secretKey)
		{
			int num = ((secretKey != null) ? secretKey.Length : 0);
			if (num != 32)
			{
				throw new CryptographicException($"Key size ({num}) not supported by algorithm - expected {32} bytes");
			}
			return new RijndaelManaged
			{
				Key = secretKey,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor().TransformFinalBlock(whatToDecrypt, 0, whatToDecrypt.Length);
		}
	}
}
