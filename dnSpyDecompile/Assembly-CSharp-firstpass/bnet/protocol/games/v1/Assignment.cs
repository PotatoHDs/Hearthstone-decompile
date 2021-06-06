using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A7 RID: 935
	public class Assignment : IProtoBuf
	{
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000C38D7 File Offset: 0x000C1AD7
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x000C38DF File Offset: 0x000C1ADF
		public EntityId GameAccountId { get; set; }

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000C38E8 File Offset: 0x000C1AE8
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000C38F1 File Offset: 0x000C1AF1
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x000C38F9 File Offset: 0x000C1AF9
		public uint TeamIndex { get; set; }

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000C3902 File Offset: 0x000C1B02
		public void SetTeamIndex(uint val)
		{
			this.TeamIndex = val;
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000C390C File Offset: 0x000C1B0C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.GameAccountId.GetHashCode() ^ this.TeamIndex.GetHashCode();
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000C3940 File Offset: 0x000C1B40
		public override bool Equals(object obj)
		{
			Assignment assignment = obj as Assignment;
			return assignment != null && this.GameAccountId.Equals(assignment.GameAccountId) && this.TeamIndex.Equals(assignment.TeamIndex);
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000C3987 File Offset: 0x000C1B87
		public static Assignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Assignment>(bs, 0, -1);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000C3991 File Offset: 0x000C1B91
		public void Deserialize(Stream stream)
		{
			Assignment.Deserialize(stream, this);
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000C399B File Offset: 0x000C1B9B
		public static Assignment Deserialize(Stream stream, Assignment instance)
		{
			return Assignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000C39A8 File Offset: 0x000C1BA8
		public static Assignment DeserializeLengthDelimited(Stream stream)
		{
			Assignment assignment = new Assignment();
			Assignment.DeserializeLengthDelimited(stream, assignment);
			return assignment;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000C39C4 File Offset: 0x000C1BC4
		public static Assignment DeserializeLengthDelimited(Stream stream, Assignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Assignment.Deserialize(stream, instance, num);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000C39EC File Offset: 0x000C1BEC
		public static Assignment Deserialize(Stream stream, Assignment instance, long limit)
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
						instance.TeamIndex = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.GameAccountId == null)
				{
					instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000C3A9E File Offset: 0x000C1C9E
		public void Serialize(Stream stream)
		{
			Assignment.Serialize(stream, this);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000C3AA8 File Offset: 0x000C1CA8
		public static void Serialize(Stream stream, Assignment instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.TeamIndex);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000C3B08 File Offset: 0x000C1D08
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt32(this.TeamIndex) + 2U;
		}
	}
}
