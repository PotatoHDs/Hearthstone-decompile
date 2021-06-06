using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace bgs
{
	public class BigInteger
	{
		private DigitsArray m_digits;

		public bool IsNegative => m_digits.IsNegative;

		public bool IsZero => m_digits.IsZero;

		public BigInteger()
		{
			m_digits = new DigitsArray(1, 1);
		}

		public BigInteger(long number)
		{
			m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0L && m_digits.DataUsed < m_digits.Count)
			{
				m_digits[m_digits.DataUsed] = (uint)(number & DigitsArray.AllBits);
				number >>= DigitsArray.DataSizeBits;
				m_digits.DataUsed++;
			}
			m_digits.ResetDataUsed();
		}

		public BigInteger(ulong number)
		{
			m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0L && m_digits.DataUsed < m_digits.Count)
			{
				m_digits[m_digits.DataUsed] = (uint)(number & DigitsArray.AllBits);
				number >>= DigitsArray.DataSizeBits;
				m_digits.DataUsed++;
			}
			m_digits.ResetDataUsed();
		}

		public BigInteger(byte[] array)
		{
			ConstructFrom(array, 0, array.Length);
		}

		public BigInteger(byte[] array, int length)
		{
			ConstructFrom(array, 0, length);
		}

		public BigInteger(byte[] array, int offset, int length)
		{
			ConstructFrom(array, offset, length);
		}

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
			m_digits = new DigitsArray(num + 1, 0);
			int num3 = offset + length - 1;
			int num4 = 0;
			while (num3 - offset >= 3)
			{
				m_digits[num4] = (uint)((array[num3 - 3] << 24) + (array[num3 - 2] << 16) + (array[num3 - 1] << 8) + array[num3]);
				m_digits.DataUsed++;
				num3 -= 4;
				num4++;
			}
			uint num5 = 0u;
			for (int num6 = num2; num6 > 0; num6--)
			{
				uint num7 = array[offset + num2 - num6];
				num7 <<= (num6 - 1) * 8;
				num5 |= num7;
			}
			m_digits[m_digits.DataUsed] = num5;
			m_digits.ResetDataUsed();
		}

		public BigInteger(string digits)
		{
			Construct(digits, 10);
		}

		public BigInteger(string digits, int radix)
		{
			Construct(digits, radix);
		}

		private void Construct(string digits, int radix)
		{
			if (digits == null)
			{
				throw new ArgumentNullException("digits");
			}
			BigInteger bigInteger = new BigInteger(1L);
			BigInteger bigInteger2 = new BigInteger();
			digits = digits.ToUpper(CultureInfo.CurrentCulture).Trim();
			int num = ((digits[0] == '-') ? 1 : 0);
			for (int num2 = digits.Length - 1; num2 >= num; num2--)
			{
				int num3 = digits[num2];
				if (num3 >= 48 && num3 <= 57)
				{
					num3 -= 48;
				}
				else
				{
					if (num3 < 65 || num3 > 90)
					{
						throw new ArgumentOutOfRangeException("digits");
					}
					num3 = num3 - 65 + 10;
				}
				if (num3 >= radix)
				{
					throw new ArgumentOutOfRangeException("digits");
				}
				bigInteger2 += bigInteger * num3;
				bigInteger *= (BigInteger)radix;
			}
			if (digits[0] == '-')
			{
				bigInteger2 = -bigInteger2;
			}
			m_digits = bigInteger2.m_digits;
		}

		private BigInteger(DigitsArray digits)
		{
			digits.ResetDataUsed();
			m_digits = digits;
		}

		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(int value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger((ulong)value);
		}

		public static BigInteger operator +(BigInteger leftSide, BigInteger rightSide)
		{
			DigitsArray digitsArray = new DigitsArray(Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed) + 1);
			long num = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num2 = (long)leftSide.m_digits[i] + (long)rightSide.m_digits[i] + num;
				num = num2 >> DigitsArray.DataSizeBits;
				digitsArray[i] = (uint)(num2 & DigitsArray.AllBits);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Add(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		public static BigInteger operator ++(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		public static BigInteger Increment(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		public static BigInteger operator -(BigInteger leftSide, BigInteger rightSide)
		{
			DigitsArray digitsArray = new DigitsArray(Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed) + 1);
			long num = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num2 = (long)leftSide.m_digits[i] - (long)rightSide.m_digits[i] - num;
				digitsArray[i] = (uint)(num2 & DigitsArray.AllBits);
				digitsArray.DataUsed++;
				num = ((num2 < 0) ? 1 : 0);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Subtract(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		public static BigInteger operator --(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		public static BigInteger Decrement(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		public static BigInteger operator -(BigInteger leftSide)
		{
			if ((object)leftSide == null)
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
				long num2 = (long)digitsArray[num] + 1L;
				digitsArray[num] = (uint)(num2 & DigitsArray.AllBits);
				flag = num2 >> DigitsArray.DataSizeBits > 0;
				num++;
			}
			return new BigInteger(digitsArray);
		}

		public BigInteger Negate()
		{
			return -this;
		}

		public static BigInteger Abs(BigInteger leftSide)
		{
			if ((object)leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (leftSide.IsNegative)
			{
				return -leftSide;
			}
			return leftSide;
		}

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

		public static BigInteger operator *(BigInteger leftSide, BigInteger rightSide)
		{
			if ((object)leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if ((object)rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			bool isNegative = leftSide.IsNegative;
			bool isNegative2 = rightSide.IsNegative;
			leftSide = Abs(leftSide);
			rightSide = Abs(rightSide);
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.DataUsed + rightSide.m_digits.DataUsed);
			digitsArray.DataUsed = digitsArray.Count;
			for (int i = 0; i < leftSide.m_digits.DataUsed; i++)
			{
				ulong num = 0uL;
				int num2 = 0;
				int num3 = i;
				while (num2 < rightSide.m_digits.DataUsed)
				{
					ulong num4 = (ulong)((long)leftSide.m_digits[i] * (long)rightSide.m_digits[num2] + digitsArray[num3]) + num;
					digitsArray[num3] = (uint)(num4 & DigitsArray.AllBits);
					num = num4 >> DigitsArray.DataSizeBits;
					num2++;
					num3++;
				}
				if (num != 0L)
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

		public static BigInteger Multiply(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide * rightSide;
		}

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
			leftSide = Abs(leftSide);
			rightSide = Abs(rightSide);
			if (leftSide < rightSide)
			{
				return new BigInteger(0L);
			}
			Divide(leftSide, rightSide, out var quotient, out var _);
			if (isNegative2 == isNegative)
			{
				return quotient;
			}
			return -quotient;
		}

		public static BigInteger Divide(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide / rightSide;
		}

		private static void Divide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (leftSide.IsZero)
			{
				quotient = new BigInteger();
				remainder = new BigInteger();
			}
			else if (rightSide.m_digits.DataUsed == 1)
			{
				SingleDivide(leftSide, rightSide, out quotient, out remainder);
			}
			else
			{
				MultiDivide(leftSide, rightSide, out quotient, out remainder);
			}
		}

		private static void MultiDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			uint num = rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			int num2 = 0;
			uint num3 = DigitsArray.HiBitSet;
			while (num3 != 0 && (num & num3) == 0)
			{
				num2++;
				num3 >>= 1;
			}
			int num4 = leftSide.m_digits.DataUsed + 1;
			uint[] array = new uint[num4];
			leftSide.m_digits.CopyTo(array, 0, leftSide.m_digits.DataUsed);
			DigitsArray.ShiftLeft(array, num2);
			rightSide <<= num2;
			ulong num5 = rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			ulong num6 = ((rightSide.m_digits.DataUsed >= 2) ? rightSide.m_digits[rightSide.m_digits.DataUsed - 2] : 0u);
			int num7 = rightSide.m_digits.DataUsed + 1;
			DigitsArray digitsArray = new DigitsArray(num7, num7);
			uint[] array2 = new uint[leftSide.m_digits.Count + 1];
			int length = 0;
			ulong num8 = (ulong)(1L << DigitsArray.DataSizeBits);
			int num9 = num4 - rightSide.m_digits.DataUsed;
			int num10 = num4 - 1;
			while (num9 > 0)
			{
				ulong num11 = ((ulong)array[num10] << DigitsArray.DataSizeBits) + array[num10 - 1];
				ulong num12 = num11 / num5;
				ulong num13 = num11 % num5;
				while (num10 >= 2 && (num12 == num8 || num12 * num6 > (num13 << DigitsArray.DataSizeBits) + array[num10 - 2]))
				{
					num12--;
					num13 += num5;
					if (num13 >= num8)
					{
						break;
					}
				}
				for (int i = 0; i < num7; i++)
				{
					digitsArray[num7 - i - 1] = array[num10 - i];
				}
				BigInteger bigInteger = new BigInteger(digitsArray);
				BigInteger bigInteger2;
				for (bigInteger2 = rightSide * (long)num12; bigInteger2 > bigInteger; bigInteger2 -= rightSide)
				{
					num12--;
				}
				bigInteger2 = bigInteger - bigInteger2;
				for (int j = 0; j < num7; j++)
				{
					array[num10 - j] = bigInteger2.m_digits[rightSide.m_digits.DataUsed - j];
				}
				array2[length++] = (uint)num12;
				num9--;
				num10--;
			}
			Array.Reverse(array2, 0, length);
			quotient = new BigInteger(new DigitsArray(array2));
			int num14 = DigitsArray.ShiftRight(array, num2);
			DigitsArray digitsArray2 = new DigitsArray(num14, num14);
			digitsArray2.CopyFrom(array, 0, 0, digitsArray2.DataUsed);
			remainder = new BigInteger(digitsArray2);
		}

		private static void SingleDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.ResetDataUsed();
			int num = digitsArray.DataUsed - 1;
			ulong num2 = rightSide.m_digits[0];
			ulong num3 = digitsArray[num];
			uint[] array = new uint[leftSide.m_digits.Count];
			leftSide.m_digits.CopyTo(array, 0, array.Length);
			int num4 = 0;
			if (num3 >= num2)
			{
				array[num4++] = (uint)(num3 / num2);
				digitsArray[num] = (uint)(num3 % num2);
			}
			num--;
			while (num >= 0)
			{
				num3 = ((ulong)digitsArray[num + 1] << DigitsArray.DataSizeBits) + digitsArray[num];
				array[num4++] = (uint)(num3 / num2);
				digitsArray[num + 1] = 0u;
				digitsArray[num--] = (uint)(num3 % num2);
			}
			remainder = new BigInteger(digitsArray);
			DigitsArray digitsArray2 = new DigitsArray(num4 + 1, num4);
			int num5 = 0;
			int num6 = digitsArray2.DataUsed - 1;
			while (num6 >= 0)
			{
				digitsArray2[num5] = array[num6];
				num6--;
				num5++;
			}
			quotient = new BigInteger(digitsArray2);
		}

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
			leftSide = Abs(leftSide);
			rightSide = Abs(rightSide);
			if (leftSide < rightSide)
			{
				return leftSide;
			}
			Divide(leftSide, rightSide, out var _, out var remainder);
			if (!isNegative)
			{
				return remainder;
			}
			return -remainder;
		}

		public static BigInteger Modulus(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide % rightSide;
		}

		public static BigInteger operator &(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = leftSide.m_digits[i] & rightSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger BitwiseAnd(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide & rightSide;
		}

		public static BigInteger operator |(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = leftSide.m_digits[i] | rightSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger BitwiseOr(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide | rightSide;
		}

		public static BigInteger operator ^(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = leftSide.m_digits[i] ^ rightSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Xor(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide ^ rightSide;
		}

		public static BigInteger operator ~(BigInteger leftSide)
		{
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.Count);
			for (int i = 0; i < digitsArray.Count; i++)
			{
				digitsArray[i] = ~leftSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger OnesComplement(BigInteger leftSide)
		{
			return ~leftSide;
		}

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

		public static BigInteger LeftShift(BigInteger leftSide, int shiftCount)
		{
			return leftSide << shiftCount;
		}

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
				for (int num = digitsArray.Count - 1; num >= digitsArray.DataUsed; num--)
				{
					digitsArray[num] = DigitsArray.AllBits;
				}
				uint num2 = DigitsArray.HiBitSet;
				for (int i = 0; i < DigitsArray.DataSizeBits; i++)
				{
					if ((digitsArray[digitsArray.DataUsed - 1] & num2) == DigitsArray.HiBitSet)
					{
						break;
					}
					digitsArray[digitsArray.DataUsed - 1] |= num2;
					num2 >>= 1;
				}
				digitsArray.DataUsed = digitsArray.Count;
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger RightShift(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			return leftSide >> shiftCount;
		}

		public int CompareTo(BigInteger value)
		{
			return Compare(this, value);
		}

		public static int Compare(BigInteger leftSide, BigInteger rightSide)
		{
			if ((object)leftSide == rightSide)
			{
				return 0;
			}
			if ((object)leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if ((object)rightSide == null)
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

		public static bool operator ==(BigInteger leftSide, BigInteger rightSide)
		{
			if ((object)leftSide == rightSide)
			{
				return true;
			}
			if ((object)leftSide == null || (object)rightSide == null)
			{
				return false;
			}
			if (leftSide.IsNegative != rightSide.IsNegative)
			{
				return false;
			}
			return leftSide.Equals(rightSide);
		}

		public static bool operator !=(BigInteger leftSide, BigInteger rightSide)
		{
			return !(leftSide == rightSide);
		}

		public static bool operator >(BigInteger leftSide, BigInteger rightSide)
		{
			if ((object)leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if ((object)rightSide == null)
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
			for (int num = leftSide.m_digits.DataUsed - 1; num >= 0; num--)
			{
				if (leftSide.m_digits[num] != rightSide.m_digits[num])
				{
					return leftSide.m_digits[num] > rightSide.m_digits[num];
				}
			}
			return false;
		}

		public static bool operator <(BigInteger leftSide, BigInteger rightSide)
		{
			if ((object)leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if ((object)rightSide == null)
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
			for (int num = leftSide.m_digits.DataUsed - 1; num >= 0; num--)
			{
				if (leftSide.m_digits[num] != rightSide.m_digits[num])
				{
					return leftSide.m_digits[num] < rightSide.m_digits[num];
				}
			}
			return false;
		}

		public static bool operator >=(BigInteger leftSide, BigInteger rightSide)
		{
			return Compare(leftSide, rightSide) >= 0;
		}

		public static bool operator <=(BigInteger leftSide, BigInteger rightSide)
		{
			return Compare(leftSide, rightSide) <= 0;
		}

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
			if (m_digits.DataUsed != bigInteger.m_digits.DataUsed)
			{
				return false;
			}
			for (int i = 0; i < m_digits.DataUsed; i++)
			{
				if (m_digits[i] != bigInteger.m_digits[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return m_digits.GetHashCode();
		}

		public override string ToString()
		{
			return ToString(10);
		}

		public string ToString(int radix)
		{
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentOutOfRangeException("radix");
			}
			if (IsZero)
			{
				return "0";
			}
			BigInteger bigInteger = this;
			bool isNegative = bigInteger.IsNegative;
			bigInteger = Abs(this);
			BigInteger rightSide = new BigInteger(radix);
			ArrayList arrayList = new ArrayList();
			while (bigInteger.m_digits.DataUsed > 1 || (bigInteger.m_digits.DataUsed == 1 && bigInteger.m_digits[0] != 0))
			{
				Divide(bigInteger, rightSide, out var quotient, out var remainder);
				arrayList.Insert(0, "0123456789abcdefghijklmnopqrstuvwxyz"[(int)remainder.m_digits[0]]);
				bigInteger = quotient;
			}
			string text = new string((char[])arrayList.ToArray(typeof(char)));
			if (radix == 10 && isNegative)
			{
				return "-" + text;
			}
			return text;
		}

		public string ToHexString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0:X}", m_digits[m_digits.DataUsed - 1]);
			string format = "{0:X" + 2 * DigitsArray.DataSizeOf + "}";
			for (int num = m_digits.DataUsed - 2; num >= 0; num--)
			{
				stringBuilder.AppendFormat(format, m_digits[num]);
			}
			return stringBuilder.ToString();
		}

		public static int ToInt16(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return short.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt16(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return ushort.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static int ToInt32(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return int.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt32(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return uint.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static long ToInt64(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return long.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static ulong ToUInt64(BigInteger value)
		{
			if ((object)value == null)
			{
				throw new ArgumentNullException("value");
			}
			return ulong.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}
	}
}
