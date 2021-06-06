using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace bgs
{
	// Token: 0x02000218 RID: 536
	public class BigInteger
	{
		// Token: 0x06002279 RID: 8825 RVA: 0x000792B8 File Offset: 0x000774B8
		public BigInteger()
		{
			this.m_digits = new DigitsArray(1, 1);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000792D0 File Offset: 0x000774D0
		public BigInteger(long number)
		{
			this.m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0L && this.m_digits.DataUsed < this.m_digits.Count)
			{
				this.m_digits[this.m_digits.DataUsed] = (uint)(number & (long)((ulong)DigitsArray.AllBits));
				number >>= DigitsArray.DataSizeBits;
				DigitsArray digits = this.m_digits;
				int dataUsed = digits.DataUsed;
				digits.DataUsed = dataUsed + 1;
			}
			this.m_digits.ResetDataUsed();
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00079360 File Offset: 0x00077560
		public BigInteger(ulong number)
		{
			this.m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0UL && this.m_digits.DataUsed < this.m_digits.Count)
			{
				this.m_digits[this.m_digits.DataUsed] = (uint)(number & (ulong)DigitsArray.AllBits);
				number >>= DigitsArray.DataSizeBits;
				DigitsArray digits = this.m_digits;
				int dataUsed = digits.DataUsed;
				digits.DataUsed = dataUsed + 1;
			}
			this.m_digits.ResetDataUsed();
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000793F0 File Offset: 0x000775F0
		public BigInteger(byte[] array)
		{
			this.ConstructFrom(array, 0, array.Length);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00079403 File Offset: 0x00077603
		public BigInteger(byte[] array, int length)
		{
			this.ConstructFrom(array, 0, length);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00079414 File Offset: 0x00077614
		public BigInteger(byte[] array, int offset, int length)
		{
			this.ConstructFrom(array, offset, length);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00079428 File Offset: 0x00077628
		private void ConstructFrom(byte[] array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset > array.Length || length > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length > array.Length || offset + length > array.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = length / 4;
			int num2 = length & 3;
			if (num2 != 0)
			{
				num++;
			}
			this.m_digits = new DigitsArray(num + 1, 0);
			int num3 = offset + length - 1;
			int num4 = 0;
			while (num3 - offset >= 3)
			{
				this.m_digits[num4] = (uint)(((int)array[num3 - 3] << 24) + ((int)array[num3 - 2] << 16) + ((int)array[num3 - 1] << 8) + (int)array[num3]);
				DigitsArray digits = this.m_digits;
				int dataUsed = digits.DataUsed;
				digits.DataUsed = dataUsed + 1;
				num3 -= 4;
				num4++;
			}
			uint num5 = 0U;
			for (int i = num2; i > 0; i--)
			{
				uint num6 = (uint)array[offset + num2 - i];
				num6 <<= (i - 1) * 8;
				num5 |= num6;
			}
			this.m_digits[this.m_digits.DataUsed] = num5;
			this.m_digits.ResetDataUsed();
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0007953E File Offset: 0x0007773E
		public BigInteger(string digits)
		{
			this.Construct(digits, 10);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0007954F File Offset: 0x0007774F
		public BigInteger(string digits, int radix)
		{
			this.Construct(digits, radix);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00079560 File Offset: 0x00077760
		private void Construct(string digits, int radix)
		{
			if (digits == null)
			{
				throw new ArgumentNullException("digits");
			}
			BigInteger leftSide = new BigInteger(1L);
			BigInteger bigInteger = new BigInteger();
			digits = digits.ToUpper(CultureInfo.CurrentCulture).Trim();
			int num = (digits[0] == '-') ? 1 : 0;
			for (int i = digits.Length - 1; i >= num; i--)
			{
				int num2 = (int)digits[i];
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else
				{
					if (num2 < 65 || num2 > 90)
					{
						throw new ArgumentOutOfRangeException("digits");
					}
					num2 = num2 - 65 + 10;
				}
				if (num2 >= radix)
				{
					throw new ArgumentOutOfRangeException("digits");
				}
				bigInteger += leftSide * num2;
				leftSide *= radix;
			}
			if (digits[0] == '-')
			{
				bigInteger = -bigInteger;
			}
			this.m_digits = bigInteger.m_digits;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0007964E File Offset: 0x0007784E
		private BigInteger(DigitsArray digits)
		{
			digits.ResetDataUsed();
			this.m_digits = digits;
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00079663 File Offset: 0x00077863
		public bool IsNegative
		{
			get
			{
				return this.m_digits.IsNegative;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x00079670 File Offset: 0x00077870
		public bool IsZero
		{
			get
			{
				return this.m_digits.IsZero;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0007967D File Offset: 0x0007787D
		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00079685 File Offset: 0x00077885
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0007968D File Offset: 0x0007788D
		public static implicit operator BigInteger(int value)
		{
			return new BigInteger((long)value);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00079696 File Offset: 0x00077896
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger((ulong)value);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000796A0 File Offset: 0x000778A0
		public static BigInteger operator +(BigInteger leftSide, BigInteger rightSide)
		{
			DigitsArray digitsArray = new DigitsArray(Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed) + 1);
			long num = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num2 = (long)((ulong)leftSide.m_digits[i] + (ulong)rightSide.m_digits[i] + (ulong)num);
				num = num2 >> DigitsArray.DataSizeBits;
				digitsArray[i] = (uint)(num2 & (long)((ulong)DigitsArray.AllBits));
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00079723 File Offset: 0x00077923
		public static BigInteger Add(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0007972C File Offset: 0x0007792C
		public static BigInteger operator ++(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0007972C File Offset: 0x0007792C
		public static BigInteger Increment(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0007973C File Offset: 0x0007793C
		public static BigInteger operator -(BigInteger leftSide, BigInteger rightSide)
		{
			DigitsArray digitsArray = new DigitsArray(Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed) + 1);
			long num = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num2 = (long)((ulong)leftSide.m_digits[i] - (ulong)rightSide.m_digits[i] - (ulong)num);
				digitsArray[i] = (uint)(num2 & (long)((ulong)DigitsArray.AllBits));
				DigitsArray digitsArray2 = digitsArray;
				int dataUsed = digitsArray2.DataUsed;
				digitsArray2.DataUsed = dataUsed + 1;
				num = ((num2 < 0L) ? 1L : 0L);
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00079723 File Offset: 0x00077923
		public static BigInteger Subtract(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000797D1 File Offset: 0x000779D1
		public static BigInteger operator --(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000797D1 File Offset: 0x000779D1
		public static BigInteger Decrement(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000797E0 File Offset: 0x000779E0
		public static BigInteger operator -(BigInteger leftSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (leftSide.IsZero)
			{
				return new BigInteger(0L);
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.DataUsed + 1, leftSide.m_digits.DataUsed + 1);
			for (int i = 0; i < digitsArray.Count; i++)
			{
				digitsArray[i] = ~leftSide.m_digits[i];
			}
			bool flag = true;
			int num = 0;
			while (flag && num < digitsArray.Count)
			{
				long num2 = (long)((ulong)digitsArray[num] + 1UL);
				digitsArray[num] = (uint)(num2 & (long)((ulong)DigitsArray.AllBits));
				flag = (num2 >> DigitsArray.DataSizeBits > 0L);
				num++;
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007989A File Offset: 0x00077A9A
		public BigInteger Negate()
		{
			return -this;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000798A2 File Offset: 0x00077AA2
		public static BigInteger Abs(BigInteger leftSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (leftSide.IsNegative)
			{
				return -leftSide;
			}
			return leftSide;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000798C4 File Offset: 0x00077AC4
		public static BigInteger PowMod(BigInteger b, BigInteger exp, BigInteger mod)
		{
			BigInteger bigInteger = new BigInteger(1L);
			b %= mod;
			while (exp > 0)
			{
				if ((exp % 2).CompareTo(1) == 0)
				{
					bigInteger = bigInteger * b % mod;
				}
				exp >>= 1;
				b = b * b % mod;
			}
			return bigInteger;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00079934 File Offset: 0x00077B34
		public static BigInteger operator *(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			bool isNegative = leftSide.IsNegative;
			bool isNegative2 = rightSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.DataUsed + rightSide.m_digits.DataUsed);
			digitsArray.DataUsed = digitsArray.Count;
			for (int i = 0; i < leftSide.m_digits.DataUsed; i++)
			{
				ulong num = 0UL;
				int j = 0;
				int num2 = i;
				while (j < rightSide.m_digits.DataUsed)
				{
					ulong num3 = (ulong)leftSide.m_digits[i] * (ulong)rightSide.m_digits[j] + (ulong)digitsArray[num2] + num;
					digitsArray[num2] = (uint)(num3 & (ulong)DigitsArray.AllBits);
					num = num3 >> DigitsArray.DataSizeBits;
					j++;
					num2++;
				}
				if (num != 0UL)
				{
					digitsArray[i + rightSide.m_digits.DataUsed] = (uint)num;
				}
			}
			BigInteger bigInteger = new BigInteger(digitsArray);
			if (isNegative == isNegative2)
			{
				return bigInteger;
			}
			return -bigInteger;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00079A65 File Offset: 0x00077C65
		public static BigInteger Multiply(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide * rightSide;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00079A70 File Offset: 0x00077C70
		public static BigInteger operator /(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			bool isNegative = rightSide.IsNegative;
			bool isNegative2 = leftSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			if (leftSide < rightSide)
			{
				return new BigInteger(0L);
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger.Divide(leftSide, rightSide, out bigInteger, out bigInteger2);
			if (isNegative2 == isNegative)
			{
				return bigInteger;
			}
			return -bigInteger;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00079AF9 File Offset: 0x00077CF9
		public static BigInteger Divide(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide / rightSide;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x00079B02 File Offset: 0x00077D02
		private static void Divide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (leftSide.IsZero)
			{
				quotient = new BigInteger();
				remainder = new BigInteger();
				return;
			}
			if (rightSide.m_digits.DataUsed == 1)
			{
				BigInteger.SingleDivide(leftSide, rightSide, out quotient, out remainder);
				return;
			}
			BigInteger.MultiDivide(leftSide, rightSide, out quotient, out remainder);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00079B3C File Offset: 0x00077D3C
		private static void MultiDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			uint num = rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			int num2 = 0;
			uint num3 = DigitsArray.HiBitSet;
			while (num3 != 0U && (num & num3) == 0U)
			{
				num2++;
				num3 >>= 1;
			}
			int num4 = leftSide.m_digits.DataUsed + 1;
			uint[] array = new uint[num4];
			leftSide.m_digits.CopyTo(array, 0, leftSide.m_digits.DataUsed);
			DigitsArray.ShiftLeft(array, num2);
			rightSide <<= num2;
			ulong num5 = (ulong)rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			ulong num6 = (ulong)((rightSide.m_digits.DataUsed < 2) ? 0U : rightSide.m_digits[rightSide.m_digits.DataUsed - 2]);
			int num7 = rightSide.m_digits.DataUsed + 1;
			DigitsArray digitsArray = new DigitsArray(num7, num7);
			uint[] array2 = new uint[leftSide.m_digits.Count + 1];
			int length = 0;
			ulong num8 = 1UL << DigitsArray.DataSizeBits;
			int i = num4 - rightSide.m_digits.DataUsed;
			int num9 = num4 - 1;
			while (i > 0)
			{
				ulong num10 = ((ulong)array[num9] << DigitsArray.DataSizeBits) + (ulong)array[num9 - 1];
				ulong num11 = num10 / num5;
				ulong num12 = num10 % num5;
				while (num9 >= 2 && (num11 == num8 || num11 * num6 > (num12 << DigitsArray.DataSizeBits) + (ulong)array[num9 - 2]))
				{
					num11 -= 1UL;
					num12 += num5;
					if (num12 >= num8)
					{
						break;
					}
				}
				for (int j = 0; j < num7; j++)
				{
					digitsArray[num7 - j - 1] = array[num9 - j];
				}
				BigInteger bigInteger = new BigInteger(digitsArray);
				BigInteger bigInteger2 = rightSide * (long)num11;
				while (bigInteger2 > bigInteger)
				{
					num11 -= 1UL;
					bigInteger2 -= rightSide;
				}
				bigInteger2 = bigInteger - bigInteger2;
				for (int k = 0; k < num7; k++)
				{
					array[num9 - k] = bigInteger2.m_digits[rightSide.m_digits.DataUsed - k];
				}
				array2[length++] = (uint)num11;
				i--;
				num9--;
			}
			Array.Reverse(array2, 0, length);
			quotient = new BigInteger(new DigitsArray(array2));
			int num13 = DigitsArray.ShiftRight(array, num2);
			DigitsArray digitsArray2 = new DigitsArray(num13, num13);
			digitsArray2.CopyFrom(array, 0, 0, digitsArray2.DataUsed);
			remainder = new BigInteger(digitsArray2);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00079DBC File Offset: 0x00077FBC
		private static void SingleDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.ResetDataUsed();
			int i = digitsArray.DataUsed - 1;
			ulong num = (ulong)rightSide.m_digits[0];
			ulong num2 = (ulong)digitsArray[i];
			uint[] array = new uint[leftSide.m_digits.Count];
			leftSide.m_digits.CopyTo(array, 0, array.Length);
			int num3 = 0;
			if (num2 >= num)
			{
				array[num3++] = (uint)(num2 / num);
				digitsArray[i] = (uint)(num2 % num);
			}
			i--;
			while (i >= 0)
			{
				num2 = ((ulong)digitsArray[i + 1] << DigitsArray.DataSizeBits) + (ulong)digitsArray[i];
				array[num3++] = (uint)(num2 / num);
				digitsArray[i + 1] = 0U;
				digitsArray[i--] = (uint)(num2 % num);
			}
			remainder = new BigInteger(digitsArray);
			DigitsArray digitsArray2 = new DigitsArray(num3 + 1, num3);
			int num4 = 0;
			int j = digitsArray2.DataUsed - 1;
			while (j >= 0)
			{
				digitsArray2[num4] = array[j];
				j--;
				num4++;
			}
			quotient = new BigInteger(digitsArray2);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00079EE8 File Offset: 0x000780E8
		public static BigInteger operator %(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			bool isNegative = leftSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			if (leftSide < rightSide)
			{
				return leftSide;
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger.Divide(leftSide, rightSide, out bigInteger, out bigInteger2);
			if (!isNegative)
			{
				return bigInteger2;
			}
			return -bigInteger2;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00079F63 File Offset: 0x00078163
		public static BigInteger Modulus(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide % rightSide;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00079F6C File Offset: 0x0007816C
		public static BigInteger operator &(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] & rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00079FCF File Offset: 0x000781CF
		public static BigInteger BitwiseAnd(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide & rightSide;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00079FD8 File Offset: 0x000781D8
		public static BigInteger operator |(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] | rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0007A03B File Offset: 0x0007823B
		public static BigInteger BitwiseOr(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide | rightSide;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0007A044 File Offset: 0x00078244
		public static BigInteger operator ^(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] ^ rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0007A0A7 File Offset: 0x000782A7
		public static BigInteger Xor(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide ^ rightSide;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007A0B0 File Offset: 0x000782B0
		public static BigInteger operator ~(BigInteger leftSide)
		{
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.Count);
			for (int i = 0; i < digitsArray.Count; i++)
			{
				digitsArray[i] = ~leftSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007A0F9 File Offset: 0x000782F9
		public static BigInteger OnesComplement(BigInteger leftSide)
		{
			return ~leftSide;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007A101 File Offset: 0x00078301
		public static BigInteger operator <<(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.DataUsed = digitsArray.ShiftLeftWithoutOverflow(shiftCount);
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007A134 File Offset: 0x00078334
		public static BigInteger LeftShift(BigInteger leftSide, int shiftCount)
		{
			return leftSide << shiftCount;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007A140 File Offset: 0x00078340
		public static BigInteger operator >>(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.DataUsed = digitsArray.ShiftRight(shiftCount);
			if (leftSide.IsNegative)
			{
				for (int i = digitsArray.Count - 1; i >= digitsArray.DataUsed; i--)
				{
					digitsArray[i] = DigitsArray.AllBits;
				}
				uint num = DigitsArray.HiBitSet;
				int num2 = 0;
				while (num2 < DigitsArray.DataSizeBits && (digitsArray[digitsArray.DataUsed - 1] & num) != DigitsArray.HiBitSet)
				{
					DigitsArray digitsArray2 = digitsArray;
					int index = digitsArray.DataUsed - 1;
					digitsArray2[index] |= num;
					num >>= 1;
					num2++;
				}
				digitsArray.DataUsed = digitsArray.Count;
			}
			return new BigInteger(digitsArray);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0007A20D File Offset: 0x0007840D
		public static BigInteger RightShift(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			return leftSide >> shiftCount;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007A22A File Offset: 0x0007842A
		public int CompareTo(BigInteger value)
		{
			return BigInteger.Compare(this, value);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0007A233 File Offset: 0x00078433
		public static int Compare(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == rightSide)
			{
				return 0;
			}
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide > rightSide)
			{
				return 1;
			}
			if (leftSide == rightSide)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007A26E File Offset: 0x0007846E
		public static bool operator ==(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide == rightSide || (leftSide != null && rightSide != null && leftSide.IsNegative == rightSide.IsNegative && leftSide.Equals(rightSide));
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0007A295 File Offset: 0x00078495
		public static bool operator !=(BigInteger leftSide, BigInteger rightSide)
		{
			return !(leftSide == rightSide);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007A2A4 File Offset: 0x000784A4
		public static bool operator >(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide.IsNegative != rightSide.IsNegative)
			{
				return rightSide.IsNegative;
			}
			if (leftSide.m_digits.DataUsed != rightSide.m_digits.DataUsed)
			{
				return leftSide.m_digits.DataUsed > rightSide.m_digits.DataUsed;
			}
			for (int i = leftSide.m_digits.DataUsed - 1; i >= 0; i--)
			{
				if (leftSide.m_digits[i] != rightSide.m_digits[i])
				{
					return leftSide.m_digits[i] > rightSide.m_digits[i];
				}
			}
			return false;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007A364 File Offset: 0x00078564
		public static bool operator <(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide.IsNegative != rightSide.IsNegative)
			{
				return leftSide.IsNegative;
			}
			if (leftSide.m_digits.DataUsed != rightSide.m_digits.DataUsed)
			{
				return leftSide.m_digits.DataUsed < rightSide.m_digits.DataUsed;
			}
			for (int i = leftSide.m_digits.DataUsed - 1; i >= 0; i--)
			{
				if (leftSide.m_digits[i] != rightSide.m_digits[i])
				{
					return leftSide.m_digits[i] < rightSide.m_digits[i];
				}
			}
			return false;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007A421 File Offset: 0x00078621
		public static bool operator >=(BigInteger leftSide, BigInteger rightSide)
		{
			return BigInteger.Compare(leftSide, rightSide) >= 0;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007A430 File Offset: 0x00078630
		public static bool operator <=(BigInteger leftSide, BigInteger rightSide)
		{
			return BigInteger.Compare(leftSide, rightSide) <= 0;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0007A440 File Offset: 0x00078640
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			BigInteger bigInteger = (BigInteger)obj;
			if (this.m_digits.DataUsed != bigInteger.m_digits.DataUsed)
			{
				return false;
			}
			for (int i = 0; i < this.m_digits.DataUsed; i++)
			{
				if (this.m_digits[i] != bigInteger.m_digits[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0007A4AC File Offset: 0x000786AC
		public override int GetHashCode()
		{
			return this.m_digits.GetHashCode();
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007A4B9 File Offset: 0x000786B9
		public override string ToString()
		{
			return this.ToString(10);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007A4C4 File Offset: 0x000786C4
		public string ToString(int radix)
		{
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentOutOfRangeException("radix");
			}
			if (this.IsZero)
			{
				return "0";
			}
			bool isNegative = this.IsNegative;
			BigInteger bigInteger = BigInteger.Abs(this);
			BigInteger rightSide = new BigInteger((long)radix);
			ArrayList arrayList = new ArrayList();
			while (bigInteger.m_digits.DataUsed > 1 || (bigInteger.m_digits.DataUsed == 1 && bigInteger.m_digits[0] != 0U))
			{
				BigInteger bigInteger2;
				BigInteger bigInteger3;
				BigInteger.Divide(bigInteger, rightSide, out bigInteger2, out bigInteger3);
				arrayList.Insert(0, "0123456789abcdefghijklmnopqrstuvwxyz"[(int)bigInteger3.m_digits[0]]);
				bigInteger = bigInteger2;
			}
			string text = new string((char[])arrayList.ToArray(typeof(char)));
			if (radix == 10 && isNegative)
			{
				return "-" + text;
			}
			return text;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007A5A8 File Offset: 0x000787A8
		public string ToHexString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0:X}", this.m_digits[this.m_digits.DataUsed - 1]);
			string format = "{0:X" + 2 * DigitsArray.DataSizeOf + "}";
			for (int i = this.m_digits.DataUsed - 2; i >= 0; i--)
			{
				stringBuilder.AppendFormat(format, this.m_digits[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007A637 File Offset: 0x00078837
		public static int ToInt16(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (int)short.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007A658 File Offset: 0x00078858
		public static uint ToUInt16(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (uint)ushort.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007A679 File Offset: 0x00078879
		public static int ToInt32(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return int.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007A69A File Offset: 0x0007889A
		public static uint ToUInt32(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return uint.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007A6BB File Offset: 0x000788BB
		public static long ToInt64(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return long.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007A6DC File Offset: 0x000788DC
		public static ulong ToUInt64(BigInteger value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return ulong.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		// Token: 0x04000E4A RID: 3658
		private DigitsArray m_digits;
	}
}
