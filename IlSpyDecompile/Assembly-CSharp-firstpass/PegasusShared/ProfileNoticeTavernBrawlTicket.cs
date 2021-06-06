using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeTavernBrawlTicket : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 18
		}

		public int TicketType { get; set; }

		public int Quantity { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ TicketType.GetHashCode() ^ Quantity.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeTavernBrawlTicket profileNoticeTavernBrawlTicket = obj as ProfileNoticeTavernBrawlTicket;
			if (profileNoticeTavernBrawlTicket == null)
			{
				return false;
			}
			if (!TicketType.Equals(profileNoticeTavernBrawlTicket.TicketType))
			{
				return false;
			}
			if (!Quantity.Equals(profileNoticeTavernBrawlTicket.Quantity))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeTavernBrawlTicket Deserialize(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeTavernBrawlTicket DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeTavernBrawlTicket profileNoticeTavernBrawlTicket = new ProfileNoticeTavernBrawlTicket();
			DeserializeLengthDelimited(stream, profileNoticeTavernBrawlTicket);
			return profileNoticeTavernBrawlTicket;
		}

		public static ProfileNoticeTavernBrawlTicket DeserializeLengthDelimited(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeTavernBrawlTicket Deserialize(Stream stream, ProfileNoticeTavernBrawlTicket instance, long limit)
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
					instance.TicketType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ProfileNoticeTavernBrawlTicket instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.TicketType);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Quantity);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt64((ulong)TicketType) + ProtocolParser.SizeOfUInt64((ulong)Quantity) + 2;
		}
	}
}
