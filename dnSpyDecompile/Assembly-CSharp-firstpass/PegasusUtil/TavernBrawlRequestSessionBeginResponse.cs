using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000E6 RID: 230
	public class TavernBrawlRequestSessionBeginResponse : IProtoBuf
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00037C42 File Offset: 0x00035E42
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x00037C4A File Offset: 0x00035E4A
		public ErrorCode ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00037C5A File Offset: 0x00035E5A
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x00037C62 File Offset: 0x00035E62
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

		// Token: 0x06000F84 RID: 3972 RVA: 0x00037C78 File Offset: 0x00035E78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasPlayerRecord)
			{
				num ^= this.PlayerRecord.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00037CC8 File Offset: 0x00035EC8
		public override bool Equals(object obj)
		{
			TavernBrawlRequestSessionBeginResponse tavernBrawlRequestSessionBeginResponse = obj as TavernBrawlRequestSessionBeginResponse;
			return tavernBrawlRequestSessionBeginResponse != null && this.HasErrorCode == tavernBrawlRequestSessionBeginResponse.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(tavernBrawlRequestSessionBeginResponse.ErrorCode)) && this.HasPlayerRecord == tavernBrawlRequestSessionBeginResponse.HasPlayerRecord && (!this.HasPlayerRecord || this.PlayerRecord.Equals(tavernBrawlRequestSessionBeginResponse.PlayerRecord));
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00037D46 File Offset: 0x00035F46
		public void Deserialize(Stream stream)
		{
			TavernBrawlRequestSessionBeginResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00037D50 File Offset: 0x00035F50
		public static TavernBrawlRequestSessionBeginResponse Deserialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			return TavernBrawlRequestSessionBeginResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00037D5C File Offset: 0x00035F5C
		public static TavernBrawlRequestSessionBeginResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlRequestSessionBeginResponse tavernBrawlRequestSessionBeginResponse = new TavernBrawlRequestSessionBeginResponse();
			TavernBrawlRequestSessionBeginResponse.DeserializeLengthDelimited(stream, tavernBrawlRequestSessionBeginResponse);
			return tavernBrawlRequestSessionBeginResponse;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00037D78 File Offset: 0x00035F78
		public static TavernBrawlRequestSessionBeginResponse DeserializeLengthDelimited(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlRequestSessionBeginResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00037DA0 File Offset: 0x00035FA0
		public static TavernBrawlRequestSessionBeginResponse Deserialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x06000F8B RID: 3979 RVA: 0x00037E59 File Offset: 0x00036059
		public void Serialize(Stream stream)
		{
			TavernBrawlRequestSessionBeginResponse.Serialize(stream, this);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00037E64 File Offset: 0x00036064
		public static void Serialize(Stream stream, TavernBrawlRequestSessionBeginResponse instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			if (instance.HasPlayerRecord)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.PlayerRecord.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.PlayerRecord);
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00037EBC File Offset: 0x000360BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			if (this.HasPlayerRecord)
			{
				num += 1U;
				uint serializedSize = this.PlayerRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040004FD RID: 1277
		public bool HasErrorCode;

		// Token: 0x040004FE RID: 1278
		private ErrorCode _ErrorCode;

		// Token: 0x040004FF RID: 1279
		public bool HasPlayerRecord;

		// Token: 0x04000500 RID: 1280
		private TavernBrawlPlayerRecord _PlayerRecord;

		// Token: 0x020005EA RID: 1514
		public enum PacketID
		{
			// Token: 0x04002004 RID: 8196
			ID = 347
		}
	}
}
