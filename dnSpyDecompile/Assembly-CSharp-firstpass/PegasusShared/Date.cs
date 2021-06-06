using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200011F RID: 287
	public class Date : IProtoBuf
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00041D9D File Offset: 0x0003FF9D
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x00041DA5 File Offset: 0x0003FFA5
		public int Year { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00041DAE File Offset: 0x0003FFAE
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00041DB6 File Offset: 0x0003FFB6
		public int Month { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00041DBF File Offset: 0x0003FFBF
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x00041DC7 File Offset: 0x0003FFC7
		public int Day { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00041DD8 File Offset: 0x0003FFD8
		public int Hours { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00041DE1 File Offset: 0x0003FFE1
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00041DE9 File Offset: 0x0003FFE9
		public int Min { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00041DF2 File Offset: 0x0003FFF2
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x00041DFA File Offset: 0x0003FFFA
		public int Sec { get; set; }

		// Token: 0x060012DF RID: 4831 RVA: 0x00041E04 File Offset: 0x00040004
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Year.GetHashCode() ^ this.Month.GetHashCode() ^ this.Day.GetHashCode() ^ this.Hours.GetHashCode() ^ this.Min.GetHashCode() ^ this.Sec.GetHashCode();
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00041E78 File Offset: 0x00040078
		public override bool Equals(object obj)
		{
			Date date = obj as Date;
			return date != null && this.Year.Equals(date.Year) && this.Month.Equals(date.Month) && this.Day.Equals(date.Day) && this.Hours.Equals(date.Hours) && this.Min.Equals(date.Min) && this.Sec.Equals(date.Sec);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00041F22 File Offset: 0x00040122
		public void Deserialize(Stream stream)
		{
			Date.Deserialize(stream, this);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00041F2C File Offset: 0x0004012C
		public static Date Deserialize(Stream stream, Date instance)
		{
			return Date.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00041F38 File Offset: 0x00040138
		public static Date DeserializeLengthDelimited(Stream stream)
		{
			Date date = new Date();
			Date.DeserializeLengthDelimited(stream, date);
			return date;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00041F54 File Offset: 0x00040154
		public static Date DeserializeLengthDelimited(Stream stream, Date instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Date.Deserialize(stream, instance, num);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00041F7C File Offset: 0x0004017C
		public static Date Deserialize(Stream stream, Date instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.Year = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Month = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Day = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Hours = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Min = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.Sec = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00042081 File Offset: 0x00040281
		public void Serialize(Stream stream)
		{
			Date.Serialize(stream, this);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004208C File Offset: 0x0004028C
		public static void Serialize(Stream stream, Date instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Year));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Month));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Day));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Hours));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Min));
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Sec));
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00042118 File Offset: 0x00040318
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Year)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Month)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Day)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Hours)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Min)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Sec)) + 6U;
		}
	}
}
