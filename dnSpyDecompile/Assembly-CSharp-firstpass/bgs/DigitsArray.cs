using System;
using System.Collections.Generic;

namespace bgs
{
	// Token: 0x02000217 RID: 535
	internal class DigitsArray
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x00078EB2 File Offset: 0x000770B2
		internal DigitsArray(int size)
		{
			this.Allocate(size, 0);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00078EC2 File Offset: 0x000770C2
		internal DigitsArray(int size, int used)
		{
			this.Allocate(size, used);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00078ED2 File Offset: 0x000770D2
		internal DigitsArray(uint[] copyFrom)
		{
			this.Allocate(copyFrom.Length);
			this.CopyFrom(copyFrom, 0, 0, copyFrom.Length);
			this.ResetDataUsed();
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00078EF5 File Offset: 0x000770F5
		internal DigitsArray(DigitsArray copyFrom)
		{
			this.Allocate(copyFrom.Count, copyFrom.DataUsed);
			Array.Copy(copyFrom.m_data, 0, this.m_data, 0, copyFrom.Count);
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x00078F28 File Offset: 0x00077128
		internal static int DataSizeOf
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00078F2B File Offset: 0x0007712B
		internal static int DataSizeBits
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00078F48 File Offset: 0x00077148
		public void Allocate(int size)
		{
			this.Allocate(size, 0);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00078F52 File Offset: 0x00077152
		public void Allocate(int size, int used)
		{
			this.m_data = new uint[size + 1];
			this.m_dataUsed = used;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00078F69 File Offset: 0x00077169
		internal void CopyFrom(uint[] source, int sourceOffset, int offset, int length)
		{
			Array.Copy(source, sourceOffset, this.m_data, 0, length);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00078F7B File Offset: 0x0007717B
		internal void CopyTo(uint[] array, int offset, int length)
		{
			Array.Copy(this.m_data, 0, array, offset, length);
		}

		// Token: 0x17000664 RID: 1636
		internal uint this[int index]
		{
			get
			{
				if (index < this.m_dataUsed)
				{
					return this.m_data[index];
				}
				if (!this.IsNegative)
				{
					return 0U;
				}
				return DigitsArray.AllBits;
			}
			set
			{
				this.m_data[index] = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x00078FBA File Offset: 0x000771BA
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x00078FC2 File Offset: 0x000771C2
		internal int DataUsed
		{
			get
			{
				return this.m_dataUsed;
			}
			set
			{
				this.m_dataUsed = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00078FCB File Offset: 0x000771CB
		internal int Count
		{
			get
			{
				return this.m_data.Length;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x00078FD5 File Offset: 0x000771D5
		internal bool IsZero
		{
			get
			{
				return this.m_dataUsed == 0 || (this.m_dataUsed == 1 && this.m_data[0] == 0U);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x00078FF7 File Offset: 0x000771F7
		internal bool IsNegative
		{
			get
			{
				return (this.m_data[this.m_data.Length - 1] & DigitsArray.HiBitSet) == DigitsArray.HiBitSet;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00079018 File Offset: 0x00077218
		internal void ResetDataUsed()
		{
			this.m_dataUsed = this.m_data.Length;
			if (this.IsNegative)
			{
				while (this.m_dataUsed > 1 && this.m_data[this.m_dataUsed - 1] == DigitsArray.AllBits)
				{
					this.m_dataUsed--;
				}
				this.m_dataUsed++;
				return;
			}
			while (this.m_dataUsed > 1 && this.m_data[this.m_dataUsed - 1] == 0U)
			{
				this.m_dataUsed--;
			}
			if (this.m_dataUsed == 0)
			{
				this.m_dataUsed = 1;
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000790B0 File Offset: 0x000772B0
		internal int ShiftRight(int shiftCount)
		{
			return DigitsArray.ShiftRight(this.m_data, shiftCount);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000790C0 File Offset: 0x000772C0
		internal static int ShiftRight(uint[] buffer, int shiftCount)
		{
			int num = DigitsArray.DataSizeBits;
			int num2 = 0;
			int num3 = buffer.Length;
			while (num3 > 1 && buffer[num3 - 1] == 0U)
			{
				num3--;
			}
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
					num2 = DigitsArray.DataSizeBits - num;
				}
				ulong num4 = 0UL;
				for (int j = num3 - 1; j >= 0; j--)
				{
					ulong num5 = (ulong)buffer[j] >> num;
					num5 |= num4;
					num4 = (ulong)buffer[j] << num2;
					buffer[j] = (uint)num5;
				}
			}
			while (num3 > 1 && buffer[num3 - 1] == 0U)
			{
				num3--;
			}
			return num3;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00079152 File Offset: 0x00077352
		internal int ShiftLeft(int shiftCount)
		{
			return DigitsArray.ShiftLeft(this.m_data, shiftCount);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00079160 File Offset: 0x00077360
		internal static int ShiftLeft(uint[] buffer, int shiftCount)
		{
			int num = DigitsArray.DataSizeBits;
			int num2 = buffer.Length;
			while (num2 > 1 && buffer[num2 - 1] == 0U)
			{
				num2--;
			}
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
				}
				ulong num3 = 0UL;
				for (int j = 0; j < num2; j++)
				{
					ulong num4 = (ulong)buffer[j] << num;
					num4 |= num3;
					buffer[j] = (uint)(num4 & (ulong)DigitsArray.AllBits);
					num3 = num4 >> DigitsArray.DataSizeBits;
				}
				if (num3 != 0UL)
				{
					if (num2 + 1 > buffer.Length)
					{
						throw new OverflowException();
					}
					buffer[num2] = (uint)num3;
					num2++;
				}
			}
			return num2;
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000791FC File Offset: 0x000773FC
		internal int ShiftLeftWithoutOverflow(int shiftCount)
		{
			List<uint> list = new List<uint>(this.m_data);
			int num = DigitsArray.DataSizeBits;
			for (int i = shiftCount; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
				}
				ulong num2 = 0UL;
				for (int j = 0; j < list.Count; j++)
				{
					ulong num3 = (ulong)list[j] << num;
					num3 |= num2;
					list[j] = (uint)(num3 & (ulong)DigitsArray.AllBits);
					num2 = num3 >> DigitsArray.DataSizeBits;
				}
				if (num2 != 0UL)
				{
					list.Add(0U);
					list[list.Count - 1] = (uint)num2;
				}
			}
			this.m_data = new uint[list.Count];
			list.CopyTo(this.m_data);
			return this.m_data.Length;
		}

		// Token: 0x04000E46 RID: 3654
		private uint[] m_data;

		// Token: 0x04000E47 RID: 3655
		internal static readonly uint AllBits = uint.MaxValue;

		// Token: 0x04000E48 RID: 3656
		internal static readonly uint HiBitSet = 1U << DigitsArray.DataSizeBits - 1;

		// Token: 0x04000E49 RID: 3657
		private int m_dataUsed;
	}
}
