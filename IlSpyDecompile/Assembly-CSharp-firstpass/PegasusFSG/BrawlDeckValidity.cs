using System.IO;

namespace PegasusFSG
{
	public class BrawlDeckValidity : IProtoBuf
	{
		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		public int SeasonId { get; set; }

		public bool ValidDeck { get; set; }

		public int BrawlLibraryItemId
		{
			get
			{
				return _BrawlLibraryItemId;
			}
			set
			{
				_BrawlLibraryItemId = value;
				HasBrawlLibraryItemId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= SeasonId.GetHashCode();
			hashCode ^= ValidDeck.GetHashCode();
			if (HasBrawlLibraryItemId)
			{
				hashCode ^= BrawlLibraryItemId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			BrawlDeckValidity brawlDeckValidity = obj as BrawlDeckValidity;
			if (brawlDeckValidity == null)
			{
				return false;
			}
			if (!SeasonId.Equals(brawlDeckValidity.SeasonId))
			{
				return false;
			}
			if (!ValidDeck.Equals(brawlDeckValidity.ValidDeck))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != brawlDeckValidity.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(brawlDeckValidity.BrawlLibraryItemId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BrawlDeckValidity Deserialize(Stream stream, BrawlDeckValidity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BrawlDeckValidity DeserializeLengthDelimited(Stream stream)
		{
			BrawlDeckValidity brawlDeckValidity = new BrawlDeckValidity();
			DeserializeLengthDelimited(stream, brawlDeckValidity);
			return brawlDeckValidity;
		}

		public static BrawlDeckValidity DeserializeLengthDelimited(Stream stream, BrawlDeckValidity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BrawlDeckValidity Deserialize(Stream stream, BrawlDeckValidity instance, long limit)
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
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ValidDeck = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, BrawlDeckValidity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.ValidDeck);
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			num++;
			if (HasBrawlLibraryItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			return num + 2;
		}
	}
}
