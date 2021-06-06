using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000E7 RID: 231
	public class TavernBrawlRequestSessionRetireResponse : IProtoBuf
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00037F0A File Offset: 0x0003610A
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00037F12 File Offset: 0x00036112
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00037F1B File Offset: 0x0003611B
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00037F23 File Offset: 0x00036123
		public TavernBrawlPlayerRecord PlayerRecord
		{
			get
			{
				return this._PlayerRecord;
			}
			set
			{
				this._PlayerRecord = value;
				this.HasPlayerRecord = (value != null);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00037F36 File Offset: 0x00036136
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00037F3E File Offset: 0x0003613E
		public RewardChest Chest
		{
			get
			{
				return this._Chest;
			}
			set
			{
				this._Chest = value;
				this.HasChest = (value != null);
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00037F54 File Offset: 0x00036154
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasPlayerRecord)
			{
				num ^= this.PlayerRecord.GetHashCode();
			}
			if (this.HasChest)
			{
				num ^= this.Chest.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00037FB4 File Offset: 0x000361B4
		public override bool Equals(object obj)
		{
			TavernBrawlRequestSessionRetireResponse tavernBrawlRequestSessionRetireResponse = obj as TavernBrawlRequestSessionRetireResponse;
			return tavernBrawlRequestSessionRetireResponse != null && this.ErrorCode.Equals(tavernBrawlRequestSessionRetireResponse.ErrorCode) && this.HasPlayerRecord == tavernBrawlRequestSessionRetireResponse.HasPlayerRecord && (!this.HasPlayerRecord || this.PlayerRecord.Equals(tavernBrawlRequestSessionRetireResponse.PlayerRecord)) && this.HasChest == tavernBrawlRequestSessionRetireResponse.HasChest && (!this.HasChest || this.Chest.Equals(tavernBrawlRequestSessionRetireResponse.Chest));
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00038047 File Offset: 0x00036247
		public void Deserialize(Stream stream)
		{
			TavernBrawlRequestSessionRetireResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00038051 File Offset: 0x00036251
		public static TavernBrawlRequestSessionRetireResponse Deserialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			return TavernBrawlRequestSessionRetireResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003805C File Offset: 0x0003625C
		public static TavernBrawlRequestSessionRetireResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionRetireResponse tavernBrawlRequestSessionRetireResponse = new TavernBrawlRequestSessionRetireResponse();
			TavernBrawlRequestSessionRetireResponse.DeserializeLengthDelimited(stream, tavernBrawlRequestSessionRetireResponse);
			return tavernBrawlRequestSessionRetireResponse;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00038078 File Offset: 0x00036278
		public static TavernBrawlRequestSessionRetireResponse DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlRequestSessionRetireResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000380A0 File Offset: 0x000362A0
		public static TavernBrawlRequestSessionRetireResponse Deserialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Chest == null)
						{
							instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
						}
						else
						{
							RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
						}
					}
					else if (instance.PlayerRecord == null)
					{
						instance.PlayerRecord = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.PlayerRecord);
					}
				}
				else
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00038188 File Offset: 0x00036388
		public void Serialize(Stream stream)
		{
			TavernBrawlRequestSessionRetireResponse.Serialize(stream, this);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00038194 File Offset: 0x00036394
		public static void Serialize(Stream stream, TavernBrawlRequestSessionRetireResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
			}
			if (instance.HasChest)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00038210 File Offset: 0x00036410
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			if (this.HasPlayerRecord)
			{
				num += 1U;
				uint serializedSize = this.PlayerRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChest)
			{
				num += 1U;
				uint serializedSize2 = this.Chest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x04000502 RID: 1282
		public bool HasPlayerRecord;

		// Token: 0x04000503 RID: 1283
		private TavernBrawlPlayerRecord _PlayerRecord;

		// Token: 0x04000504 RID: 1284
		public bool HasChest;

		// Token: 0x04000505 RID: 1285
		private RewardChest _Chest;

		// Token: 0x020005EB RID: 1515
		public enum PacketID
		{
			// Token: 0x04002006 RID: 8198
			ID = 348
		}
	}
}
