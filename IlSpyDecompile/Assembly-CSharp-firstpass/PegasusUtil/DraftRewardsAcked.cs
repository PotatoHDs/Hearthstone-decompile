using System.IO;

namespace PegasusUtil
{
	public class DraftRewardsAcked : IProtoBuf
	{
		public enum PacketID
		{
			ID = 288
		}

		public long DeckId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ DeckId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DraftRewardsAcked draftRewardsAcked = obj as DraftRewardsAcked;
			if (draftRewardsAcked == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftRewardsAcked.DeckId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftRewardsAcked Deserialize(Stream stream, DraftRewardsAcked instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftRewardsAcked DeserializeLengthDelimited(Stream stream)
		{
			DraftRewardsAcked draftRewardsAcked = new DraftRewardsAcked();
			DeserializeLengthDelimited(stream, draftRewardsAcked);
			return draftRewardsAcked;
		}

		public static DraftRewardsAcked DeserializeLengthDelimited(Stream stream, DraftRewardsAcked instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftRewardsAcked Deserialize(Stream stream, DraftRewardsAcked instance, long limit)
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

		public static void Serialize(Stream stream, DraftRewardsAcked instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)DeckId) + 1;
		}
	}
}
