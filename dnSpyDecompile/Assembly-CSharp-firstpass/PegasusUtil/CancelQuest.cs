using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000069 RID: 105
	public class CancelQuest : IProtoBuf
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x000194C2 File Offset: 0x000176C2
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x000194CA File Offset: 0x000176CA
		public int QuestId { get; set; }

		// Token: 0x060006A7 RID: 1703 RVA: 0x000194D4 File Offset: 0x000176D4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.QuestId.GetHashCode();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000194FC File Offset: 0x000176FC
		public override bool Equals(object obj)
		{
			CancelQuest cancelQuest = obj as CancelQuest;
			return cancelQuest != null && this.QuestId.Equals(cancelQuest.QuestId);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001952E File Offset: 0x0001772E
		public void Deserialize(Stream stream)
		{
			CancelQuest.Deserialize(stream, this);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00019538 File Offset: 0x00017738
		public static CancelQuest Deserialize(Stream stream, CancelQuest instance)
		{
			return CancelQuest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00019544 File Offset: 0x00017744
		public static CancelQuest DeserializeLengthDelimited(Stream stream)
		{
			CancelQuest cancelQuest = new CancelQuest();
			CancelQuest.DeserializeLengthDelimited(stream, cancelQuest);
			return cancelQuest;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00019560 File Offset: 0x00017760
		public static CancelQuest DeserializeLengthDelimited(Stream stream, CancelQuest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelQuest.Deserialize(stream, instance, num);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00019588 File Offset: 0x00017788
		public static CancelQuest Deserialize(Stream stream, CancelQuest instance, long limit)
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
				else if (num == 8)
				{
					instance.QuestId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060006AE RID: 1710 RVA: 0x00019608 File Offset: 0x00017808
		public void Serialize(Stream stream)
		{
			CancelQuest.Serialize(stream, this);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00019611 File Offset: 0x00017811
		public static void Serialize(Stream stream, CancelQuest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.QuestId));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00019627 File Offset: 0x00017827
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.QuestId)) + 1U;
		}

		// Token: 0x0200057C RID: 1404
		public enum PacketID
		{
			// Token: 0x04001ED0 RID: 7888
			ID = 281,
			// Token: 0x04001ED1 RID: 7889
			System = 0
		}
	}
}
