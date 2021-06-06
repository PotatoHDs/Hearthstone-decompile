using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020009A9 RID: 2473
public static class Crypto
{
	// Token: 0x02002666 RID: 9830
	public static class SHA1
	{
		// Token: 0x06013706 RID: 79622 RVA: 0x00533FCC File Offset: 0x005321CC
		public static string Calc(byte[] bytes, int start, int count)
		{
			byte[] array = System.Security.Cryptography.SHA1.Create().ComputeHash(bytes, start, count);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06013707 RID: 79623 RVA: 0x00534018 File Offset: 0x00532218
		public static string Calc(byte[] bytes)
		{
			return Crypto.SHA1.Calc(bytes, 0, bytes.Length);
		}

		// Token: 0x06013708 RID: 79624 RVA: 0x00534024 File Offset: 0x00532224
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
			foreach (byte b in hasher.Hash)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			hash(stringBuilder.ToString());
			yield break;
		}

		// Token: 0x06013709 RID: 79625 RVA: 0x00534044 File Offset: 0x00532244
		public static string Calc(string message)
		{
			byte[] array = new byte[message.Length * 2];
			Buffer.BlockCopy(message.ToCharArray(), 0, array, 0, array.Length);
			return Crypto.SHA1.Calc(array);
		}

		// Token: 0x0601370A RID: 79626 RVA: 0x00534076 File Offset: 0x00532276
		public static string Calc(FileInfo path)
		{
			return Crypto.SHA1.Calc(File.ReadAllBytes(path.FullName));
		}

		// Token: 0x0400F086 RID: 61574
		public const int Length = 40;

		// Token: 0x0400F087 RID: 61575
		public const string Null = "0000000000000000000000000000000000000000";
	}

	// Token: 0x02002667 RID: 9831
	public static class Rijndael
	{
		// Token: 0x0601370B RID: 79627 RVA: 0x00534088 File Offset: 0x00532288
		public static byte[] Encrypt(byte[] whatToEncrypt, byte[] secretKey)
		{
			int num = (secretKey == null) ? 0 : secretKey.Length;
			if (num != 32)
			{
				throw new CryptographicException(string.Format("Key size ({0}) not supported by algorithm - expected {1} bytes", num, 32));
			}
			return new RijndaelManaged
			{
				Key = secretKey,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateEncryptor().TransformFinalBlock(whatToEncrypt, 0, whatToEncrypt.Length);
		}

		// Token: 0x0601370C RID: 79628 RVA: 0x005340EC File Offset: 0x005322EC
		public static byte[] Decrypt(byte[] whatToDecrypt, byte[] secretKey)
		{
			int num = (secretKey == null) ? 0 : secretKey.Length;
			if (num != 32)
			{
				throw new CryptographicException(string.Format("Key size ({0}) not supported by algorithm - expected {1} bytes", num, 32));
			}
			return new RijndaelManaged
			{
				Key = secretKey,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor().TransformFinalBlock(whatToDecrypt, 0, whatToDecrypt.Length);
		}

		// Token: 0x0400F088 RID: 61576
		public const int REQUIRED_SECRET_KEY_LENGTH = 32;
	}
}
