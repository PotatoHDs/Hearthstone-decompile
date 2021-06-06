using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000096 RID: 150
	public class ProfileDeckLimit : IProtoBuf
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002588B File Offset: 0x00023A8B
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00025893 File Offset: 0x00023A93
		public int DeckLimit { get; set; }

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002589C File Offset: 0x00023A9C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.DeckLimit.GetHashCode();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000258C4 File Offset: 0x00023AC4
		public override bool Equals(object obj)
		{
			ProfileDeckLimit profileDeckLimit = obj as ProfileDeckLimit;
			return profileDeckLimit != null && this.DeckLimit.Equals(profileDeckLimit.DeckLimit);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000258F6 File Offset: 0x00023AF6
		public void Deserialize(Stream stream)
		{
			ProfileDeckLimit.Deserialize(stream, this);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00025900 File Offset: 0x00023B00
		public static ProfileDeckLimit Deserialize(Stream stream, ProfileDeckLimit instance)
		{
			return ProfileDeckLimit.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002590C File Offset: 0x00023B0C
		public static ProfileDeckLimit DeserializeLengthDelimited(Stream stream)
		{
			ProfileDeckLimit profileDeckLimit = new ProfileDeckLimit();
			ProfileDeckLimit.DeserializeLengthDelimited(stream, profileDeckLimit);
			return profileDeckLimit;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00025928 File Offset: 0x00023B28
		public static ProfileDeckLimit DeserializeLengthDelimited(Stream stream, ProfileDeckLimit instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileDeckLimit.Deserialize(stream, instance, num);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00025950 File Offset: 0x00023B50
		public static ProfileDeckLimit Deserialize(Stream stream, ProfileDeckLimit instance, long limit)
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
					instance.DeckLimit = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000A29 RID: 2601 RVA: 0x000259D0 File Offset: 0x00023BD0
		public void Serialize(Stream stream)
		{
			ProfileDeckLimit.Serialize(stream, this);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000259D9 File Offset: 0x00023BD9
		public static void Serialize(Stream stream, ProfileDeckLimit instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckLimit));
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000259EF File Offset: 0x00023BEF
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckLimit)) + 1U;
		}

		// Token: 0x020005A7 RID: 1447
		public enum PacketID
		{
			// Token: 0x04001F53 RID: 8019
			ID = 231
		}
	}
}
