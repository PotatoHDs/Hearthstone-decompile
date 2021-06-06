using System.IO;

namespace PegasusUtil
{
	public class DraftMakePick : IProtoBuf
	{
		public enum PacketID
		{
			ID = 245,
			System = 0
		}

		public long DeckId { get; set; }

		public int Slot { get; set; }

		public int Index { get; set; }

		public int Premium { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ DeckId.GetHashCode() ^ Slot.GetHashCode() ^ Index.GetHashCode() ^ Premium.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			DraftMakePick draftMakePick = obj as DraftMakePick;
			if (draftMakePick == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftMakePick.DeckId))
			{
				return false;
			}
			if (!Slot.Equals(draftMakePick.Slot))
			{
				return false;
			}
			if (!Index.Equals(draftMakePick.Index))
			{
				return false;
			}
			if (!Premium.Equals(draftMakePick.Premium))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftMakePick Deserialize(Stream stream, DraftMakePick instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftMakePick DeserializeLengthDelimited(Stream stream)
		{
			DraftMakePick draftMakePick = new DraftMakePick();
			DeserializeLengthDelimited(stream, draftMakePick);
			return draftMakePick;
		}

		public static DraftMakePick DeserializeLengthDelimited(Stream stream, DraftMakePick instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftMakePick Deserialize(Stream stream, DraftMakePick instance, long limit)
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
				case 24:
					instance.Index = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DraftMakePick instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Slot);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Index);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Premium);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)DeckId) + ProtocolParser.SizeOfUInt64((ulong)Slot) + ProtocolParser.SizeOfUInt64((ulong)Index) + ProtocolParser.SizeOfUInt64((ulong)Premium) + 4;
		}
	}
}
