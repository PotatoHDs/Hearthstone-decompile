using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using bgs;

namespace VerifyScanSignature
{
	// Token: 0x02000B48 RID: 2888
	public class Verifier
	{
		// Token: 0x060098FF RID: 39167 RVA: 0x00317D80 File Offset: 0x00315F80
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

		// Token: 0x06009900 RID: 39168 RVA: 0x00317DA9 File Offset: 0x00315FA9
		private static bool AreByteArraysEqual(byte[] left, byte[] right)
		{
			return left.Length == right.Length && Verifier.AreByteArraysEqual(left, 0, right, 0, left.Length);
		}

		// Token: 0x06009901 RID: 39169 RVA: 0x00317DC4 File Offset: 0x00315FC4
		private static byte[] MakePKCS1Padding(byte[] hash)
		{
			byte[] array = new byte[Verifier.s_modulus.Length];
			array[0] = 0;
			array[1] = 1;
			int i = Verifier.s_modulus.Length;
			i -= hash.Length;
			Array.Copy(hash, 0, array, i, hash.Length);
			i -= Verifier.s_hashDER.Length;
			Array.Copy(Verifier.s_hashDER, 0, array, i, Verifier.s_hashDER.Length);
			i--;
			array[i] = 0;
			while (i > 2)
			{
				i--;
				array[i] = byte.MaxValue;
			}
			Array.Reverse(array);
			return array;
		}

		// Token: 0x06009902 RID: 39170 RVA: 0x00317E40 File Offset: 0x00316040
		private static BigInteger ByteArrayToBigInteger(byte[] data, int offset, int length)
		{
			byte[] array = new byte[checked(length + 1)];
			Array.Copy(data, offset, array, 0, length);
			array[length] = 0;
			Array.Reverse(array);
			return new BigInteger(array);
		}

		// Token: 0x06009903 RID: 39171 RVA: 0x00317E70 File Offset: 0x00316070
		private static BigInteger ByteArrayToBigInteger(byte[] data)
		{
			if (data.Length == 0)
			{
				return new BigInteger(0L);
			}
			if (data[data.Length - 1] >= 128)
			{
				return Verifier.ByteArrayToBigInteger(data, 0, data.Length);
			}
			return new BigInteger(data);
		}

