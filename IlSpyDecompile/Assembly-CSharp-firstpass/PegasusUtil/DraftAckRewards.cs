using System.IO;

namespace PegasusUtil
{
	public class DraftAckRewards : IProtoBuf
	{
		public enum PacketID
		{
			ID = 287,
			System = 0
		}

		public long DeckId { get; set; }

		public int Slot { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ DeckId.GetHashCode() ^ Slot.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DraftAckRewards draftAckRewards = obj as DraftAckRewards;
			if (draftAckRewards == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftAckRewards.DeckId))
			{
				return false;
			}
			if (!Slot.Equals(draftAckRewards.Slot))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftAckRewards Deserialize(Stream stream, DraftAckRewards instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftAckRewards DeserializeLengthDelimited(Stream stream)
		{
			DraftAckRewards draftAckRewards = new DraftAckRewards();
			DeserializeLengthDelimited(stream, draftAckRewards);
			return draftAckRewards;
		}

		public static DraftAckRewards DeserializeLengthDelimited(Stream stream, DraftAckRewards instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftAckRewards Deserialize(Stream stream, DraftAckRewards instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Slot = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DraftAckRewards instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Slot);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)DeckId) + ProtocolParser.SizeOfUInt64((ulong)Slot) + 2;
		}
	}
}
