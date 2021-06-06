using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using bgs;

namespace VerifyScanSignature
{
	public class Verifier
	{
		private static byte[] s_magic = Encoding.UTF8.GetBytes("NGIS");

		private static byte[] s_modulus = new byte[256]
		{
			71, 147, 189, 90, 135, 248, 118, 158, 9, 35,
			81, 227, 220, 190, 70, 67, 131, 87, 138, 136,
			32, 95, 228, 80, 60, 61, 203, 23, 67, 213,
			44, 40, 101, 167, 34, 67, 50, 88, 7, 55,
			202, 163, 63, 214, 194, 117, 161, 34, 91, 196,
			172, 134, 21, 98, 196, 41, 156, 246, 223, 49,
			186, 224, 19, 41, 73, 44, 186, 3, 227, 167,
			254, 36, 60, 183, 180, 102, 225, 65, 184, 159,
			132, 9, 209, 125, 62, 9, 248, 172, 44, 88,
			247, 75, 144, 147, 59, 207, 190, 37, 227, 163,
			49, 252, 46, 114, 51, 214, 24, 224, 228, 215,
			91, 168, 117, 41, 170, 140, 185, 72, 152, 206,
			153, 90, 158, 222, 139, 166, 82, 91, 28, 4,
			231, 28, 98, 37, 167, 66, 13, 32, 41, 233,
			79, 235, 215, 29, 14, 86, 107, 118, 30, 161,
			7, 153, 56, 252, 114, 50, 103, 106, 93, 148,
			5, 85, 70, 93, 125, 56, 77, 142, 82, 48,
			180, 15, 74, 94, 78, 183, 45, 227, 155, 135,
			37, 243, 25, 43, 68, 194, 54, 131, 205, 254,
			118, 193, 50, 233, 180, 29, 241, 114, 192, 180,
			157, 162, 140, 69, 105, 112, 107, 96, 120, 192,
			219, 142, 47, 11, 171, 209, 62, 83, 63, 151,
			78, 78, 76, 39, 43, 52, 51, 85, 208, 39,
			186, 163, 186, 199, 75, 99, 233, 91, 167, 58,
			139, 100, 134, 177, 152, 177, 131, 140, 232, 10,
			59, 40, 62, 42, 150, 168
		};

		private static uint s_publicExponent = 65537u;

		private static byte[] s_hashDER = new byte[19]
		{
			48, 49, 48, 13, 6, 9, 96, 134, 72, 1,
			101, 3, 4, 2, 1, 5, 0, 4, 32
		};

		private static int s_reservedLength = s_magic.Length + s_modulus.Length;

		private SHA256 m_hash = SHA256.Create();

		private byte[] m_reserved = new byte[s_reservedLength];

		private int m_reservedCount;

		private static bool AreByteArraysEqual(byte[] left, int leftOffset, byte[] right, int rightOffset, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (left[leftOffset + i] != right[rightOffset + i])
				{
					return false;
				}
			}
			return true;
		}

		private static bool AreByteArraysEqual(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			return AreByteArraysEqual(left, 0, right, 0, left.Length);
		}

		private static byte[] MakePKCS1Padding(byte[] hash)
		{
			byte[] array = new byte[s_modulus.Length];
			array[0] = 0;
			array[1] = 1;
			int num = s_modulus.Length;
			num -= hash.Length;
			Array.Copy(hash, 0, array, num, hash.Length);
			num -= s_hashDER.Length;
			Array.Copy(s_hashDER, 0, array, num, s_hashDER.Length);
			num--;
			array[num] = 0;
			while (num > 2)
			{
				num--;
				array[num] = byte.MaxValue;
			}
			Array.Reverse(array);
			return array;
		}

		private static BigInteger ByteArrayToBigInteger(byte[] data, int offset, int length)
		{
			byte[] array = new byte[checked(length + 1)];
			Array.Copy(data, offset, array, 0, length);
			array[length] = 0;
			Array.Reverse(array);
			return new BigInteger(array);
		}

		private static BigInteger ByteArrayToBigInteger(byte[] data)
		{
			if (data.Length == 0)
			{
				return new BigInteger(0L);
			}
			if (data[data.Length - 1] >= 128)
			{
				return ByteArrayToBigInteger(data, 0, data.Length);
			}
			return new BigInteger(data);
		}

		private static bool RSAEncrypt(byte[] data, int offset, int length, out BigInteger output)
		{
			output = null;
			if (length != s_modulus.Length)
			{
				return false;
			}
			BigInteger bigInteger = ByteArrayToBigInteger(s_modulus);
			BigInteger exp = new BigInteger(s_publicExponent);
			BigInteger bigInteger2 = ByteArrayToBigInteger(data, offset, length);
			if (bigInteger2 < 0)
			{
				return false;
			}
			if (bigInteger2 >= bigInteger)
			{
				return false;
			}
			output = BigInteger.PowMod(bigInteger2, exp, bigInteger);
			return true;
		}

		public void Process(byte[] data, int offset, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (m_reservedCount < s_reservedLength)
			{
				int num = s_reservedLength - m_reservedCount;
				if (num > length)
				{
					num = length;
				}
				Array.Copy(data, offset, m_reserved, m_reservedCount, num);
				m_reservedCount += num;
				offset += num;
				length -= num;
			}
			if (length != 0)
			{
				if (length >= s_reservedLength)
				{
					m_hash.TransformBlock(m_reserved, 0, s_reservedLength, m_reserved, 0);
					m_hash.TransformBlock(data, offset, length - s_reservedLength, data, offset);
					Array.Copy(data, offset + (length - s_reservedLength), m_reserved, 0, s_reservedLength);
				}
				else
				{
					m_hash.TransformBlock(m_reserved, 0, length, m_reserved, 0);
					Array.Copy(m_reserved, length, m_reserved, 0, s_reservedLength - length);
					Array.Copy(data, offset, m_reserved, s_reservedLength - length, length);
				}
			}
		}

		public bool Finish(string tag)
		{
			if (m_reservedCount < s_reservedLength)
			{
				return false;
			}
			if (!AreByteArraysEqual(m_reserved, 0, s_magic, 0, s_magic.Length))
			{
				return false;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(tag);
			m_hash.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = MakePKCS1Padding(m_hash.Hash);
			BigInteger output = null;
			if (!RSAEncrypt(m_reserved, s_magic.Length, m_reserved.Length - s_magic.Length, out output))
			{
				return false;
			}
			byte[] array2 = new byte[s_magic.Length + array.Length];
			Array.Copy(s_magic, array2, s_magic.Length);
			Array.Copy(array, array2, array.Length);
			Array.Reverse(array2);
			return new BigInteger(array2).CompareTo(output) == 0;
		}

		public static bool VerifyStreamSignature(Stream stream, string tag, int bufferSize = 16384)
		{
			Verifier verifier = new Verifier();
			byte[] array = new byte[bufferSize];
			int num;
			do
			{
				num = stream.Read(array, 0, array.Length);
				verifier.Process(array, 0, num);
			}
			while (num > 0);
			return verifier.Finish(tag);
		}
	}
}
