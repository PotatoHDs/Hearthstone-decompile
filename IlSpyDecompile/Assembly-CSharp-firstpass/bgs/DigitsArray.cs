using System;
using System.Collections.Generic;

namespace bgs
{
	internal class DigitsArray
	{
		private uint[] m_data;

		internal static readonly uint AllBits;

		internal static readonly uint HiBitSet;

		private int m_dataUsed;

		internal static int DataSizeOf => 4;

		internal static int DataSizeBits => 32;

		internal uint this[int index]
		{
			get
			{
				if (index < m_dataUsed)
				{
					return m_data[index];
				}
				if (!IsNegative)
				{
					return 0u;
				}
				return AllBits;
			}
			set
			{
				m_data[index] = value;
			}
		}

		internal int DataUsed
		{
			get
			{
				return m_dataUsed;
			}
			set
			{
				m_dataUsed = value;
			}
		}

		internal int Count => m_data.Length;

		internal bool IsZero
		{
			get
			{
				if (m_dataUsed != 0)
				{
					if (m_dataUsed == 1)
					{
						return m_data[0] == 0;
					}
					return false;
				}
				return true;
			}
		}

		internal bool IsNegative => (m_data[m_data.Length - 1] & HiBitSet) == HiBitSet;

		internal DigitsArray(int size)
		{
			Allocate(size, 0);
		}

		internal DigitsArray(int size, int used)
		{
			Allocate(size, used);
		}

		internal DigitsArray(uint[] copyFrom)
		{
			Allocate(copyFrom.Length);
			CopyFrom(copyFrom, 0, 0, copyFrom.Length);
			ResetDataUsed();
		}

		internal DigitsArray(DigitsArray copyFrom)
		{
			Allocate(copyFrom.Count, copyFrom.DataUsed);
			Array.Copy(copyFrom.m_data, 0, m_data, 0, copyFrom.Count);
		}

		static DigitsArray()
		{
			AllBits = uint.MaxValue;
			HiBitSet = (uint)(1 << DataSizeBits - 1);
		}

		public void Allocate(int size)
		{
			Allocate(size, 0);
		}

		public void Allocate(int size, int used)
		{
			m_data = new uint[size + 1];
			m_dataUsed = used;
		}

		internal void CopyFrom(uint[] source, int sourceOffset, int offset, int length)
		{
			Array.Copy(source, sourceOffset, m_data, 0, length);
		}

		internal void CopyTo(uint[] array, int offset, int length)
		{
			Array.Copy(m_data, 0, array, offset, length);
		}

		internal void ResetDataUsed()
		{
			m_dataUsed = m_data.Length;
			if (IsNegative)
			{
				while (m_dataUsed > 1 && m_data[m_dataUsed - 1] == AllBits)
				{
					m_dataUsed--;
				}
				m_dataUsed++;
				return;
			}
			while (m_dataUsed > 1 && m_data[m_dataUsed - 1] == 0)
			{
				m_dataUsed--;
			}
			if (m_dataUsed == 0)
			{
				m_dataUsed = 1;
			}
		}

		internal int ShiftRight(int shiftCount)
		{
			return ShiftRight(m_data, shiftCount);
		}

		internal static int ShiftRight(uint[] buffer, int shiftCount)
		{
			int num = DataSizeBits;
			int num2 = 0;
			int num3 = buffer.Length;
			while (num3 > 1 && buffer[num3 - 1] == 0)
			{
				num3--;
			}
			for (int num4 = shiftCount; num4 > 0; num4 -= num)
			{
				if (num4 < num)
				{
					num = num4;
					num2 = DataSizeBits - num;
				}
				ulong num5 = 0uL;
				for (int num6 = num3 - 1; num6 >= 0; num6--)
				{
					ulong num7 = (ulong)buffer[num6] >> num;
					num7 |= num5;
					num5 = (ulong)buffer[num6] << num2;
					buffer[num6] = (uint)num7;
				}
			}
			while (num3 > 1 && buffer[num3 - 1] == 0)
			{
				num3--;
			}
			return num3;
		}

		internal int ShiftLeft(int shiftCount)
		{
			return ShiftLeft(m_data, shiftCount);
		}

		internal static int ShiftLeft(uint[] buffer, int shiftCount)
		{
			int num = DataSizeBits;
			int num2 = buffer.Length;
			while (num2 > 1 && buffer[num2 - 1] == 0)
			{
				num2--;
			}
			for (int num3 = shiftCount; num3 > 0; num3 -= num)
			{
				if (num3 < num)
				{
					num = num3;
				}
				ulong num4 = 0uL;
				for (int i = 0; i < num2; i++)
				{
					ulong num5 = (ulong)buffer[i] << num;
					num5 |= num4;
					buffer[i] = (uint)(num5 & AllBits);
					num4 = num5 >> DataSizeBits;
				}
				if (num4 != 0L)
				{
					if (num2 + 1 > buffer.Length)
					{
						throw new OverflowException();
					}
					buffer[num2] = (uint)num4;
					num2++;
					num4 = 0uL;
				}
			}
			return num2;
		}

		internal int ShiftLeftWithoutOverflow(int shiftCount)
		{
			List<uint> list = new List<uint>(m_data);
			int num = DataSizeBits;
			for (int num2 = shiftCount; num2 > 0; num2 -= num)
			{
				if (num2 < num)
				{
					num = num2;
				}
				ulong num3 = 0uL;
				for (int i = 0; i < list.Count; i++)
				{
					ulong num4 = (ulong)list[i] << num;
					num4 |= num3;
					list[i] = (uint)(num4 & AllBits);
					num3 = num4 >> DataSizeBits;
				}
				if (num3 != 0L)
				{
					list.Add(0u);
					list[list.Count - 1] = (uint)num3;
				}
			}
			m_data = new uint[list.Count];
			list.CopyTo(m_data);
			return m_data.Length;
		}
	}
}
