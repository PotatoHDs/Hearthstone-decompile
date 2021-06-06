using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x0200008E RID: 142
	public class DraftRetired : IProtoBuf
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00022DB2 File Offset: 0x00020FB2
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00022DBA File Offset: 0x00020FBA
		public long DeckId { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00022DC3 File Offset: 0x00020FC3
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00022DCB File Offset: 0x00020FCB
		public RewardChest Chest { get; set; }

		// Token: 0x0600097C RID: 2428 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.DeckId.GetHashCode() ^ this.Chest.GetHashCode();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00022E08 File Offset: 0x00021008
		public override bool Equals(object obj)
		{
			DraftRetired draftRetired = obj as DraftRetired;
			return draftRetired != null && this.DeckId.Equals(draftRetired.DeckId) && this.Chest.Equals(draftRetired.Chest);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00022E4F File Offset: 0x0002104F
		public void Deserialize(Stream stream)
		{
			DraftRetired.Deserialize(stream, this);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00022E59 File Offset: 0x00021059
		public static DraftRetired Deserialize(Stream stream, DraftRetired instance)
		{
			return DraftRetired.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00022E64 File Offset: 0x00021064
		public static DraftRetired DeserializeLengthDelimited(Stream stream)
		{
			DraftRetired draftRetired = new DraftRetired();
			DraftRetired.DeserializeLengthDelimited(stream, draftRetired);
			return draftRetired;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00022E80 File Offset: 0x00021080
		public static DraftRetired DeserializeLengthDelimited(Stream stream, DraftRetired instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftRetired.Deserialize(stream, instance, num);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00022EA8 File Offset: 0x000210A8
		public static DraftRetired Deserialize(Stream stream, DraftRetired instance, long limit)
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
				else
				{
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00022F59 File Offset: 0x00021159
		public void Serialize(Stream stream)
		{
			DraftRetired.Serialize(stream, this);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00022F64 File Offset: 0x00021164
		public static void Serialize(Stream stream, DraftRetired instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.Chest == null)
			{
				throw new ArgumentNullException("Chest", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.Chest);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00022FC4 File Offset: 0x000211C4
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			uint serializedSize = this.Chest.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}

		// Token: 0x020005A2 RID: 1442
		public enum PacketID
		{
			// Token: 0x04001F49 RID: 8009
			ID = 247
		}
	}
}
