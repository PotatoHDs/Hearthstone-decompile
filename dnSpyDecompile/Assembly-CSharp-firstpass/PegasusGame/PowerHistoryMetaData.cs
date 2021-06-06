using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001C2 RID: 450
	public class PowerHistoryMetaData : IProtoBuf
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000649E9 File Offset: 0x00062BE9
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x000649F1 File Offset: 0x00062BF1
		public List<int> Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000649FA File Offset: 0x00062BFA
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x00064A02 File Offset: 0x00062C02
		public HistoryMeta.Type Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00064A12 File Offset: 0x00062C12
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x00064A1A File Offset: 0x00062C1A
		public int Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = true;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00064A2A File Offset: 0x00062C2A
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00064A32 File Offset: 0x00062C32
		public List<int> AdditionalData
		{
			get
			{
				return this._AdditionalData;
			}
			set
			{
				this._AdditionalData = value;
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00064A3C File Offset: 0x00062C3C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (int num2 in this.Info)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			foreach (int num3 in this.AdditionalData)
			{
				num ^= num3.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00064B20 File Offset: 0x00062D20
		public override bool Equals(object obj)
		{
			PowerHistoryMetaData powerHistoryMetaData = obj as PowerHistoryMetaData;
			if (powerHistoryMetaData == null)
			{
				return false;
			}
			if (this.Info.Count != powerHistoryMetaData.Info.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Info.Count; i++)
			{
				if (!this.Info[i].Equals(powerHistoryMetaData.Info[i]))
				{
					return false;
				}
			}
			if (this.HasType != powerHistoryMetaData.HasType || (this.HasType && !this.Type.Equals(powerHistoryMetaData.Type)))
			{
				return false;
			}
			if (this.HasData != powerHistoryMetaData.HasData || (this.HasData && !this.Data.Equals(powerHistoryMetaData.Data)))
			{
				return false;
			}
			if (this.AdditionalData.Count != powerHistoryMetaData.AdditionalData.Count)
			{
				return false;
			}
			for (int j = 0; j < this.AdditionalData.Count; j++)
			{
				if (!this.AdditionalData[j].Equals(powerHistoryMetaData.AdditionalData[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00064C4F File Offset: 0x00062E4F
		public void Deserialize(Stream stream)
		{
			PowerHistoryMetaData.Deserialize(stream, this);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00064C59 File Offset: 0x00062E59
		public static PowerHistoryMetaData Deserialize(Stream stream, PowerHistoryMetaData instance)
		{
			return PowerHistoryMetaData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00064C64 File Offset: 0x00062E64
		public static PowerHistoryMetaData DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryMetaData powerHistoryMetaData = new PowerHistoryMetaData();
			PowerHistoryMetaData.DeserializeLengthDelimited(stream, powerHistoryMetaData);
			return powerHistoryMetaData;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00064C80 File Offset: 0x00062E80
		public static PowerHistoryMetaData DeserializeLengthDelimited(Stream stream, PowerHistoryMetaData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryMetaData.Deserialize(stream, instance, num);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00064CA8 File Offset: 0x00062EA8
		public static PowerHistoryMetaData Deserialize(Stream stream, PowerHistoryMetaData instance, long limit)
		{
			if (instance.Info == null)
			{
				instance.Info = new List<int>();
			}
			instance.Type = HistoryMeta.Type.TARGET;
			if (instance.AdditionalData == null)
			{
				instance.AdditionalData = new List<int>();
			}
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
						if (num != 18)
						{
							if (num == 24)
							{
								instance.Type = (HistoryMeta.Type)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.Info.Add((int)ProtocolParser.ReadUInt64(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Data = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.AdditionalData.Add((int)ProtocolParser.ReadUInt64(stream));
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

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00064DEE File Offset: 0x00062FEE
		public void Serialize(Stream stream)
		{
			PowerHistoryMetaData.Serialize(stream, this);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00064DF8 File Offset: 0x00062FF8
		public static void Serialize(Stream stream, PowerHistoryMetaData instance)
		{
			if (instance.Info.Count > 0)
			{
				stream.WriteByte(18);
				uint num = 0U;
				foreach (int num2 in instance.Info)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (int num3 in instance.Info)
				{
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num3));
				}
			}
			if (instance.HasType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			}
			if (instance.HasData)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Data));
			}
			if (instance.AdditionalData.Count > 0)
			{
				foreach (int num4 in instance.AdditionalData)
				{
					stream.WriteByte(40);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num4));
				}
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00064F48 File Offset: 0x00063148
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Info.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (int num3 in this.Info)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num3));
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			}
			if (this.HasData)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Data));
			}
			if (this.AdditionalData.Count > 0)
			{
				foreach (int num4 in this.AdditionalData)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num4));
				}
			}
			return num;
		}

		// Token: 0x04000A62 RID: 2658
		private List<int> _Info = new List<int>();

		// Token: 0x04000A63 RID: 2659
		public bool HasType;

		// Token: 0x04000A64 RID: 2660
		private HistoryMeta.Type _Type;

		// Token: 0x04000A65 RID: 2661
		public bool HasData;

		// Token: 0x04000A66 RID: 2662
		private int _Data;

		// Token: 0x04000A67 RID: 2663
		private List<int> _AdditionalData = new List<int>();
	}
}
