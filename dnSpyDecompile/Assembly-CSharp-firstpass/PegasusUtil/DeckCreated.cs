using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A3 RID: 163
	public class DeckCreated : IProtoBuf
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00029460 File Offset: 0x00027660
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00029468 File Offset: 0x00027668
		public DeckInfo Info { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00029471 File Offset: 0x00027671
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00029479 File Offset: 0x00027679
		public int RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = true;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002948C File Offset: 0x0002768C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Info.GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000294D0 File Offset: 0x000276D0
		public override bool Equals(object obj)
		{
			DeckCreated deckCreated = obj as DeckCreated;
			return deckCreated != null && this.Info.Equals(deckCreated.Info) && this.HasRequestId == deckCreated.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(deckCreated.RequestId));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002952D File Offset: 0x0002772D
		public void Deserialize(Stream stream)
		{
			DeckCreated.Deserialize(stream, this);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00029537 File Offset: 0x00027737
		public static DeckCreated Deserialize(Stream stream, DeckCreated instance)
		{
			return DeckCreated.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00029544 File Offset: 0x00027744
		public static DeckCreated DeserializeLengthDelimited(Stream stream)
		{
			DeckCreated deckCreated = new DeckCreated();
			DeckCreated.DeserializeLengthDelimited(stream, deckCreated);
			return deckCreated;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00029560 File Offset: 0x00027760
		public static DeckCreated DeserializeLengthDelimited(Stream stream, DeckCreated instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckCreated.Deserialize(stream, instance, num);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00029588 File Offset: 0x00027788
		public static DeckCreated Deserialize(Stream stream, DeckCreated instance, long limit)
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
				else if (num != 10)
				{
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Info == null)
				{
					instance.Info = DeckInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					DeckInfo.DeserializeLengthDelimited(stream, instance.Info);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002963B File Offset: 0x0002783B
		public void Serialize(Stream stream)
		{
			DeckCreated.Serialize(stream, this);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00029644 File Offset: 0x00027844
		public static void Serialize(Stream stream, DeckCreated instance)
		{
			if (instance.Info == null)
			{
				throw new ArgumentNullException("Info", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
			DeckInfo.Serialize(stream, instance.Info);
			if (instance.HasRequestId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestId));
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000296AC File Offset: 0x000278AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Info.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRequestId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestId));
			}
			return num + 1U;
		}

		// Token: 0x040003C6 RID: 966
		public bool HasRequestId;

		// Token: 0x040003C7 RID: 967
		private int _RequestId;

		// Token: 0x020005AD RID: 1453
		public enum PacketID
		{
			// Token: 0x04001F60 RID: 8032
			ID = 217
		}
	}
}
