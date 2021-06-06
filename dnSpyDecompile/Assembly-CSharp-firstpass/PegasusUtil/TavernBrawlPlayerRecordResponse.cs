using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000BF RID: 191
	public class TavernBrawlPlayerRecordResponse : IProtoBuf
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000310F2 File Offset: 0x0002F2F2
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x000310FA File Offset: 0x0002F2FA
		public TavernBrawlPlayerRecord Record
		{
			get
			{
				return this._Record;
			}
			set
			{
				this._Record = value;
				this.HasRecord = (value != null);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00031110 File Offset: 0x0002F310
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRecord)
			{
				num ^= this.Record.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00031140 File Offset: 0x0002F340
		public override bool Equals(object obj)
		{
			TavernBrawlPlayerRecordResponse tavernBrawlPlayerRecordResponse = obj as TavernBrawlPlayerRecordResponse;
			return tavernBrawlPlayerRecordResponse != null && this.HasRecord == tavernBrawlPlayerRecordResponse.HasRecord && (!this.HasRecord || this.Record.Equals(tavernBrawlPlayerRecordResponse.Record));
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00031185 File Offset: 0x0002F385
		public void Deserialize(Stream stream)
		{
			TavernBrawlPlayerRecordResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003118F File Offset: 0x0002F38F
		public static TavernBrawlPlayerRecordResponse Deserialize(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			return TavernBrawlPlayerRecordResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003119C File Offset: 0x0002F39C
		public static TavernBrawlPlayerRecordResponse DeserializeLengthDelimited(Stream stream)
		{
			TavernBrawlPlayerRecordResponse tavernBrawlPlayerRecordResponse = new TavernBrawlPlayerRecordResponse();
			TavernBrawlPlayerRecordResponse.DeserializeLengthDelimited(stream, tavernBrawlPlayerRecordResponse);
			return tavernBrawlPlayerRecordResponse;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000311B8 File Offset: 0x0002F3B8
		public static TavernBrawlPlayerRecordResponse DeserializeLengthDelimited(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TavernBrawlPlayerRecordResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000311E0 File Offset: 0x0002F3E0
		public static TavernBrawlPlayerRecordResponse Deserialize(Stream stream, TavernBrawlPlayerRecordResponse instance, long limit)
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
				else if (num == 50)
				{
					if (instance.Record == null)
					{
						instance.Record = TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						TavernBrawlPlayerRecord.DeserializeLengthDelimited(stream, instance.Record);
					}
				}
				else
				{
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

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003127A File Offset: 0x0002F47A
		public void Serialize(Stream stream)
		{
			TavernBrawlPlayerRecordResponse.Serialize(stream, this);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00031283 File Offset: 0x0002F483
		public static void Serialize(Stream stream, TavernBrawlPlayerRecordResponse instance)
		{
			if (instance.HasRecord)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Record.GetSerializedSize());
				TavernBrawlPlayerRecord.Serialize(stream, instance.Record);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000312B4 File Offset: 0x0002F4B4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRecord)
			{
				num += 1U;
				uint serializedSize = this.Record.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400048F RID: 1167
		public bool HasRecord;

		// Token: 0x04000490 RID: 1168
		private TavernBrawlPlayerRecord _Record;

		// Token: 0x020005CE RID: 1486
		public enum PacketID
		{
			// Token: 0x04001FB5 RID: 8117
			ID = 317
		}
	}
}
