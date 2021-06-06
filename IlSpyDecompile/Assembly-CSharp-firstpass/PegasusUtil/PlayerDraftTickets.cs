using System.IO;

namespace PegasusUtil
{
	public class PlayerDraftTickets : IProtoBuf
	{
		public bool HasUnusedTicketBalance;

		private int _UnusedTicketBalance;

		public int UnusedTicketBalance
		{
			get
			{
				return _UnusedTicketBalance;
			}
			set
			{
				_UnusedTicketBalance = value;
				HasUnusedTicketBalance = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUnusedTicketBalance)
			{
				num ^= UnusedTicketBalance.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerDraftTickets playerDraftTickets = obj as PlayerDraftTickets;
			if (playerDraftTickets == null)
			{
				return false;
			}
			if (HasUnusedTicketBalance != playerDraftTickets.HasUnusedTicketBalance || (HasUnusedTicketBalance && !UnusedTicketBalance.Equals(playerDraftTickets.UnusedTicketBalance)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerDraftTickets Deserialize(Stream stream, PlayerDraftTickets instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerDraftTickets DeserializeLengthDelimited(Stream stream)
		{
			PlayerDraftTickets playerDraftTickets = new PlayerDraftTickets();
			DeserializeLengthDelimited(stream, playerDraftTickets);
			return playerDraftTickets;
		}

		public static PlayerDraftTickets DeserializeLengthDelimited(Stream stream, PlayerDraftTickets instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerDraftTickets Deserialize(Stream stream, PlayerDraftTickets instance, long limit)
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
					instance.UnusedTicketBalance = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PlayerDraftTickets instance)
		{
			if (instance.HasUnusedTicketBalance)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UnusedTicketBalance);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUnusedTicketBalance)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)UnusedTicketBalance);
			}
			return num;
		}
	}
}
