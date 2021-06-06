using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000123 RID: 291
	public class FixedRewardVirtualCardData : IProtoBuf
	{
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00042C1F File Offset: 0x00040E1F
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x00042C27 File Offset: 0x00040E27
		public int FixedRewardMapId { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00042C30 File Offset: 0x00040E30
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x00042C38 File Offset: 0x00040E38
		public DeckCardData CardData { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00042C41 File Offset: 0x00040E41
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x00042C49 File Offset: 0x00040E49
		public string ActionType { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00042C52 File Offset: 0x00040E52
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x00042C5A File Offset: 0x00040E5A
		public int AchieveId
		{
			get
			{
				return this._AchieveId;
			}
			set
			{
				this._AchieveId = value;
				this.HasAchieveId = true;
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00042C6C File Offset: 0x00040E6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FixedRewardMapId.GetHashCode();
			num ^= this.CardData.GetHashCode();
			num ^= this.ActionType.GetHashCode();
			if (this.HasAchieveId)
			{
				num ^= this.AchieveId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00042CCC File Offset: 0x00040ECC
		public override bool Equals(object obj)
		{
			FixedRewardVirtualCardData fixedRewardVirtualCardData = obj as FixedRewardVirtualCardData;
			return fixedRewardVirtualCardData != null && this.FixedRewardMapId.Equals(fixedRewardVirtualCardData.FixedRewardMapId) && this.CardData.Equals(fixedRewardVirtualCardData.CardData) && this.ActionType.Equals(fixedRewardVirtualCardData.ActionType) && this.HasAchieveId == fixedRewardVirtualCardData.HasAchieveId && (!this.HasAchieveId || this.AchieveId.Equals(fixedRewardVirtualCardData.AchieveId));
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00042D56 File Offset: 0x00040F56
		public void Deserialize(Stream stream)
		{
			FixedRewardVirtualCardData.Deserialize(stream, this);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00042D60 File Offset: 0x00040F60
		public static FixedRewardVirtualCardData Deserialize(Stream stream, FixedRewardVirtualCardData instance)
		{
			return FixedRewardVirtualCardData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00042D6C File Offset: 0x00040F6C
		public static FixedRewardVirtualCardData DeserializeLengthDelimited(Stream stream)
		{
			FixedRewardVirtualCardData fixedRewardVirtualCardData = new FixedRewardVirtualCardData();
			FixedRewardVirtualCardData.DeserializeLengthDelimited(stream, fixedRewardVirtualCardData);
			return fixedRewardVirtualCardData;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00042D88 File Offset: 0x00040F88
		public static FixedRewardVirtualCardData DeserializeLengthDelimited(Stream stream, FixedRewardVirtualCardData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FixedRewardVirtualCardData.Deserialize(stream, instance, num);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00042DB0 File Offset: 0x00040FB0
		public static FixedRewardVirtualCardData Deserialize(Stream stream, FixedRewardVirtualCardData instance, long limit)
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
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.FixedRewardMapId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.CardData == null)
							{
								instance.CardData = DeckCardData.DeserializeLengthDelimited(stream);
								continue;
							}
							DeckCardData.DeserializeLengthDelimited(stream, instance.CardData);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.ActionType = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.AchieveId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001332 RID: 4914 RVA: 0x00042E9C File Offset: 0x0004109C
		public void Serialize(Stream stream)
		{
			FixedRewardVirtualCardData.Serialize(stream, this);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00042EA8 File Offset: 0x000410A8
		public static void Serialize(Stream stream, FixedRewardVirtualCardData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FixedRewardMapId));
			if (instance.CardData == null)
			{
				throw new ArgumentNullException("CardData", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.CardData.GetSerializedSize());
			DeckCardData.Serialize(stream, instance.CardData);
			if (instance.ActionType == null)
			{
				throw new ArgumentNullException("ActionType", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ActionType));
			if (instance.HasAchieveId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.AchieveId));
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00042F5C File Offset: 0x0004115C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FixedRewardMapId));
			uint serializedSize = this.CardData.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ActionType);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasAchieveId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.AchieveId));
			}
			return num + 3U;
		}

		// Token: 0x040005EF RID: 1519
		public bool HasAchieveId;

		// Token: 0x040005F0 RID: 1520
		private int _AchieveId;
	}
}
