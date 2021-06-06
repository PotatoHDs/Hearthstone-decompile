using System.IO;

namespace PegasusUtil
{
	public class DraftRetire : IProtoBuf
	{
		public enum PacketID
		{
			ID = 242,
			System = 0
		}

		public bool HasSeasonId;

		private int _SeasonId;

		public long DeckId { get; set; }

		public int Slot { get; set; }

		public int SeasonId
		{
			get
			{
				return _SeasonId;
			}
			set
			{
				_SeasonId = value;
				HasSeasonId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= DeckId.GetHashCode();
			hashCode ^= Slot.GetHashCode();
			if (HasSeasonId)
			{
				hashCode ^= SeasonId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DraftRetire draftRetire = obj as DraftRetire;
			if (draftRetire == null)
			{
				return false;
			}
			if (!DeckId.Equals(draftRetire.DeckId))
			{
				return false;
			}
			if (!Slot.Equals(draftRetire.Slot))
			{
				return false;
			}
			if (HasSeasonId != draftRetire.HasSeasonId || (HasSeasonId && !SeasonId.Equals(draftRetire.SeasonId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DraftRetire Deserialize(Stream stream, DraftRetire instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftRetire DeserializeLengthDelimited(Stream stream)
		{
			DraftRetire draftRetire = new DraftRetire();
			DeserializeLengthDelimited(stream, draftRetire);
			return draftRetire;
		}

		public static DraftRetire DeserializeLengthDelimited(Stream stream, DraftRetire instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftRetire Deserialize(Stream stream, DraftRetire instance, long limit)
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
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DraftRetire instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Slot);
			if (instance.HasSeasonId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			num += ProtocolParser.SizeOfUInt64((ulong)Slot);
			if (HasSeasonId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			}
			return num + 2;
		}
	}
}