		// Token: 0x06009904 RID: 39172 RVA: 0x00317EA0 File Offset: 0x003160A0
		private static bool RSAEncrypt(byte[] data, int offset, int length, out BigInteger output)
		{
			output = null;
			if (length != Verifier.s_modulus.Length)
			{
				return false;
			}
			BigInteger bigInteger = Verifier.ByteArrayToBigInteger(Verifier.s_modulus);
			BigInteger exp = new BigInteger((long)((ulong)Verifier.s_publicExponent));
			BigInteger bigInteger2 = Verifier.ByteArrayToBigInteger(data, offset, length);
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

		// Token: 0x06009906 RID: 39174 RVA: 0x00317F28 File Offset: 0x00316128
		public void Process(byte[] data, int offset, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (this.m_reservedCount < Verifier.s_reservedLength)
			{
				int num = Verifier.s_reservedLength - this.m_reservedCount;
				if (num > length)
				{
					num = length;
				}
				Array.Copy(data, offset, this.m_reserved, this.m_reservedCount, num);
				this.m_reservedCount += num;
				offset += num;
				length -= num;
			}
			if (length == 0)
			{
				return;
			}
			if (length >= Verifier.s_reservedLength)
			{
				this.m_hash.TransformBlock(this.m_reserved, 0, Verifier.s_reservedLength, this.m_reserved, 0);
				this.m_hash.TransformBlock(data, offset, length - Verifier.s_reservedLength, data, offset);
				Array.Copy(data, offset + (length - Verifier.s_reservedLength), this.m_reserved, 0, Verifier.s_reservedLength);
				return;
			}
			this.m_hash.TransformBlock(this.m_reserved, 0, length, this.m_reserved, 0);
			Array.Copy(this.m_reserved, length, this.m_reserved, 0, Verifier.s_reservedLength - length);
			Array.Copy(data, offset, this.m_reserved, Verifier.s_reservedLength - length, length);
		}

		// Token: 0x06009907 RID: 39175 RVA: 0x00318030 File Offset: 0x00316230
		public bool Finish(string tag)
		{
			if (this.m_reservedCount < Verifier.s_reservedLength)
			{
				return false;
			}
			if (!Verifier.AreByteArraysEqual(this.m_reserved, 0, Verifier.s_magic, 0, Verifier.s_magic.Length))
			{
				return false;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(tag);
			this.m_hash.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = Verifier.MakePKCS1Padding(this.m_hash.Hash);
			BigInteger value = null;
			if (!Verifier.RSAEncrypt(this.m_reserved, Verifier.s_magic.Length, this.m_reserved.Length - Verifier.s_magic.Length, out value))
			{
				return false;
			}
			byte[] array2 = new byte[Verifier.s_magic.Length + array.Length];
			Array.Copy(Verifier.s_magic, array2, Verifier.s_magic.Length);
			Array.Copy(array, array2, array.Length);
			Array.Reverse(array2);
			return new BigInteger(array2).CompareTo(value) == 0;
		}

		// Token: 0x06009908 RID: 39176 RVA: 0x00318104 File Offset: 0x00316304
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

		// Token: 0x04007FF6 RID: 32758
		private static byte[] s_magic = Encoding.UTF8.GetBytes("NGIS");

		// Token: 0x04007FF7 RID: 32759
		private static byte[] s_modulus = new byte[]
		{
			71,
			147,
			189,
			90,
			135,
			248,
			118,
			158,
			9,
			35,
			81,
			227,
			220,
			190,
			70,
			67,
			131,
			87,
			138,
			136,
			32,
			95,
			228,
			80,
			60,
			61,
			203,
			23,
			67,
			213,
			44,
			40,
			101,
			167,
			34,
			67,
			50,
			88,
			7,
			55,
			202,
			163,
			63,
			214,
			194,
			117,
			161,
			34,
			91,
			196,
			172,
			134,
			21,
			98,
			196,
			41,
			156,
			246,
			223,
			49,
			186,
			224,
			19,
			41,
			73,
			44,
			186,
			3,
			227,
			167,
			254,
			36,
			60,
			183,
			180,
			102,
			225,
			65,
			184,
			159,
			132,
			9,
			209,
			125,
			62,
			9,
			248,
			172,
			44,
			88,
			247,
			75,
			144,
			147,
			59,
			207,
			190,
			37,
			227,
			163,
			49,
			252,
			46,
			114,
			51,
			214,
			24,
			224,
			228,
			215,
			91,
			168,
			117,
			41,
			170,
			140,
			185,
			72,
			152,
			206,
			153,
			90,
			158,
			222,
			139,
			166,
			82,
			91,
			28,
			4,
			231,
			28,
			98,
			37,
			167,
			66,
			13,
			32,
			41,
			233,
			79,
			235,
			215,
			29,
			14,
			86,
			107,
			118,
			30,
			161,
			7,
			153,
			56,
			252,
			114,
			50,
			103,
			106,
			93,
			148,
			5,
			85,
			70,
			93,
			125,
			56,
			77,
			142,
			82,
			48,
			180,
			15,
			74,
			94,
			78,
			183,
			45,
			227,
			155,
			135,
			37,
			243,
			25,
			43,
			68,
			194,
			54,
			131,
			205,
			254,
			118,
			193,
			50,
			233,
			180,
			29,
			241,
			114,
			192,
			180,
			157,
			162,
			140,
			69,
			105,
			112,
			107,
			96,
			120,
			192,
			219,
			142,
			47,
			11,
			171,
			209,
			62,
			83,
			63,
			151,
			78,
			78,
			76,
			39,
			43,
			52,
			51,
			85,
			208,
			39,
			186,
			163,
			186,
			199,
			75,
			99,
			233,
			91,
			167,
			58,
			139,
			100,
			134,
			177,
			152,
			177,
			131,
			140,
			232,
			10,
			59,
			40,
			62,
			42,
			150,
			168
		};

		// Token: 0x04007FF8 RID: 32760
		private static uint s_publicExponent = 65537U;

		// Token: 0x04007FF9 RID: 32761
		private static byte[] s_hashDER = new byte[]
		{
			48,
			49,
			48,
			13,
			6,
			9,
			96,
			134,
			72,
			1,
			101,
			3,
			4,
			2,
			1,
			5,
			0,
			4,
			32
		};

		// Token: 0x04007FFA RID: 32762
		private static int s_reservedLength = Verifier.s_magic.Length + Verifier.s_modulus.Length;

		// Token: 0x04007FFB RID: 32763
		private SHA256 m_hash = SHA256.Create();

		// Token: 0x04007FFC RID: 32764
		private byte[] m_reserved = new byte[Verifier.s_reservedLength];

		// Token: 0x04007FFD RID: 32765
		private int m_reservedCount;
	}
}
